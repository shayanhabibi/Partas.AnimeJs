# Xantham AnimeJs DSL Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking. Recommended execution for this plan: native, because the adapter, projection, runtime, and emitter share interfaces that must be proven sequentially.

**Goal:** Replace the handwritten AnimeJs binding/DSL with a reproducible generated DSL targeting AnimeJs 4.5.0, sharing Xantham types, operated through Partas.Build and documented with Nacara.

**Architecture:** A compiled Partas.Build project calls a generation library that runs Xantham once, projects its structured declarations and upstream CSS metadata, and emits a validated candidate. A small typed builder runtime materializes options; generated factories explicitly start AnimeJs effects. Checked-in output, tested examples, and a Nacara reference site are verified through the same repository CLI.

**Tech Stack:** F#; .NET 10 tooling; Fable 5.20.0; Fable.Core 5.3.0; Xantham CLI 0.2.0, Generator 0.1.0, Wire 0.2.1, Core/Core.TS 0.1.1; AnimeJs 4.5.0; Partas.Build 0.8.1/Baked 0.3.0; Nacara .NET site program; NUnit; Node built-in test runner; Playwright browser tests.

**Spec:** [Approved design revision 2](../specs/2026-10-08-xantham-animejs-dsl-design.md).

## Global Constraints

- Fresh DSL; source compatibility with the existing computation expressions is not required.
- Resolve the latest AnimeJs release at implementation start; current verified latest is **4.5.0**. Record the exact version and integrity in the npm lockfile. Normal builds never resolve floating `latest`.
- Keep the existing Fable **5.20.0**, Fable.Core **5.3.0**, and stable .NET 10 SDK selection. Tooling targets `net10.0`; preserve the library's `net9.0` target unless a proven dependency requires changing it.
- Pin `xantham` **0.2.0**, `Xantham.Generator` **0.1.0**, `Xantham.TypeScript.Wire` **0.2.1**, and `Xantham.Fable.Core`/`Xantham.Fable.Core.TS` **0.1.1** from the documented Xantham Cloudsmith source. Do not add Node bindings without a demonstrated need.
- Pin `Partas.Build` **0.8.1** and `Partas.Build.Baked` **0.3.0** from the separate Partas.Build Cloudsmith source. `Build.fsproj` is the only repository command parser.
- Candidate Nacara pins: Core **2.0.0-beta.6**, FSharpApi **1.0.0-beta.5**, Markdown **1.0.0-beta.2**, Literate **1.0.0-beta.1**, Theme.Default **1.0.0-beta.14**. Prove compatibility before adopting; use exact compatible versions for additional plugins.
- Reference shared compiler-library types under `Fable.Core.TS.Dom`/`Fable.Core.TS.Es`; do not generate local CSS/DOM/ES producers or use an unversioned sibling catalog.
- CSS property names/docs come from restored Core.TS metadata. AnimeJs determines tween value shapes. Transform conveniences use a separate `Transform` surface.
- Generation is deterministic, findings are reviewed, failed candidates do not replace checked-in output, and constructing options has no AnimeJs effects.
- Returned handles and callbacks use generated raw types. No duplicated AnimeJs class/method declarations, permissive fallback casts, or string parsing of generated F# to reconstruct signatures.
- Documentation examples and verified consumer files are the same source. A packed-package Fable consumer is required.
- Existing user changes, including staged `.idea` files and the earlier Fable upgrade, are preserved. Commit only explicitly named files owned by the implementation task.
- Shell commands in this plan use the user's RTK wrapper. Product consumers must not need generation tools or repository automation packages.
- Package publication and site deployment are outside this implementation's local verification.

## Review Focus

1. A repository path containing spaces or a working directory outside the repo must still resolve locked inputs correctly; Task 1 tests this.
2. A failed generation/check must preserve existing artifacts, including on an interrupted multi-file promotion; Task 7 tests rollback and check-mode immutability.
3. Generic aliases and inherited optional members must preserve callback types and not lose union arms; Task 3 tests a synthetic declaration graph before AnimeJs integration.
4. CSS aliases, declaration-management members, and escaped property names must not generate duplicate or invalid fields; Task 4 tests metadata selection and emitted access.
5. Two options built from one reusable specification must not share mutable state; absent fields and explicit null must remain distinct; Task 5 tests emitted JavaScript behavior.

