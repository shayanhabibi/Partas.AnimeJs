module NegativeTimelineCallback
open Partas.AnimeJs
let invalid = timeline { Timeline.onComplete (fun (_: Raw.JSAnimation) -> ()) }
