(**
---
title: Getting started
order: 1
---
*)
(**

Create reusable options with typed fields. Starting an animation is an explicit call
to `Anime.animate`; building options has no animation effects. These examples compile
against the shipped library in the .NET and Fable consumer gates.
*)
module Partas.AnimeJs.Examples.GettingStarted

open Partas.AnimeJs

let options = animation {
    Animation.duration 800.
    Animation.ease (Ease.named "inOutQuad")
    Animation.autoplay false
    Transform.x (Tween.fromTo 0. 240.)
    Css.opacity (Tween.values [0.; 1.])
    Animation.onComplete (fun handle -> handle.pause() |> ignore)
}

let start selector = Anime.animate (Target.selector selector) options

let clockOptions = timer {
    Timer.duration 1000.
    Timer.autoplay false
}

let createClock () = Anime.createTimer clockOptions
