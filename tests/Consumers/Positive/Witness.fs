module Partas.AnimeJs.Positive.Witness

open Fable.Core.JsInterop
open Partas.AnimeJs

let target = createObj ["value" ==> 0.] |> unbox<Fable.Core.JS.JS.Record<string, obj>>
let options = animation {
    Animation.duration 100.
    Animation.ease (Ease.named "linear")
    Animation.autoplay false
    Animation.property "value" (Tween.number 100.)
}
let initial: float = target?value
let handle = Anime.animate (Target.object target) options
handle.seek(100.) |> ignore
let final: float = target?value
let correctHandle = handle.duration = 100.
