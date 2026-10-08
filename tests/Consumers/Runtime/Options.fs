module RuntimeConsumer

open Partas.AnimeJs
open Fable.Core.JsInterop

type AnimationContext = private AnimationContext of unit
type TimerContext = private TimerContext of unit
let animation = OptionsBuilder<AnimationContext>()
let timer = OptionsBuilder<TimerContext>()
let duration value = Field.create<AnimationContext> "duration" (box value)
let reusable = animation { duration 100. }
let duplicate = animation { duration 100.; duration 200. } |> Options.toObject
let absent = animation { if false then Field.create<AnimationContext> "onComplete" (box 1) } |> Options.toObject
let explicitNull = animation { Field.create<AnimationContext> "onComplete" null } |> Options.toObject
let first = Options.toObject reusable
let second = Options.toObject reusable
let empty = animation { yield! [] } |> Options.toObject
let fromSequence = animation { yield! [duration 50.; duration 75.] } |> Options.toObject
let fromOptions = animation { yield! reusable; duration 150. } |> Options.toObject
let loops = animation { for value in [0.; 50.; 100.] do duration value } |> Options.toObject
let ordered = animation { Field.create<AnimationContext> "keyframes" (box [| 0.; 50.; 100. |]) } |> Options.toObject
let trace = ResizeArray<float>()
let evaluationOrder =
    animation {
        for value in [0.; 50.; 100.] do
            trace.Add value
            duration value
    } |> Options.toObject
let executionTrace = trace.ToArray()
let nestedSpec = animation {
    Field.create<AnimationContext> "tween" (createObj ["values", box [|0.; 50.; 100.|]])
}
let nestedFirst = Options.toObject nestedSpec
let nestedSecond = Options.toObject nestedSpec
type ReferenceHandle() =
    member val Value = 0. with get, set
let referenceHandle = ReferenceHandle()
let referenceCallback (_: obj) = ()
let referenceCallbackValue = box referenceCallback
let retainedReferences =
    animation {
        Field.create<AnimationContext> "handle" (box referenceHandle)
        Field.create<AnimationContext> "callback" referenceCallbackValue
    } |> Options.toObject
