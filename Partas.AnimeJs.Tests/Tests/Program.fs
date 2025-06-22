module Partas.AnimeJs.Tests.Tests.Program

open Fable.Core.JsInterop
open Partas.Solid
open Partas.AnimeJs.CE
open Partas.AnimeJs.Core
open Partas.AnimeJs.Core.Operators


mkTimeLabel "Chock"
|> ignore

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
    
    ease id
}
let x3 = x2 { Unchecked.defaultof<string> }

let z2 = animate {
    mkStyle "translateY" {
        too (AnimeJs.random(-175, -255))
    }
    !~ "translateX" {
        tween {
            from "left"
        },
        tween {
            Eases.inOut 1.5
            too (AnimeJs.random(-40,40))
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





let tl =
    (
        timeline {}
    )
        {
            add
                null
                (animate {
                    !~ "y" { !-= 6 }
                    duration 50
                })
            add
                null (
                    animate {
                        style.rotate { 360 }
                        duration 1920
                        // inOutElastic
                })
                !+= 50
            
        }


let asd = animate {
    style.rotate <-- 90
    loop
    Eases.inOutExpo
}

let transformsT = animate {
    style.x <-- AnimeJs.random(-100,100)
    style.y { AnimeJs.random(-100,100) }
    style.rotate { AnimeJs.random(-180,180) }
    duration (AnimeJs.random(500,1000))
    composition.blend
}

let scroller = animate {
    style.draw {"0 0", "0 1", "1 1"}
    delay (stagger(40) {})
    Eases.inOut 3
    autoplay (
        onScroll {
            sync
        }
        )
}

let tsts = timeline {} {
    add
        ".dot"
        (animate {
            style.scale {
                stagger(10) {
                grid 13 13; staggerFrom.center
            }}
            Eases.inOutQuad
        })
        (stagger(200) { grid 13 13; staggerFrom.center })
}

let dragga = draggable {
    releaseEase (unbox<float -> float> <| spring { stiffness 120; damping 6 })
    
}

let tasdt = timeline {} {
    add
        null
        (animate {
            style.y { !-= 6 }
            duration 50
        })
        (stagger(10) {})
    add
        null
        (animate {
            style.rotate <-- 360
            duration 1920
        })
        !< 0
}

let animationExample1 = animate {
    style.y {
        tween {
            Eases.outExpo
            duration 600
            too "-2.75rem"
        },
        tween {
            Eases.outBounce
            delay 100
            duration 800 //todo: reorder to see error
            too 0.
        }
    }
    style.rotate {
        delay 0
        duration 100 //reorder to see error TODO
        from "-1turn"
    }
    delay (FunctionValue<float>(fun _ i _ -> 50. * !!i ))
    Eases.inOutCirc
    loopDelay 1000
    loop
}





