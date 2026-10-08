namespace Partas.AnimeJs.Generation

module XanthamAdapter =
    let generate (request: GenerationRequest) : Async<Result<RawGeneration, Diagnostic list>> = async {
        try
            let config = Xantham.Generator.GeneratorConfig.loadFile request.ConfigPath
            let! model = Xantham.Generator.Pipeline.generate config request.InputDirectory
            // Generator 0.1.0 leaves this generic compiler-library reference under its
            // default module despite compilerLib settings. This is a type abbreviation:
            // consumers retain the upstream identity, with no new interface or producer.
            let aliases = { RelativePath = "CompilerLibAliases.fs"; Content = "module TypeScript.Lib.Es\n\ntype Iterable<'T, 'TReturn, 'TNext> = Fable.Core.TS.Es.Iterable<'T, 'TReturn, 'TNext>\n" }
            return Ok { Model = model; Artifacts = aliases :: (model.Files |> List.map (fun (path, content) -> { RelativePath = path; Content = content })) }
        with error ->
            return Error [{ Code = "xantham/generation"; Source = Some "generation/xantham.json"; Message = error.ToString() }]
    }
