module rec Partas.AnimeJs.Binding

open System.Runtime.CompilerServices
open Browser.Types
open Browser
open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop
open Partas.Solid.Experimental.U
open Partas.Solid
open Partas.AnimeJs.Core

[<Fable.Core.Erase; AutoOpen; AbstractClass>]
type Export = class end
type Spring = interface end

[<Fable.Core.Erase; AutoOpen>]
module Enums =
    [<RequireQualifiedAccess>]
    [<StringEnum(CaseRules.None)>]
    type EaseStringParamNames =
        | linear
        | ``linear(x1, x2 25%, x3)``
        | ``in``
        | out
        | inOut
        | inQuad
        | outQuad
        | inOutQuad
        | inCubic
        | outCubic
        | inOutCubic
        | inQuart
        | outQuart
        | inOutQuart
        | inQuint
        | outQuint
        | inOutQuint
        | inSine
        | outSine
        | inOutSine
        | inCirc
        | outCirc
        | inOutCirc
        | inExpo
        | outExpo
        | inOutExpo
        | inBounce
        | outBounce
        | inOutBounce
        | inBack
        | outBack
        | inOutBack
        | inElastic
        | outElastic
        | inOutElastic
        | irregular
        | cubicBezier
        | steps
        | [<CompiledName("in(p = 1.675)")>] ``in(p = 1_675)``
        | [<CompiledName("out(p = 1.675)")>] ``out(p = 1_675)``
        | [<CompiledName("inOut(p = 1.675)")>] ``inOut(p = 1_675)``
        | [<CompiledName("inBack(overshoot = 1.70158)")>] ``inBack(overshoot = 1_70158)``
        | [<CompiledName("outBack(overshoot = 1.70158)")>] ``outBack(overshoot = 1_70158)``
        | [<CompiledName("inOutBack(overshoot = 1.70158)")>] ``inOutBack(overshoot = 1_70158)``
        | [<CompiledName("inElastic(amplitude = 1, period = .3)")>] ``inElastic(amplitude = 1, period = _3)``
        | [<CompiledName("outElastic(amplitude = 1, period = .3)")>] ``outElastic(amplitude = 1, period = _3)``
        | [<CompiledName("inOutElastic(amplitude = 1, period = .3)")>] ``inOutElastic(amplitude = 1, period = _3)``
        | ``irregular(length = 10, randomness = 1)``
        | ``cubicBezier(x1, y1, x2, y2)``
        | ``steps(steps = 10)``


[<AutoOpen; Fable.Core.Erase>]
module Types =
    type EasingFunction = float -> float
    
    [<AllowNullLiteral>]
    [<Interface>]
    type PowerEasing =
        [<Emit("$0($1...)")>]
        abstract member Invoke: ?power: U2<float, string> -> EasingFunction

    [<AllowNullLiteral>]
    [<Interface>]
    type BackEasing =
        [<Emit("$0($1...)")>]
        abstract member Invoke: ?overshoot: U2<float, string> -> EasingFunction

    [<AllowNullLiteral>]
    [<Interface>]
    type ElasticEasing =
        [<Emit("$0($1...)")>]
        abstract member Invoke: ?amplitude: U2<float, string> * ?period: U2<float, string> -> EasingFunction

    /// INTERNAL USE ONLY
    /// Used to disambiguate the primary constructor of TweenValue
    type internal TweenValueNoop =
        /// INTERNAL USE ONLY
        [<Emit("")>]
        static member inline noop: TweenValueNoop = unbox () 
    
    type Targets = U2<Target, Target array>
    type TweenParamModifier = delegate of value: float -> float
    
    type AnimatableSetter = delegate of value: U4<int, int[], float,  float[]> * ?duration: int<ms> * ?easing: Eases -> Animatable


[<Import("eases", "animejs")>]
type Eases =
    abstract irregular: ?length: float * ?randomness: float -> EasingFunction
    abstract steps: ?steps: float * ?fromStart: bool -> EasingFunction
    abstract cubicBezier: ?mX1: float * ?mY1: float * ?mX2: float * ?mY2: float -> EasingFunction
    abstract ``in``: PowerEasing
    abstract out: PowerEasing
    abstract inOut: PowerEasing
    abstract outIn: PowerEasing
    abstract inQuad: EasingFunction
    abstract outQuad: EasingFunction
    abstract inOutQuad: EasingFunction
    abstract outInQuad: EasingFunction
    abstract inCubic: EasingFunction
    abstract outCubic: EasingFunction
    abstract inOutCubic: EasingFunction
    abstract outInCubic: EasingFunction
    abstract inQuart: EasingFunction
    abstract outQuart: EasingFunction
    abstract inOutQuart: EasingFunction
    abstract outInQuart: EasingFunction
    abstract inQuint: EasingFunction
    abstract outQuint: EasingFunction
    abstract inOutQuint: EasingFunction
    abstract outInQuint: EasingFunction
    abstract inSine: EasingFunction
    abstract outSine: EasingFunction
    abstract inOutSine: EasingFunction
    abstract outInSine: EasingFunction
    abstract inCirc: EasingFunction
    abstract outCirc: EasingFunction
    abstract inOutCirc: EasingFunction
    abstract outInCirc: EasingFunction
    abstract inExpo: EasingFunction
    abstract outExpo: EasingFunction
    abstract inOutExpo: EasingFunction
    abstract outInExpo: EasingFunction
    abstract inBounce: EasingFunction
    abstract outBounce: EasingFunction
    abstract inOutBounce: EasingFunction
    abstract outInBounce: EasingFunction
    abstract inBack: BackEasing
    abstract outBack: BackEasing
    abstract inOutBack: BackEasing
    abstract outInBack: BackEasing
    abstract inElastic: ElasticEasing
    abstract outElastic: ElasticEasing
    abstract inOutElastic: ElasticEasing
    abstract outInElastic: ElasticEasing

