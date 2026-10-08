module Partas.AnimeJs.Generation.Tests.RuntimeTests

open NUnit.Framework

[<Test>]
let ``builder consumer compiles and materializes correct JavaScript semantics`` () =
    let root = AnimeJs.Build.Paths.findRoot __SOURCE_DIRECTORY__
    for executable, arguments in [
        "dotnet", ["build"; "tests/Consumers/Runtime/Runtime.fsproj"]
        "dotnet", ["fable"; "tests/Consumers/Runtime/Runtime.fsproj"; "-o"; "tests/Consumers/Runtime/output"; "-e"; ".fs.js"; "--typedArrays"; "false"]
        "node", ["--test"; "tests/Consumers/Runtime/options.test.mjs"] ] do
        let exitCode, output = XanthamAdapterTests.run root executable arguments
        Assert.That(exitCode, Is.Zero, output)

[<Test>]
let ``wrong context fails with intended type diagnostic`` () =
    let root = AnimeJs.Build.Paths.findRoot __SOURCE_DIRECTORY__
    let exitCode, output = XanthamAdapterTests.run root "dotnet" ["build"; "tests/Consumers/Negative/Context.fsproj"]
    Assert.That(exitCode, Is.Not.Zero)
    Assert.That(output, Does.Contain("FS0001"))
    Assert.That(output, Does.Contain("Field<AnimationContext>"))
    Assert.That(output, Does.Contain("Field<TimerContext>"))
    Assert.That(output, Does.Not.Contain("FS0039"))