---

## File map and execution order

The plan delivers a vertical slice in Tasks 1–8, then expands the factory families in Tasks 9–10. Task 11 closes packaging and CI. Do not start broad DSL generation before the raw binding and shared types compile.

- `NuGet.Config`, `Directory.Packages.props`: exact tool/library/site dependencies and feed ownership.
- `Build.fsproj`, `Build/{Paths,Options,Stages,Commands,Program}.fs`: tooling bootstrap, options, operations, command composition, entry point.
- `tools/Partas.AnimeJs.Generation/Partas.AnimeJs.Generation.fsproj`: generation library, no executable entry point.
- `tools/Partas.AnimeJs.Generation/{Contracts,XanthamAdapter,Projection,CssMetadata,Emit,Findings,Candidate,Pipeline}.fs`: respectively shared contracts, upstream conversion, DSL projection, shared CSS inventory, output, fidelity validation, transactional files, orchestration.
- `generation/{package.json,package-lock.json,xantham.json,dsl-policy.json,findings-baseline.json}`: locked inputs, sharing configuration, language policy, reviewed diagnostics.
- `Partas.AnimeJs/Dsl/{Runtime,Conveniences}.fs`: field accumulation and explicitly owned conveniences.
- `Partas.AnimeJs/Generated/{Raw,Dsl,Generated.props,provenance.json}`: generated source and deterministic ordered source inventory.
- `tests/Partas.AnimeJs.Generation.Tests/{*.fs,Partas.AnimeJs.Generation.Tests.fsproj}`: NUnit unit and integration tests against generation tooling only.
- `tests/Consumers/{Positive,Negative,Runtime,Browser,Package}`: independently compiled consumer projects and JS/browser harnesses.
- `examples/{GettingStarted,Timer,Timeline,Stagger,Spring,Draggable,Scroll,SvgText}.fs`: shared examples for acceptance tests and docs.
- `site/{site.fsproj,Site.fs,content/}`: Nacara site and authored guides.
- `.github/workflows/verify.yml`: calls the same Build commands on Windows/Linux; no duplicated generation algorithm.

Use an isolated worktree at execution time after reading `superpowers:using-git-worktrees`. Carry the approved spec/plan and earlier authorized Fable edits into the execution workspace without copying `.idea`/`.serena` state or disturbing staged files. Record the starting branch and baseline failures.

### Task 1: Bootstrap the compiled Partas.Build CLI independently

**Files:** Create `NuGet.Config`, `Directory.Packages.props`, `Build.fsproj`, `Build/Paths.fs`, `Build/Options.fs`, `Build/Stages.fs`, `Build/Commands.fs`, `Build/Program.fs`, and the generation library/test project skeletons. Modify `dotnet-tools.json`, `.gitignore`.

**Interfaces:** `Paths.findRoot : string -> string` locates the repository; `Commands.create : string -> RootCommandDefinition` defines a reusable CLI; `Program.main : string array -> int` invokes it. The Build project initially references only generation tooling, never the currently broken library.

- [ ] Add the NUnit project targeting `net10.0`, referencing the generation library; use the existing NUnit/Test SDK/adapter versions initially. Add failing root-discovery tests using a temporary directory named `anime repo with spaces`, a nested child, and a directory outside the repo. Root discovery uses `global.json` plus `Partas.AnimeJs/Partas.AnimeJs.fsproj`, not the caller's current directory.

```fsharp
[<Test>]
let ``nested location resolves repository root`` () =
    let actual = Paths.findRoot nestedDirectory
    Assert.That(actual, Is.EqualTo(repositoryDirectory))
```

- [ ] Run `rtk dotnet test tests/Partas.AnimeJs.Generation.Tests/Partas.AnimeJs.Generation.Tests.fsproj --filter FullyQualifiedName~Paths`; expect the new behavior test to fail before root discovery is implemented.
- [ ] Implement root discovery and the tooling-only build project. Use these verified Partas.Build entry points:

