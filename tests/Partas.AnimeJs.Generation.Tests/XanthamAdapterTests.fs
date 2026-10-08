module Partas.AnimeJs.Generation.Tests.XanthamAdapterTests

open System
open System.IO
open System.Diagnostics
open NUnit.Framework
open Partas.AnimeJs.Generation

let request () =
    let root = AnimeJs.Build.Paths.findRoot __SOURCE_DIRECTORY__
    { RepositoryRoot = root
      InputDirectory = Path.Combine(root, "generation/node_modules/animejs")
      ConfigPath = Path.Combine(root, "generation/xantham.json")
      PolicyPath = Path.Combine(root, "generation/dsl-policy.json")
      SupportAssembly = ""
      SupportDocumentation = "" }

let run workingDirectory executable arguments =
    let info = ProcessStartInfo(executable, RedirectStandardOutput = true, RedirectStandardError = true, WorkingDirectory = workingDirectory)
    for argument in arguments do info.ArgumentList.Add argument
    use child = Process.Start info
    let stdout = child.StandardOutput.ReadToEndAsync()
    let stderr = child.StandardError.ReadToEndAsync()
    child.WaitForExit()
    child.ExitCode, stdout.Result + stderr.Result

[<Test>]
let ``raw consumer reuses upstream DOM identity`` () =
    let input = request ()
    let generated = XanthamAdapter.generate input |> Async.RunSynchronously
    match generated with
    | Error diagnostics -> Assert.Fail(sprintf "%A" diagnostics)
    | Ok raw ->
        let scratch = Path.Combine(input.RepositoryRoot, "obj/raw consumer with spaces")
        Directory.CreateDirectory scratch |> ignore
        for artifact in raw.Artifacts do
            let path = Path.Combine(scratch, artifact.RelativePath)
            Directory.CreateDirectory(Path.GetDirectoryName path) |> ignore
            File.WriteAllText(path, artifact.Content)
        let files = raw.Artifacts |> List.filter (fun file -> file.RelativePath.EndsWith(".fs"))
        let compile = files |> List.map (fun file -> $"<Compile Include=\"{file.RelativePath}\" />") |> String.concat ""
        File.WriteAllText(Path.Combine(scratch, "Raw.fsproj"), $"<Project Sdk=\"Microsoft.NET.Sdk\"><PropertyGroup><TargetFramework>net9.0</TargetFramework></PropertyGroup><ItemGroup>{compile}<Compile Include=\"Program.fs\" /></ItemGroup><ItemGroup><PackageReference Include=\"Fable.Core\" /><PackageReference Include=\"Xantham.Fable.Core\" /><PackageReference Include=\"Xantham.Fable.Core.TS\" /></ItemGroup></Project>")
        File.Copy(Path.Combine(input.RepositoryRoot, "tests/Consumers/Raw/Program.fs"), Path.Combine(scratch, "Program.fs"), true)
        let exitCode, output = run input.RepositoryRoot "dotnet" [ "build"; Path.Combine(scratch, "Raw.fsproj"); "--nologo" ]
        Assert.That(exitCode, Is.Zero, output)
        let text = files |> List.map _.Content |> String.concat "\n"
        Assert.That(text.Contains("Browser.Types"), Is.False)
        Assert.That(text.Contains("type HTMLElement ="), Is.False)
        Assert.That(text.Contains("type Iterable<'T, 'TReturn, 'TNext> = Fable.Core.TS.Es.Iterable"), Is.True)
        let fableCode, fableOutput = run input.RepositoryRoot "dotnet" [ "fable"; Path.Combine(scratch, "Raw.fsproj"); "-o"; Path.Combine(scratch, "output"); "-e"; ".fs.js" ]
        Assert.That(fableCode, Is.Zero, fableOutput)

