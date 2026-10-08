module AnimationWitness

open Fable.Core
open Fable.Core.JsInterop
open Partas.AnimeJs

let createPlainObjectWitness () =
    let target = jsOptions<Raw.JSTarget> (fun target -> target["value"] <- box 0.)
    let before = target["value"]
    let mutable completed = false
    let mutable callbackHandle = Unchecked.defaultof<Raw.JSAnimation>
    let options = animation {
        Animation.duration 100.
        Animation.ease "linear"
        Animation.autoplay false
        Animation.property "value" (Tween.fromTo 0. 100.)
        Animation.onComplete (fun handle ->
            callbackHandle <- handle
            completed <- true)
    }
    let afterOptions = target["value"]
    let handle = Anime.animate (Target.object target) options
    handle.seek 100. |> ignore
    let afterSeek = target["value"]
    handle.cancel() |> ignore
    createObj [
        "before", before; "afterOptions", afterOptions; "afterSeek", afterSeek
        "completed", box completed; "correctHandle", box (obj.ReferenceEquals(handle, callbackHandle)) ]

let materialized () =
    let options = animation {
        Animation.duration 100.
        Animation.ease "linear"
        Animation.autoplay false
        Css.opacity (Tween.values [0.; 1.])
        Transform.x (Tween.fromTo 0. 100.)
        Animation.onComplete (fun _ -> ())
    }
    Options.toObject options

let rejectedReservedKey () =
    Animation.property "duration" (Tween.number 100.) |> ignore