```fsharp
let root = Command.root {
    description "Partas.AnimeJs repository automation"
    command "restore" { restoreStage }
}

[<EntryPoint>]
let main argv = (Command.invoke argv root).ExitCode
```

  Define `restoreStage` in `Build/Stages.fs`; it restores local tools and the locked generation npm input once Task 2 adds it. Stage execution uses Partas.Build command values with separate argument/path values, not shell-concatenated strings.
- [ ] Add both public Cloudsmith feeds plus nuget.org. Map `xantham`/`Xantham.*` exclusively to the Xantham feed and `Partas.Build`/`Partas.Build.*` exclusively to the Partas feed. Resolve other packages from nuget.org. Put the approved exact package versions in central package management; remove conflicting explicit Version attributes from projects being converted.
- [ ] Pin the local Xantham tool without updating the global installation. Verify `rtk dotnet run --project Build.fsproj -- --help`, `restore --help`, and `restore --explain` all exit successfully; add a CLI test proving an unknown option returns usage exit code 2 without running stages.
- [ ] Run the focused path/CLI tests and `rtk dotnet build Build.fsproj`. Commit only the named bootstrap/config/test files with `feat: bootstrap Partas.Build repository CLI`.

### Task 2: Generate raw AnimeJs bindings against shared Xantham types

**Files:** Create generation input files, `tools/Partas.AnimeJs.Generation/Contracts.fs`, `XanthamAdapter.fs`, and `tests/Partas.AnimeJs.Generation.Tests/XanthamAdapterTests.fs`; create `tests/Consumers/Raw/Raw.fsproj` and `Program.fs` as a scratch-capable raw compile consumer.

**Interfaces:** Define these repo-owned contracts in `Contracts.fs` before downstream work:

```fsharp
type Diagnostic = { Code: string; Source: string option; Message: string }
type Artifact = { RelativePath: string; Content: string }
type GenerationRequest = {
    RepositoryRoot: string
    InputDirectory: string
    ConfigPath: string
    PolicyPath: string
    SupportAssembly: string
    SupportDocumentation: string
}
type RawGeneration = {
    Artifacts: Artifact list
    Model: Xantham.Generator.RenderModel
}
// Xantham-facing types remain confined to the adapter/projection layer.
// Emit/runtime/Build contracts consume the repo-owned model added in Task 3.
```

  `XanthamAdapter.generate : GenerationRequest -> Async<Result<RawGeneration, Diagnostic list>>` calls `GeneratorConfig.loadFile` and `Pipeline.generate` once. It returns files without writing tracked output.

- [ ] Requery npm's latest tag; if it is still 4.5.0, pin `"animejs": "4.5.0"` in `generation/package.json`. If it changed, record the new exact version and adjust the provenance/spec version before running generation. Create the npm lockfile and use `npm ci` afterward.
- [ ] Put the approved reference-group/compilerLib configuration in `generation/xantham.json`. Pass the installed AnimeJs package directory as the generator input; config is a separately resolved absolute path. Initialize the compiler through the project-local Xantham CLI.
- [ ] Add an integration test that generates to memory and creates a scratch compile project containing the returned raw source and the Core/Core.TS package references. Its consumer accepts `Fable.Core.TS.Dom.HTMLElement` and invokes the raw `animate` export. Assert no emitted source declares a local DOM producer and there are no `Browser.Types` references.

```fsharp
[<Test>]
let ``raw consumer reuses upstream DOM identity`` () =
    let result = runRawConsumerCompile ()
    Assert.That(result.ExitCode, Is.Zero, result.Output)
    Assert.That(result.GeneratedText.Contains("Browser.Types"), Is.False)
```

  `runRawConsumerCompile` is the task's test helper: restore exact input, call `XanthamAdapter.generate`, write a caller-owned scratch project with source inventory, run `dotnet build`, and return captured exit/output/text.
