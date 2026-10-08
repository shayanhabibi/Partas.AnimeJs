(**
---
title: Timer callbacks
order: 2
---
*)
(**
Timer fields preserve the timer handle type; creating a timer is an explicit call.
*)
module Partas.AnimeJs.Examples.Timer
open Partas.AnimeJs
let create onUpdate = Anime.createTimer (timer {
    Timer.duration 100.
    Timer.autoplay false
    Timer.onUpdate onUpdate
    Timer.reversed false
    Timer.loop 1.
})
