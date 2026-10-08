module Partas.AnimeJs.Generation.Tests.ProjectionTests

open NUnit.Framework
open Xantham.Generator
open Partas.AnimeJs.Generation

let property name kind = FsProperty { Name = name; Docs = "documentation"; Tags = []; ReadOnly = false; Type = kind }
let interfaceDecl name parameters inherits members = FsInterface {
    Name = name; Docs = ""; Tags = []; Order = None; TypeParameters = parameters
    Inherits = inherits; Members = members; Entrypoint = None; CreateOverloads = []; Statics = [] }
let alias name parameters target = FsAbbrev {
    Name = name; Docs = ""; Tags = []; Order = None; TypeParameters = parameters; Target = target; Value = None }
let exported name parameter result = FsExports {
    Name = "Exports"; Owner = EntryModule; Members = [{
        Owner = EntryModule; HarvestIndex = 0; ExportName = name; SourceSymbolId = LanguagePrimitives.Int32WithMeasure 1; SignatureOrdinal = None
        Member = { Name = name; Docs = ""; Tags = []; TypeParameters = []; Binding = ImportNamed name; Settable = false
                   Body = ExportFunction ([{ Name = "parameters"; Optional = true; Rest = false; Type = parameter }], result) } }] }
let raw declarations = {
    Artifacts = []; Model = {
        ModuleName = "Partas.AnimeJs.Raw"; PackageName = LanguagePrimitives.StringWithMeasure "animejs"
        RuntimePackage = LanguagePrimitives.StringWithMeasure "animejs"; PackageDir = LanguagePrimitives.StringWithMeasure "/input"
        Decls = declarations; Findings = []; Files = []; ShadowedByLib = 0 } }
let policy fields = { Roots = [{ Export = "createTimer"; Name = "Timer"; Builder = "timer"; Parameter = "parameters"; Fields = fields }]; Transforms = [] }
let result declarations fields =
    match Projection.project (raw declarations) (policy fields) with
    | Ok value -> value
    | Error diagnostics -> Assert.Fail(sprintf "%A" diagnostics); Unchecked.defaultof<_>

[<Test>]
let ``aliases and generic inherited callbacks preserve handle identity`` () =
    let parameter = { Name = "T"; Constraint = None }
    let declarations = [
        interfaceDecl "Base" [parameter] [] [property "duration" (FsOption FsFloat); property "onComplete" (FsOption(FsFunc(FsTypeVar "T", FsObj)))]
        interfaceDecl "TimerParams" [] [FsApp("Base", [FsNamed "DerivedTimer"])] [property "duration" (FsOption FsFloat)]
        alias "Alias" [] (FsNamed "TimerParams")
        exported "createTimer" (FsNamed "Alias") (FsNamed "DerivedTimer") ]
    let model = result declarations ["duration"; "onComplete"]
    Assert.That(model.Fields |> List.filter (fun field -> field.JsKey = "duration") |> List.length, Is.EqualTo(1))
    let callback = model.Fields |> List.find (fun field -> field.JsKey = "onComplete")
    Assert.That(callback.ValueType, Is.EqualTo(Function(Named "DerivedTimer", Object)))
    Assert.That(callback.Optional, Is.True)
    Assert.That(callback.Docs, Is.EqualTo("documentation"))

[<Test>]
let ``optional property is distinct from optional union arm`` () =
    let model = result [
        interfaceDecl "TimerParams" [] [] [
            property "outer" (FsOption(FsErasedUnion [FsFloat; FsString; FsFloat]))
            property "inner" (FsErasedUnion [FsOption FsFloat; FsString]) ]
        exported "createTimer" (FsNamed "TimerParams") (FsNamed "Timer") ] ["outer"; "inner"]
    let outer = model.Fields |> List.find (fun field -> field.JsKey = "outer")
    let inner = model.Fields |> List.find (fun field -> field.JsKey = "inner")
    Assert.That(outer.Optional, Is.True)
    Assert.That(outer.ValueType, Is.EqualTo(Union [Number; Text]))
    Assert.That(inner.Optional, Is.False)
    Assert.That(inner.ValueType, Is.EqualTo(Union [Optional Number; Text]))

[<Test>]
let ``conflicting inherited contracts fail with field source`` () =
    let generated = raw [
        interfaceDecl "First" [] [] [property "duration" FsFloat]
        interfaceDecl "Second" [] [] [property "duration" FsString]
        interfaceDecl "TimerParams" [] [FsNamed "First"; FsNamed "Second"] []
        exported "createTimer" (FsNamed "TimerParams") (FsNamed "Timer") ]
    match Projection.project generated (policy ["duration"]) with
    | Ok _ -> Assert.Fail("Conflicting duration contracts accepted")
    | Error diagnostics -> Assert.That(diagnostics |> List.exists (fun item -> item.Code = "projection/field-conflict" && item.Source.IsSome), Is.True)

[<Test>]
let ``real AnimeJs factory roots project selected contexts`` () =
    let input = XanthamAdapterTests.request ()
    match XanthamAdapter.generate input |> Async.RunSynchronously, Policy.load input.PolicyPath with
    | Ok generated, Ok language ->
        match Projection.project generated language with
        | Error diagnostics -> Assert.Fail(sprintf "%A" diagnostics)
        | Ok model ->
            Assert.That(model.Factories.Length, Is.EqualTo(3))
            let callback = model.Fields |> List.find (fun field -> field.Context = "Animation" && field.JsKey = "onComplete")
            Assert.That(callback.ValueType, Is.EqualTo(Function(Named "JSAnimation", Object)))
            Assert.That(model.TweenType.IsSome, Is.True)
    | values -> Assert.Fail(sprintf "%A" values)

[<Test>]
let ``recursive alias terminates at named identity`` () =
    let model = result [
        alias "Recursive" [] (FsOption(FsNamed "Recursive"))
        interfaceDecl "TimerParams" [] [] [property "recursive" (FsNamed "Recursive")]
        exported "createTimer" (FsNamed "TimerParams") (FsNamed "Timer") ] ["recursive"]
    Assert.That(model.Fields.Head.ValueType, Is.EqualTo(Named "Recursive"))
    Assert.That(model.Fields.Head.Optional, Is.True)

[<Test>]
let ``unbound selected type variable is rejected`` () =
    let generated = raw [
        interfaceDecl "TimerParams" [] [] [property "duration" (FsTypeVar "Missing")]
        exported "createTimer" (FsNamed "TimerParams") (FsNamed "Timer") ]
    match Projection.project generated (policy ["duration"]) with
    | Ok _ -> Assert.Fail("Unbound variable accepted")
    | Error diagnostics -> Assert.That(diagnostics |> List.exists (fun diagnostic -> diagnostic.Code = "projection/unbound-variable"), Is.True)

[<Test>]
let ``missing and ambiguous exports fail explicitly`` () =
    let root = interfaceDecl "TimerParams" [] [] [property "duration" FsFloat]
    for declarations, code in [ [root], "projection/missing-factory"; [root; exported "createTimer" (FsNamed "TimerParams") FsObj; exported "createTimer" (FsNamed "TimerParams") FsFloat], "projection/ambiguous-factory" ] do
        match Projection.project (raw declarations) (policy ["duration"]) with
        | Ok _ -> Assert.Fail("Invalid export selection accepted")
        | Error diagnostics -> Assert.That(diagnostics |> List.exists (fun diagnostic -> diagnostic.Code = code), Is.True)
