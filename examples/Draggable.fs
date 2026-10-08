(**
---
title: Draggable and animatable
order: 7
---
Nested options have their own contexts and are materialized explicitly.
Dynamic property names do not prove that a target owns that property.
*)
module Partas.AnimeJs.Examples.Draggable
open Partas.AnimeJs
let create onGrab = Anime.createDraggable (Target.selector "#drag") (draggable {
    Draggable.x (DragAxis.parameters (dragAxis { DragAxis.snap 10. }))
    Draggable.y false
    Draggable.onGrab onGrab
})
let createAnimatable () = Anime.createAnimatable (Target.selector "#box") (animatable {
    Animatable.duration 100.
    Animatable.ease "linear"
    Animatable.property ("opacity", 100.)
})
