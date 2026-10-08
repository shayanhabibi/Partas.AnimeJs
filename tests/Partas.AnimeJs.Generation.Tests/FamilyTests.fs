module Partas.AnimeJs.Generation.Tests.FamilyTests
open NUnit.Framework
[<Test>]
let ``timer timeline keyframes and stagger compile and run`` () =
    let root = AnimeJs.Build.Paths.findRoot __SOURCE_DIRECTORY__
    for arguments in [ ["build"; "tests/Consumers/Families/Families.fsproj"]; ["fable"; "tests/Consumers/Families/Families.fsproj"; "-o"; "tests/Consumers/Families/output"; "-e"; ".fs.js"] ] do
        let code, output = XanthamAdapterTests.run root "dotnet" arguments
        Assert.That(code, Is.Zero, output)
    let code, output = XanthamAdapterTests.run root "node" ["--test"; "tests/Consumers/Families/witness.test.mjs"]
    Assert.That(code, Is.Zero, output)
