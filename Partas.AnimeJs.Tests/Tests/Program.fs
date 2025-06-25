module Partas.AnimeJs.Tests.Tests.Program

open Fable.Core.JsInterop
// open Partas.Solid
open Partas.AnimeJs.Style
open Partas.AnimeJs.Core
open Partas.AnimeJs.Core.Operators

// let ``builders work`` () =
//     // let animationOptions = animate {}
//     // let animatableBuild = animatable {}
//     let animatableObj = animatableBuild { "target" }
//     let workingAnimatable = animatableObj {
//         mkStyle "test"
//         3.,4.,5.
//         duration 500.
//         Eases.inCubic
//     }
//     ignore <| animatableObj {
//         Eases.inOutExpo
//         1.2
//         style.backgroundColor
//         duration 300
//     }
//     let workingAnimation = animationOptions {
//         "target"
//     }
//     
//     ()
let x2 = animate {
    duration 500
    alternate
    style.border { "1px solid red" }
    style.backgroundColor {
        duration 500
        too "sd"
    }
    keyframes <-- [
        
        keyframe {
            style.accentColor { "accent" }
            duration 500
        }
    ]
    
    ease Eases.inCubic
}
let x3 = x2 { Unchecked.defaultof<string> }

let z2 = animate {
    mkStyle "translateY" {
        too (Utils.random(-175, -255))
    }
    !~ "translateX" {
        tween {
            from "left"
        },
        tween {
            Eases.inOut 1.5
            too (Utils.random(-40,40))
        }
    }
    style.color { from "#FFDD8E" }
    style.scale {1,1.2,1,0.8}
    Eases.inBounce
    style.opacity { 0 }
}

let tests = animate {
                keyframes <-- [
                    !% 50 { // percentage keyframes
                        style.translateX { 0 }
                        Eases.inElastic(1, 0.5) // ease sugar
                    }
                ]
                }

let clickAnimation2 =
    timeline {
        loop 500
        onLoop (_.refresh() >> ignore)
        autoplay false
    } {
        play
        add
            null
            (animate {
                keyframes <-- [
                    !% 50 { // percentage keyframes
                        style.translateX { 0 }
                        Eases.inElastic(1, 0.5) // ease sugar
                    }
                ]
                delay(
                    stagger 50 {
                        grid 0.3 2
                        reversed
                        from staggerFrom.center
                        // staggerFrom.first // stagger sugar
                    }
                )
                Eases.inElastic()
                // autoplay
                //     (onScroll {
                //         enter "max" "min"
                //         leave
                //             (ObserverThreshold.Left + ObserverThreshold.Right)
                //     })
                onLoop(fun a -> a {
                    // chain sugar
                    pause
                    play
                    reverse
                    stretch 3
                } >> ignore)
            })
        set
            null
            (animate {
                !~ "rotate" {0}
                
            })
        init
        refresh
    }

let animationObj = animate {
    delay (stagger 50 { reversed })
    keyframes <-- [
        keyframe {            
            Eases.inOut 3
            style.maxHeight { 50 }
        }
        keyframe {
            !~ "x" { 5 }
        }
    ]
    
}




let a1 =
    (animate {
        style.rotate <-- 90
        loop
        Eases.inOutExpo
    }) { unbox<Selector> null }

let a2 =
    (animate {
        style.x { Utils.random(-100,100)}
        style.y { Utils.random(-100,100)}
        style.rotate { Utils.random(-180,180)}
        duration (Utils.random(500,1000))
        composition.blend
    }) { ".shape" }

let drawa =
    (animate {
        style.draw {
            "0 0",
            "0 1",
            "1 1"
        }
        delay (stagger(40).asFunctionValue)
        Eases.inOut 3
        autoplay (onScroll { sync })
    }) { Svg.createDrawable(Selector "path") }

let tl = timeline {} {
    add
        ".dot"
        (animate {
            style.scale { stagger(1.1,0.75) { grid 13 13; staggerFrom.center}}
            Eases.inOutQuad
        })
        (stagger(200) { grid 13 13; staggerFrom.center})

}

let dragg =
    (draggable {
        releaseEase (spring {
            stiffness 120
            damping 6
        })
    }) { yield ".circle" }

let timelinesdf =
    timeline {} {
        add null (animate {
            style.y { !-= 6 }
            duration 500
        }) (stagger(10).asFunctionValue)
        add null (animate {
            style.rotate {360}
            duration 1920
        }) !< 0
    }

let a11 =
    (animate {
        style.rotate <-- 90
        loop
        Eases.inOutExpo
    }) { ".square" }
    