[<Fable.Core.Erase>]
type ChainedUtils =
    abstract clamp: min: float * max: float -> ChainedUtils
    abstract round: decimalLength: float -> ChainedUtils
    abstract snap: increment: float -> ChainedUtils
    abstract wrap:  min: float * max: float -> ChainedUtils
    abstract interpolate: start: float * ``end``: float -> ChainedUtils
    abstract mapRange: inLow: float * inHigh: float * outLow: float * outHigh: float -> ChainedUtils
    abstract roundPad: decimalLength: float -> ChainedUtils
    abstract padStart:  totalLength: float * padString: string -> ChainedUtils
    abstract padEnd: totalLength: float * padString: string -> ChainedUtils
    abstract degToRad: unit -> ChainedUtils
    abstract radToDeg: unit -> ChainedUtils
    [<Emit("$0(...$1)")>]
    abstract member Invoke: float -> float

[<Interface>]
[<Import("utils", "animejs")>]
type Utils =
    static member get (targetSelector: Node, propName: string) : string = nativeOnly
    static member get (targetSelector: obj, propName: string) : U2<float, string> = nativeOnly
    static member get (targetSelector: HtmlElement, propName: string, unit: string) : string = nativeOnly
    static member get (targetSelector: Targets, propName: string, unit: bool) : float = nativeOnly
    static member sync (?callback: Timer -> unit) : Timer = nativeOnly
    static member set (targets: Targets, parameters: AnimationOptions) : Animation = nativeOnly
    static member remove (targets: Targets, ?renderable: U2<obj, obj>, ?propertyName: string) : Targets = nativeOnly
    static member lerp (start: float, ``end``: float, amount: float, ?renderable: U2<obj, bool>) : float = nativeOnly
    static member ``$`` (targets: Targets) : Target[] = nativeOnly
    [<Import("utils.$", "animejs")>]
    static member register (targets: Targets) : Target[] = nativeOnly
    static member clamp(v: float, min: float, max: float): float = jsNative
    static member round(v: float, decimalLength: float): float = jsNative
    static member snap(v: float, increment: U2<float, float array>): float = jsNative
    static member wrap(v: float, min: float, max: float): float = jsNative
    static member interpolate(start: float, end': float, progress: float): float = jsNative
    static member mapRange(value: float, inLow: float, inHigh: float, outLow: float, outHigh: float): float = jsNative
    static member roundPad(v: float, decimalLength: float): string = jsNative
    static member roundPad(v: string, decimalLength: float): string = jsNative
    static member padStart(v: float, totalLength: float, padString: string): string = jsNative
    static member padEnd(v: float, totalLength: float, padString: string): string = jsNative
    static member degToRad(degrees: float): float = jsNative
    static member radToDeg(radians: float): float = jsNative
    static member clamp(min: float, max: float): ChainedUtils = jsNative
    static member round(decimalLength: float): ChainedUtils = jsNative
    static member snap(increment: U2<float, float array>): ChainedUtils = jsNative
    static member wrap(min: float, max: float): ChainedUtils = jsNative
    static member interpolate(start: float, ``end``: float): ChainedUtils = jsNative
    static member mapRange(inLow: float, inHigh: float, outLow: float, outHigh: float): ChainedUtils = jsNative
    static member roundPad(decimalLength: float): ChainedUtils = jsNative
    static member padStart(totalLength: float, padString: string): ChainedUtils = jsNative
    static member padEnd(totalLength: float, padString: string): ChainedUtils = jsNative
    static member degToRad(): ChainedUtils = jsNative
    static member radToDeg(): ChainedUtils = jsNative

    

and ScrollObserverOptions =
    abstract container: U4<string, Node, Element, HtmlElement> with set
    abstract target: U4<string, Node, Element, HtmlElement> with set
    abstract debug: bool with set
    abstract axis: Axis with set
    abstract repeat: bool with set
    abstract enter: ScrollObserverThreshold with set
    abstract leave: ScrollObserverThreshold with set
    abstract sync: U4<string, float, int, Eases> with set
    abstract onEnter: Callback<ScrollObserver> with set
    abstract onEnterForward: Callback<ScrollObserver> with set
    abstract onEnterBackward: Callback<ScrollObserver> with set
    abstract onLeave: Callback<ScrollObserver> with set
    abstract onLeaveForward: Callback<ScrollObserver> with set
    abstract onLeaveBackward: Callback<ScrollObserver> with set
    abstract onUpdate: Callback<ScrollObserver> with set
    abstract onSyncComplete: Callback<ScrollObserver> with set
and ScrollObserver =
    abstract member link: U3<Animation, Timer, Timeline> -> ScrollObserver
    abstract member refresh: unit -> ScrollObserver
    abstract member revert: unit -> ScrollObserver
    abstract member id: float with get,set
    abstract member container: ScrollContainer with get
    abstract member target: HTMLElement with get
    abstract member linked: U3<Animation, Timer, Timeline> with get
    abstract member repeat: bool with get
    abstract member horizontal: bool with get
    abstract member enter: U2<string, float> with get
    abstract member leave:  U2<string, float> with get
    abstract member sync: bool with get
    abstract member velocity: float with get
    abstract member backward: bool with get
    abstract member scroll: float with get
    abstract member progress: float with get
    abstract member completed: bool with get
    abstract member began: bool with get
    abstract member isInView: bool with get
    abstract member offset: float<px> with get
    abstract member offsetStart: float<px> with get
    abstract member offsetEnd: float<px> with get
    abstract member distance: float<px> with get
    
    

and TimerOptions =
    abstract delay: int<ms> with set
    abstract duration: int<ms> with set
    abstract loop: int with set
    abstract loopDelay: int<ms> with set
    abstract alternate: bool with set
    abstract reversed: bool with set
    abstract autoplay: U2<bool, ScrollObserver> with set
    abstract frameRate: int<frames/s> with set
    // Multiple of speed
    abstract playbackRate: float with set
    abstract onBegin: Callback<Timer> with set
    abstract onBeforeUpdate: Callback<Timer> with set
    abstract onUpdate: Callback<Timer> with set
    abstract onLoop: Callback<Timer> with set
    abstract onPause: Callback<Timer> with set
    abstract onComplete: Callback<Timer> with set
    abstract member ``then``: (Callback<Timer> -> JS.Promise<unit>) with set
and Timer =
    abstract member play: ChainMethod<Timer>
    abstract member reverse: ChainMethod<Timer>
    abstract member pause: ChainMethod<Timer>
    abstract member restart: ChainMethod<Timer>
    abstract member alternate: ChainMethod<Timer>
    abstract member resume: ChainMethod<Timer>
    abstract member complete: ChainMethod<Timer>
    /// Pauses the timer, removes it from the engine's main loop and
    /// frees up the memory
    abstract member cancel: ChainMethod<Timer>
    /// <summary>
    /// Cancels the timer, sets its <c>currentTime</c> to <c>0</c> and reverts
    /// the linked <c>onScroll()</c> instance if necessary.<br/><br/> Use <c>.revert()</c>
    /// when you want to completely stop and destroy a timer
    /// </summary>
    abstract member revert: ChainMethod<Timer>
    abstract member seek: time: int<ms> * ?muteCallbacks: bool -> Timer
    /// <summary>
    /// Changes the total duration of a timer to fit a specific time. The total
    /// duration is equal to the duration of an iteration multiplied with the total
    /// number of iterations. So if a timer has a duration of 1000ms and loops twice
    /// (3 iterations in total) then the total duration is 3000ms.
    /// </summary>
    /// <param name="duration">Duration in ms</param>
    abstract member stretch: duration: int<ms> -> Timer
    
    abstract member id: U2<string, int> with get,set
    abstract member deltaTime: int<ms> with get
    abstract member currentTime: int<ms> with get,set
    abstract member iterationCurrentTime: int<ms> with get,set
    /// <summary>
    /// Value between <c>0.</c> and <c>1.</c>
    /// </summary>
    abstract member progress: float with get,set
    abstract member iterationProgress: float with get,set
    abstract member currentIteration: int with get,set
    abstract member speed: float with get,set
    abstract member fps: int<fps> with get,set
    abstract member paused: bool with get,set
    abstract member began: bool with get,set
    abstract member completed: bool with get,set
    abstract member reversed: bool with get,set

and
    [<AllowNullLiteral>]
    [<Interface>] ScrollContainer =
    abstract member element: HTMLElement with get, set
    abstract member useWin: bool with get, set
    abstract member winWidth: float with get, set
    abstract member winHeight: float with get, set
    abstract member width: float with get, set
    abstract member height: float with get, set
    abstract member left: float with get, set
    abstract member top: float with get, set
    abstract member zIndex: float with get, set
    abstract member scrollX: float with get, set
    abstract member scrollY: float with get, set
    abstract member prevScrollX: float with get, set
    abstract member prevScrollY: float with get, set
    abstract member scrollWidth: float with get, set
    abstract member scrollHeight: float with get, set
    abstract member velocity: float with get, set
    abstract member backwardX: bool with get, set
    abstract member backwardY: bool with get, set
    abstract member scrollTicker: Timer with get, set
    abstract member dataTimer: Timer with get, set
    abstract member resizeTicker: Timer with get, set
    abstract member wakeTicker: Timer with get, set
    abstract member _head: ScrollObserver with get, set
    abstract member _tail: ScrollObserver with get, set
    abstract member resizeObserver: ResizeObserverType with get, set
    abstract member updateScrollCoords: unit -> unit
    abstract member updateWindowBounds: unit -> unit
    abstract member updateBounds: unit -> unit
    abstract member refreshScrollObservers: unit -> unit
    abstract member refresh: unit -> unit
    abstract member handleScroll: unit -> unit
    abstract member handleEvent: e: Event -> unit
    abstract member revert: unit -> unit

and [<Global>] TimePosition private (noop: TweenValueNoop) =
    [<Emit("$0")>]
    new(_: int) = TimePosition(TweenValueNoop.noop)
    [<Emit("$0")>]
    new(_: float) = TimePosition(TweenValueNoop.noop)
    [<Emit("$0")>]
    new(label: string) = TimePosition(TweenValueNoop.noop)
    [<Emit("$0")>]
    new(_: RelativeTweenValue) = TimePosition(TweenValueNoop.noop)
    [<Emit("$0")>]
    new(_: RelativeTimePosition) = TimePosition(TweenValueNoop.noop)
    [<Emit("$0")>]
    new(stagger: Stagger) = TimePosition(TweenValueNoop.noop)

and AnimationOptions =
    inherit CSSStyleDeclaration
    abstract translateX: float<px> with set
    abstract translateY: float<px> with set
    abstract translateZ: float<px> with set
    abstract rotate: float<px> with set
    abstract rotateX: float<px> with set
    abstract rotateY: float<px> with set
    abstract rotateZ: float<px> with set
    abstract scale: float with set
    abstract scaleX: float with set
    abstract scaleY: float with set
    abstract scaleZ: float with set
    abstract skew: float<deg> with set
    abstract skewX: float<deg> with set
    abstract skewY: float<deg> with set
    abstract perspective: float<px> with set
    // global tween param values    
    abstract delay: U2<int<ms>, FunctionValue<int<ms>>> with set
    abstract duration: U2<int<ms>, FunctionValue<int<ms>>> with set
    abstract ease: Eases with set
    abstract composition: Composition with set
    abstract modifier: TweenParamModifier with set
    // playback settings
    abstract loop: int with set
    abstract loopDelay: int<ms> with set
    abstract alternate: bool with set
    abstract reversed: bool with set
    abstract autoplay: U2<bool, ScrollObserver> with set
    abstract frameRate: int<frames/s> with set
    abstract playbackRate: float with set
    abstract playbackEase: Eases with set
    // Callbacks
    abstract onBegin: Callback<Animation> with set
    abstract onComplete: Callback<Animation> with set
    abstract onBeforeUpdate: Callback<Animation> with set
    abstract onUpdate: Callback<Animation> with set
    abstract onRender: Callback<Animation> with set
    abstract onLoop: Callback<Animation> with set
    abstract onPause: Callback<Animation> with set
    abstract ``then``: (Callback<Animation> -> JS.Promise<unit>) with set
and Animation =
    abstract member play: unit -> Animation
    abstract member reverse: unit -> Animation
    abstract member pause: unit -> Animation
    abstract member restart: unit -> Animation
    abstract member alternate: unit -> Animation
    abstract member resume: unit -> Animation
    abstract member complete: unit -> Animation
    /// Pauses the timer, removes it from the engine's main loop and
    /// frees up the memory
    abstract member cancel: unit -> Animation
    /// <summary>
    /// Cancels the timer, sets its <c>currentTime</c> to <c>0</c> and reverts
    /// the linked <c>onScroll()</c> instance if necessary.<br/><br/> Use <c>.revert()</c>
    /// when you want to completely stop and destroy a timer
    /// </summary>
    abstract member revert: unit -> Animation
    abstract member seek: time: int<ms> * ?muteCallbacks: bool -> Animation
    /// <summary>
    /// Changes the total duration of a timer to fit a specific time. The total
    /// duration is equal to the duration of an iteration multiplied with the total
    /// number of iterations. So if a timer has a duration of 1000ms and loops twice
    /// (3 iterations in total) then the total duration is 3000ms.
    /// </summary>
    /// <param name="duration">Duration in ms</param>
    abstract member stretch: duration: int<ms> -> Animation
    // Properties
    abstract member id: string with get,set
    abstract member targets: Targets with get
    abstract member currentTime: int<ms> with get,set
    abstract member iterationCurrentTime: int<ms> with get,set
    abstract member deltaTime: int<ms> with get
    abstract member progress: float with get,set
    abstract member iterationProgress: float with get,set
    abstract member currentIteration: int with get,set
    abstract member duration: int<ms> with get
    abstract member speed: float with get,set
    abstract member fps: int<frames/s> with get,set
    abstract member paused: bool with get,set
    abstract member began: bool with get,set
    abstract member completed: bool with get,set
    abstract member reversed: bool with get,set
and TimeLineOptionsDefaults =
    abstract loop: int
    abstract loopDelay: int<ms>
    abstract alternate: bool
    abstract reversed: bool
    abstract autoPlay: U2<bool, ScrollObserver>
    abstract frameRate: int<frames/s>
    abstract playbackRate: float
    abstract playbackEase: float
    abstract delay: U2<int<ms>, FunctionValue<int<ms>>>
    abstract duration: U2<int<ms>, FunctionValue<int<ms>>>
    abstract ease: Eases
    abstract composition: Composition
    abstract modifier: TweenParamModifier    
and TimelineOptions =
    abstract defaults: TimeLineOptionsDefaults with set // & callbacks
    abstract delay: int<ms> with set
    abstract loop: int with set
    abstract loopDelay: int<ms> with set
    abstract alternate: bool with set
    abstract reversed: bool with set
    abstract autoPlay: U2<bool, ScrollObserver> with set
    abstract frameRate: int<frames/s> with set
    abstract playbackRate: float with set
    abstract playbackEase: float with set
    abstract onBegin: Callback<Timeline> with set
    abstract onComplete: Callback<Timeline> with set
    abstract onBeforeUpdate: Callback<Timeline> with set
    abstract onUpdate: Callback<Timeline> with set
    abstract onRender: Callback<Timeline> with set
    abstract onLoop: Callback<Timeline> with set
    abstract onPause: Callback<Timeline> with set
    abstract ``then``: (Callback<Timeline> -> JS.Promise<unit>) with set
and Timeline =
    abstract member add: U2<Targets * AnimationOptions, TimerOptions> * ?position: TimePosition -> Timeline
    abstract member set: targets: Targets * animatableProperties: CSSStyleDeclaration * ?position: TimePosition -> Timeline
    abstract member sync: U3<Animation, Timer, Timeline> * ?position: TimePosition -> Timeline
    abstract member label: labelName: string * ?position: TimePosition -> Timeline
    abstract member remove:
        U5< Animation * TimePosition,
            Timer * TimePosition,
            Timeline * TimePosition,
            Targets,
            Targets * string > -> Timeline
    abstract member call: Callback<unit> * ?position: TimePosition -> Timeline
    abstract member init: unit -> Timeline
    abstract member play: unit -> Timeline
    abstract member reverse: unit -> Timeline
    abstract member pause: unit -> Timeline
    abstract member restart: unit -> Timeline
    abstract member alternate: unit -> Timeline
    abstract member resume: unit -> Timeline
    abstract member complete: unit -> Timeline
    /// Pauses the timer, removes it from the engine's main loop and
    /// frees up the memory
    abstract member cancel: unit -> Timeline
    /// <summary>
    /// Cancels the timer, sets its <c>currentTime</c> to <c>0</c> and reverts
    /// the linked <c>onScroll()</c> instance if necessary.<br/><br/> Use <c>.revert()</c>
    /// when you want to completely stop and destroy a timer
    /// </summary>
    abstract member revert: unit -> Timeline
    abstract member seek: time: int<ms> * ?muteCallbacks: bool -> Timeline
    /// <summary>
    /// Changes the total duration of a timer to fit a specific time. The total
    /// duration is equal to the duration of an iteration multiplied with the total
    /// number of iterations. So if a timer has a duration of 1000ms and loops twice
    /// (3 iterations in total) then the total duration is 3000ms.
    /// </summary>
    /// <param name="duration">Duration in ms</param>
    abstract member stretch: duration: int<ms> -> Timeline
    /// <summary>
    /// Re-computes the timeline children animated values defined with a Function based value by updating their from values to their current target values, and their to values to their newly computed values.
    /// </summary>
    abstract member refresh: unit -> Timeline
    // Properties
    abstract member id: string with get,set
    /// Gets and sets the map of timeline labels
    abstract member labels: obj with get,set
    abstract member targets: Targets with get
    abstract member currentTime: int<ms> with get,set
    abstract member iterationCurrentTime: int<ms> with get,set
    abstract member deltaTime: int<ms> with get
    abstract member progress: float with get,set
    abstract member iterationProgress: float with get,set
    abstract member currentIteration: int with get,set
    abstract member duration: int<ms> with get
    abstract member speed: float with get,set
    abstract member fps: int<frames/s> with get,set
    abstract member paused: bool with get,set
    abstract member began: bool with get,set
    abstract member completed: bool with get,set
    abstract member reversed: bool with get,set

and [<Global>] AnimatableSettings private (noop: TweenValueNoop) =
    [<Emit("$0"); ParamObject>]
    new(
        ?unit: string,
        ?duration: U2<int<ms>, FunctionValue<int<ms>>>,
        ?ease: Eases,
        ?modifier: (float -> float)
        ) = AnimatableSettings(TweenValueNoop.noop)

and AnimatableOptions =
    inherit CSSStyleDeclaration
    abstract unit: string with set
    abstract duration: U2<int<ms>, FunctionValue<int<ms>>> with set
    abstract ease: Eases with set
    abstract modifier: (float -> float) with set


and Animatable =
    inherit CSSStyleDeclaration
    [<EmitIndexer>]
    abstract member Item: string -> AnimatableSetter
    abstract targets: Targets
    /// property ==> Animation
    abstract animations: obj

[<Global; AllowNullLiteral>]
type AxisParameter
    [<ParamObject; Emit("$0")>]
        (?snap:
        U8<float, float[], FunctionValue<float>, FunctionValue<int>,
        int, int[], FunctionValue<float[]>, FunctionValue<int[]>>,
        ?modifier: float -> float,
        ?mapTo: float) =
        
        [<Emit("$0")>]
        new(value: bool) = AxisParameter()

type DraggableOptions =
    abstract x: AxisParameter with set
    abstract y: AxisParameter with set
    abstract snap: U8<float, float[], FunctionValue<float>, FunctionValue<int>, int, int[], FunctionValue<float[]>, FunctionValue<int[]>> with set
    abstract modifier: (float -> float) with set
    // Maps axis value to different property
    abstract mapTo: string with set
    /// <summary>
    /// Specifies a different element than the defined target to trigger the drag animation
    /// </summary>
    abstract trigger: Target with set
    abstract container: U4<string, HTMLElement, DraggableBounds, unit -> DraggableBounds> with set
    abstract containerPadding: U3<float<px>, DraggableBounds, unit -> DraggableBounds> with set
    abstract containerFriction: float with set
    abstract releaseContainerFriction: U2<float, unit -> float> with set
    abstract releaseMass: float with set
    abstract releaseStiffness: float with set
    abstract releaseDamping: float with set
    abstract velocityMultiplier: U2<float, unit -> float> with set
    abstract minVelocity: U2<float, unit -> float> with set
    abstract maxVelocity: U2<float, unit -> float> with set
    abstract releaseEase: Eases with set
    abstract dragSpeed: U2<float, unit -> float> with set
    abstract scrollThreshold: U2<float, unit -> float> with set
    abstract scrollSpeed: U2<float, unit -> float> with set
    //callbacks
    abstract onGrab: Callback<Draggable> with set
    abstract onDrag: Callback<Draggable> with set
    abstract onUpdate: Callback<Draggable> with set
    abstract onRelease: Callback<Draggable> with set
    abstract onSnap: Callback<Draggable> with set
    abstract onSettle: Callback<Draggable> with set
    abstract onResize: Callback<Draggable> with set
    abstract onAfterResize: Callback<Draggable> with set
and Draggable =
    abstract member disable: unit -> Draggable
    abstract member enable: unit -> Draggable
    abstract member setX: float<px> * ?muteCallback: bool -> Draggable
    abstract member setY: float<px> * ?muteCallback: bool -> Draggable
    abstract member animateInView: ?duration: int<ms> * ?gap: bool * ?ease: Eases -> Draggable
    abstract member scrollInView: ?duration: int<ms> * ?gap: bool * ?ease: Eases -> Draggable
    abstract member stop: unit -> Draggable
    abstract member reset: unit -> Draggable
    abstract member revert: unit -> Draggable
    abstract member refresh: unit -> Draggable
    //properties
    abstract member snapX: U2<int<px>, int<px>[]> with get,set
    abstract member snapY: U2<int<px>, int<px>[]> with get,set
    abstract member scrollSpeed: float with get,set
    abstract member scrollThreshold: float<px> with get,set
    abstract member dragSpeed: float with get,set
    abstract member maxVelocity: float with get,set
    abstract member minVelocity: float with get,set
    abstract member velocityMultiplier: float with get,set
    abstract member releaseEase: (unit -> Eases) with get,set
    abstract member releaseSpring: Spring with get
    abstract member containerPadding: DraggableBounds with get,set
    abstract member containerFriction: float with get,set
    abstract member containerBounds: DraggableBounds with get
    abstract member containerArray: Option<HTMLElement array> with get
    abstract member ``$container``: HTMLElement with get,set
    abstract member ``$target``: HTMLElement with get
    abstract member ``$scrollContainer``: U2<Window, HTMLElement> with get
    abstract member x: float<px> with get,set
    abstract member y: float<px> with get,set
    abstract member progressX: float with get,set
    abstract member progressY: float with get,set
    abstract member velocity: float with get
    abstract member angle: float<rad> with get
    abstract member xProp: string with get
    abstract member yProp: string with get
    abstract member destX: float<px> with get
    abstract member destY: float<px> with get
    abstract member deltaX: float<px> with get
    abstract member deltaY: float<px> with get
    abstract member enabled: bool with get
    abstract member grabbed: bool with get
    abstract member dragged: bool with get
    abstract member cursor: DraggableCursor with get,set
    abstract member disabled: x: float<px> * y: float<px> with get
    abstract member ``fixed``: bool with get
    abstract member useWin: bool with get
    abstract member isFinePointer: bool with get,set
    abstract member initialized: bool with get
    abstract member canScroll: bool with get
    abstract member contained: bool with get
    abstract member manual: bool with get
    abstract member released: bool with get
    abstract member updated: bool with get
    abstract member scroll: JSCoordinatePojo with get
    abstract member coords: x: float<px> * y: float<px> * prevX: float<px> * prevY: float<px> with get
    abstract member snapped: x: float<px> * y: float<px> with get
    abstract member pointer: x: float<px> * y: float<px> * prevX: float<px> * prevY: float<px> with get
    abstract member scrollView: width: float<px> * height: float<px> with get
    abstract member dragArea: x: float<px> * y: float<px> * width: float<px> * height: float<px> with get
    abstract member scrollBounds: top: float<px> * right: float<px> * bottom: float<px> * left: float<px> with get
    abstract member targetBounds: top: float<px> * right: float<px> * bottom: float<px> * left: float<px> with get
    abstract member window: width: float<px> * height: float<px> with get
    abstract member pointerVelocity: float with get
    abstract member pointerAngle: float<rad> with get
    abstract member activeProp: string with get
    abstract member onGrab: Callback<Draggable> with get,set
    abstract member onDrag: Callback<Draggable> with get,set
    abstract member onRelease: Callback<Draggable> with get,set
    abstract member onUpdate: Callback<Draggable> with get,set
    abstract member onSettle: Callback<Draggable> with get,set
    abstract member onSnap: Callback<Draggable> with get,set
    abstract member onResize: Callback<Draggable> with get,set
    abstract member onAfterResize: Callback<Draggable> with get,set

type ScopeOptionsDefaults =
    abstract loop: int with set
    abstract loopDelay: int<ms> with set
    abstract alternate: bool with set
    abstract reversed: bool with set
    abstract autoPlay: U2<bool, ScrollObserver> with set
    abstract frameRate: int<frames/s> with set
    abstract playbackRate: float with set
    abstract playbackEase: float with set
    abstract delay: U2<int<ms>, FunctionValue<int<ms>>> with set
    abstract duration: U2<int<ms>, FunctionValue<int<ms>>> with set
    abstract ease: Eases with set
    abstract composition: Composition with set
    abstract modifier: TweenParamModifier with set  
    abstract onBegin: Callback<unit> with set
    abstract onUpdate: Callback<unit> with set
    abstract onRender: Callback<unit> with set
    abstract onLoop: Callback<unit> with set
    abstract onComplete: Callback<unit> with set
and ScopeOptions =
    abstract root: U3<string, Element, HtmlElement> with set
    abstract defaults: ScopeOptionsDefaults with set
    abstract mediaQueries: obj with set
and Scope =
    abstract member add: Scope -> (unit -> unit)
    abstract member refresh: unit -> Scope
    abstract member revert: unit -> Scope
    abstract member data: obj with get,set
    abstract member defaults: ScopeOptionsDefaults  with get
    abstract member root: U2<Document, HTMLElement> with get
    abstract member constructors: (unit -> unit)[] with get
    abstract member revertConstructors: (unit -> unit)[] with get
    abstract member revertibles: obj[] with get
    abstract member methods: obj with get
    abstract member matches: obj with get
    abstract member mediaQueryLists: obj with get

type IStagger<'T> =
    abstract start: 'T with set
    abstract ``from``: U2<int, StaggerFrom> with set
    abstract reversed: bool with set
    abstract ease: Eases with set
    abstract grid: int * int with set
    abstract axis: Axis with set
    abstract modifier: (float -> U2<float, string>) with set
type StaggerTimeline =
    inherit IStagger<U2<float, TimePosition>>
type Stagger =
    inherit IStagger<float>
[<Import("svg", "animejs")>]
type Svg =
    static member morphTo (path2: SVGElement, ?precision: float) : FunctionValue<string> = nativeOnly
    static member createMotionPath (path: SVGElement) : MotionPath = nativeOnly
    /// Exposes a 'draw' property
    static member createDrawable (selector: SVGElement, ?start: float, ?``end``: float) : SVGElement[] = nativeOnly


type Engine =
    abstract member timeUnit: TimeUnit with get,set
    abstract member speed: float with get,set
    abstract member fps: int<frames/s> with get,set
    /// Value of 0 will skip the rounding process.
    /// Only rounds properties that are string values internally
    abstract member precision: int
    abstract member pauseOnDocumentHidden: bool with get,set
    abstract member update: unit -> Engine
    abstract member pause: unit -> Engine
    abstract member resume: unit -> Engine
    abstract member currentTime: float with get,set
    abstract member deltaTime: float with get
    abstract member useDefaultMainLoop: bool with get,set
    
    abstract member playbackEase: Eases with get, set
    abstract member playbackRate: float with get, set
    abstract member frameRate: int<frames/s> with get, set
    abstract member loop: U2<float, bool> with get, set
    abstract member reversed: bool with get, set
    abstract member alternate: bool with get, set
    abstract member autoplay: bool with get, set
    abstract member duration: U2<float<ms>, FunctionValue<float>> with get, set
    abstract member delay: U2<float, FunctionValue<float>> with get, set
    abstract member loopDelay: float with get, set
    abstract member ease: Eases with get, set
    abstract member composition: Composition with get, set
    abstract member modifier: (float -> float) with get, set
    abstract member onBegin: Callback<obj> with get, set
    abstract member onBeforeUpdate: Callback<obj> with get, set
    abstract member onUpdate: Callback<obj> with get, set
    abstract member onLoop: Callback<obj> with get, set
    abstract member onPause: Callback<obj> with get, set
    abstract member onComplete: Callback<obj> with get, set
    abstract member onRender: Callback<obj> with get, set

type Export with
    [<Import("createTimer", "animejs")>]
    static member createTimer (parameters: TimerOptions) : Timer = nativeOnly
    [<Import("cleanInlineStyles", "animejs")>]
    static member cleanInlineStyles (renderable: 'T) : 'T = nativeOnly    
    [<Import("animate", "animejs"); Emit("animate($0, {...$1, {{...$2}}})")>]
    static member animate (targets: Targets, parameters: AnimationOptions, ?others: obj) : Animation = nativeOnly
    [<Import("random", "animejs")>]
    static member random (min: float, max: float, ?decimalLength: float) : float = nativeOnly
    [<Import("randomPick", "animejs")>]
    static member randomPick (items: U2<string, ResizeArray<obj>>) : obj = nativeOnly
    [<Import("shuffle", "animejs")>]
    static member shuffle (items: ResizeArray<obj>) : ResizeArray<obj> = nativeOnly
    [<ImportMember "animejs">]
    static member stagger (target: Staggerable, ?params: Stagger) : FunctionValue<TweenValue> = nativeOnly

[<AllowNullLiteral; Interface>]
type MotionPath =
    abstract member translateX : FunctionValue<float<px>> with get, set
    abstract member translateY : FunctionValue<float<px>> with get, set
    abstract member rotate : FunctionValue<float<deg>> with get, set

[<Import("svg", "animejs")>]
type SvgExports =
    static member morphTo (path2: SVGElement, ?precision: float) : FunctionValue<_> = nativeOnly
    static member createMotionPath (path: SVGElement) : MotionPath = nativeOnly
    static member createDrawable (selector: SVGElement, ?start: float, ?``end``: float) : ResizeArray<SVGElement> = nativeOnly

type Export with
    [<Import("createAnimatable", "animejs")>]
    static member createAnimatable (targets: Targets, parameters: AnimatableOptions) : Animatable = nativeOnly

[<AbstractClass>]
[<Fable.Core.Erase>]
type Exports =
    [<Import("createDraggable", "animejs")>]
    static member createDraggable (target: Targets, ?parameters: DraggableOptions) : Draggable = nativeOnly

[<AllowNullLiteral>]
[<Interface>]
type AnimatablePropertySetter =
    [<Emit("$0($1...)")>]
    abstract member Invoke: ``to``: U2<float, ResizeArray<float>> * ?duration: float * ?ease: EasingFunction -> Animation

[<AllowNullLiteral>]
[<Interface>]
type AnimatablePropertyGetter =
    [<Emit("$0($1...)")>]
    abstract member Invoke: unit -> U2<float, ResizeArray<float>>

type Exports with
    [<Import("createScope", "animejs")>]
    static member createScope (?``params``: ScopeOptions) : Scope = nativeOnly
    [<Import("onScroll", "animejs")>]
    static member onScroll (?parameters: ScrollObserverOptions) : ScrollObserver = nativeOnly
    [<Import("createSpring", "animejs"); ParamObject>]
    static member createSpring (?mass: float, ?stiffness: float, ?damping: float, ?velocity: float) : Spring = nativeOnly
    // [<Import("easingToLinear", "animejs")>]
    // static member easingToLinear (fn: EasingFunction, ?samples: float) : string = nativeOnly
