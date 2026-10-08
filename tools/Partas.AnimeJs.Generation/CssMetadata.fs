namespace Partas.AnimeJs.Generation

open System
open System.IO
open System.Reflection.Metadata
open System.Reflection.PortableExecutable
open System.Security.Cryptography
open System.Text.RegularExpressions
open System.Xml.Linq

type SupportFingerprint = { Package: string; Version: string; AssemblyHash: string; DocumentationHash: string }

module CssMetadata =
    let private sha256 path = File.ReadAllBytes path |> SHA256.HashData |> Convert.ToHexString |> _.ToLowerInvariant()

    let fingerprint assemblyPath xmlPath =
        use stream = File.OpenRead assemblyPath
        use pe = new PEReader(stream)
        let reader = pe.GetMetadataReader()
        let definition = reader.GetAssemblyDefinition()
        let version = definition.GetCustomAttributes() |> Seq.tryPick (fun handle ->
            let attribute = reader.GetCustomAttribute handle
            if attribute.Constructor.Kind <> HandleKind.MemberReference then None
            else
                let parent = (reader.GetMemberReference(MemberReferenceHandle.op_Explicit attribute.Constructor)).Parent
                if parent.Kind <> HandleKind.TypeReference then None
                elif reader.GetString((reader.GetTypeReference(TypeReferenceHandle.op_Explicit parent)).Name) <> "AssemblyInformationalVersionAttribute" then None
                else
                    let mutable blob = reader.GetBlobReader attribute.Value
                    if blob.ReadUInt16() <> 1us then None
                    else
                        let text = blob.ReadSerializedString()
                        Some (text.Split('+')[0])) |> Option.defaultWith (fun () -> invalidOp "Support package lacks informational version metadata.")
        { Package = "Xantham.Fable.Core.TS"; Version = version
          AssemblyHash = sha256 assemblyPath; DocumentationHash = sha256 xmlPath }

    let read (assemblyPath: string) (xmlPath: string) : Result<CssProperty list, Diagnostic list> =
        try
            use stream = File.OpenRead assemblyPath
            use pe = new PEReader(stream)
            let reader = pe.GetMetadataReader()
            let rec definitionName (handle: TypeDefinitionHandle) =
                let definition = reader.GetTypeDefinition handle
                let name = reader.GetString definition.Name
                let declaring = definition.GetDeclaringType()
                if declaring.IsNil then
                    let space = reader.GetString definition.Namespace
                    if space = "" then name else space + "." + name
                else definitionName declaring + "." + name
            let referenceName (handle: TypeReferenceHandle) =
                let reference = reader.GetTypeReference handle
                reader.GetString reference.Namespace + "." + reader.GetString reference.Name
            let definitions = reader.TypeDefinitions |> Seq.map (fun handle -> definitionName handle, handle) |> Map.ofSeq
            let attributeName (attribute: CustomAttribute) =
                let parent =
                    match attribute.Constructor.Kind with
                    | HandleKind.MemberReference -> (reader.GetMemberReference(MemberReferenceHandle.op_Explicit attribute.Constructor)).Parent
                    | HandleKind.MethodDefinition ->
                        let method = reader.GetMethodDefinition(MethodDefinitionHandle.op_Explicit attribute.Constructor)
                        TypeDefinitionHandle.op_Implicit (method.GetDeclaringType())
                    | _ -> Unchecked.defaultof<EntityHandle>
                match parent.Kind with
                | HandleKind.TypeReference -> referenceName (TypeReferenceHandle.op_Explicit parent)
                | HandleKind.TypeDefinition -> definitionName (TypeDefinitionHandle.op_Explicit parent)
                | _ -> ""
            let attributeKey attributes =
                attributes |> Seq.choose (fun handle ->
                    let attribute = reader.GetCustomAttribute handle
                    let name = attributeName attribute
                    if name.EndsWith("CompiledNameAttribute", StringComparison.Ordinal) || name.EndsWith("EmitAttribute", StringComparison.Ordinal) then
                        let mutable blob = reader.GetBlobReader attribute.Value
                        if blob.ReadUInt16() <> 1us then None
                        else
                            let value = blob.ReadSerializedString()
                            if isNull value then None
                            elif name.EndsWith("CompiledNameAttribute", StringComparison.Ordinal) then Some value
                            elif value.StartsWith("$0.", StringComparison.Ordinal) && not (value.Contains("(")) then Some(value.Substring 3)
                            else None
                    else None) |> Seq.distinct |> Seq.toList
            let documents =
                // Published 0.1.1 XML includes bare ampersands in member identities
                // and text. Escape non-entity ampersands; preserve valid XML entities.
                let xml = Regex.Replace(File.ReadAllText xmlPath, "&(?!amp;|lt;|gt;|quot;|apos;|#[0-9]+;|#x[0-9a-fA-F]+;)", "&amp;")
                XDocument.Parse(xml).Descendants(XName.Get "member")
                |> Seq.choose (fun memberDoc ->
                    let name = memberDoc.Attribute(XName.Get "name")
                    if isNull name then None
                    else
                        let segments = name.Value.Split('(')
                        let identity = (segments[0]).Replace('+', '.')
                        Some(identity, String.concat " " (memberDoc.Value.Split([|'\r'; '\n'|], StringSplitOptions.RemoveEmptyEntries) |> Array.map _.Trim())))
                |> Map.ofSeq
            let excluded = Set.ofList [ "cssText"; "length"; "parentRule"; "Item"; "setProperty"; "getPropertyValue"; "getPropertyPriority"; "removeProperty"; "item" ]
            let errors = ResizeArray<Diagnostic>()
            let rec properties visited handle =
                let name = definitionName handle
                if Set.contains name visited || name.EndsWith("CSSStyleDeclarationBase", StringComparison.Ordinal) then []
                else
                    let visited = Set.add name visited
                    let definition = reader.GetTypeDefinition handle
                    let inherited =
                        definition.GetInterfaceImplementations()
                        |> Seq.collect (fun inherited ->
                            let contract = (reader.GetInterfaceImplementation inherited).Interface
                            match contract.Kind with
                            | HandleKind.TypeDefinition -> properties visited (TypeDefinitionHandle.op_Explicit contract)
                            | HandleKind.TypeReference ->
                                match Map.tryFind (referenceName (TypeReferenceHandle.op_Explicit contract)) definitions with
                                | Some handle -> properties visited handle
                                | None -> []
                            | _ -> [])
                        |> Seq.toList
                    let own = definition.GetProperties() |> Seq.choose (fun handle ->
                        let property = reader.GetPropertyDefinition handle
                        let propertyName = reader.GetString property.Name
                        let accessors = property.GetAccessors()
                        if accessors.Setter.IsNil || Set.contains propertyName excluded then None
                        else
                            let attributes = [ yield! property.GetCustomAttributes()
                                               if not accessors.Getter.IsNil then yield! (reader.GetMethodDefinition accessors.Getter).GetCustomAttributes() ]
                            let mappings = attributeKey attributes
                            if mappings.Length > 1 then
                                errors.Add { Code = "css/alias-conflict"; Source = Some(name + "." + propertyName); Message = "Property metadata declares conflicting JavaScript keys." }
                                None
                            else
                                let key = mappings |> List.tryHead |> Option.defaultValue propertyName
                                if Set.contains key excluded then None
                                else Some { Name = propertyName; JsKey = key; Source = name + "." + propertyName
                                            Docs = Map.tryFind ("P:" + name + "." + propertyName) documents |> Option.defaultValue "" }) |> Seq.toList
                    inherited @ own
            match Map.tryFind "Fable.Core.TS.Dom.CSSStyleProperties" definitions with
            | None -> Error [{ Code = "css/missing-root"; Source = Some "Fable.Core.TS.Dom.CSSStyleProperties"; Message = "Support assembly does not contain the expected CSS property contract." }]
            | Some root ->
                let candidates = properties Set.empty root
                let conflictingNames = candidates |> List.groupBy _.Name |> List.filter (fun (_, group) -> group |> List.map _.JsKey |> List.distinct |> List.length > 1)
                for name, _ in conflictingNames do errors.Add { Code = "css/alias-conflict"; Source = Some name; Message = "Inherited aliases map the same emitted name to different JavaScript keys." }
                if errors.Count > 0 then Error(List.ofSeq errors)
                else
                    candidates |> List.groupBy _.JsKey |> List.map (fun (key, group) ->
                        group |> List.sortBy (fun property -> (property.Name <> key), (property.Docs = ""), property.Name) |> List.head)
                    |> List.sortBy _.JsKey |> Ok
        with error -> Error [{ Code = "css/metadata"; Source = Some "Xantham.Fable.Core.TS"; Message = error.Message }]

    let attach (model: DslModel) (properties: CssProperty list) =
        match model.TweenType with
        | None -> Error [{ Code = "css/missing-tween"; Source = Some "TweenOptions"; Message = "AnimeJs tween value contract is unavailable." }]
        | Some valueType ->
            let fields = properties |> List.map (fun property -> {
                Context = "Animation"; Name = property.Name; JsKey = property.JsKey; Docs = property.Docs
                Source = property.Source; ValueType = valueType; Optional = true })
            Ok { model with CssProperties = properties; CssFields = fields }
