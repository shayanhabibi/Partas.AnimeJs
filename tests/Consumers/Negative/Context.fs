module InvalidContext

open Partas.AnimeJs
type AnimationContext = private AnimationContext of unit
type TimerContext = private TimerContext of unit
let animation = OptionsBuilder<AnimationContext>()
let wrongField = Field.create<TimerContext> "duration" (box 1.)
let invalid = animation { wrongField }
