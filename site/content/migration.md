---
title: Migrating from the handwritten DSL
---

Version 0.1.0 introduces a fresh DSL. Existing computation expressions require migration.
Build options with `animation`, `timer` or `timeline` and explicit typed fields such as
`Animation.duration`, `Timer.autoplay`, `Css.opacity` and `Transform.x`. Call an `Anime`
factory to start an effect; constructing options does not animate anything.

Returned handles and callback arguments use the generated `Partas.AnimeJs.Raw` types.
Use Xantham's `Fable.Core.TS.Dom` types for DOM and SVG values. Remove old Browser package
references when your other dependencies no longer need them.

Fields are context-specific. An animation field cannot appear in a timer expression.
Duplicate keys use the final field; `yield!` reuses fields or complete options. Each
factory call materializes independent plain objects and arrays, preserving callback
and DOM object identity.

Use `Tween.number`, `Tween.text`, `Tween.fromTo` and `Tween.values` for common values.
`Animation.property` is an explicit custom target-property escape accepting the maintained
raw tween contract. It rejects reserved option names. Raw APIs remain available for
signatures whose fidelity findings require an explicit escape.

See the [tested example](../../examples/GettingStarted.fs) and [generated reference](/reference/index.md).
