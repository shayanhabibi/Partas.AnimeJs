module Partas.AnimeJs.Tests.Tests.Program

open Partas.AnimeJs.FSharp
open Partas.AnimeJs.FSharp.UnsafeOperators

AnimeJs.createSpring [
    Spring.damping 5.
    "fd" ==< "2"
] |> ignore

AnimatedProp.translateX 5
|> ignore

TimeLabel "Chock"
|> ignore
