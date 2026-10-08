module Partas.AnimeJs.Families.Witness
open Fable.Core
open Fable.Core.JsInterop
open Partas.AnimeJs
let objectTarget () = createObj ["value" ==> 0.] |> unbox<Fable.Core.JS.JS.Record<string, obj>>
let firstTarget = objectTarget ()
let secondTarget = objectTarget ()
let handle = Examples.Timeline.create firstTarget secondTarget
let firstAtStart: float = firstTarget?value
handle.seek(200.) |> ignore
let firstAtEnd: float = firstTarget?value
let secondAtEnd: float = secondTarget?value
let frames = Examples.Keyframes.durationFrames
let materializedOrder: float[] = frames |> Array.map (fun frame -> frame?value)
let percentageTarget = objectTarget ()
let percentageHandle = Examples.Keyframes.animate percentageTarget (U2.Case2 Examples.Keyframes.percentageFrames)
percentageHandle.seek(50.) |> ignore
let percentageMiddle: float = percentageTarget?value
let durationTarget = objectTarget ()
let durationHandle = Examples.Keyframes.animate durationTarget (U2.Case1 frames)
durationHandle.seek(durationHandle.duration) |> ignore
let durationEnd: float = durationTarget?value
let staggerFirst = Examples.Stagger.first
let staggerSecond = Examples.Stagger.second
let mutable updates = 0
let timerHandle = Examples.Timer.create (fun _ -> updates <- updates + 1)
timerHandle.seek(50.) |> ignore
let timerUpdated = updates > 0
let gridSpecification = stagger { Stagger.grid [|2.; 1.|] }
let gridOne = Stagger.parameters gridSpecification
let gridTwo = Stagger.parameters gridSpecification
let percentagePosition value = Keyframes.at value (percentageKeyframe { PercentageKeyframe.property("value", 0.) })
let customProperty key = Animation.property key (Tween.number 1.)
let callbackArraySpecification = draggable {
    Draggable.snap (fun (_: Raw.Draggable) -> U2.Case2 [|0.; 100.|])
    Draggable.container (fun (_: Raw.Draggable) -> U5.Case2 [|0.; 100.; 100.; 0.|])
    Draggable.containerPadding (fun (_: Raw.Draggable) -> U2.Case2 [|1.; 2.; 3.; 4.|])
}
let callbackArrayOptions = Draggable.parameters callbackArraySpecification
[<Emit("(function (handle) { return new Float64Array([this.offset, handle.offset]); })")>]
let receiverCallback () : Raw.Draggable -> U2<float, float[]> = jsNative
let receiverOptions = Draggable.parameters (draggable { Draggable.snap (U3.Case3 (receiverCallback ())) })
let axisOptions = DragAxis.parameters (dragAxis { DragAxis.snap (fun (_: Raw.Draggable) -> U2.Case2 [|0.; 100.|]) })
