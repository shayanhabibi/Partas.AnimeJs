# Partas.AnimeJs

> [!WARNING]
> Work in progress.

![Animation.gif](Animation.gif)

## Intention

Fable bindings for the phenomenal [v4+ AnimeJs animation library](https://animejs.com/).

> Check out their v4 landing page

## Computation Expressions

After different iterations including Records with DUs, and Feliz style lists, I settled on the only reasonable and enjoyable
experience using this library in F# to be with computation expressions.

This introduces a whole host of complications and overhead, but using normal Fable interop constructs like `jsOptions` are still
viable. Or just create an object from a key,value list using `createObj` and feed it to the `AnimeJs.` exported functions/methods.

The advantage of the computation expressions is being able to use monadic transformations to make one type capable
of producing a variety of types that are mutually exclusive from each other based on non joining properties, and then have
these values situationally accepted in different contexts. It also provides lots of opportunity for sugar to make the whole 
process more enjoyable and smooth.

## AnimeJs

This library is an absolutely blunderbuss of power. Within, you have access to COMPLETE framework agnostic animation of
SVG, HTML, and *any* object. Draggables, Springs, Eases, Scroll Observers, SVG path drawing, Timelines, and a host of utilities for
animating.

This library is definitely beautiful.

## The AnimeJs Docs

The library __mostly__ stays true to the original documentation, so you can just use that for the actual function of the
library. The computational syntax while be described below in short, as the syntax needs continual refinement to roll
out all the incorrect combinations.

> This relatively small binding balloons to 5000loc because of the computations.

## Operators

> [!NOTE]
> The Operators module contains a litany of useful operators for usage in this library. It is entirely OPT-IN.
> 
> Access via Partas.AnimeJs.Core.Operators
> 
> When referring to operators, they may be only accessible via this module.

```f#
open Partas.AnimeJs.Core.Operators

// Creating Relative Tween Values from the docs
!+= 0 = "+=0"
!-= 0 = "-=0"
!*= 0 = "*=0"

// Creating Relative Time Positions from the docs
!< 0 = "<"
!<< 0 = "<<"

!<<+= 0 = "<<+=0"
!<<-= 0 = "<<-=0"
!<<*= 0 = "<<*=0"

// Creating Time Labels as a concrete type
timeLabel "something" = unbox<TimeLabel> "something"

// Creating Percentage Keyframes (required for computations relating
// to percentage keyframes)
!% 50 = "50%"

// Creating style computation properties
!~ "x" |> _.Value = "x"

// Creating a key value pair that will unbox to whatever type is required for the list
!~ "NoneStringKey" ==< 5
```

Other operators, such as those that can be used to assign values to `style` properties in computations, are defined
on the types themselves.

## Computation Expression Overview

In AnimeJs, methods to create animations/draggables etc will take a target, and an object of options.

In this binding, the computation expressions will create the options objects.

The option objects then have computation expressions to accept targets.

The final types then also have computation expressions for easy usage.

```f#
// animate
let animationOptions = animate { ... } // Creates a AnimationOptions object
let anim = animationOptions { ".target" } // Creates an Animation
ignore <| anim { play } // Exposes API with CEs, but can also be used as a standard interface

// createTimer
let _ = timer { ... } // Creates a Timer which exposes API via CE or interface

// createTimeline
let tl = timeline { ... } // Creates a Timeline which exposes API via CE or interface

// createAnimatable
let animat = animatable { ... } // Creates an AnimatableOptions Object
let _ = animat { ".target" } // Creates a Animatable instance which exposes API via CE or interface

// createDraggable
let dragger = draggable { ... } // Creates a DraggableOptions object
let _ = dragger { ".target" } // Creates a Draggable instance which exposes API via CE or interface

// onScroll
let _ = onScroll { ... } // Creates an instance of the ScrollObserver

// stagger
let _ = stagger(_) // Creates a stagger type
let _ = stagger(_,_) // Creates a stagger type
// The stagger type can instantiate a FunctionValue using a ComputationExpression with
// further options/no options, or by the `.asFunctionValue` property
let _ = stagger(0) {} // FunctionValue<float>
let _ = stagger(0).asFunctionValue // FunctionValue<float>

// createSpring
let _ = spring { ... } // Creates a spring EasingFun

// engine
let _ = engine // Exposes the engine. Can interact with CE or via interface

// Create a tween value parameter object
let _ = tween { ... }
// Create a keyframe for `keyframes`
let _ = keyframe { ... }
// Create axis options object for draggables axis
let _ = axisOptions {...}
// Or directly assign x/y via their named axisOption types
let _ = axisY { ... }
let _ = axisX { ... }
// Package targets of different types (HTMLElement, string selectors etc)
let _ = targets { ... }
// Create bounds for scrollobserver/draggable
let _ = bounds { ... }
// Create cursor options for Draggables
let _ = cursor { ... }
```

The SVG utility methods don't have computation expressions of note, and while the Scope object has been bound, I
haven't yet had a chance to test usage and develop a CE API for it.

## Differences in API

Where the object would have an optional `composition` parameter, you can directly pass a `compsition` union
value instead of using the `composition` CustomOperation.

```F#
{ composition composition.blend }
// Same as
{ composition.blend }
```

Where the object would have an optional `ease` parameter, you can directly pass a `EasingFun` delegate
, which are easily accessible via the `Eases` type, instead of using the `ease` CustomOperation.

```f#
{ ease Eases.inOutQuad }
// Same as
{ Eases.inOutQuad }
```

> [!NOTE]
> Eases which accept parameters are defined as methods, and will need to be called, even if you wish to use the defaults: `Eases.inOut()`.

Where a parameter is a logical boolean, you can either use the custom operation of its name with a boolean value,
or just use the custom operation with no boolean value, and it will act as a `true` switch. This is the case
even if the parameter can also accept other values.

```f#
{ autoplay true }
// Same as
{ autoplay }

// Can still accept ScrollObservers
{ autoplay (onScroll {...}) }
```

Where an object instance provides chain methods (where the method accepts `unit` and returns itself, such as `play()`), the CE will have a CustomOperation of the name which takes no parameters.

```f#
let animationInstance = ...
animationInstance.play().reverse()
// Same as
animationInstance { play; reverse }
```

Style properties can either have values directly assigned using literals via the `<--` operator, or via CEs when
passing tween options, or building sequences of keyframes

```f#
{
    style.x <-- 5
    style.x <-- [ 5; 7 ]
    style.x <-- [
            tween { ... }
        ]
    style.x { 5 }
    style.x { 5, 7 }
    style.x {
        tween { ... },
        5,
        tween { ... }
        }
    style.x { stagger(10) { ... } }
}
```

The keyframes can similarly be passed a list of keyframes or percent keyframes, or be done via computation expressions.

Unlike the style properties, the computation expression does not accept keyframes in tupled form.

```f#
{
    keyframes <-- [
            keyframe { ... }
            keyframe { ... }
        ]
    //
    keyframes <-- [
            !% 50 { ... }
            !% 100 { ... }
        ]
    //
    keyframes {
            keyframe { ... }
            keyframe { ... }
        }
    //
    keyframes {
            !% 50 { ... }
            !% 100 { ... }
        }
}
```

## Utils

They are available scattered via AnimeJs or Utils types.

# Examples

These are direct translations of the animations on the website on their landing page for the ones that I've tested to be working.

```f#
(animate {
        style.rotate <-- 90
        loop
        Eases.inOutExpo
    }) { ".square" }

(animate {
    style.x { AnimeJs.random(-100,100)}
    style.y { AnimeJs.random(-100,100)}
    style.rotate { AnimeJs.random(-180,180)}
    duration (AnimeJs.random(500,1000))
    composition.blend
}) { ".shape" }

(animate {
    style.draw {
        "0 0",
        "0 1",
        "1 1"
    }
    delay (stagger(40).asFunctionValue)
    Eases.inOut 3
    autoplay (onScroll { sync })
}) { AnimeJs.createDrawable(Selector "path") }

timeline {} {
    add
        ".dot"
        (animate {
            style.scale {
                stagger(1.1,0.75) {
                    grid 13 13
                    staggerFrom.center
                    }
                }
            Eases.inOutQuad
        })
        (stagger(200) { grid 13 13; staggerFrom.center})
}

(draggable {
    releaseEase (spring {
        stiffness 120
        damping 6
    })
}) { yield ".circle" }

timeline {} {
    add ".tick" (animate {
        style.y { !-= 6 }
        duration 500
    }) (stagger(10) {})
    add ".ticker" (animate {
        style.rotate {360}
        duration 1920
    }) !< 0
}
```

### Breathing Sphere

An example from Partas.Solid

![Animation4.gif](Animation4.gif)

```fsharp
[<Erase>]
type MyButton() =
    interface VoidNode
    [<SolidTypeComponent>]
    member private props.__ =
        onMount(fun () ->
            let paths = Selector ".sphere path" |> _.find
            
            let anim =
                animate {
                    !~ "strokeWidth" { 1.,8.,1. }
                    style.draw {
                        too
                            "0.5 0.5"
                            "0.5 1.6"
                        duration 200.
                    }
                    style.stroke {
                        too
                            "#E879f9"
                            "rgba(80,80,80,.35)"
                        Eases.outBounce
                        duration 2000.
                    }
                    delay (stagger(100).asFunctionValue)
                    duration 2000.
                    loop
                    autoplay
                    composition.blend
                    alternate
                }
            for i in 0..paths.length-1 do
                let delay = stagger(45) {
                    Eases.inOutQuad
                }
                let delayValue = delay.Invoke(paths[i], i, paths.length)
                ignore <|
                (animate {
                    style.x {
                        too 2 -4
                    }
                    style.y {
                        too 2 -4
                    }
                    alternate
                    loop
                    autoplay
                    composition.blend
                    Eases.inOutQuad
                    delay delayValue
                }) { yield unbox<Selector> paths[i] }
            let _ = anim { yield unbox<Selector> (AnimeJs.createDrawable(Selector".sphere path")) }
            ()
            
        )
        let paths = [|
                            "M361.604 361.238c-24.407 24.408-51.119 37.27-59.662 28.727-8.542-8.543 4.319-35.255 28.726-59.663 24.408-24.407 51.12-37.269 59.663-28.726 8.542 8.543-4.319 35.255-28.727 59.662z"
                            "M360.72 360.354c-35.879 35.88-75.254 54.677-87.946 41.985-12.692-12.692 6.105-52.067 41.985-87.947 35.879-35.879 75.254-54.676 87.946-41.984 12.692 12.692-6.105 52.067-41.984 87.946z"
                            "M357.185 356.819c-44.91 44.91-94.376 68.258-110.485 52.149-16.11-16.11 7.238-65.575 52.149-110.485 44.91-44.91 94.376-68.259 110.485-52.15 16.11 16.11-7.239 65.576-52.149 110.486z"
                            "M350.998 350.632c-53.21 53.209-111.579 81.107-130.373 62.313-18.794-18.793 9.105-77.163 62.314-130.372 53.209-53.21 111.579-81.108 130.373-62.314 18.794 18.794-9.105 77.164-62.314 130.373z"
                            "M343.043 342.677c-59.8 59.799-125.292 91.26-146.283 70.268-20.99-20.99 10.47-86.483 70.269-146.282 59.799-59.8 125.292-91.26 146.283-70.269 20.99 20.99-10.47 86.484-70.27 146.283z"
                            "M334.646 334.28c-65.169 65.169-136.697 99.3-159.762 76.235-23.065-23.066 11.066-94.593 76.235-159.762s136.697-99.3 159.762-76.235c23.065 23.065-11.066 94.593-76.235 159.762z"
                            "M324.923 324.557c-69.806 69.806-146.38 106.411-171.031 81.76-24.652-24.652 11.953-101.226 81.759-171.032 69.806-69.806 146.38-106.411 171.031-81.76 24.652 24.653-11.953 101.226-81.759 171.032z"
                            "M312.99 312.625c-73.222 73.223-153.555 111.609-179.428 85.736-25.872-25.872 12.514-106.205 85.737-179.428s153.556-111.609 179.429-85.737c25.872 25.873-12.514 106.205-85.737 179.429z"
                            "M300.175 299.808c-75.909 75.909-159.11 115.778-185.837 89.052-26.726-26.727 13.143-109.929 89.051-185.837 75.908-75.908 159.11-115.778 185.837-89.051 26.726 26.726-13.143 109.928-89.051 185.836z"
                            "M284.707 284.34c-77.617 77.617-162.303 118.773-189.152 91.924-26.848-26.848 14.308-111.534 91.924-189.15C265.096 109.496 349.782 68.34 376.63 95.188c26.849 26.849-14.307 111.535-91.923 189.151z"
                            "M269.239 267.989c-78.105 78.104-163.187 119.656-190.035 92.807-26.849-26.848 14.703-111.93 92.807-190.035 78.105-78.104 163.187-119.656 190.035-92.807 26.849 26.848-14.703 111.93-92.807 190.035z"
                            "M252.887 252.52C175.27 330.138 90.584 371.294 63.736 344.446 36.887 317.596 78.043 232.91 155.66 155.293 233.276 77.677 317.962 36.521 344.81 63.37c26.85 26.848-14.307 111.534-91.923 189.15z"
                            "M236.977 236.61C161.069 312.52 77.867 352.389 51.14 325.663c-26.726-26.727 13.143-109.928 89.052-185.837 75.908-75.908 159.11-115.777 185.836-89.05 26.727 26.726-13.143 109.928-89.051 185.836z"
                            "M221.067 220.7C147.844 293.925 67.51 332.31 41.639 306.439c-25.873-25.873 12.513-106.206 85.736-179.429C200.6 53.786 280.931 15.4 306.804 41.272c25.872 25.873-12.514 106.206-85.737 179.429z"
                            "M205.157 204.79c-69.806 69.807-146.38 106.412-171.031 81.76-24.652-24.652 11.953-101.225 81.759-171.031 69.806-69.807 146.38-106.411 171.031-81.76 24.652 24.652-11.953 101.226-81.759 171.032z"
                            "M189.247 188.881c-65.169 65.169-136.696 99.3-159.762 76.235-23.065-23.065 11.066-94.593 76.235-159.762s136.697-99.3 159.762-76.235c23.065 23.065-11.066 94.593-76.235 159.762z"
                            "M173.337 172.971c-59.799 59.8-125.292 91.26-146.282 70.269-20.991-20.99 10.47-86.484 70.268-146.283 59.8-59.799 125.292-91.26 146.283-70.269 20.99 20.991-10.47 86.484-70.269 146.283z"
                            "M157.427 157.061c-53.209 53.21-111.578 81.108-130.372 62.314-18.794-18.794 9.104-77.164 62.313-130.373 53.21-53.209 111.58-81.108 130.373-62.314 18.794 18.794-9.105 77.164-62.314 130.373z"
                            "M141.517 141.151c-44.91 44.91-94.376 68.259-110.485 52.15-16.11-16.11 7.239-65.576 52.15-110.486 44.91-44.91 94.375-68.258 110.485-52.15 16.109 16.11-7.24 65.576-52.15 110.486z"
                            "M125.608 125.241c-35.88 35.88-75.255 54.677-87.947 41.985-12.692-12.692 6.105-52.067 41.985-87.947C115.525 43.4 154.9 24.603 167.592 37.295c12.692 12.692-6.105 52.067-41.984 87.946z"
                            "M109.698 109.332c-24.408 24.407-51.12 37.268-59.663 28.726-8.542-8.543 4.319-35.255 28.727-59.662 24.407-24.408 51.12-37.27 59.662-28.727 8.543 8.543-4.319 35.255-28.726 59.663z"
                        |]
        div(class'="flex h-[800px] w-full items-center justify-center") {
            div(class'="relative w-1/2 pb-[50%]") {
                div(
                    class'="absolute left-1/2 top-1/2 -ml-[290px] -mt-[290px] h-[580px] w-[580px]"
                ) {
                    Svg.svg(
                        class'="sphere",
                        viewBox="0 0 440 440",
                        stroke="rgba(80,80,80,.35)"
                    ) {
                        Svg.defs() {
                            Svg.linearGradient(
                                id="sphereGradient",
                                x1="5%",
                                x2="5%",
                                y1="0%",
                                y2="15%"
                            ) {
                                Svg.stop(stopColor="#373734", offset="0%")
                                Svg.stop(stopColor="#242423", offset="50%")
                                Svg.stop(stopColor="#0D0D0C", offset="100%")
                            }
                        }
                        For(each = paths) { yield fun d idx ->
                            Svg.path(
                                d = d
                                )
                        }
                    }
                }
            }
        }
```
