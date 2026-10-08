namespace Partas.AnimeJs.Generation

open System
open System.Text
open System.Text.Json
open System.Text.RegularExpressions

module Emit =
    let private unionName count = (if count > 9 then "Raw.U" else "U") + string count
    let private identifier name =
        if String.IsNullOrEmpty name || name.Contains("``") || name |> Seq.exists Char.IsControl then invalidArg "name" "Unspellable F# identifier."
        "``" + name + "``"
    let private literal (value: string) = JsonSerializer.Serialize value
    let private qualified (name: string) =
        if name.StartsWith("Fable.", StringComparison.Ordinal) || name.StartsWith("TypeScript.", StringComparison.Ordinal) || name.StartsWith("System.", StringComparison.Ordinal) || name.StartsWith("Partas.AnimeJs.Raw.", StringComparison.Ordinal) then name
        elif ["Record"; "ReadonlyRecord"; "PropertyRecord"; "keyof"; "IndexedAccess"; "NoInfer"] |> List.contains name then "Fable.Core.JS.JS." + name
        else "Raw." + name
    let rec typeText kind =
        let print = typeText
        match kind with
        | Boolean -> "bool" | Text -> "string" | Number -> "float" | BigInteger -> "System.Numerics.BigInteger"
        | Unit -> "unit" | Object -> "obj"
        | Named name -> qualified name | Variable name -> "'" + name
        | Application(name, arguments) -> qualified name + "<" + (arguments |> List.map print |> String.concat ", ") + ">"
        | Optional item -> "(" + print item + ") option"
        | Array item -> "(" + print item + ")[]"
        | Tuple items -> "(" + (items |> List.map print |> String.concat " * ") + ")"
        | Union arms -> unionName arms.Length + "<" + (arms |> List.map print |> String.concat ", ") + ">"
        | Function(argument, result) -> "(" + print argument + " -> " + print result + ")"
        | Delegate(arguments, Unit) -> "System.Action<" + (arguments |> List.map print |> String.concat ", ") + ">"
        | Delegate([], result) -> "System.Func<" + print result + ">"
        | Delegate(arguments, result) -> "System.Func<" + (arguments @ [result] |> List.map print |> String.concat ", ") + ">"
        | Brand(primitive, measure) -> print primitive + "<" + qualified measure + ">"

    let sources (model: DslModel) : Result<Artifact list, Diagnostic list> =
        try
            let output = StringBuilder()
            let line (value: string) = output.Append(value).Append('\n') |> ignore
            let context name = name + "Context"
            let fieldType name = "Field<" + context name + ">"
            let construct name key value = "Field.create<" + context name + "> " + literal key + " (box (" + value + "))"
            let docs text =
                if text <> "" then line ("    /// " + System.Security.SecurityElement.Escape(text).Replace("\r", " ").Replace("\n", " "))
            let emitField (field: FieldSpec) =
                docs field.Docs
                let name = identifier field.Name
                let write name valueType value =
                    line ("    static member " + name + " (value: " + typeText valueType + ") : " + fieldType field.Context + " = " + construct field.Context field.JsKey value)
                match field.ValueType with
                | Function(argument, Object) when field.Name.StartsWith("on", StringComparison.Ordinal) ->
                    write (identifier (field.Name + "Value")) field.ValueType "value"
                    line ("    static member " + name + " (value: " + typeText (Function(argument, Unit)) + ") : " + fieldType field.Context + " =")
                    line ("        let callback (handle: " + typeText argument + ") : obj = value handle; box ()")
                    line ("        " + construct field.Context field.JsKey "callback")
                | Union arms ->
                    write name field.ValueType "value"
                    let types = arms |> List.map typeText
                    for index, arm in List.indexed arms do
                        if (types |> List.filter ((=) (typeText arm))).Length = 1 then
                            write name arm (unionName arms.Length + ".Case" + string(index + 1) + " value")
                | kind -> write name kind "value"
            let staticType name = line "[<AbstractClass; Sealed>]"; line ("type " + identifier name + " private () =")
            let unionCase kind arm value =
                match kind with
                | Union arms ->
                    match List.tryFindIndex ((=) arm) arms with
                    | Some index -> unionName arms.Length + ".Case" + string(index + 1) + " (" + value + ")"
                    | None -> invalidOp ("Convenience arm absent: " + typeText arm)
                | valueType when valueType = arm -> value
                | _ -> invalidOp "Convenience is not supported by the projected value contract."
            let definition name = Map.find name model.TypeDefinitions
            line "// Generated from Xantham declarations and the reviewed DSL policy."
            line "namespace Partas.AnimeJs"
            line ""
            line "open Fable.Core"
            line "open Fable.Core.JsInterop"
            line "open Fable.Core.JS"
            line ""
            for factory in model.Factories do
                if not (Regex.IsMatch(factory.Context, "^[A-Za-z_][A-Za-z0-9_]*$")) then invalidOp "Invalid context name in policy."
                line ("type " + context factory.Context + " = private " + context factory.Context + " of unit")
            for value in model.Values do
                if not (Regex.IsMatch(value.Name, "^[A-Za-z_][A-Za-z0-9_]*$")) then invalidOp "Invalid value context name in policy."
                line ("type " + context value.Name + " = private " + context value.Name + " of unit")
            if model.Values |> List.exists (fun value -> value.Name = "PercentageKeyframe") then
                line "type PercentageStop = private PercentageStop of string * Options<PercentageKeyframeContext>"
            line ""
            line "[<AutoOpen>]"
            line "module Builders ="
            for factory in model.Factories do
                line ("    let " + identifier factory.Builder + " = OptionsBuilder<" + context factory.Context + ">()")
            for value in model.Values do
                line ("    let " + identifier value.Builder + " = OptionsBuilder<" + context value.Name + ">()")
            line ""
            for factory in model.Factories do
                staticType factory.Context
                model.Fields |> List.filter (fun field -> field.Context = factory.Context) |> List.iter emitField
                line ("    static member parameters (options: Options<" + context factory.Context + ">) : " + typeText factory.ParameterType + " = Options.toObject options |> unbox")
                match factory.DynamicType with
                | Some (_, kind) ->
                    let reserved = (model.Fields |> List.filter (fun field -> field.Context = factory.Context) |> List.map _.JsKey) @ ["__proto__"; "constructor"; "prototype"] |> List.map literal |> String.concat "; "
                    let write arm expression =
                        line ("    static member property (key: string, value: " + typeText arm + ") : " + fieldType factory.Context + " =")
                        line ("        if System.String.IsNullOrWhiteSpace key || List.contains key [" + reserved + "] then invalidArg \"key\" \"Custom keys must be nonempty and cannot name an option.\"")
                        line ("        Field.create<" + context factory.Context + "> key (box (" + expression + "))")
                    write kind "value"
                    match kind with
                    | Union arms -> for arm in List.distinct arms do write arm (unionCase kind arm "value")
                    | _ -> ()
                | None -> ()
                if factory.Context = "Animation" then
                    let reserved = model.ReservedKeys @ ["__proto__"; "constructor"; "prototype"] |> List.map literal |> String.concat "; "
                    line "    /// Animate a dynamic object property. The key does not prove target-property existence."
                    line "    static member property (key: string) (value: Raw.TweenOptions) : Field<AnimationContext> ="
                    line ("        if System.String.IsNullOrWhiteSpace key || List.contains key [" + reserved + "] then invalidArg \"key\" \"Custom keys must be nonempty and cannot name an option.\"")
                    line ("        " + "Field.create<AnimationContext> key (box value)")
                line ""
            for value in model.Values do
                staticType value.Name
                let fields = model.Fields |> List.filter (fun field -> field.Context = value.Name)
                fields |> List.iter emitField
                line ("    static member parameters (options: Options<" + context value.Name + ">) : " + typeText value.Type + " = Options.toObject options |> unbox")
                match value.DynamicType with
                | None -> ()
                | Some kind ->
                    let reserved = (fields |> List.map _.JsKey) @ ["__proto__"; "constructor"; "prototype"] |> List.map literal |> String.concat "; "
                    let write kind expression =
                        line ("    static member property (key: string, value: " + typeText kind + ") : " + fieldType value.Name + " =")
                        line ("        if System.String.IsNullOrWhiteSpace key || List.contains key [" + reserved + "] then invalidArg \"key\" \"Custom keys must be nonempty and cannot name an option.\"")
                        line ("        Field.create<" + context value.Name + "> key (box (" + expression + "))")
                    write kind "value"
                    match kind with
                    | Union arms -> for arm in List.distinct arms do write arm (unionCase kind arm "value")
                    | _ -> ()
                line ""
            if model.Values |> List.exists (fun value -> value.Name = "DurationKeyframe") then
                staticType "Keyframes"
                let percentage = model.Values |> List.find (fun value -> value.Name = "PercentageKeyframe")
                line ("    static member duration (frames: Options<DurationKeyframeContext> list) : " + typeText (definition "DurationKeyframes") + " = frames |> List.map DurationKeyframe.parameters |> List.toArray")
                line "    static member at (percent: float) (options: Options<PercentageKeyframeContext>) : PercentageStop ="
                line "        if System.Double.IsNaN percent || System.Double.IsInfinity percent || percent < 0. || percent > 100. then invalidArg \"percent\" \"Percentage must be finite and between zero and 100.\""
                line "        PercentageStop (percent.ToString(\"0.################\", System.Globalization.CultureInfo.InvariantCulture) + \"%\", options)"
                line ("    static member percentage (frames: PercentageStop list) : " + typeText (definition "PercentageKeyframes") + " =")
                line ("        frames |> List.map (fun (PercentageStop (key, options)) -> key, box (" + identifier percentage.Name + ".parameters options)) |> createObj |> unbox")
                line ""
            staticType "Css"
            model.CssFields |> List.iter emitField
            line ""
            staticType "Transform"
            for name in model.Transforms do
                emitField { Context = "Animation"; Name = name; JsKey = name; ValueType = Named "TweenOptions"; Optional = true; Docs = ""; Source = "policy/transforms/" + name }
            line ""
            staticType "Tween"
            let tween = definition "TweenOptions"
            line ("    static member number (value: float) : Raw.TweenOptions = " + unionCase tween Number "value")
            line ("    static member text (value: string) : Raw.TweenOptions = " + unionCase tween Text "value")
            let tweenFields = Map.find "TweenKeyValue" model.ValueFields
            let fromField = tweenFields |> List.find (fun field -> field.JsKey = "from")
            let toField = tweenFields |> List.find (fun field -> field.JsKey = "to")
            let fromValue = unionCase fromField.ValueType Number "first"
            let toValue = unionCase toField.ValueType Number "last"
            line "    static member fromTo (first: float) (last: float) : Raw.TweenOptions ="
            let discriminator = tweenFields |> List.find (fun field -> field.JsKey <> "from" && field.JsKey <> "to" && field.JsKey <> "fromTo" && field.Optional)
            let objectValue = "Raw.TweenKeyValue.Create(from = " + fromValue + ", ``to`` = " + toValue + ", ?" + identifier discriminator.Name + " = None)"
            line ("        " + unionCase tween (Named "TweenKeyValue") objectValue)
            let arrayArm = match tween with Union arms -> arms |> List.find (function Array _ -> true | _ -> false) | _ -> invalidOp "Tween array arm missing."
            let elementType = match arrayArm with Array element -> element | _ -> invalidOp "Not an array."
            let elementValue = unionCase elementType Number "value"
            line "    static member values (values: float list) : Raw.TweenOptions ="
            line ("        let items : " + typeText arrayArm + " = values |> List.map (fun value -> " + elementValue + ") |> List.toArray")
            line ("        " + unionCase tween arrayArm "items")
            line ""
            staticType "Target"
            let targets = definition "TargetsParam"
            line ("    static member selector (value: string) : Raw.TargetsParam = " + unionCase targets Text "value")
            let objectType = Application("Record", [Text; DslType.Object])
            line ("    static member ``object`` (value: " + typeText objectType + ") : Raw.TargetsParam = " + unionCase targets objectType "value")
            let element = Named "Fable.Core.TS.Dom.HTMLElement"
            line ("    static member element (value: " + typeText element + ") : Raw.TargetsParam = " + unionCase targets element "value")
            let svgElement = Named "Fable.Core.TS.Dom.SVGElement"
            line ("    static member svg (value: " + typeText svgElement + ") : Raw.TargetsParam = " + unionCase targets svgElement "value")
            line ""
            line "module Anime ="
            for factory in model.Factories do
                if not factory.Constraints.IsEmpty then invalidOp ("Generic factory requires explicit adaptation: " + factory.Name)
                let arguments = factory.Arguments |> List.indexed |> List.map (fun (index, argument) ->
                    if index = factory.ParameterIndex then "(options: Options<" + context factory.Context + ">)"
                    else "(" + identifier argument.Name + ": " + typeText argument.Type + ")") |> String.concat " "
                line ("    let " + identifier factory.Name + " " + arguments + " : " + typeText factory.Result + " =")
                line ("        let parameters = Options.toObject options |> unbox<" + typeText factory.ParameterType + ">")
                let callArguments = factory.Arguments |> List.indexed |> List.map (fun (index, argument) ->
                    if index = factory.ParameterIndex then identifier argument.Name + " = parameters"
                    elif argument.Optional then "?" + identifier argument.Name + " = " + identifier argument.Name
                    else identifier argument.Name + " = " + identifier argument.Name) |> String.concat ", "
                line ("        Raw." + factory.ExportOwner + "." + identifier factory.ExportName + "(" + callArguments + ")")
            Ok [{ RelativePath = "Dsl.fs"; Content = output.ToString() }]
        with error -> Error [{ Code = "emit/unsupported"; Source = Some "DSL"; Message = error.Message }]
