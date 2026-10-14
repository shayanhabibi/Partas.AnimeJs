module Partas.AnimeJs.Generation.Tests.PackageTests

open System
open System.IO
open System.IO.Compression
open NUnit.Framework

[<Test>]
let ``packed NuGet sources compile and run in an isolated consumer`` () =
    let root = AnimeJs.Build.Paths.findRoot __SOURCE_DIRECTORY__
    let scratch = Path.Combine(Path.GetTempPath(), "anime package consumer " + Guid.NewGuid().ToString("N"))
    Directory.CreateDirectory scratch |> ignore
    let run cwd executable arguments =
        let code, output = XanthamAdapterTests.run cwd executable arguments
        Assert.That(code, Is.Zero, output)
    try
        let feed = Path.Combine(scratch, "feed")
        run root "dotnet" ["pack"; "Partas.AnimeJs/Partas.AnimeJs.fsproj"; "-c"; "Release"; "-o"; feed]
        let package = Path.Combine(feed, "Partas.AnimeJs.0.1.1.nupkg")
        use archive = ZipFile.OpenRead package
        let sources = archive.Entries |> Seq.filter (fun entry -> entry.FullName.StartsWith("fable/") && entry.FullName.EndsWith(".fs")) |> Seq.map _.FullName |> Seq.toList
        Assert.That(sources |> List.exists (fun name -> name.EndsWith("Dsl.fs")), Is.True, "DSL source must ship")
        Assert.That(sources |> List.exists (fun name -> name.EndsWith("Runtime.fs")), Is.True, "Runtime source must ship")
        archive.Dispose()
        File.WriteAllText(Path.Combine(scratch, "Consumer.fsproj"), """<Project Sdk="Microsoft.NET.Sdk"><PropertyGroup><TargetFramework>net9.0</TargetFramework><RestorePackagesPath>packages</RestorePackagesPath><DisableImplicitFSharpCoreReference>true</DisableImplicitFSharpCoreReference></PropertyGroup><ItemGroup><Compile Include="Witness.fs"/><PackageReference Include="Partas.AnimeJs" Version="0.1.1"/><PackageReference Include="FSharp.Core" Version="10.1.401"/></ItemGroup></Project>""")
        let escape value = System.Security.SecurityElement.Escape value
        File.WriteAllText(Path.Combine(scratch, "NuGet.Config"), $"""<configuration><packageSources><clear/><add key="local" value="{escape feed}"/><add key="nuget.org" value="https://api.nuget.org/v3/index.json"/><add key="xantham" value="https://nuget.cloudsmith.io/shayanhabibi/xantham/v3/index.json"/></packageSources><packageSourceMapping><packageSource key="local"><package pattern="Partas.AnimeJs"/></packageSource><packageSource key="xantham"><package pattern="Xantham.*"/></packageSource><packageSource key="nuget.org"><package pattern="*"/></packageSource></packageSourceMapping></configuration>""")
        for source, destination in ["tests/Consumers/Positive/Witness.fs", "Witness.fs"; "tests/Consumers/Positive/witness.test.mjs", "witness.test.mjs"; "global.json", "global.json"; "package.json", "package.json"; "package-lock.json", "package-lock.json"] do
            File.Copy(Path.Combine(root, source), Path.Combine(scratch, destination))
        run scratch "dotnet" ["restore"; "Consumer.fsproj"; "--packages"; Path.Combine(scratch, "packages")]
        let assets = File.ReadAllText(Path.Combine(scratch, "obj/project.assets.json"))
        for forbidden in ["Xantham.Generator/"; "Xantham.Wire/"; "Partas.Build/"; "Nacara."] do Assert.That(assets, Does.Not.Contain(forbidden))
        for required in ["Xantham.Fable.Core/0.1.1"; "Xantham.Fable.Core.TS/0.1.1"] do Assert.That(assets, Does.Contain(required))
        run scratch "dotnet" ["build"; "Consumer.fsproj"; "--no-restore"]
        if OperatingSystem.IsWindows() then
            let code, nodePath = XanthamAdapterTests.run root "node" ["--print"; "process.execPath"]
            Assert.That(code, Is.Zero, nodePath)
            let npmCli = Path.Combine(Path.GetDirectoryName(nodePath.Trim()), "node_modules/npm/bin/npm-cli.js")
            run scratch "node" [npmCli; "ci"; "--omit=dev"; "--ignore-scripts"]
        else run scratch "npm" ["ci"; "--omit=dev"; "--ignore-scripts"]
        run root "dotnet" ["fable"; Path.Combine(scratch, "Consumer.fsproj"); "--noCache"; "-o"; Path.Combine(scratch, "output"); "-e"; ".fs.js"]
        run scratch "node" ["--test"; "witness.test.mjs"]
    finally
        if Directory.Exists scratch then Directory.Delete(scratch, true)
