module Partas.AnimeJs.Generation.Tests.ShippingTests

open System.IO
open NUnit.Framework

[<Test>]
let ``actual shipped library compiles the documented example and runs AnimeJs`` () =
    let root = AnimeJs.Build.Paths.findRoot __SOURCE_DIRECTORY__
    for arguments in [ ["build"; "tests/Consumers/Positive/Positive.fsproj"]; ["fable"; "tests/Consumers/Positive/Positive.fsproj"; "-o"; "tests/Consumers/Positive/output"; "-e"; ".fs.js"] ] do
        let code, output = XanthamAdapterTests.run root "dotnet" arguments
        Assert.That(code, Is.Zero, output)
    let code, output = XanthamAdapterTests.run root "node" ["--test"; "tests/Consumers/Positive/witness.test.mjs"]
    Assert.That(code, Is.Zero, output)

[<Test>]
let ``Nacara publishes tested guide migration and API reference`` () =
    let root = AnimeJs.Build.Paths.findRoot __SOURCE_DIRECTORY__
    let code, output = XanthamAdapterTests.run root "dotnet" [Path.Combine(System.AppContext.BaseDirectory, "Build.dll"); "docs"]
    Assert.That(code, Is.Zero, output)
    let site = Path.Combine(root, "site/output")
    Assert.That(File.Exists(Path.Combine(site, "getting-started/index.html")), Is.True)
    Assert.That(File.Exists(Path.Combine(site, "migration/index.html")), Is.True)
    Assert.That(Directory.EnumerateFiles(Path.Combine(site, "reference"), "*.html", SearchOption.AllDirectories) |> Seq.length, Is.GreaterThan(3))
    Assert.That(File.ReadAllText(Path.Combine(site, "getting-started/index.html")), Does.Contain("Tween.fromTo"))
