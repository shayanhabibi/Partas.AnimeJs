namespace Partas.AnimeJs.Generation

open System.IO
open System.Text.Json
open Xantham.Generator

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Policy =
    let load (path: string) : Result<Policy, Diagnostic list> =
        try
            use document = JsonDocument.Parse(File.ReadAllText path)
            let strings (value: JsonElement) = value.EnumerateArray() |> Seq.map _.GetString() |> Seq.toList
            let roots = document.RootElement.GetProperty("roots").EnumerateArray() |> Seq.map (fun root ->
                { Export = root.GetProperty("export").GetString()
                  Name = root.GetProperty("name").GetString()
                  Builder = root.GetProperty("builder").GetString()
                  Parameter = root.GetProperty("parameter").GetString()
                  Fields = strings (root.GetProperty("fields")) }) |> Seq.toList
            Ok { Roots = roots; Transforms = strings (document.RootElement.GetProperty("transforms")) }
        with error -> Error [{ Code = "policy/invalid"; Source = Some "generation/dsl-policy.json"; Message = error.Message }]

module Projection =
    let project (raw: RawGeneration) (policy: Policy) : Result<DslModel, Diagnostic list> =
        let declarations = raw.Model.Decls
        let aliases = declarations |> List.choose (function FsAbbrev item -> Some(item.Name, item) | _ -> None) |> Map.ofList
        let interfaces = declarations |> List.choose (function FsInterface item -> Some(item.Name, item) | _ -> None) |> Map.ofList
        let diagnostics = ResizeArray<Diagnostic>()
        let report code source message = diagnostics.Add { Code = code; Source = Some source; Message = message }

        let bind source (parameters: FsTypeParam list) arguments =
            if parameters.Length <> List.length arguments then
                report "projection/generic-arity" source "Generic application does not match declaration arity."
                Map.empty
            else List.zip (parameters |> List.map _.Name) arguments |> Map.ofList

        let rec convert source visited substitutions reference =
            let recurse = convert source visited substitutions
            match reference with
            | FsBool -> Boolean | FsString -> Text | FsFloat -> Number | FsBigInt -> BigInteger
            | FsUnit -> Unit | FsObj -> Object
            | FsOption item -> Optional(recurse item)
            | FsArray item -> Array(recurse item)
            | FsTuple items -> Tuple(List.map recurse items)
            | FsErasedUnion items ->
                match items |> List.map recurse |> List.distinct with
                | [single] -> single
                | arms -> Union arms
            | FsFunc(argument, result) -> Function(recurse argument, recurse result)
            | FsDelegate(arguments, result) -> Delegate(List.map recurse arguments, recurse result)
            | FsBranded(primitive, measure) -> Brand(recurse primitive, measure)
            | FsTypeVar name ->
                match Map.tryFind name substitutions with
                | Some kind -> kind
                | None -> report "projection/unbound-variable" source $"Unbound type variable '{name}."; Variable name
            | FsNamed name -> named source visited name []
            | FsApp(name, arguments) -> named source visited name (List.map recurse arguments)
        and named source visited name arguments =
            match Map.tryFind name aliases with
            | Some alias when not (Set.contains name visited) ->
                let substitutions = bind source alias.TypeParameters arguments
                convert source (Set.add name visited) substitutions alias.Target
            | _ -> if List.isEmpty arguments then Named name else Application(name, arguments)

        let rec fields context visited substitutions reference =
            match reference with
            | FsOption value -> fields context visited substitutions value
            | FsNamed name -> resolveFields context visited name []
            | FsApp(name, arguments) -> resolveFields context visited name (arguments |> List.map (convert context Set.empty substitutions))
            | _ -> report "projection/root-shape" context "Option root is not a named interface or alias."; []
        and resolveFields context visited name arguments =
            if Set.contains name visited then []
            else
                let visited = Set.add name visited
                match Map.tryFind name aliases, Map.tryFind name interfaces with
                | Some alias, _ -> fields context visited (bind name alias.TypeParameters arguments) alias.Target
                | None, Some declaration ->
                    let substitutions = bind name declaration.TypeParameters arguments
                    let inherited = declaration.Inherits |> List.collect (fields context visited substitutions)
                    let own = declaration.Members |> List.choose (function
                        | FsProperty property when not property.ReadOnly ->
                            let source = declaration.Name + "." + property.Name
                            let kind = convert source Set.empty substitutions property.Type
                            let optional, value = match kind with Optional value -> true, value | value -> false, value
                            Some { Context = context; Name = property.Name; JsKey = property.Name
                                   ValueType = value; Optional = optional; Docs = property.Docs; Source = source }
                        | _ -> None)
                    inherited @ own
                | _ -> report "projection/missing-root" name "Option root has no interface declaration."; []

        let deduplicate context (candidates: FieldSpec list) =
            candidates |> List.groupBy _.JsKey |> List.map (fun (key, group) ->
                let contracts = group |> List.map (fun field -> field.ValueType, field.Optional) |> List.distinct
                if contracts.Length > 1 then report "projection/field-conflict" (context + "." + key) "Inherited properties disagree on value type or optionality."
                List.last group)

        let exports = declarations |> List.collect (function
            | FsExports container when container.Owner = EntryModule -> container.Members |> List.map (fun owned -> container.Name, owned)
            | _ -> [])
        let excluded = ResizeArray<string>()
        let reserved = ResizeArray<string>()
        let selected = ResizeArray<FieldSpec>()
        let factories = ResizeArray<FactorySpec>()
        let contexts = policy.Roots |> List.map _.Name
        if (List.distinct contexts).Length <> contexts.Length then report "policy/duplicate-context" "roots" "Each public option context must have a unique name."
        for root in policy.Roots do
            let matching = exports |> List.filter (fun (_, owned) -> owned.ExportName = root.Export)
            match matching with
            | [] -> report "projection/missing-factory" root.Export "No matching root export."
            | [owner, owned] ->
                match owned.Member.Body with
                | ExportFunction(arguments, result) ->
                    match arguments |> List.tryFindIndex (fun parameter -> parameter.Name = root.Parameter) with
                    | None -> report "projection/missing-parameter" root.Export $"No option parameter named {root.Parameter}."
                    | Some index ->
                        let parameter = arguments[index]
                        let candidates = fields root.Name Set.empty Map.empty parameter.Type |> deduplicate root.Name
                        for field in candidates do
                            reserved.Add field.JsKey
                            if root.Fields.IsEmpty || List.contains field.JsKey root.Fields then
                                if field.ValueType = Object then report "projection/unsupported-field" field.Source "Selected field has an unstructured object contract; retain raw access."
                                else selected.Add field
                            else excluded.Add field.Source
                        for key in root.Fields do
                            if not (candidates |> List.exists (fun field -> field.JsKey = key)) then
                                report "projection/missing-field" (root.Name + "." + key) "Selected field is absent from upstream option root."
                        let scope = owned.Member.TypeParameters |> List.map (fun item -> item.Name, Variable item.Name) |> Map.ofList
                        let convert = convert root.Export Set.empty scope
                        factories.Add {
                            Name = root.Export; Builder = root.Builder; ExportOwner = owner; ExportName = owned.Member.Name
                            Arguments = arguments |> List.map (fun item -> { Name = item.Name; Type = convert item.Type; Optional = item.Optional; Rest = item.Rest })
                            Result = convert result; Context = root.Name; ParameterIndex = index
                            ParameterType = (match convert parameter.Type with Optional value -> value | value -> value)
                            Constraints = owned.Member.TypeParameters |> List.map (fun item -> item.Name, Option.map convert item.Constraint) }
                | _ -> report "projection/factory-shape" root.Export "Selected export is not a function."
            | _ -> report "projection/ambiguous-factory" root.Export "Policy must select a unique factory signature."
        if diagnostics.Count > 0 then Error(List.ofSeq diagnostics)
        else
            let definitions = aliases |> Map.map (fun name alias ->
                let scope = alias.TypeParameters |> List.map (fun item -> item.Name, Variable item.Name) |> Map.ofList
                convert name (Set.singleton name) scope alias.Target)
            Ok {
                Fields = List.ofSeq selected; Factories = List.ofSeq factories; Diagnostics = []
                ExcludedFields = List.ofSeq excluded |> List.distinct |> List.sort
                TweenType = if Map.containsKey "TweenOptions" aliases then Some(Named "TweenOptions") else None
                ReservedKeys = List.ofSeq reserved |> List.distinct |> List.sort; TypeDefinitions = definitions; CssProperties = []; CssFields = []
                Transforms = policy.Transforms
                ValueFields = if Map.containsKey "TweenKeyValue" interfaces then Map.ofList [ "TweenKeyValue", fields "Tween" Set.empty Map.empty (FsNamed "TweenKeyValue") |> deduplicate "Tween" ] else Map.empty }
