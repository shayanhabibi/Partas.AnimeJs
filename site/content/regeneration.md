---
title: Regeneration and fidelity
---

Run repository automation with `dotnet run --project Build.fsproj -- <command>`.
`restore` installs local tools and the locked npm dependencies. `generate` runs Xantham
once, derives typed fields and the maintained CSS catalog, compiles a candidate with
.NET and Fable, then promotes it. `generate --check` reports drift without writing output.
`--explain` describes the stages without running them. `docs` builds this site;
`docs --watch` starts the local documentation server.

Normal generation uses exact package versions and npm integrity values. The provenance
file records versions, declaration/config/policy/generator hashes, reviewed findings and
artifact hashes. Textual input hashes use UTF-8 with LF line endings so checkouts behave
consistently across Windows and Linux. Support assembly and documentation hashes use
their original package bytes.

New widened or escaping findings on selected fields and their alias dependencies fail
generation until reviewed by stable source and code with an explicit reason. Published
raw signatures retain their own full Xantham manifest. Callback `any` returns have a
reviewed unit adapter; easing and composition strings retain documented raw escapes.

Promotion stages files beside the managed directory, preserves unrelated files and
removes only previously owned artifacts. A failed write preserves the old directory.
If a process is interrupted between directory renames, the next promotion recovers its
stable backup. Check mode performs no recovery or cleanup.
