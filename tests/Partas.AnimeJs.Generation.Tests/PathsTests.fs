module Partas.AnimeJs.Generation.Tests.PathsTests

open System
open System.IO
open NUnit.Framework
open AnimeJs.Build

[<Test>]
let ``nested location resolves repository root with spaces`` () =
    let parent = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"))
    let root = Path.Combine(parent, "anime repo with spaces")
    let nested = Path.Combine(root, "nested", "child")
    try
        Directory.CreateDirectory(nested) |> ignore
        Directory.CreateDirectory(Path.Combine(root, "Partas.AnimeJs")) |> ignore
        File.WriteAllText(Path.Combine(root, "global.json"), "{}")
        File.WriteAllText(Path.Combine(root, "Partas.AnimeJs", "Partas.AnimeJs.fsproj"), "<Project />")
        Assert.That(Paths.findRoot nested, Is.EqualTo(root))
    finally
        Directory.Delete(parent, true)

[<Test>]
let ``unrelated working directory does not select itself`` () =
    let expected = Path.GetFullPath(Path.Combine(__SOURCE_DIRECTORY__, "../.."))
    Assert.That(Paths.findRoot (Path.GetTempPath()), Is.EqualTo(expected))

[<TestCase("restore")>]
[<TestCase("generate")>]
let ``unknown option returns usage error`` command =
    Assert.That(Program.main [| command; "--not-an-option" |], Is.EqualTo(2))
