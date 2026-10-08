module Partas.AnimeJs.Generation.Tests.EmitTests

open System
open System.IO
open NUnit.Framework
open Partas.AnimeJs.Generation

let generate () =
    let input = XanthamAdapterTests.request ()
    let raw = XanthamAdapter.generate input |> Async.RunSynchronously |> Result.toOption |> Option.get
    let policy = Policy.load input.PolicyPath |> Result.toOption |> Option.get
    let model = Projection.project raw policy |> Result.toOption |> Option.get
    let properties = CssMetadata.read (Path.Combine(AppContext.BaseDirectory, "Xantham.Fable.Core.TS.dll")) (Path.Combine(AppContext.BaseDirectory, "Xantham.Fable.Core.TS.xml")) |> Result.toOption |> Option.get
    let model = CssMetadata.attach model properties |> Result.toOption |> Option.get
    match Emit.sources model with
    | Ok files -> raw, files
    | Error diagnostics -> Assert.Fail(sprintf "%A" diagnostics); Unchecked.defaultof<_>

[<Test>]
let ``emitted first slice compiles and drives actual AnimeJs`` () =
    let raw, files = generate ()
    let root = AnimeJs.Build.Paths.findRoot __SOURCE_DIRECTORY__
    let scratch = Path.Combine(root, "tests/Consumers/Animation")
    let generatedDirectory = Path.Combine(scratch, "input")
    Directory.CreateDirectory generatedDirectory |> ignore
    let all = (raw.Artifacts @ files) |> List.filter (fun artifact -> artifact.RelativePath.EndsWith(".fs"))
    let inventory = ResizeArray<string>()
    for artifact in all do
        let path = Path.Combine(generatedDirectory, artifact.RelativePath)
        Directory.CreateDirectory(Path.GetDirectoryName path) |> ignore
        File.WriteAllText(path, artifact.Content)
        if artifact.RelativePath <> "Dsl.fs" then inventory.Add $"<Compile Include=\"input/{artifact.RelativePath}\" />"
    let inventory = String.concat "" inventory
    let dslPath = "input/Dsl.fs"
    File.WriteAllText(Path.Combine(scratch, "Animation.fsproj"), $"<Project Sdk=\"Microsoft.NET.Sdk\"><PropertyGroup><TargetFramework>net9.0</TargetFramework></PropertyGroup><ItemGroup>{inventory}<Compile Include=\"../../../Partas.AnimeJs/Dsl/Runtime.fs\" /><Compile Include=\"{dslPath}\" /><Compile Include=\"../../../Partas.AnimeJs/Dsl/Conveniences.fs\" /><Compile Include=\"../../../examples/GettingStarted.fs\" /><Compile Include=\"Witness.fs\" /></ItemGroup><ItemGroup><PackageReference Include=\"Fable.Core\" /><PackageReference Include=\"Xantham.Fable.Core\" /><PackageReference Include=\"Xantham.Fable.Core.TS\" /></ItemGroup></Project>")
    for arguments in [ ["build"; "tests/Consumers/Animation/Animation.fsproj"]; ["fable"; "tests/Consumers/Animation/Animation.fsproj"; "-o"; "tests/Consumers/Animation/output"; "-e"; ".fs.js"] ] do
        let code, output = XanthamAdapterTests.run root "dotnet" arguments
        Assert.That(code, Is.Zero, output)
    let code, output = XanthamAdapterTests.run root "node" ["--test"; "tests/Consumers/Animation/witness.test.mjs"]
    Assert.That(code, Is.Zero, output)
    let javascript = File.ReadAllText(Path.Combine(scratch, "output/input/Dsl.fs.js"))
    Assert.That(javascript, Does.Contain("from \"animejs\""))
    Assert.That(javascript, Does.Contain("createTimer"))
    Assert.That(javascript, Does.Contain("animate"))

[<TestCase("AnimationContext", "FS0001", "AnimationContext")>]
[<TestCase("Callback", "FS0001", "JSAnimation")>]
[<TestCase("Value", "FS0041", "duration")>]
[<TestCase("Readonly", "FS0810", "pathLength")>]
[<TestCase("KeyframeShape", "FS0193", "PercentageKeyframeContext")>]
[<TestCase("TimelineCallback", "FS0001", "JSAnimation")>]
let ``invalid DSL consumers fail for the intended contract`` (fixture: string) code expected =
    let raw, files = generate ()
    let root = AnimeJs.Build.Paths.findRoot __SOURCE_DIRECTORY__
    let scratch = Path.Combine(root, ".superpowers/sdd/2026-10-08-xantham-animejs-dsl", "negative-" + fixture)
    Directory.CreateDirectory scratch |> ignore
    for artifact in raw.Artifacts @ files do
        let path = Path.Combine(scratch, artifact.RelativePath)
        Directory.CreateDirectory(Path.GetDirectoryName path) |> ignore
        File.WriteAllText(path, artifact.Content)
    let rawItems = raw.Artifacts |> List.filter (fun artifact -> artifact.RelativePath.EndsWith(".fs")) |> List.map (fun artifact -> $"<Compile Include=\"{artifact.RelativePath}\" />") |> String.concat ""
    let runtime = Path.Combine(root, "Partas.AnimeJs/Dsl/Runtime.fs")
    let consumer = Path.Combine(root, "tests/Consumers/Negative", fixture + ".fs")
    let project = Path.Combine(scratch, "Negative.fsproj")
    File.WriteAllText(project, $"<Project Sdk=\"Microsoft.NET.Sdk\"><PropertyGroup><TargetFramework>net9.0</TargetFramework></PropertyGroup><ItemGroup>{rawItems}<Compile Include=\"{runtime}\" /><Compile Include=\"Dsl.fs\" /><Compile Include=\"{consumer}\" /></ItemGroup><ItemGroup><PackageReference Include=\"Fable.Core\" /><PackageReference Include=\"Xantham.Fable.Core.TS\" /></ItemGroup></Project>")
    let exitCode, output = XanthamAdapterTests.run root "dotnet" ["build"; project]
    Assert.That(exitCode, Is.Not.Zero)
    Assert.That(output, Does.Contain(code))
    Assert.That(output, Does.Contain(expected))
    Assert.That(output, Does.Not.Contain("FS0039"), output)
    let fableCode, fableOutput = XanthamAdapterTests.run root "dotnet" ["fable"; project; "--noCache"; "-o"; Path.Combine(scratch, "output"); "-e"; ".fs.js"]
    Assert.That(fableCode, Is.Not.Zero, fableOutput)
    Assert.That(fableOutput, Does.Contain(expected))
    Assert.That(fableOutput, Does.Not.Contain("(code 39)"), fableOutput)
