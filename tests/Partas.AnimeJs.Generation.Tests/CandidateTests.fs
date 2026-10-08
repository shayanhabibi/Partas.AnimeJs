module Partas.AnimeJs.Generation.Tests.CandidateTests

open System
open System.IO
open NUnit.Framework
open Partas.AnimeJs.Generation

let candidate artifacts = {
    Artifacts = artifacts; Diagnostics = []
    Provenance = {
        SchemaVersion = 1; Versions = Map.empty; InputIntegrity = "fixture"; InputHashes = Map.empty
        ArtifactHashes = Map.empty; Findings = []; ExcludedFields = []; CompilationDigest = "fixture" } }

let withOutput test =
    let root = Path.Combine(Path.GetTempPath(), "anime-output-" + Guid.NewGuid().ToString("N"))
    Directory.CreateDirectory root |> ignore
    try
        File.WriteAllText(Path.Combine(root, "Raw.fs"), "old raw")
        File.WriteAllText(Path.Combine(root, "Dsl.fs"), "old dsl")
        File.WriteAllText(Path.Combine(root, "unrelated.txt"), "keep me")
        test root
    finally Directory.Delete(root, true)

[<Test>]
let ``check reports drift without changing existing output`` () = withOutput (fun root ->
    let proposed = candidate [{ RelativePath = "Raw.fs"; Content = "new raw" }]
    Assert.That(Candidate.compare root proposed |> Result.isError, Is.True)
    Assert.That(File.ReadAllText(Path.Combine(root, "Raw.fs")), Is.EqualTo("old raw"))
    Assert.That(File.ReadAllText(Path.Combine(root, "Dsl.fs")), Is.EqualTo("old dsl"))
    Assert.That(File.ReadAllText(Path.Combine(root, "unrelated.txt")), Is.EqualTo("keep me")))

[<Test>]
let ``failed second promotion write preserves all prior files`` () = withOutput (fun root ->
    let mutable count = 0
    let write (path: string) (text: string) =
        count <- count + 1
        if count = 2 then raise (IOException "Injected second write failure")
        File.WriteAllText(path, text)
    let proposed = candidate [{ RelativePath = "Raw.fs"; Content = "new raw" }; { RelativePath = "Dsl.fs"; Content = "new dsl" }]
    Assert.That(Candidate.promoteWith write root proposed |> Result.isError, Is.True)
    Assert.That(File.ReadAllText(Path.Combine(root, "Raw.fs")), Is.EqualTo("old raw"))
    Assert.That(File.ReadAllText(Path.Combine(root, "Dsl.fs")), Is.EqualTo("old dsl"))
    Assert.That(File.ReadAllText(Path.Combine(root, "unrelated.txt")), Is.EqualTo("keep me")))

[<Test>]
let ``promotion rejects paths outside the managed root`` () = withOutput (fun root ->
    let proposed = candidate [{ RelativePath = "../outside.fs"; Content = "outside" }]
    Assert.That(Candidate.promote root proposed |> Result.isError, Is.True))

[<Test>]
let ``successful promotion preserves unrelated files`` () = withOutput (fun root ->
    let proposed = candidate [{ RelativePath = "Raw.fs"; Content = "new raw" }; { RelativePath = "Dsl.fs"; Content = "new dsl" }]
    Assert.That(Candidate.promote root proposed |> Result.isOk, Is.True)
    Assert.That(File.ReadAllText(Path.Combine(root, "Raw.fs")), Is.EqualTo("new raw"))
    Assert.That(File.ReadAllText(Path.Combine(root, "unrelated.txt")), Is.EqualTo("keep me"))
    Assert.That(Candidate.compare root proposed |> Result.isOk, Is.True))

