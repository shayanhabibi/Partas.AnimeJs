(**
---
title: Timeline defaults and actions
order: 3
---
Use typed defaults and generated instance methods for timeline actions.
*)
module Partas.AnimeJs.Examples.Timeline
open Fable.Core
open Partas.AnimeJs
let create first second =
    let shared = Defaults.parameters (defaults { Defaults.duration 100.; Defaults.ease "linear" })
    let handle = Anime.createTimeline (timeline { Timeline.autoplay false; Timeline.defaults shared })
    let child value = Animation.parameters (animation { Animation.property "value" (Tween.number value) })
    handle.add(Target.object first, child 100., U4.Case2 0.) |> ignore
    handle.add(Target.object second, child 200., U4.Case2 100.) |> ignore
    handle
