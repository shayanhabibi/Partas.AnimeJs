module Partas.AnimeJs.Tests.Main

open Fli
open FsUnit
open FsUnitTyped
open Partas.AnimeJs.Tests.Common
open NUnit.Framework

[<SetUpFixture>]
type SetupUtils() =
    inherit FSharpCustomMessageFormatter()
    [<OneTimeSetUpAttribute>]
    static member FableCompile () =
        let dir = $"{__SOURCE_DIRECTORY__}/Tests"
        cli {
            Shell platformShell
            WorkingDirectory dir
            Command "fable -e .fs.jsx -o output --typedArrays false --run dotnet restore"
        }
        |> Command.execute
        |> Output.toExitCode
        |> shouldEqual 0

[<Test>]
let Test1 () =
    Assert.Pass()
