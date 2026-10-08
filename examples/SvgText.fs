(**
---
title: SVG and text
order: 9
---
SVG utilities retain their generated signatures. Targets use the shared Xantham SVG type directly.
Text splitting keeps AnimeJs accessibility defaults; the browser fixture disables the extra accessibility span solely to count word spans.
*)
module Partas.AnimeJs.Examples.SvgText
open Fable.Core
open Partas.AnimeJs
let drawable (path: Fable.Core.TS.Dom.SVGPathElement) = Raw.Exports.svg.createDrawable.Invoke(Target.svg path, None, None)
let motionPath (path: Fable.Core.TS.Dom.SVGPathElement) = Raw.Exports.svg.createMotionPath.Invoke(Target.svg path, None)
let split () = Anime.splitText (U4.Case1 "#split") (textSplitter {
    TextSplitter.words true
    TextSplitter.lines false
    TextSplitter.chars false
})