- [ ] Run the new test; record its actual failing resolution/compile diagnostic. Implement the adapter and correct producer qualification using supported settings or explicit reviewed mappings. Do not suppress missing producer identities with `obj`. No sibling checkout catalog is required.
- [ ] Verify raw .NET compilation, raw Fable compilation, and an actual shared-DOM consumer. Snapshot input version, generated ordered file names, and package identities; do not claim the existing golden proves success.
- [ ] Commit the raw adapter/input/consumer files with `feat: generate AnimeJs raw bindings with shared Xantham types`.

### Task 3: Project the structured declaration model into DSL contexts

**Files:** Create `Projection.fs`, `generation/dsl-policy.json`, and `ProjectionTests.fs`; extend `Contracts.fs`.

**Interfaces:** Introduce `DslType` cases for primitives, named identities, generic applications, type variables, optional values, arrays, tuples, unions, functions, delegates, and brands. Preserve generic constraints separately. Define `FieldSpec` with context, F# name, original JS key, value type, docs, source identity, and optionality; `FactorySpec` carries owned raw export, arguments, result, and context. `DslModel` contains those plus diagnostics. `Projection.project : RawGeneration -> Policy -> Result<DslModel, Diagnostic list>` resolves aliases/heritage while preserving generic substitution. `Policy.load : string -> Result<Policy, Diagnostic list>` lives with projection for the first implementation.

- [ ] Add synthetic `FsDecl` fixtures for an alias of `TimerParams`, inherited `duration`, a callback using the derived handle type, `option<union>` versus `union<option<_>>`, and a generic base instantiated at a concrete type. Assert exact projected types and one deduplicated inherited field.

```fsharp
Assert.That(field.JsKey, Is.EqualTo("onComplete"))
Assert.That(field.ValueType, Is.EqualTo(expectedDerivedCallbackType))
Assert.That(projectedFields |> List.filter (fun f -> f.JsKey = "duration") |> List.length,
            Is.EqualTo(1))
```

- [ ] Run `rtk dotnet test tests/Partas.AnimeJs.Generation.Tests/Partas.AnimeJs.Generation.Tests.fsproj --filter FullyQualifiedName~Projection`; expect the projection assertions to fail before implementation.
- [ ] Implement exhaustive `FsTypeRef` conversion. Resolve aliases with a visited set; on recursive traversal retain named identity rather than recurse infinitely. Flatten heritage with substituted generic arguments; reject conflicting same-key contracts with a source diagnostic.
- [ ] Select initial `animate`, `createTimer`, and `createTimeline` contexts from actual raw export parameters. Policy assigns public names and initial conveniences; it does not reproduce raw signatures. Restrict initial emitted fields to `duration`, `ease`, `autoplay`, `onComplete`, and selected tween fields while recording the rest as intentionally outside the first slice.
- [ ] Reject absent policy roots, ambiguous factories, unbound type parameters, and unsupported selected field shapes with specific codes. Canonicalize union arms by mapped type identity and preserve a canonical union entry point when arms have colliding signatures.
- [ ] Run synthetic tests plus the real AnimeJs projection integration test; commit `feat: project Xantham declarations into typed DSL contexts`.

### Task 4: Derive CSS fields from the restored support assembly

**Files:** Create `CssMetadata.fs` and `CssMetadataTests.fs`; extend `DslModel` with `CssProperty` records carrying emitted name, JS key, upstream identity, and docs.

**Interfaces:** `CssMetadata.read : assemblyPath:string -> xmlPath:string -> Result<CssProperty list, Diagnostic list>` reads metadata without executing the support assembly. Use `System.Reflection.Metadata`/`PEReader` with interface-heritage traversal; do not depend on loading a compatible FSharp.Core into the tool process.

- [ ] Add a test fixture assembly with ordinary string properties, inherited properties, a backtick-escaped property name, duplicate aliases, an indexer, a readonly management property, and `cssText`. Add matching XML comments. Assert stable sorted names, preserved JS keys/docs, and explicit management-member exclusions.
- [ ] Add a real-package test with these expectations:

```fsharp
Assert.That(keys, Does.Contain("opacity"))
Assert.That(keys, Does.Contain("backgroundColor"))
Assert.That(keys, Does.Not.Contain("cssText"))
Assert.That(keys, Does.Not.Contain("length"))
Assert.That(keys, Does.Not.Contain("setProperty"))
```

