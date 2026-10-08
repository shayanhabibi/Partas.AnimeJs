module InvalidAnimationContext
open Partas.AnimeJs
let invalid = animation { Timer.duration 100. }
