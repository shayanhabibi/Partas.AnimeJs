module Partas.AnimeJs.Tests.Tests.Program

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
