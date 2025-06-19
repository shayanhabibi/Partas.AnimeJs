module Partas.AnimeJs.Tests.Tests.Program

open Fable.Core.JsInterop
open Partas.AnimeJs.FSharp
open Partas.AnimeJs.FSharp.UnsafeOperators
open Partas.Solid

AnimeJs.createSpring [
    Spring.damping 5.
    "fd" ==< "2"
] |> ignore


TimeLabel "Chock"
|> ignore


let x = AnimeJs.animate (unbox null) [
    Animation.duration 500
    Animation.alternate true
    Animation.Style.border "1px solid red"
    Animation.Style.backgroundColor [
        Keyframe.duration 500
        Keyframe.to' "sd"
    ]
    Animation.keyframes [
        KeyframeValue.create [
            Keyframe.Style.accentColor "accent"
            Keyframe.duration 500
        ]
    ]
    Animation.ease Ease.InOutSine
]

let z = AnimeJs.animate (unbox null) [
    Animation.Style.translateY [
        Keyframe.to' ( AnimeJs.random(-175, -225) )
        Keyframe.ease Ease.Out
    ]
    Animation.Style.translateX [|
        Keyframe [
            Keyframe.from "left"
            Keyframe.to' (AnimeJs.random(-40, 40) |> unbox |> (+) "left")
            Keyframe.ease Ease.Out
        ]
        Keyframe [
            Keyframe.to' (AnimeJs.random(-40, 40) |> unbox |> (+) "left")
            Keyframe.ease (Ease.inOut 2)
        ]
    |]
    Animation.Style.color [
        Keyframe.from "#FFDD8E"
    ]
    Animation.Style.scale [|
        KeyframeValue 1
        KeyframeValue 1.2
        KeyframeValue 1
        KeyframeValue 0.8
    |]
    Animation.ease (Ease.inOut(2))
    Animation.Style.opacity "0"
]
open Partas.AnimeJs.Core
let clickAnimation =
    AnimeJs.createTimeline [
        Timeline.loop 500
        Timeline.onLoop (_.refresh() >> ignore)
        Timeline.autoplay false
    ]
    |> _.add(
            Selector ".star-button",
            [
                
                yield Animation.Style.scale [|
                    KeyframeValue 1
                    KeyframeValue 0.97
                    KeyframeValue 1
                |]
                yield Animation.Style.rotate [| KeyframeValue 0 |]
            ]
        )
