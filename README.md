# Partas.AnimeJs

> [!WARNING]
> Work in progress.

![Animation.gif](Animation.gif)

## Intention

Fable bindings for the phenomenal [v4+ AnimeJs animation library](https://animejs.com/).

> Check out their v4 landing page


## Progress

The bindings are essentially done. There are some dependencies on Partas.Solid that will have to be cleaned up though.

I'm creating the wrapper before I clean it all up.

The binding/wrapper will function as so:

```fsharp
// Core types and helpers for both the bindings
// and the wrapper
open Partas.AnimeJs.Core

// Recommended to EITHER open and use the bindings
open Partas.AnimeJs.Bindings
// ... OR the wrapper
open Partas.AnimeJs.FSharp

// Measurements are used to prevent the requirement for strings to be used in inputs.
// and to just make things more explicit in general
let length = 300.<px>
// We have to explicitly declare the render type, as measurements are erased and are
// unsafe in Fable to rely on for overloading.
length |> renderPx |> console.log // "300px"
let rotation = 180.<deg>
let draggableAngle = somedraggable.angle // ?float<rad>
// ... Doesn't compile:
// rotation + draggableAngle
rotation + convertRadToDeg draggableAngle
// uses animejs `utils.radToDeg`

// The wrapper will try to be more FSharp orientated.
// The Binding is just intended to be use loosely by normal Fable constructs.
open Partas.AnimeJs.Binding

let selector = ".button"
let targets = Utils.``$`` selector
animate(targets, jsOptions<AnimationOptions> (fun opts ->
        opts.backgroundColor <- "#FFFFFF"
        opts.delay <- !^5<ms>
    )).play() |> ignore
```
