module Partas.AnimeJs.Generation.Tests.CssMetadataTests

open System.IO
open NUnit.Framework
open Partas.AnimeJs.Generation

let selected assembly xml =
    match CssMetadata.read assembly xml with
    | Ok values -> values
    | Error diagnostics -> Assert.Fail(sprintf "%A" diagnostics); []

[<Test>]
let ``CSS metadata preserves keys and docs without management or duplicate aliases`` () =
    let assembly = typeof<Fable.Core.TS.Dom.CSSStyleProperties>.Assembly.Location
    let properties = selected assembly (Path.ChangeExtension(assembly, ".xml"))
    let keys = properties |> List.map _.JsKey
    Assert.That(keys, Is.EqualTo(["backgroundColor"; "border-color"; "opacity"]))
    Assert.That(properties |> List.find (fun item -> item.JsKey = "opacity") |> _.Docs, Does.Contain("Opacity from the fixture"))
    Assert.That(properties |> List.find (fun item -> item.JsKey = "border-color") |> _.Name, Is.EqualTo("border-color"))

[<Test>]
let ``real Core TS CSS property selection excludes declaration management`` () =
    let packageRoot = System.AppContext.BaseDirectory
    let properties = selected (Path.Combine(packageRoot, "Xantham.Fable.Core.TS.dll")) (Path.Combine(packageRoot, "Xantham.Fable.Core.TS.xml"))
    let keys = properties |> List.map _.JsKey
    Assert.That(keys, Does.Contain("opacity"))
    Assert.That(keys, Does.Contain("backgroundColor"))
    for management in ["cssText"; "length"; "setProperty"; "Item"; "parentRule"] do Assert.That(keys, Does.Not.Contain(management))
    Assert.That(keys, Is.EqualTo(keys |> List.sort))
    Assert.That(keys.Length, Is.GreaterThan(500))

[<Test>]
let ``support fingerprint records package version and both content hashes`` () =
    let packageRoot = System.AppContext.BaseDirectory
    let assembly = Path.Combine(packageRoot, "Xantham.Fable.Core.TS.dll")
    let xml = Path.Combine(packageRoot, "Xantham.Fable.Core.TS.xml")
    let actual = CssMetadata.fingerprint assembly xml
    Assert.That(actual.Package, Is.EqualTo("Xantham.Fable.Core.TS"))
    Assert.That(actual.Version, Is.EqualTo("0.1.1"))
    Assert.That(actual.AssemblyHash.Length, Is.EqualTo(64))
    Assert.That(actual.DocumentationHash.Length, Is.EqualTo(64))
    Assert.That(actual, Is.EqualTo(CssMetadata.fingerprint assembly xml))

[<Test>]
let ``CSS wrappers use the AnimeJs tween contract`` () =
    let input = XanthamAdapterTests.request ()
    let generated = XanthamAdapter.generate input |> Async.RunSynchronously |> Result.toOption |> Option.get
    let language = Policy.load input.PolicyPath |> Result.toOption |> Option.get
    let model = Projection.project generated language |> Result.toOption |> Option.get
    let properties = selected (Path.Combine(System.AppContext.BaseDirectory, "Xantham.Fable.Core.TS.dll")) (Path.Combine(System.AppContext.BaseDirectory, "Xantham.Fable.Core.TS.xml"))
    let enriched = CssMetadata.attach model properties |> Result.toOption |> Option.get
    let opacity = enriched.CssFields |> List.find (fun field -> field.JsKey = "opacity")
    Assert.That(opacity.ValueType, Is.EqualTo(Named "TweenOptions"))
    Assert.That(opacity.Context, Is.EqualTo("Animation"))

[<Test>]
let ``malformed ampersands are repaired without double escaping valid entities`` () =
    let assembly = typeof<Fable.Core.TS.Dom.CSSStyleProperties>.Assembly.Location
    let original = File.ReadAllText(Path.ChangeExtension(assembly, ".xml"))
    let scratch = Path.Combine(Path.GetTempPath(), System.Guid.NewGuid().ToString("N") + ".xml")
    try
        File.WriteAllText(scratch, original.Replace("Opacity from the fixture documentation.", "R&D &amp; CSS documentation."))
        let properties = selected assembly scratch
        Assert.That(properties |> List.find (fun property -> property.JsKey = "opacity") |> _.Docs, Does.Contain("R&D & CSS"))
    finally File.Delete scratch
