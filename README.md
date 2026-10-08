# Partas.AnimeJs

A generated, typed AnimeJs DSL for Fable F#. This migration targets **AnimeJs 4.5.0**,
**Fable 5.20.0**, and **Fable.Core 5.3.0**. Version 0.1.0 introduces a fresh DSL;
existing computation expressions require migration.

Build reusable options with `animation`, `timer` and `timeline`. Fields such as
`Animation.duration`, `Css.opacity` and `Transform.x` retain their context and value
types. Call `Anime.animate`, `Anime.createTimer` or `Anime.createTimeline` to start
effects. Handles and callback arguments use the generated `Partas.AnimeJs.Raw` API.

See the [tested getting-started source](examples/GettingStarted.fs),
[migration guide](site/content/migration.md), and [regeneration guide](site/content/regeneration.md).
The Nacara site renders the same F# examples compiled by the consumer tests and
generates reference pages from the shipped assembly and XML documentation.

CSS property names and documentation, DOM/SVG and ECMAScript identities come from
`Xantham.Fable.Core.TS`, rather than a repository-maintained catalog. AnimeJs determines
the tween and callback contracts. `Animation.property` provides an explicit custom
target-property escape, and the full generated raw API remains available.

Install `Partas.AnimeJs` and its maintained Xantham dependencies, then install
`animejs@4.5.0` in your JavaScript project. The repository's [NuGet.Config](NuGet.Config)
documents the public Xantham Cloudsmith source. Consumers do not need generation
tools, Partas.Build, or Nacara.

Development uses the stable .NET 10 SDK selected by `global.json`. The library and
consumer projects target .NET 9; running their .NET tests requires the .NET 9 runtime.
All repository commands use the compiled Partas.Build CLI:

```shell
dotnet run --project Build.fsproj -- restore
dotnet run --project Build.fsproj -- generate
dotnet run --project Build.fsproj -- generate --check
dotnet run --project Build.fsproj -- docs
dotnet run --project Build.fsproj -- docs --watch
```

`--help` describes options, and `--explain` describes stages without running them.
Generation analyzes the locked AnimeJs declarations once, projects fields and shared
CSS metadata, checks selected fidelity findings, and compiles a scratch candidate with
.NET and Fable before promotion. Check mode reports drift without modifying output.
Provenance records exact versions, input integrity, reviewed findings and artifact hashes.

The migration replaces the legacy static abstract interface calls that produced
FS3866 under the stable SDK. Acceptance gates exercise actual AnimeJs effects in
JavaScript, shared type identities, rejected invalid consumers, immutable option
construction, deterministic generation and failed-promotion preservation.

Package publication and site deployment are separate from local verification.