- [ ] Run the fixture tests and observe failed selection assertions. Implement selection rooted at `Fable.Core.TS.Dom.CSSStyleProperties`, consulting inheritance and relevant compiled-name/interop metadata rather than guessing from accessor names. Treat conflicting alias mappings as diagnostics; deduplicate exact synonyms deterministically.
- [ ] Add generated CSS field specs using AnimeJs's resolved tween value type, never the DOM property's assignment type. Keep `Transform.x` in AnimeJs policy. Test that support package version and assembly/XML hashes enter provenance.
- [ ] Run metadata and projection tests; commit `feat: derive CSS DSL fields from Xantham support metadata`.

### Task 5: Implement context-safe builder accumulation and JavaScript semantics

**Files:** Create `Partas.AnimeJs/Dsl/Runtime.fs`, a small isolated runtime consumer project, `tests/Consumers/Runtime/Options.fs`, and `options.test.mjs`.

**Interfaces:** An opaque `Field<'Context>` stores one validated key/value; `Options<'Context>` holds immutable field accumulation; `OptionsBuilder<'Context>` implements the ordinary CE operations. Internal `Field.create` and `Options.toObject` are callable by generated code only. Generated factory adapters convert options to their raw parameter type. No public inline member may reference inaccessible private implementation across a package boundary.

- [ ] Write runtime consumer fixtures for zero fields, repeated duration fields, conditional inclusion, loops, `yield!`, arrays, explicit null, and building two independent options values. Export their materialized objects to a Node test module.

```javascript
import assert from 'node:assert/strict';
import { test } from 'node:test';
import { duplicate, absent, explicitNull, first, second } from './output/Options.fs.js';
test('last field wins', () => assert.equal(duplicate.duration, 200));
test('absence differs from null', () => {
  assert.equal(Object.hasOwn(absent, 'onComplete'), false);
  assert.equal(explicitNull.onComplete, null);
});
test('options do not share mutation', () => {
  first.duration = 999;
  assert.equal(second.duration, 100);
});
```

- [ ] Compile the fixture with the pinned Fable tool and run `rtk node --test tests/Consumers/Runtime/options.test.mjs`; confirm failure against the unfinished builder.
- [ ] Implement `Yield`, `YieldFrom`, `Zero`, `Combine`, `Delay`, `For`, and `Run` with immutable accumulation and left-to-right execution. Materialize a fresh JS object once per factory conversion. Last duplicate key wins; optional omission happens only for absent field tokens, not all nullish values. Keep arrays/keyframes in source order.
- [ ] Add a compile-negative fixture mixing `Field<TimerContext>` into an animation builder. It must fail with the intended context type mismatch, not a missing assembly/import error. Add a consumer proving ordinary `if` and `for` compile without ambiguous CE overloads.
- [ ] Run .NET compile and Node tests; commit `feat: add context-safe option builder runtime`.

### Task 6: Emit the first usable animation DSL and prove real effects

**Files:** Create `Emit.fs`, emitter tests, `Dsl/Conveniences.fs`, `examples/GettingStarted.fs`, and positive/negative/plain-object runtime consumers.

**Interfaces:** `Emit.sources : DslModel -> Result<Artifact list, Diagnostic list>` emits stable F# source using validated identifiers/qualified raw type identities. Initial public surface: `animation`, `timer`, `Animation.duration/ease/autoplay/onComplete`, `Timer.duration/autoplay`, `Css.opacity`, `Transform.x`, `Tween.fromTo/values`, `Ease.named`, `Target.selector/object/element`, and `Anime.animate/createTimer`. Functions have types derived from the projected raw contracts.

- [ ] Write a positive consumer for the approved syntax using `Transform.x` and `Css.opacity`, and a plain-object consumer animating an explicit custom numeric property. Add negatives for wrong context, wrong callback handle, invalid value arm, and readonly-handle assignment. Assert the expected diagnostic for each.
- [ ] Define an explicit escape for dynamic object keys; reject reserved option keys supplied through that route. Document that a custom string key cannot prove target-property existence.
- [ ] Write the first real runtime witness, emitting its values from F# and asserting them in Node:

