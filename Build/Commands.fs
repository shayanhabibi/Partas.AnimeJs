namespace AnimeJs.Build

open Partas.Build
open System
open System.IO
open Partas.AnimeJs.Generation

module Commands =
    let private fail (diagnostics: Diagnostic list) =
        diagnostics |> List.map (fun diagnostic -> diagnostic.Code + ": " + diagnostic.Message) |> String.concat "\n" |> invalidOp

    let generation root =
        let check = Input.option<bool> "--check" |> Input.description "Validate output without changing tracked artifacts"
        let candidate = Producer.emptyDefine "compile generated candidate" (Operation.ofAsync (async {
            let support = Path.Combine(AppContext.BaseDirectory, "Xantham.Fable.Core.TS.dll")
            let documentation = Path.Combine(AppContext.BaseDirectory, "Xantham.Fable.Core.TS.xml")
            let request = {
                RepositoryRoot = root; InputDirectory = Path.Combine(root, "generation/node_modules/animejs")
                ConfigPath = Path.Combine(root, "generation/xantham.json"); PolicyPath = Path.Combine(root, "generation/dsl-policy.json")
                SupportAssembly = support; SupportDocumentation = documentation }
            let! result = Pipeline.generate request
            return match result with Ok value -> value | Error errors -> fail errors
        }))
        input {
            let! check = check
            return stage "validate or promote candidate" {
                consumes (DependencySpec.require candidate) (fun candidate ->
                    Operation.ofAsync (async {
                        let output = Path.Combine(root, "Partas.AnimeJs/Generated")
                        match (if check then Candidate.compare output candidate else Candidate.promote output candidate) with
                        | Ok () -> ()
                        | Error errors -> fail errors
                    }))
            }
        }

    let create (root: string) = Command.root {
        description "Partas.AnimeJs repository automation"
        workingDir root
        command "restore" {
            description "Restore pinned local generation tools"
            Stages.restore
        }
        command "generate" {
            description "Generate and compile a candidate, then promote it transactionally"
            generation root
        }
    }
