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

The superseded `Binding.fs`, `Core.fs`, and `Style.fs` catalogs are removed. CSS comes
from Xantham metadata; all upstream factories and instance methods are available in
`Raw.Exports`. Timer, timeline, numeric stagger, spring, animatable, draggable, scroll,
scope and text splitting have generated option contexts. Timeline actions use
`Animation.parameters`; SVG utilities use `Raw.Exports.svg` and `Target.svg`.
Other stagger overloads, WAAPI, layout, utility and text-scrambling APIs retain their
generated raw signatures. String alternatives widened upstream remain explicit string
escapes, recorded in the fidelity baseline. No local DOM/CSS catalog is maintained.

Typed fields whose callbacks return numeric arrays adapt those return values to ordinary
JavaScript arrays, including draggable snap, container and padding callbacks. Event
callbacks retain their identity. The fidelity gate follows factory signatures and
reachable named raw contracts; raw `obj` members and layout timing intersections remain
explicit unchecked boundaries. The generated animatable property indexer retains the
setter overload; the upstream getter overload is a recorded raw limitation.