```javascript
test('real AnimeJs changes a plain-object property deterministically', () => {
  const witness = createPlainObjectWitness();
  assert.equal(witness.before, 0);
  assert.equal(witness.afterOptions, 0);
  assert.equal(witness.afterSeek, 100);
  assert.equal(witness.completed, true);
});
```

  Implement `createPlainObjectWitness` in the F# runtime consumer: target `{ value = 0. }`, pure options with autoplay false and linear ease, explicit factory call, seek to completion, and a callback recording completion. Verify the installed AnimeJs seek/callback semantics before finalizing the exact expected callback trigger; invoke its documented complete method if completion callbacks require it. No wall-clock sleeps.
- [ ] Run the positive/negative compile consumers and the real runtime witness to capture failures. Implement canonical union constructors plus deliberate numeric/string conveniences. Adapt `unit`-returning callbacks to the raw binding's result explicitly, preserving receiver and argument arity.
- [ ] Emit CSS fields from Task 4 metadata and factory bodies calling owned raw exports; prohibit standalone static-abstract interfaces. Add emitted-JS checks for imports, keys, callback receiver/arity, and tween arrays/objects. Run all first-slice tests with Fable 5.20.0.
- [ ] Commit `feat: emit and verify the first AnimeJs animation DSL`.

### Task 7: Validate candidates, track fidelity, and promote transactionally

**Files:** Create `Findings.fs`, `Candidate.fs`, `Pipeline.fs`, baseline/provenance files, candidate tests; extend Build stages/commands.

**Interfaces:** `Candidate = { Artifacts: Artifact list; Diagnostics: Diagnostic list; Provenance: Provenance }`; `Pipeline.generate : GenerationRequest -> Async<Result<Candidate, Diagnostic list>>`; `Candidate.compare : outputRoot:string -> Candidate -> Result<unit, Diagnostic list>`; `Candidate.promote : outputRoot:string -> Candidate -> Result<unit, Diagnostic list>`. Provenance stores approved versions, input integrity, compiler/generator identity, policy/config/support metadata hashes, artifact hashes, and reviewed findings without absolute workspace paths.

- [ ] Add tests generating twice under different scratch roots and comparing normalized byte-identical artifacts; do not normalize away meaningful signatures or keys. Add an unreviewed selected-field finding fixture and assert the source-specific rejection diagnostic.
- [ ] Add a fixture with existing output sentinel files. Force candidate compilation failure and a failure on the second promotion write; assert prior content survives. `--check` must neither change files nor delete stale output even when reporting drift. Reject artifact paths escaping the managed output root.
- [ ] Run failing candidate tests. Implement scratch emission, finding review keyed by stable source/code/reason, and candidate compilation before promotion. Back up the managed output set, promote staged files, and roll back on any failure. Clean only verified paths owned by generation; preserve all unrelated files.
- [ ] Define Partas.Build `generate` inputs including `--check`, stage-derived help, and candidate production once per invocation. `--check` returns nonzero on drift; ordinary `generate` validates then promotes. `--explain` must describe work without executing generation.
- [ ] Run generation twice, `generate --check`, failed-candidate preservation tests, and a CLI test with an unknown flag. Commit `feat: validate and promote generated artifacts reproducibly`.

### Task 8: Replace shipped source inventory and add a working Nacara site

**Files:** Modify `Partas.AnimeJs.fsproj`, both package manifests, test project inventory, README and solution; create `Generated/Generated.props`, `site/site.fsproj`, `site/Site.fs`, `site/content/{index,migration,regeneration}.md`; add docs stages.

**Interfaces:** The library imports an explicit ordered generated-source inventory: raw bindings, builder runtime, generated DSL, conveniences. The site resolves `Partas.AnimeJs.dll` and shared dependencies from the same configuration used in validation. `docs` builds; `docs --watch` serves; neither deploys.

