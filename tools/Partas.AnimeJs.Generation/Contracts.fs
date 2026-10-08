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
