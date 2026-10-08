(**
---
title: Duration and percentage keyframes
order: 5
---
*)

(**
Duration and percentage keyframes have separate contexts. Lists retain duration order;
percentage positions must be finite and between zero and 100. Custom keys are explicit escapes.
*)
module Partas.AnimeJs.Examples.Keyframes
open Partas.AnimeJs
let durationFrames = Keyframes.duration [
    durationKeyframe { DurationKeyframe.duration 50.; DurationKeyframe.property("value", 0.) }
    durationKeyframe { DurationKeyframe.duration 50.; DurationKeyframe.property("value", 50.) }
    durationKeyframe { DurationKeyframe.duration 50.; DurationKeyframe.property("value", 100.) }
]
let percentageFrames = Keyframes.percentage [
    Keyframes.at 0. (percentageKeyframe { PercentageKeyframe.property("value", 0.) })
    Keyframes.at 50. (percentageKeyframe { PercentageKeyframe.property("value", 50.) })
    Keyframes.at 100. (percentageKeyframe { PercentageKeyframe.property("value", 100.) })
]
let animate target (frames: Fable.Core.U2<Raw.DurationKeyframes, Raw.PercentageKeyframes>) = Anime.animate (Target.object target) (animation {
    Animation.autoplay false
    Animation.duration 100.
    Animation.ease "linear"
    Animation.keyframes frames
})