- [ ] Add an acceptance consumer referencing the actual library project rather than scratch output, and a docs smoke check that must find getting-started, migration, and API-reference routes. Capture the current library's FS3866 failures before changing inventory.
- [ ] Replace compiled `Binding.fs`, `Core.fs`, and `Style.fs` entries with the verified generated/runtime source set. Retain the old files out of compilation until the family migration is complete. Remove obsolete Browser package dependencies only after shipped signatures and consumers use the shared type family. Update AnimeJs npm metadata and release notes to the exact new target; mark the DSL change in release documentation.
- [ ] Replace the placeholder test setup with meaningful new consumers, avoiding a global `fable` invocation or incidental module initialization executing browser animation code in .NET tests. Keep the old tests as migration evidence only if still runnable; do not leave a false passing placeholder as acceptance coverage.
- [ ] Adopt the candidate Nacara package set, resolving incompatible plugin versions to exact pins if restore or site compilation proves a conflict. Enable XML docs on the library and configure FSharpApi with shared-assembly search paths. Use the actual getting-started F# source via Literate inclusion/rendering, and assert its code is compiled in the consumer gate.

```fsharp
let site =
    Site.create "Partas.AnimeJs"
    |> Markdown.register
    |> Literate.register
    |> FSharpApi.register apiOptions
    |> Theme.register theme
    |> Site.collection (Theme.docs theme "content")

[<EntryPoint>]
let main argv = Nacara.run site argv
```

  Define `theme` with the default theme's guide/reference navigation; define `apiOptions` with the exact library DLL/XML doc pair from the selected build configuration. Add a compatible highlighting and internal-link/anchor validation plugin with exact pins.
- [ ] Run the root solution build, first-slice .NET/Fable/runtime consumers, and `rtk dotnet run --project Build.fsproj -- docs`. Verify reference pages resolve shared types and guide snippets match the tested source. Commit `feat: ship generated DSL and Nacara getting-started docs`.

### Task 9: Generate timer, timeline, keyframe, and stagger families

**Files:** Extend policy/projection/emitter through their existing interfaces; add `examples/Timer.fs`, `Timeline.fs`, `Stagger.fs`, family consumers, runtime tests, and guide pages.

**Interfaces:** Generate full reviewed `timer`/`timeline` option builders and explicit `Anime.createTimer/createTimeline`; expose keyframes and stagger through raw-derived typed values. Timeline actions remain typed instance methods, not a second implicit effectful CE.

- [ ] Add .NET/Fable consumers creating timer callbacks, timeline defaults, duration-keyframes, percentage-keyframes, and stagger function values. Add negatives swapping percentage/duration-keyframe shapes and supplying a timeline callback with a timer-only handle contract.
- [ ] Add real runtime witnesses with explicit duration/ease/autoplay false, seek controls, and two timeline targets:

```javascript
assert.equal(timelineWitness.firstAtStart, 0);
assert.equal(timelineWitness.firstAtEnd, 100);
assert.equal(timelineWitness.secondAtEnd, 200);
assert.deepEqual(keyframeWitness.materializedOrder, [0, 50, 100]);
assert.equal(staggerWitness.valueAtFirst, 0);
assert.equal(staggerWitness.valueAtSecond, 25);
```

- [ ] Run the family tests to capture failure. Add policy roots based on the real factory parameter/return identities; expand emission without copying signatures. Preserve callback generic substitution and keyframe array order. Constrain percentage-keyframe helpers to the actual upstream key representation and document explicit custom-key escapes.
- [ ] Review new selected-surface findings, run generation drift/compile/runtime/docs checks, and commit `feat: generate timer timeline keyframe and stagger DSL families`.

### Task 10: Expand remaining factories and prove browser integration

**Files:** Extend policy/emitter only where projection requires new supported shapes; create Spring/Draggable/Scroll/SvgText examples, browser F# fixture, pinned Playwright harness, and family guides.

**Interfaces:** Generate raw-derived options/factory adapters for spring, animatable, draggable, scroll observer, scope, and text splitting where present in the current AnimeJs exports. SVG utilities retain raw generated signatures plus explicitly tested conveniences. Existing projection/emitter contracts remain unchanged.

