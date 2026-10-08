module NegativeKeyframeShape
open Partas.AnimeJs
let invalid = Keyframes.duration [percentageKeyframe { PercentageKeyframe.property("value", 10.) }]
