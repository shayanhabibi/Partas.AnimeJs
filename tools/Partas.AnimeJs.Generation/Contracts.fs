namespace Partas.AnimeJs.Generation

type Diagnostic = { Code: string; Source: string option; Message: string }
type Artifact = { RelativePath: string; Content: string }
type GenerationRequest = {
    RepositoryRoot: string
    InputDirectory: string
    ConfigPath: string
    PolicyPath: string
    SupportAssembly: string
    SupportDocumentation: string
}
type RawGeneration = {
    Artifacts: Artifact list
    Model: Xantham.Generator.RenderModel
}

[<StructuralEquality; StructuralComparison>]
type DslType =
    | Boolean | Text | Number | BigInteger | Unit | Object
    | Named of string | Variable of string
    | Application of string * DslType list
    | Optional of DslType | Array of DslType | Tuple of DslType list
    | Union of DslType list | Function of DslType * DslType
    | Delegate of DslType list * DslType | Brand of DslType * string

type FieldSpec = {
    Context: string
    Name: string
    JsKey: string
    ValueType: DslType
    Docs: string
    Source: string
    Optional: bool
}
type FactoryArgument = { Name: string; Type: DslType; Optional: bool; Rest: bool }
type FactorySpec = {
    Name: string
    Builder: string
    ExportOwner: string
    ExportName: string
    Arguments: FactoryArgument list
    Result: DslType
    Context: string
    ParameterIndex: int
    ParameterType: DslType
    Constraints: (string * DslType option) list
}
type PolicyRoot = { Export: string; Name: string; Builder: string; Parameter: string; Fields: string list }
type Policy = { Roots: PolicyRoot list; Transforms: string list }
type CssProperty = { Name: string; JsKey: string; Source: string; Docs: string }
type DslModel = {
    Fields: FieldSpec list
    Factories: FactorySpec list
    Diagnostics: Diagnostic list
    ExcludedFields: string list
    TweenType: DslType option
    ReservedKeys: string list
    TypeDefinitions: Map<string, DslType>
    CssProperties: CssProperty list
    CssFields: FieldSpec list
    Transforms: string list
    ValueFields: Map<string, FieldSpec list>
}