[<Test>]
let ``unreviewed selected finding fails with source diagnostic`` () =
    let generated = ProjectionTests.raw [
        ProjectionTests.interfaceDecl "TimerParams" [] [] [ProjectionTests.property "duration" Xantham.Generator.FsFloat]
        ProjectionTests.exported "createTimer" (Xantham.Generator.FsNamed "TimerParams") (Xantham.Generator.FsNamed "Timer") ]
    let model = Projection.project generated (ProjectionTests.policy ["duration"]) |> Result.toOption |> Option.get
    let finding = Xantham.Generator.Finding.make "TimerParams.duration" Xantham.Generator.TypeReference.UnknownToObj
    match Findings.validate model [finding] [] with
    | Ok _ -> Assert.Fail("Unreviewed selected finding accepted")
    | Error diagnostics ->
        Assert.That(diagnostics.Head.Source, Is.EqualTo(Some "TimerParams.duration"))
        Assert.That(diagnostics.Head.Code, Is.EqualTo("fidelity/unreviewed"))

[<Test>]
let ``promotion removes stale owned artifacts and check remains read only`` () = withOutput (fun root ->
    let first = candidate [{ RelativePath = "Raw.fs"; Content = "raw" }; { RelativePath = "Nested/Old.fs"; Content = "old" }]
    Assert.That(Candidate.promote root first |> Result.isOk, Is.True)
    let next = candidate [{ RelativePath = "Raw.fs"; Content = "raw" }]
    Assert.That(Candidate.compare root next |> Result.isError, Is.True)
    Assert.That(File.Exists(Path.Combine(root, "Nested/Old.fs")), Is.True)
    Assert.That(Candidate.promote root next |> Result.isOk, Is.True)
    Assert.That(File.Exists(Path.Combine(root, "Nested/Old.fs")), Is.False)
    Assert.That(File.ReadAllText(Path.Combine(root, "unrelated.txt")), Is.EqualTo("keep me")))

[<Test>]
let ``promotion recovers previous directory after interrupted swap`` () = withOutput (fun root ->
    let backup = root + ".generation-backup"
    Directory.Move(root, backup)
    try
        let failWrite (_: string) (_: string) = raise (IOException "Injected write failure after recovery")
        let proposed = candidate [{ RelativePath = "Raw.fs"; Content = "new raw" }]
        Assert.That(Candidate.promoteWith failWrite root proposed |> Result.isError, Is.True)
        Assert.That(File.ReadAllText(Path.Combine(root, "Raw.fs")), Is.EqualTo("old raw"))
        Assert.That(File.ReadAllText(Path.Combine(root, "unrelated.txt")), Is.EqualTo("keep me"))
    finally
        if Directory.Exists backup then Directory.Move(backup, root))

[<Test>]
let ``candidate pipeline produces compilation validated deterministic artifacts`` () =
    let original = XanthamAdapterTests.request ()
    let request = {
        original with
            SupportAssembly = Path.Combine(AppContext.BaseDirectory, "Xantham.Fable.Core.TS.dll")
            SupportDocumentation = Path.Combine(AppContext.BaseDirectory, "Xantham.Fable.Core.TS.xml") }
    let generate () =
        match Pipeline.generate request |> Async.RunSynchronously with
        | Ok result -> result
        | Error diagnostics -> Assert.Fail(sprintf "%A" diagnostics); Unchecked.defaultof<_>
    let first = generate ()
    let second = generate ()
    Assert.That(second.Artifacts, Is.EqualTo(first.Artifacts))
    Assert.That(first.Provenance.CompilationDigest, Has.Length.EqualTo(64))
    Assert.That(first.Provenance.ArtifactHashes.Count, Is.GreaterThan(3))
    Assert.That(first.Artifacts |> List.exists (fun artifact -> artifact.Content.Contains(request.RepositoryRoot)), Is.False)

[<Test>]
let ``failed candidate compilation preserves prior output`` () = withOutput (fun root ->
    let request = XanthamAdapterTests.request ()
    let result = Pipeline.validateSources request [{ RelativePath = "Dsl.fs"; Content = "module Broken\nlet value: int = \"wrong\"\n" }] |> Async.RunSynchronously
    match result with
    | Ok () -> Assert.Fail("Invalid candidate compiled")
    | Error errors ->
        Assert.That(errors.Head.Code, Is.EqualTo("candidate/dotnet"))
        Assert.That(errors.Head.Message, Does.Contain("FS0001"))
    Assert.That(File.ReadAllText(Path.Combine(root, "Raw.fs")), Is.EqualTo("old raw"))
    Assert.That(File.ReadAllText(Path.Combine(root, "unrelated.txt")), Is.EqualTo("keep me")))
