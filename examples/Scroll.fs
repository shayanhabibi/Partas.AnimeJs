(**
---
title: Scroll and scope
order: 8
---
*)
(**
Scroll callbacks preserve the generated observer handle. Scope defaults reuse the shared defaults context.
*)
module Partas.AnimeJs.Examples.Scroll
open Partas.AnimeJs
let create onUpdate = Anime.onScroll (scroll {
    Scroll.container (Target.selector "#scroll")
    Scroll.target (Target.selector "#scroll-target")
    Scroll.onUpdate onUpdate
})
let createScope () = Anime.createScope (scope {
    Scope.root "#fixture"
    Scope.defaults (Defaults.parameters (defaults { Defaults.duration 100. }))
})
