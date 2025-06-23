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
