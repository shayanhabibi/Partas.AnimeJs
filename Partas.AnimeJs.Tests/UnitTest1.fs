module Partas.AnimeJs.Tests.Main

open System
open NUnit.Framework

[<Test>]
let ``shipped assembly uses maintained Xantham identities without generation tooling`` () =
    let assembly = typeof<Partas.AnimeJs.Raw.JSAnimation>.Assembly
    let dependencies = assembly.GetReferencedAssemblies() |> Array.map _.Name
    Assert.That(dependencies, Does.Contain("Xantham.Fable.Core.TS"))
    for forbidden in ["Xantham.Generator"; "Xantham.TypeScript.Wire"; "Partas.Build"; "Nacara.Core"; "Browser"] do
        Assert.That(dependencies |> Array.exists (fun name -> name.StartsWith(forbidden, StringComparison.Ordinal)), Is.False, forbidden)
    Assert.That(typeof<Partas.AnimeJs.Raw.JSAnimation>.GetInterfaces(), Does.Contain(typeof<Partas.AnimeJs.Raw.Timer>))
    Assert.That(typeof<Partas.AnimeJs.Raw.Timer>.GetProperty("duration"), Is.Not.Null)
