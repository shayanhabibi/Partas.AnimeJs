(**
---
title: Spring easing
order: 6
---
Spring configuration and the returned easing function use the generated upstream types.
*)
module Partas.AnimeJs.Examples.Spring
open Partas.AnimeJs
let create () = Anime.createSpring (spring { Spring.mass 1.; Spring.stiffness 100.; Spring.damping 10. })
