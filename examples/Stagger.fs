(**
---
title: Stagger values
order: 4
---
*)
(**
Numeric stagger returns the maintained five-argument delegate. Other overloads remain in Raw.
*)
module Partas.AnimeJs.Examples.Stagger
open Fable.Core
open Fable.Core.JsInterop
open Partas.AnimeJs
let spacing = Anime.stagger 25. (stagger { Stagger.from 0.; Stagger.reversed false })
let target () : Raw.Target = U3.Case2 (createObj [] |> unbox<Raw.JSTarget>)
let targets = [|target (); target ()|]
let first = spacing.Invoke(Some targets[0], Some 0., Some targets, None, None)
let second = spacing.Invoke(Some targets[1], Some 1., Some targets, None, None)