- [ ] Add positive consumers for each root using actual installed 4.5.0 signatures, and negative consumers for wrong SVG target type, wrong nested draggable options, and readonly shared-DOM assignments. Use shared Xantham SVG/DOM/collection types directly.
- [ ] Add Playwright tests against a local fixture with a real element, SVG path, draggable target, scroll container, and text span. Assert observable outcomes rather than exact generated source only:

```javascript
await expect(page.locator('#box')).toHaveCSS('opacity', '0.5');
await expect(page.locator('#split span')).toHaveCount(2);
assert.equal(await page.evaluate(() => window.witnesses.svgHasDrawable), true);
assert.equal(await page.evaluate(() => window.witnesses.dragCallbacks), 1);
```

  The fixture exposes a small test-only witness object. Drive animation seek deterministically; use browser pointer/scroll actions and event-based waits for drag/scroll. Spring tests assert finite output and correct factory interaction; do not hard-code an undocumented settling time.
- [ ] Run failing compile/browser tests. Generate field constructors for the newly selected contexts and explicit adapters using raw types. Where the typed projection cannot express a contract, preserve raw access and emit a reviewed diagnostic rather than faking a safe DSL wrapper.
- [ ] Add guides from the actual verified examples, run the complete raw/DSL/fidelity/browser/docs gates, and remove superseded handwritten source files after all retained features have generated or documented raw equivalents. Commit `feat: generate browser animation factory families`.

### Task 11: Close packed-package consumption and repository CI

**Files:** Create packed-package consumer/harness, `.github/workflows/verify.yml`; finalize Build `build/test/verify/pack/docs` commands, README and generated provenance.

**Interfaces:** `verify` composes all read/check gates; `pack` validates and creates a local `.nupkg`. An isolated consumer takes only the package and documented shared dependencies; it cannot reference the repository source or generation assembly.

- [ ] Add an integration test packing the library, restoring it into a fresh consumer with a private local package source, then compiling/running the same plain-object witness through Fable. Assert its NuGet dependency graph contains no Generator, Wire, Partas.Build, or Nacara tooling dependency. Prove shared Core/Core.TS references and ordered Fable source packaging survive distribution.
- [ ] Run the package test before completing pack metadata and capture its concrete failure. Correct source inclusion using the existing Fable.Package.SDK packaging contract; do not copy output into a package without testing the actual installed artifact.
- [ ] Compose `verify` from candidate drift, finding validation, .NET/Fable/negative/runtime/browser/package/docs stages. Use Partas.Build structured reports and exit codes. Ensure `pack` includes required validation and has no publication side effect.
- [ ] Add Windows/Linux CI installing the stable SDK selected by `global.json`, the pinned Node toolchain and browser dependencies, restoring tools, and invoking the same Build `verify` command. Use cache keys derived from package locks/configs. Retain diagnostics and generated candidate diffs on failure; do not mutate tracked files or publish from PR jobs.
- [ ] Run fresh local `rtk dotnet run --project Build.fsproj -- verify`, `pack`, and `generate --check`; inspect exit codes and artifacts. Verify the isolated consumer and documentation links. Commit `ci: verify generated DSL package and documentation`.

## Self-review and handoff

- Approved spec coverage: Tasks 2–4 cover shared bindings and structured projection; Tasks 5–6 cover fresh DSL semantics; Task 7 covers deterministic candidates/fidelity; Task 1/7/11 cover Partas.Build; Task 8/9/10 cover Nacara/tested examples; Tasks 9–10 cover retained factory families; Task 11 covers packaging and CI.
- All five Review Focus conditions have named test owners. The first-slice package/type identities and callback/union behavior are acceptance gates, not assumptions.
- Restore/bootstrap can run without compiling the old library. Production generation uses one Xantham analysis and no separate generator CLI. There is no dependency on sibling checkouts or a catalog absent from the package.
- Version pins are current observations; any compatibility-driven change must be documented with the failing/passing evidence and updated provenance, not silently substituted.
- The snippets above define behavior/contracts, not generated source to paste blindly. Package and raw-export APIs must be verified by actual compile gates before generating a public adapter.
- This plan has not been executed. Review it and select native or subagent-driven execution before implementation.
