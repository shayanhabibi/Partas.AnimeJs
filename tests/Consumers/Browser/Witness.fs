module BrowserWitness

open Fable.Core
open Fable.Core.JsInterop
open Partas.AnimeJs

let springHandle = Partas.AnimeJs.Examples.Spring.create ()
let animatableHandle = Partas.AnimeJs.Examples.Draggable.createAnimatable ()
let mutable dragCallbacks = 0
let mutable scrollCallbacks = 0
let draggableHandle = Partas.AnimeJs.Examples.Draggable.create (fun _ -> dragCallbacks <- dragCallbacks + 1)
let scrollHandle = Partas.AnimeJs.Examples.Scroll.create (fun _ -> scrollCallbacks <- scrollCallbacks + 1)
let scopeHandle = Partas.AnimeJs.Examples.Scroll.createScope ()
let splitter = Anime.splitText (U4.Case1 "#split") (textSplitter { TextSplitter.words true; TextSplitter.lines false; TextSplitter.chars false; TextSplitter.accessible false })

[<Emit("document.getElementById('path')")>]
let path () : Fable.Core.TS.Dom.SVGPathElement = jsNative
let drawable = Partas.AnimeJs.Examples.SvgText.drawable (path ())
let motionPath = Partas.AnimeJs.Examples.SvgText.motionPath (path ())
let animationHandle = Anime.animate (Target.selector "#box") (animation {
    Animation.duration 100.
    Animation.autoplay false
    Animation.ease "linear"
    Css.opacity (Tween.fromTo 0. 1.)
})
animationHandle.seek 50. |> ignore

[<Emit("window.witnesses = $0")>]
let publish (value: obj) : unit = jsNative
publish (createObj [
    "svgHasDrawable" ==> (drawable.Length = 1 && (path ()).getTotalLength() > 0.)
    "springFinite" ==> (not (System.Double.IsNaN(springHandle.ease 0.5)) && springHandle.duration > 0.)
    "dragCount" ==> (fun () -> dragCallbacks)
    "scrollCount" ==> (fun () -> scrollCallbacks)
    "animatableReady" ==> (animatableHandle.targets.Length = 1)
    "scopeReady" ==> (not (isNull (box scopeHandle.root)))
])
