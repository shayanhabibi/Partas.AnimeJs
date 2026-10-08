namespace AnimeJs.Build

open Partas.Build
open System
open System.IO
open Partas.AnimeJs.Generation

module Commands =
    let private fail (diagnostics: Diagnostic list) =
        diagnostics |> List.map (fun diagnostic -> diagnostic.Code + (diagnostic.Source |> Option.map (fun source -> " [" + source + "]") |> Option.defaultValue "") + ": " + diagnostic.Message) |> String.concat "\n" |> invalidOp

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

    let documentation root = input {
        let! watch = Input.option<bool> "--watch" |> Input.description "Serve documentation and watch sources"
        return pipeline "documentation" {
            stage "compile and exercise documented source" {
                run (cmd $"dotnet" |> Cmd.args ["build"; "tests/Consumers/Positive/Positive.fsproj"])
                run (cmd $"dotnet" |> Cmd.args ["fable"; "tests/Consumers/Positive/Positive.fsproj"; "-o"; "tests/Consumers/Positive/output"; "-e"; ".fs.js"])
                run (cmd $"node" |> Cmd.args ["--test"; "tests/Consumers/Positive/witness.test.mjs"])
                run (cmd $"dotnet" |> Cmd.args ["build"; "tests/Consumers/Families/Families.fsproj"])
                run (cmd $"dotnet" |> Cmd.args ["fable"; "tests/Consumers/Families/Families.fsproj"; "-o"; "tests/Consumers/Families/output"; "-e"; ".fs.js"])
                run (cmd $"node" |> Cmd.args ["--test"; "tests/Consumers/Families/witness.test.mjs"])
                run (cmd $"dotnet" |> Cmd.args ["build"; "tests/Consumers/Browser/Browser.fsproj"])
                run (cmd $"dotnet" |> Cmd.args ["fable"; "tests/Consumers/Browser/Browser.fsproj"; "-o"; "tests/Consumers/Browser/output"; "-e"; ".fs.js"])
                run (cmd $"npx" |> Cmd.args ["playwright"; "test"])
            }
            stage "Nacara site" {
                workingDir (Path.Combine(root, "site"))
                run (cmd $"dotnet" |> Cmd.args [
                    if watch then
                        "watch"
                        "--no-hot-reload"
                    else
                        "run"
                    "--project"
                    "site.fsproj"
                    if not watch then "-p:OutputPath=.superpowers/documentation-bin/"
                    "--"
                    if watch then "watch" else "build"
                ])
            }
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
        command "docs" {
            description "Compile documented examples and build or watch the Nacara site"
            documentation root
        }
        command "build" {
            description "Build the complete repository"
            stage "build solution" {
                run (cmd $"dotnet" |> Cmd.args ["build"; "Partas.AnimeJs.slnx"; "-p:OutputPath=.superpowers/build-bin/"])
            }
        }
        command "test" {
            description "Run generation, consumer, negative, runtime and package tests"
            stage "acceptance tests" {
                run (cmd $"dotnet" |> Cmd.args ["test"; "tests/Partas.AnimeJs.Generation.Tests/Partas.AnimeJs.Generation.Tests.fsproj"; "-p:OutputPath=.superpowers/verification-bin/"])
                run (cmd $"dotnet" |> Cmd.args ["test"; "Partas.AnimeJs.Tests/Partas.AnimeJs.Tests.fsproj"])
            }
        }
        command "verify" {
            description "Check generation, all acceptance tests, browser behavior and documentation"
            pipeline "verify repository" {
                stage "read-only generation check" {
                    run (cmd $"dotnet" |> Cmd.args [Path.Combine(AppContext.BaseDirectory, "Build.dll"); "generate"; "--check"])
                }
                stage "complete build" {
                    run (cmd $"dotnet" |> Cmd.args [Path.Combine(AppContext.BaseDirectory, "Build.dll"); "build"])
                }
                stage "complete acceptance suite" {
                    run (cmd $"dotnet" |> Cmd.args [Path.Combine(AppContext.BaseDirectory, "Build.dll"); "test"])
                }
                stage "browser and documentation" {
                    run (cmd $"dotnet" |> Cmd.args [Path.Combine(AppContext.BaseDirectory, "Build.dll"); "docs"])
                }
            }
        }
        command "pack" {
            description "Verify the repository and create a local NuGet package"
            pipeline "pack verified library" {
                stage "validate" {
                    run (cmd $"dotnet" |> Cmd.args [Path.Combine(AppContext.BaseDirectory, "Build.dll"); "verify"])
                }
                stage "local package" {
                    run (cmd $"dotnet" |> Cmd.args ["pack"; "Partas.AnimeJs/Partas.AnimeJs.fsproj"; "-c"; "Release"; "-o"; "artifacts/packages"])
                }
            }
        }
    }
