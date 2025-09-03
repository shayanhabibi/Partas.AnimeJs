[<AutoOpen>]
module Partas.AnimeJs.Core

open System
open System.Runtime.CompilerServices
open Fable.Core
open Fable.Core.JsInterop
open Browser.Types
open Partas.AnimeJs.Bindings

#nowarn 3535
    
[<Erase>]
module Spec =
    let [<Literal; Erase>] path = "animejs"
    let [<Literal; Erase>] version = "4.0.2"

[<AutoOpen; Erase>]
module Types =
    type AnimeJs = interface end
    /// <summary>
    /// Alias for <c>'T -> unit</c>.
    /// </summary>
    type Callback<'T> = 'T -> unit
    /// <summary>
    /// Alias for <c>unit -> 'T</c>
    /// </summary>
    type ChainMethod<'T> = unit -> 'T
    /// <summary>
    /// CSS Selector.<br/> Provides the animejs utility <c>utils.$</c> to find all nodes by the selector
    /// </summary>
    /// <example><code>
    /// Selector ".code"
    /// |> _.find  // : NodeList
    /// </code></example>
    [<Erase>]
    type Selector = Selector of string with
        member inline this.find with get(): NodeList = !!this |> Utils.``$``
    [<Erase>]
    type Target<'Type> = Target of 'Type
    [<Erase>]
    type Targets = Targets of ResizeArray<obj>

    /// <summary>
    /// Alias for <c>float -> float</c>
    /// </summary>
    type FloatModifier = float -> float
    /// <summary>
    /// Delegate for <c>float -> float</c> which is the signature for any easing function.
    /// </summary>
    /// <remarks>
    /// The difference between using a normal function and the delegate pertains mostly to
    /// the syntax sugar of an easing function being automatically associated with the
    /// <c>ease</c> property in computations. Since the library easing functions all produce
    /// this delegate, they will be automatically picked up without having to wrap them in the
    /// delegate type.
    /// </remarks>
    type EasingFun = delegate of float -> float
    /// <summary>
    /// Interface backed by a simple string.
    /// </summary>
    /// <remarks>
    /// You can create a percent keyframe using the operator <c>!%</c> or via the
    /// static member <c>create</c>. You will have to ensure you pass valid input if using
    /// the static member. The operator will automatically suffix the number with <c>%</c>
    /// <br/>
    /// You can access the backing string by <c>.Value</c>
    /// </remarks>
    /// <seealso href="https://animejs.com/documentation/animation/keyframes/percentage-based-keyframes">
    /// AnimeJs Documentation
    /// </seealso>
    [<Interface>]
    type KeyframePercentValue =
        [<Emit "$0">]
        static member create: string -> KeyframePercentValue = jsNative
        [<Emit "$0">]
        abstract member Value: string with get

    /// <summary>
    /// Returned and set in some properties. Can use the <c>Coordinate</c> record with
    /// <c>.toPojo</c>. Can also create a record using <c>Coordinate.fromPojo</c> 
    /// </summary>
    [<Erase; AllowNullLiteral>]
    type JSCoordinatePojo =
        abstract member x: float with get,set
        abstract member y: float with get,set
    /// <summary>
    /// Returned and set in some properties. Can use the <c>Coordinate</c> record with
    /// <c>.toTuple</c>. Can also create a record using <c>Coordinate.fromTuple</c>
    /// </summary>
    [<Erase>]
    type JSCoordinate = JSCoordinate of x: float * y: float
    /// <summary>
    /// Returned and set in some properties. Can use the <c>CoordinateHistory</c> record with
    /// <c>.toTuple</c>. Can also create a record using <c>CoordinateHistory.fromTuple</c>
    /// </summary>
    [<Erase>]
    type JSCoordinateHistory = JSCoordinateHistory of x: float * y: float * prevX: float * prevY: float 
    
    /// FSharp wrapping type for use with the Draggable and others. Can compile to/from tuples and pojos using
    /// the static methods.
    type CoordinateHistory = {
        x: float
        y: float
        prevX: float
        prevY: float
    } with
        member inline this.toPojo = this |> toPlainJsObj
        member inline this.toTuple = JSCoordinateHistory (this.x,this.y,this.prevX,this.prevY)
        static member inline fromTuple (JSCoordinateHistory (x,y,prevX,prevY)) = { x = x; y = y; prevX = prevX; prevY = prevY }
        static member inline unsafeFromTuple value =
            value |> unbox<JSCoordinateHistory> |> CoordinateHistory.fromTuple

    /// FSharp wrapping type for use with the Draggable and others. Can compile to/from tuples and pojos using
    /// the static methods.
    type Coordinate = {
        x: float
        y: float
    } with
        member inline this.toPojo: JSCoordinatePojo = this |> JsInterop.toPlainJsObj |> unbox
        member inline this.toTuple = JSCoordinate (this.x, this.y)
        static member inline fromPojo (
                pojo: 'a when 'a:(member x: float) and 'a:(member y: float)
            ): Coordinate = { x = LanguagePrimitives.FloatWithMeasure pojo.x; y = LanguagePrimitives.FloatWithMeasure pojo.y }
        static member inline unsafeFromPojo (
                object: obj
            ): Coordinate = { x = LanguagePrimitives.FloatWithMeasure object?x; y = LanguagePrimitives.FloatWithMeasure object?y }
        static member inline fromTuple (value: float * float): Coordinate = {
            x = LanguagePrimitives.FloatWithMeasure (fst value)
            y = LanguagePrimitives.FloatWithMeasure (snd value)
        }
        static member inline fromTuple (JSCoordinate (x: float , y: float)) = { x = x; y = y }
        static member inline unsafeFromTuple (value: obj): Coordinate = {
            x = fst !!value
            y = snd !!value
        }
    
    /// <summary>
    /// Interface backed by a simple string to represent Relative Time Positions in AnimeJs
    /// </summary>
    /// <remarks>
    /// You can create relative time positions by the static method <c>create</c>. Ensure you
    /// pass a valid string. You can access the backing string using <c>.Value</c>
    /// <br/>
    /// Alternatively, use the operators in the <c>Operators</c> module such as <c>!&lt;&lt;*= 5</c>. 
    /// </remarks>
    /// <seealso href="https://animejs.com/documentation/timeline/time-position">
    /// AnimeJs Documentation
    /// </seealso>
    [<Interface>]
    type RelativeTimePosition =
        [<Emit("$0")>]
        abstract member Value: string with get
        [<Emit("$0")>]
        static member create: string -> RelativeTimePosition = jsNative
    type RelativeTweenValue = inherit RelativeTimePosition
    /// <summary>
    /// Interface backed by a simple string to represent a time label as a concrete type.
    /// </summary>
    /// <remarks>
    /// <para>You can create a time label using the static member create, or using the operator
    /// accessed method <c>timeLabel</c></para>
    /// <para>The underlying value is accessible by <c>.Value</c></para>
    /// </remarks>
    /// <seealso href="https://animejs.com/documentation/timeline/timeline-methods/label">
    /// AnimeJs Documentation
    /// </seealso>
    [<Interface>]
    type TimeLabel =
        inherit RelativeTimePosition
        [<Emit "$0">]
        static member create: string -> TimeLabel = jsNative
    [<Interface; AllowNullLiteral>]
    type DraggableCursor =
        abstract member onGrab: string with get,set
        abstract member onDrag: string with get,set
    
[<Import("eases", Spec.path); Interface>]
type Eases =
    static abstract linear: ?x1: float * ?m1: string * ?x2: float -> EasingFun
    static abstract irregular: ?length: float * ?randomness: float -> EasingFun
    static abstract steps: ?steps: float * ?fromStart: bool -> EasingFun
    static abstract cubicBezier: ?mX1: float * ?mY1: float * ?mX2: float * ?mY2: float -> EasingFun
    static abstract ``in``: ?power: float -> EasingFun
    static abstract out: ?power: float -> EasingFun
    static abstract inOut: ?power: float -> EasingFun
    static abstract inQuad: EasingFun
    static abstract outQuad: EasingFun
    static abstract inOutQuad: EasingFun
    static abstract inCubic: EasingFun
    static abstract outCubic: EasingFun
    static abstract inOutCubic: EasingFun
    static abstract inQuart: EasingFun
    static abstract outQuart: EasingFun
    static abstract inOutQuart: EasingFun
    static abstract inQuint: EasingFun
    static abstract outQuint: EasingFun
    static abstract inOutQuint: EasingFun
    static abstract inSine: EasingFun
    static abstract outSine: EasingFun
    static abstract inOutSine: EasingFun
    static abstract inCirc: EasingFun
    static abstract outCirc: EasingFun
    static abstract inOutCirc: EasingFun
    static abstract inExpo: EasingFun
    static abstract outExpo: EasingFun
    static abstract inOutExpo: EasingFun
    static abstract inBounce: EasingFun
    static abstract outBounce: EasingFun
    static abstract inOutBounce: EasingFun
    static abstract inBack: ?overshoot: float -> EasingFun
    static abstract outBack: ?overshoot: float -> EasingFun
    static abstract inOutBack: ?overshoot: float -> EasingFun
    static abstract inElastic: ?amplitude: float * ?period: float -> EasingFun
    static abstract outElastic: ?amplitude: float * ?period: float -> EasingFun
    static abstract inOutElastic: ?amplitude: float * ?period: float -> EasingFun
    [<Import("createSpring", Spec.path); ParamObject>] static member
        createSpring(?mass:float,?stiffness:float,?damping:float,?velocity:float): EasingFun = jsNative
    [<Import("createSpring", Spec.path)>] static member
        createSpring(options: obj): EasingFun = jsNative

[<AutoOpen; Erase>]
module rec AutoOpenInstanceDefinitions =
    [<AllowNullLiteral; Interface>]
    type Bounds =
        abstract member top: float with get,set
        abstract member left: float with get,set
        abstract member right: float with get,set
        abstract member bottom: float with get,set
    [<AllowNullLiteral>]
    [<Interface>]
    type ScrollContainer =
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
    type ScrollObserver = Bindings.Types.ScrollObserver
    type ScrollObserverOptions = Bindings.Types.ScrollObserverOptions
    type Timer = Bindings.Types.Timer
    type Animation =
        inherit TimerObjectInjection<Animation>
        abstract member targets: Targets with get
    type AnimationOptions = Bindings.Types.AnimationOptions
    type Timeline =
        inherit TimerObjectInjection<Timeline>
        [<EmitMethod "add">]
        abstract member add2: obj * ?position: RelativeTimePosition -> Timeline
        [<EmitMethod "add">]
        abstract member add3: obj * obj * ?position: RelativeTimePosition -> Timeline
        abstract member set: targets: Targets * animatableProperties: obj * ?position: RelativeTimePosition -> Timeline
        abstract member sync: obj * ?position: RelativeTimePosition -> Timeline
        abstract member label: labelName: string * ?position: RelativeTimePosition -> Timeline
        abstract member remove: obj * label: string -> Timeline
        abstract member call: Callback<unit> * ?position: RelativeTimePosition -> Timeline
        abstract member init: ChainMethod<Timeline>
        abstract member refresh: ChainMethod<Timeline>
        /// Gets and sets the map of timeline labels
        abstract member labels: obj with get,set
        abstract member targets: Targets with get
        abstract member duration: int with get
    type TimelineOptions = Bindings.Types.TimelineOptions
    type TimelineOptionsDefaults = Bindings.Types.TimelineOptionsDefaults
    type Draggable = Bindings.Types.Draggable
    type DraggableOptions = Bindings.Types.DraggableOptions
    type AxisOptions = interface end
    type Scope = Bindings.Types.Scope
    type ScopeOptions = Bindings.Types.ScopeOptions
    type EngineDefaults =        
        abstract member playbackEase: EasingFun with get, set
        abstract member playbackRate: float with get, set
        abstract member frameRate: int with get, set
        abstract member loop: U2<float, bool> with get, set
        abstract member reversed: bool with get, set
        abstract member alternate: bool with get, set
        abstract member autoplay: bool with get, set
        abstract member duration: U2<float, FunctionValue<float>> with get, set
        abstract member delay: U2<float, FunctionValue<float>> with get, set
        abstract member loopDelay: float with get, set
        abstract member ease: EasingFun with get, set
        abstract member composition: composition with get, set
        abstract member modifier: (float -> float) with get, set
        abstract member onBegin: Callback<obj> with get, set
        abstract member onBeforeUpdate: Callback<obj> with get, set
        abstract member onUpdate: Callback<obj> with get, set
        abstract member onLoop: Callback<obj> with get, set
        abstract member onPause: Callback<obj> with get, set
        abstract member onComplete: Callback<obj> with get, set
        abstract member onRender: Callback<obj> with get, set
    type Engine =
        abstract member timeUnit: Enums.timeUnit with get,set
        abstract member speed: float with get,set
        abstract member fps: int with get,set
        /// Value of 0 will skip the rounding process.
        /// Only rounds properties that are string values internally
        abstract member precision: int with get,set
        abstract member pauseOnDocumentHidden: bool with get,set
        abstract member update: unit -> Engine
        abstract member pause: unit -> Engine
        abstract member resume: unit -> Engine
        abstract member currentTime: float with get,set
        abstract member deltaTime: float with get,set
        abstract member useDefaultMainLoop: bool with get,set
        abstract member defaults: EngineDefaults with get,set
        
    type ScopeOptionsDefaults = Bindings.Types.ScopeOptionsDefaults
    [<AllowNullLiteral; Interface>]
    type MotionPath =
        abstract member translateX : FunctionValue<float> with get, set
        abstract member translateY : FunctionValue<float> with get, set
        abstract member rotate : FunctionValue<float> with get, set

    [<AllowNullLiteral>]
    [<Interface>]
    type AnimatablePropertySetter =
        [<Emit("$0($1...)")>]
        abstract member Invoke: ``to``: U2<float, ResizeArray<float>> * ?duration: float * ?ease: EasingFun -> Animation

    [<AllowNullLiteral>]
    [<Interface>]
    type AnimatablePropertyGetter =
        [<Emit("$0($1...)")>]
        abstract member Invoke: unit -> U2<float, ResizeArray<float>>

    type Animatable = Bindings.Types.Animatable
    type AnimatableOptions = Bindings.Types.AnimatableOptions
    type TimerObjectInjection<'Type> =
        abstract member play: ChainMethod<'Type>
        abstract member reverse: ChainMethod<'Type>
        abstract member pause: ChainMethod<'Type>
        abstract member restart: ChainMethod<'Type>
        abstract member alternate: ChainMethod<'Type>
        abstract member resume: ChainMethod<'Type>
        abstract member complete: ChainMethod<'Type>
        abstract member cancel: ChainMethod<'Type>
        abstract member revert: ChainMethod<'Type>
        abstract member seek: time: int * ?muteCallbacks: bool -> 'Type
        abstract member stretch: duration: int -> 'Type
        
        abstract member id: string with get,set
        abstract member deltaTime: float with get
        abstract member currentTime: float with get,set
        abstract member iterationCurrentTime: float with get,set
        abstract member progress: float with get,set
        abstract member iterationProgress: float with get,set
        abstract member currentIteration: float with get,set
        abstract member speed: float with get,set
        abstract member fps: int with get,set
        abstract member paused: bool with get,set
        abstract member began: bool with get,set
        abstract member completed: bool with get,set
        abstract member reversed: bool with get,set

type TextSplitterOptions = Bindings.Types.TextSplitterOptions
type SplitParameters = Bindings.Types.SplitParameters
type TextSplitter = Bindings.Types.TextSplitter
type Utils = Bindings.Types.Utils
type Svg = Bindings.Types.Svg

type AnimeJs with
    [<Import("createTimer", "animejs")>]
    static member createTimer (parameters: obj) : Timer = nativeOnly
    [<ImportMember(Spec.path)>]
    static member createTimeline (parameters: obj): Timeline = nativeOnly
    [<Import("animate", "animejs")>]
    static member animate (targets: Targets, parameters: obj) : Animation = nativeOnly
    [<ImportMember "animejs">]
    static member stagger (target: obj, ?parameters: obj) : FunctionValue<float> = nativeOnly
    [<Import("createAnimatable", "animejs")>]
    static member createAnimatable (targets: Targets, parameters: obj) : Animatable = nativeOnly
    [<Import("createDraggable", "animejs")>]
    static member createDraggable (target: Targets, ?parameters: obj) : Draggable = nativeOnly
    [<Import("createSpring", "animejs"); ParamObject>]
    static member createSpring (?mass: float, ?stiffness: float, ?damping: float, ?velocity: float) : EasingFun = nativeOnly
    [<Import("createScope", "animejs")>]
    static member createScope (?``params``: obj) : Scope = nativeOnly
    [<Import("onScroll", "animejs")>]
    static member inline onScroll (options: obj): ScrollObserver = jsNative 

type Targets  with
    [<Emit "$0">]
    member inline this.Value = let (Targets value) = this in value  
    member inline this.Yield(value: obj) = value |> this.Value.Add
    member inline this.Yield(value: obj seq) = value |> unbox<obj seq> |> this.Value.AddRange
    member inline this.Yield(value: unit -> obj) =
        value() |> this.Value.Add
    member inline this.Yield(value: unit -> obj seq) =
        value() |> this.Value.AddRange
    member inline this.Run _ = this
let targets (value: obj list) = value |> id |> ResizeArray |> Targets

type FableObject = (string * obj) list
type FableObjectBuilder = interface end
type EasePropertyInjection = interface end
type PlaybackPropertyInjection =
    inherit FableObjectBuilder
    inherit EasePropertyInjection
type PercentKeyframe = PercentKeyframe of string with
    interface FableObjectBuilder
    interface EasePropertyInjection
    static member inline (<--) (x: PercentKeyframe, keyframeOptions: (string * obj) list): KeyframePercentValue =
        !!(!!x ==> createObj keyframeOptions)
type StyleValue = StyleValue of string * obj
type StyleValueList = StyleValueList of string * obj list
type StyleValueFunction = StyleValueFunction of string * FunctionValue<obj>

type TweenPropertyInjection =
    inherit FableObjectBuilder
    inherit EasePropertyInjection
type CssStyle =
    inherit TweenPropertyInjection

type stagger [<Emit("$0")>] private (value: obj) =
    interface FableObjectBuilder
    interface EasePropertyInjection
    [<Emit("[$0,$1]")>] new (value: string, value2: string) = stagger([| value; value2 |])
    [<Emit("[$0,$1]")>] new (value: float, value2: string) = stagger([| value; !!value2 |])
    [<Emit("[$0,$1]")>] new (value: float,value2: float) = stagger([| value; value2 |])
    [<Emit("[$0,$1]")>] new (value: string, value2: float) = stagger([| value; !!value2 |])
    [<Emit("$0")>] new (value: string) = stagger(value)
    [<Emit("$0")>] new (value: float) = stagger(value)
    member inline this.asFunctionValue = AnimeJs.stagger(this)

[<Interface>]
type ICssStyle =
    inherit CssStyle
    static member inline (<--) (x: ICssStyle, stagger: stagger) = unbox<StyleValue>(x, AnimeJs.stagger(stagger))
    static member inline (<--) (x: ICssStyle,y: int) = StyleValue(!!x, y)
    static member inline (<--) (x: ICssStyle,y: float) = StyleValue(!!x, y)
    static member inline (<--) (x: ICssStyle, y: string) = StyleValue(!!x,y)
    static member inline (<--) (x: ICssStyle, y: obj list) = StyleValueList(!!x,!!y)
    static member inline (<--) (x: ICssStyle, y: FunctionValue<obj>) = StyleValueFunction(!!x,y)

type StyleObj = interface end
type StyleAnimationObj =
    inherit StyleObj
type StyleToObj =
    inherit StyleAnimationObj
type StyleFromObj =
    inherit StyleAnimationObj
type StyleAnimatableObj =
    inherit StyleObj

type StyleArray() =
    interface CssStyle
    member inline _.Run(state: FableObject): StyleObj = !!createObj state
    member inline _.Run(state: ^T when ^T :> StyleAnimationObj): ^T = !!createObj state
    
type KeyframeValue = interface end
type KeyframeBuilder() =
    interface FableObjectBuilder
    interface TweenPropertyInjection
[<Erase>]
type StyleArrayBuilder = StyleArrayBuilder of string with
    static member inline (<--) (x: StyleArrayBuilder, keyframes: KeyframeValue list): StyleArray =
        !!(!!x ==> (keyframes |> List.toArray))
    static member inline (<--) (x: StyleArrayBuilder, keyframes: PercentKeyframe list): StyleArray =
        !!(!!x ==> (createObj !!keyframes))
type TimerCallbackInjection<'Type> = interface end
type AnimationCallbackInjection<'Type> = inherit TimerCallbackInjection<'Type>
type ScrollObserverCallbackInjection<'Type> = interface end
type DraggableCallbackInjection<'Type> = interface end
type TweenObjectBuilder =
    inherit FableObjectBuilder
    inherit EasePropertyInjection
type BoundsBuilder = inherit FableObjectBuilder
type SpringBuilder = inherit FableObjectBuilder
type TimerPropertyInjection =
    inherit PlaybackPropertyInjection
    inherit TimerCallbackInjection<Timer>

type TimerBuilder =
    inherit FableObjectBuilder
    inherit TimerPropertyInjection

type TimelineBuilder =
    inherit FableObjectBuilder
    inherit PlaybackPropertyInjection
    inherit AnimationCallbackInjection<Timeline>
type AnimatableBuilder =
    inherit FableObjectBuilder
    inherit EasePropertyInjection
[<Erase>]
type StyleNoStyle = StyleNoStyle of unit
[<Erase>]
type NoStyleValue = NoStyleValue of obj
[<Erase>]
type NoStyleWithDuration = NoStyleWithDuration of duration: float
[<Erase>]
type NoStyleWithEase = NoStyleWithEase of ease: EasingFun
[<Erase>]
type NoStyleWithDurationEase = NoStyleWithDurationEase of duration: float * ease: EasingFun
[<Erase>]
type NoStyleValueDuration = NoStyleValueDuration of value: obj * duration: float
[<Erase>]
type NoStyleValueEase = NoStyleValueEase of value: obj * ease: EasingFun
[<Erase>]
type NoStyleValueDurationEase = NoStyleValueDurationEase of value: obj * duration: float * ease: EasingFun

[<Erase>]
type StyleNoValue = StyleNoValue of string
[<Erase>]
type StyleWithValue = StyleWithValue of string * obj
[<Erase>]
type StyleWithDuration = StyleWithDuration of prop: string * duration: float
[<Erase>]
type StyleWithEase = StyleWithEase of prop: string * ease: EasingFun
[<Erase>]
type StyleWithDurationEase = StyleWithDurationEase of prop: string * duration: float * ease: EasingFun
[<Erase>]
type StyleWithValueDuration = StyleWithValueDuration of prop: string * value: obj * duration: float
[<Erase>]
type StyleWithValueEase = StyleWithValueEase of prop: string * value: obj * ease: EasingFun
[<Erase>]
type StyleWithValueDurationEase = StyleWithValueDurationEase of prop: string * value: obj * duration: float * ease: EasingFun
type CursorBuilder = interface end
type AxisBuilder =
    inherit FableObjectBuilder
type AxisX() =
    interface AxisBuilder
    member inline _.Run(state: bool): string * obj = "x" ==> state
    member inline _.Run(state: FableObject): string * obj = "x" ==> createObj state
type AxisY() =
    interface AxisBuilder
    member inline _.Run(state: bool): string * obj = "y" ==> state
    member inline _.Run(state: FableObject): string * obj = "y" ==> createObj state
type DraggableBuilder =
    inherit AxisBuilder
    inherit DraggableCallbackInjection<Draggable>
type ScrollObserverBuilder =
    inherit FableObjectBuilder
    inherit ScrollObserverCallbackInjection<ScrollObserver>
type AnimationBuilder =
    inherit TweenPropertyInjection
    inherit FableObjectBuilder
    inherit AnimationCallbackInjection<Animation>
    inherit PlaybackPropertyInjection
    inherit EasePropertyInjection

type DrawableBuilder = interface end

type UtilsBuilder = interface end

type ScopeDefaultOptionsBuilder =
    inherit PlaybackPropertyInjection
    inherit TimerCallbackInjection<ScopeOptionsDefaults>
type ScopeBuilder =
    inherit FableObjectBuilder

type TimelineDefaultsBuilder =
    inherit TimerPropertyInjection
    inherit FableObjectBuilder
    inherit TweenPropertyInjection

type TextSplitterBuilder =
    inherit FableObjectBuilder
type SplitParameterBuilder =
    inherit FableObjectBuilder
[<Erase; Struct>]
type TextSplitterParameter = TextSplitterParameter of string * obj
type TextSplitterParameterBuilder(parameter: string) =
    interface SplitParameterBuilder
    member inline this.Run(state: FableObject) =
        parameter ==> createObj state |> TextSplitterParameter
    member inline this.Run(state: string) =
        parameter ==> state |> TextSplitterParameter
    member inline this.Run(state: bool) =
        parameter ==> state |> TextSplitterParameter
    

[<AutoOpen; Erase>]
module AutoOpenComputationImplementations =
    [<Erase>]
    module Helpers =
        let inline add state value = value :: state
        let inline addTail state value = state @ [ value ]
    open Helpers
    type FableObjectBuilder with
        member inline _.Yield(_: unit): FableObject = []
        member inline _.Yield(value: string * obj) = [ value ]
        member inline _.Yield(object: FableObject) = object
        member inline _.Combine(_: unit, value: string * obj) =
            [ value ]
        member inline _.Combine(value: string * obj, _: unit) =
            [ value ]
        member inline _.Combine(_: unit, value: FableObject) = value
        member inline _.Combine(value: FableObject, _: unit) = value
        member inline _.Combine(left: string * obj, right: string * obj) =
            [ left; right ]
        member inline _.Combine(left: FableObject, right: string * obj) =
            right :: left
        member inline _.Combine(left: string * obj, right: FableObject) =
            left :: right
        member inline _.Combine(left: FableObject, right: FableObject) =
            left @ right
        member inline _.Combine(y) = fun () -> y
        member inline _.Delay([<InlineIfLambda>] value) = value()
        member inline _.For(state: FableObject, [<InlineIfLambda>] value: string * obj -> FableObject) =
            state |> List.collect value
        member inline _.For(state: FableObject, [<InlineIfLambda>] value: unit -> FableObject) =
            state @ value()
        member inline _.For(state: FableObject, [<InlineIfLambda>] value: unit -> string * obj) =
            value() :: state
        member inline _.For(state: ^T, [<InlineIfLambda>] value: ^T -> _) =
            state |> value
        // member inline _.For(_:unit, [<InlineIfLambda>] value) = value()
    type TimelineDefaultsBuilder with
        member inline _.Run(state: FableObject) = createObj state |> unbox<TimelineOptionsDefaults>
            
    type TimerBuilder with
        member inline _.Run(state: FableObject) = AnimeJs.createTimer(createObj state)
    type DraggableCallbackInjection<'Type> with
        [<CustomOperation "onGrab">]
        member inline _.onGrabOp(state: FableObject, [<InlineIfLambda>] value: Callback<'Type>) =
            "onGrab" ==> value |> add state
        [<CustomOperation "onDrag">]
        member inline _.onDragOp(state: FableObject, [<InlineIfLambda>] value: Callback<'Type>) =
            "onDrag" ==> value |> add state
        [<CustomOperation "onRelease">]
        member inline _.onReleaseOp(state: FableObject, [<InlineIfLambda>] value: Callback<'Type>) =
            ("onRelease" ==> value)
            :: state
        [<CustomOperation "onSnap">]
        member inline _.onSnapOp(state: FableObject, [<InlineIfLambda>] value: Callback<'Type>) =
            ("onSnap" ==> value)
            :: state
        [<CustomOperation "onResize">]
        member inline _.onResizeOp(state: FableObject, [<InlineIfLambda>] value: Callback<'Type>) =
            ("onResize" ==> value)
            :: state
        [<CustomOperation "onAfterResize">]
        member inline _.onAfterResizeOp(state: FableObject, [<InlineIfLambda>] value: Callback<'Type>) =
            ("onAfterResize" ==> value)
            :: state
        [<CustomOperation "onSettle">]
        member inline _.onSettleOp(state: FableObject, [<InlineIfLambda>] value: Callback<'Type>) =
            ("onSettle" ==> value)
            :: state
        [<CustomOperation "onUpdate">]
        member inline _.onUpdateOp(state: FableObject, [<InlineIfLambda>] value: Callback<'Type>) =
            ("onUpdate" ==> value)
            :: state
        
    type TimerCallbackInjection<'Type> with
        [<CustomOperation "onBegin">]
        member inline _.onBeginOp(state: FableObject, [<InlineIfLambda>] value: Callback<'Type>) =
            "onBegin" ==> value |> add state
        [<CustomOperation "onUpdate">]
        member inline _.onUpdateOp(state: FableObject, [<InlineIfLambda>] value: Callback<'Type>) =
            "onUpdate" ==> value |> add state
        [<CustomOperation "onLoop">]
        member inline _.onLoopOp(state: FableObject, [<InlineIfLambda>] value: Callback<'Type>) =
            "onLoop" ==> value |> add state
        [<CustomOperation "onPause">]
        member inline _.onPauseOp(state: FableObject, [<InlineIfLambda>] value: Callback<'Type>) =
            "onPause" ==> value |> add state
        [<CustomOperation "onComplete">]
        member inline _.onCompleteOp(state: FableObject, [<InlineIfLambda>] value: Callback<'Type>) =
            "onComplete" ==> value |> add state
        [<CustomOperation "andThen">]
        member inline _.andThenOp(state: FableObject, [<InlineIfLambda>] value: Callback<'Type>) =
            "then" ==> value |> add state
    
    type ScrollObserverCallbackInjection<'Type> with
        [<CustomOperation "onEnter">]
        member inline _.onEnterOp(state: FableObject, [<InlineIfLambda>] value: Callback<'Type>) =
            "onEnter" ==> value |> add state
        [<CustomOperation "onEnterForward">]
        member inline _.onEnterForwardOp(state: FableObject, [<InlineIfLambda>] value: Callback<'Type>) =
            "onEnterForward" ==> value |> add state
        [<CustomOperation "onEnterBackward">]
        member inline _.onEnterBackwardOp(state: FableObject, [<InlineIfLambda>] value: Callback<'Type>) =
            "onEnterBackward" ==> value |> add state
        [<CustomOperation "onLeave">]
        member inline _.onLeaveOp(state: FableObject, [<InlineIfLambda>] value: Callback<'Type>) =
            "onLeave" ==> value |> add state
        [<CustomOperation "onLeaveForward">]
        member inline _.onLeaveForwardOp(state: FableObject, [<InlineIfLambda>] value: Callback<'Type>) =
            "onLeaveForward" ==> value |> add state
        [<CustomOperation "onLeaveBackward">]
        member inline _.onLeaveBackwardOp(state: FableObject, [<InlineIfLambda>] value: Callback<'Type>) =
            "onLeaveBackward" ==> value |> add state
        [<CustomOperation "onUpdate">]
        member inline _.onUpdateOp(state: FableObject, [<InlineIfLambda>] value: Callback<'Type>) =
            "onUpdate" ==> value |> add state
        [<CustomOperation "onSyncComplete">]
        member inline _.onSyncCompleteOp(state: FableObject, [<InlineIfLambda>] value: Callback<'Type>) =
            "onSyncComplete" ==> value |> add state
    
    type AnimationCallbackInjection<'Type> with
        [<CustomOperation "onBeforeUpdate">]
        member inline _.onBeforeUpdateOp(state: FableObject, [<InlineIfLambda>] value: Callback<'Type>) =
            "onBeforeUpdate" ==> value |> add state
        [<CustomOperation "onRender">]
        member inline _.onRenderOp(state: FableObject, [<InlineIfLambda>] value: Callback<'Type>) =
            "onRender" ==> value |> add state

    
    type TimerObjectInjection<'Type> with
        member inline this.Yield(_: unit) = this
        member inline this.For(_, action) = action()
        member inline _.Zero() = ()
        [<CustomOperation "play">]
        member inline this.playOp _ = this.play()
        [<CustomOperation "reverse">]
        member inline this.reverseOp _ = this.reverse()
        [<CustomOperation "pause">]
        member inline this.pauseOp _ = this.pause()
        [<CustomOperation "restart">]
        member inline this.restartOp _ = this.restart()
        [<CustomOperation "alternate">]
        member inline this.alternateOp _ = this.alternate()
        [<CustomOperation "resume">]
        member inline this.resumeOp _ = this.resume()
        [<CustomOperation "complete">]
        member inline this.completeOp _ = this.complete()
        [<CustomOperation "cancel">]
        member inline this.cancelOp _ = this.cancel()
        [<CustomOperation "revert">]
        member inline this.revertOp _ = this.revert()
        [<CustomOperation "seek">]
        member inline this.seekOp(_, time: float, ?muteCallbacks: bool) = this.seek(!!time,?muteCallbacks=muteCallbacks)
        [<CustomOperation "stretch">]
        member inline this.stretchOp(_, duration: float) = this.stretch(!!duration)
        member inline this.Run(_) = this
    type TweenPropertyInjection with
        member inline _.Yield(value: Enums.composition): FableObject = [ "composition" ==> value ]
        [<CustomOperation "delay">]
        member inline _.delayOp(state: FableObject, value: float) =
            "delay" ==> value |> add state
        [<CustomOperation "delay">]
        member inline _.delayOp(state: FableObject, [<InlineIfLambda>] value: FunctionValue<float>) =
            "delay" ==> value |> add state
        [<CustomOperation "duration">]
        member inline _.durationOp(state: FableObject, value: float) =
            "duration" ==> value |> add state
        [<CustomOperation "duration">]
        member inline _.durationOp(state: FableObject, [<InlineIfLambda>] value: FunctionValue<float>) =
            "duration" ==> value |> add state
        [<CustomOperation "composition">]
        member inline _.compositionOp(state: FableObject, value: Enums.composition) =
            "composition" ==> value |> add state
        [<CustomOperation "modifier">]
        member inline _.modifierOp(state: FableObject, [<InlineIfLambda>] value: FloatModifier) =
            "modifier" ==> value |> add state
    
    type PlaybackPropertyInjection with
        [<CustomOperation "loop">]
        member inline _.loopOp(state: FableObject, value: int) =
            "loop" ==> value |> add state
        [<CustomOperation "loop">]
        member inline _.loopOp(state: FableObject, [<InlineIfLambda>] value: FunctionValue<int>) =
            "loop" ==> value |> add state
        [<CustomOperation "loop">]
        member inline _.loopOp(state: FableObject, value: bool) =
            "loop" ==> value |> add state
        [<CustomOperation "loop">]
        member inline _.loopOp(state: FableObject) =
            "loop" ==> true |> add state
        [<CustomOperation "loopDelay">]
        member inline _.loopDelayOp(state: FableObject, value: float) =
            "loopDelay" ==> value |> add state
        [<CustomOperation "loopDelay">]
        member inline _.loopDelayOp(state: FableObject, [<InlineIfLambda>] value: FunctionValue<float>) =
            "loopDelay" ==> value |> add state
        [<CustomOperation "alternate">]
        member inline _.alternateOp(state: FableObject, value: bool) =
            "alternate" ==> value |> add state
        [<CustomOperation "alternate">]
        member inline _.alternateOp(state: FableObject) =
            "alternate" ==> true |> add state
        [<CustomOperation "reversed">]
        member inline _.reversedOp(state: FableObject, value: bool) =
            "reversed" ==> value |> add state
        [<CustomOperation "reversed">]
        member inline _.reversedOp(state: FableObject) =
            "reversed" ==> true |> add state
        [<CustomOperation "autoplay">]
        member inline _.autoplayOp(state: FableObject) =
            "autoplay" ==> true |> add state
        [<CustomOperation "autoplay">]
        member inline _.autoplayOp(state: FableObject, value: bool) =
            "autoplay" ==> value |> add state
        [<CustomOperation "autoplay">]
        member inline _.autoplayOp(state: FableObject, value: ScrollObserver) =
            "autoplay" ==> value |> add state
        [<CustomOperation "frameRate">]
        member inline _.frameRate(state: FableObject, value: int) =
            "frameRate" ==> value |> add state
        [<CustomOperation "playbackRate">]
        member inline _.playbackRate(state: FableObject, value: float) =
            "playbackRate" ==> value |> add state
        [<CustomOperation "playbackEase">]
        member inline _.playbackEase(state: FableObject, [<InlineIfLambda>] value: FloatModifier) =
            "playbackEase" ==> value |> add state
    type Timeline with
        member inline this.Yield(_: unit) = ()
        member inline this.Zero() = ()
        [<CustomOperation "add">]
        member inline this.AddOp(_, target: obj, options: obj) =
            this.add2(target, unbox (options)) |> ignore
        [<CustomOperation "add">]
        member inline this.AddOp(_, target: obj, options: obj, value: RelativeTimePosition) =
            this.add3(target, unbox (options), !!value) |> ignore
        [<CustomOperation "add">]
        member inline this.AddOp(_, target: obj, options: obj, value: FunctionValue<_>) =
            this.add3(target, unbox (options), !!value) |> ignore
        [<CustomOperation "init">]
        member inline this.initOp _ = this.init() |> ignore
        [<CustomOperation "refresh">]
        member inline this.refreshOp _ = this.refresh() |> ignore
        [<CustomOperation "set">]
        member inline this.setOp(_, targets: obj, properties: obj, position: RelativeTimePosition) = this.set(!!targets,properties,position = !!position) |> ignore
        [<CustomOperation "set">]
        member inline this.setOp(_, targets: obj, properties: obj) = this.set(!!targets,properties) |> ignore
        [<CustomOperation "label">]
        member inline this.label(_, label: string, ?position: RelativeTimePosition) = this.label(label, ?position = position) |> ignore
        member inline this.Run _ = this
    type stagger with
        member inline _.Yield(value: staggerFrom) = [ "from" ==> value ]
        [<CustomOperation "start">]
        member inline _.startOp(state: FableObject, value: float) = "start" ==> value |> add state
        [<CustomOperation "start">]
        member inline _.startOp(state: FableObject, value: RelativeTimePosition) = "start" ==> value |> add state
        [<CustomOperation "from">]
        member inline _.fromOp(state: FableObject, value: int) = "from" ==> value |> add state
        [<CustomOperation "from">]
        member inline _.fromOp(state: FableObject, value: staggerFrom) = "from" ==> value |> add state
        [<CustomOperation "reversed">]
        member inline _.reversedOp(state: FableObject, ?value: bool) = "reversed" ==> (value |> Option.defaultValue true) |> add state
        [<CustomOperation "grid">]
        member inline _.gridOp(state: FableObject, value1: float, value2: float) = "grid" ==> [|value1;value2|] |> add state
        [<CustomOperation "axis">]
        member inline _.axisOp(state: FableObject, value: Enums.axis) = "axis" ==> value |> add state 
        [<CustomOperation "modifier">]
        member inline _.modifierOp(state: FableObject, value: FloatModifier) = "modifier" ==> value |> add state 
        [<CustomOperation "modifier">]
        member inline _.modifierOp(state: FableObject, value: float -> string) = "modifier" ==> value |> add state
        [<CustomOperation "use'">]
        member inline _.useOp(state: FableObject, value: string) =
            "use" ==> value |> add state
        [<CustomOperation "total">]
        member inline _.totalOp(state: FableObject, value: int) =
            "total" ==> value |> add state
        member inline this.Run(state: FableObject) = AnimeJs.stagger(this,createObj state)

    type EasePropertyInjection with
        member inline _.Yield(value: EasingFun) = [("ease" ==> value)]
        [<CustomOperation "ease">]
        member inline _.easeOp(state: FableObject, value: float -> float) =
            "ease" ==> value |> add state
        [<CustomOperation "ease">]
        member inline _.easeOp(state: FableObject, value: EasingFun) =
            "ease" ==> value |> add state
        // [<CompilerMessage("You can directly yield an EasingFun delegate and it will be transformed into a 'ease' key value pair. The 'ease' operation is not required",0)>]
        // [<CustomOperation "ease">]
        // member inline _.easeOp(state: FableObject, value: EasingFun) =
        //     "ease" ==> value |> add state

    type BoundsBuilder with
        [<CustomOperation "top">]
        member inline _.TopOp(_object_builder, _property_value: float) = ("top" ==> _property_value) :: _object_builder
        [<CustomOperation "bottom">]
        member inline _.BottomOp(_object_builder, _property_value: float) = ("bottom" ==> _property_value) :: _object_builder
        [<CustomOperation "left">]
        member inline _.LeftOp(_object_builder, _property_value: float) = ("left" ==> _property_value) :: _object_builder
        [<CustomOperation "right">]
        member inline _.RightOp(_object_builder, _property_value: float) = ("right" ==> _property_value) :: _object_builder
        member inline _.Run(_object_builder_run: FableObject): Bounds =
            unbox (_object_builder_run |> createObj)
    
    type SpringBuilder with
        [<CustomOperation "mass">]
        member inline _.MassOp(_object_builder, _property_value: float) = _object_builder @ [("mass" ==> _property_value)]
        [<CustomOperation "stiffness">]
        member inline _.StiffnessOp(_object_builder, _property_value: float) = _object_builder @ [("stiffness" ==> _property_value)]
        [<CustomOperation "damping">]
        member inline _.DampingOp(_object_builder, _property_value: float) = _object_builder @ [("damping" ==> _property_value)]
        [<CustomOperation "velocity">]
        member inline _.VelocityOp(_object_builder, _property_value: float) = _object_builder @ [("velocity" ==> _property_value)]
        member inline _.Run(_object_builder_run: FableObject): EasingFun =
            Eases.createSpring(_object_builder_run |> createObj)
    
    type KeyframePercentValue with
        member inline _.Yield(_: unit): FableObject = []
        member inline _.Yield(value: string * obj) = value
        member inline _.Yield(object: FableObject) = object
        member inline _.Combine(_: unit, value: string * obj) =
            [ value ]
        member inline _.Combine(value: string * obj, _: unit) =
            [ value ]
        member inline _.Combine(_: unit, value: FableObject) = value
        member inline _.Combine(value: FableObject, _: unit) = value
        member inline _.Combine(left: string * obj, right: string * obj) =
            [ left; right ]
        member inline _.Combine(left: FableObject, right: string * obj) =
            right :: left
        member inline _.Combine(left: string * obj, right: FableObject) =
            left :: right
        member inline _.Combine(left: FableObject, right: FableObject) =
            left @ right
        member inline _.Delay([<InlineIfLambda>] value) = value()
        member inline _.For(state: FableObject, [<InlineIfLambda>] value: string * obj -> FableObject) =
            state |> List.collect value
        member inline _.For(state: FableObject, [<InlineIfLambda>] value: unit -> FableObject) =
            state @ value()
        member inline _.For(state: FableObject, [<InlineIfLambda>] value: unit -> string * obj) =
            value() :: state
        member inline _.For(state, [<InlineIfLambda>] value) =
            state |> value
        member inline _.Yield(value: StyleValue) = [unbox<string * obj> value]
        member inline _.Yield([<InlineIfLambda>] value: EasingFun) = [ "ease" ==> value ]
        member inline _.Yield([<InlineIfLambda>] value: FloatModifier) = [ "ease" ==> value ]
        member inline this.Run(state: FableObject): PercentKeyframe = !!(!!this ==> createObj state)
    type AnimationBuilder with
        member inline _.Yield(value: StyleValue) = [unbox<string * obj> value]
        member inline _.Yield(value: StyleValueFunction) = [ unbox<string * obj> value ]
        member inline _.Yield(value: StyleObj) = [ unbox<string * obj> value ]
        member inline _.Yield(value: #StyleAnimationObj) = [ unbox<string * obj> value ]
        member inline _.Yield(value: StyleArray) = [ unbox<string * obj> value ]
        member inline _.Run(state: FableObject) =
            createObj state |> unbox<AnimationOptions>
    type Bindings.Types.AnimationOptions with
        member inline _.Yield(_: unit) = ()
        member inline _.Yield(value: obj) = Target value
        member inline _.Yield(value: obj list) = targets value
        member inline _.Yield(value: Targets) = value
        member inline _.Delay(value) = value()
        member inline _.Combine(x) = fun () -> x
        member inline this.Run(state: Target<_>) =
            AnimeJs.animate(!!state, this)
        member inline this.Run(state: Targets) =
            AnimeJs.animate(state, this)
    type CssStyle with
        member inline _.Yield(value: stagger): StyleValueFunction = !!AnimeJs.stagger(value)
        member inline _.Yield(value: string): StyleValue = !!value
        member inline _.Yield(value: float): StyleValue = !!value
        member inline _.Yield(value: ITuple): StyleValue = !!value
        member inline _.Yield(value: RelativeTweenValue): StyleValue = !!value
        member inline _.Yield([<InlineIfLambda>] value: FunctionValue<_>): StyleValueFunction = !!value
        member inline _.Combine(left: string * obj, right: string * obj): FableObject =
            [left;right]
        member inline _.Combine(left: FableObject, right: string * obj): FableObject =
            right |> add left
        member inline _.Combine(left: string * obj, right: FableObject): FableObject =
            left :: right
        member inline _.Combine(left: FableObject, right: FableObject): FableObject =
            left @ right
        member inline _.Combine(left: FableObject, right: ^T when ^T :> StyleObj) =
            (left @ !!right) |> unbox<^T>
        member inline _.Combine(left: ^T when ^T :> StyleObj, right: FableObject) =
            (!!left @ right) |> unbox<^T>
        member inline _.Combine(left: ^T when ^T :> StyleObj, right: string * obj) =
            right |> add (unbox<FableObject> left) |> unbox<^T>
        member inline _.Combine(_:unit, right: ^T when ^T :> StyleObj) = right
        member inline _.Combine(left: ^T when ^T :> StyleObj, _: unit) = left
        member inline _.Combine(left: string * obj, right: ^T when ^T :> StyleObj) =
            left :: (unbox<FableObject> right) |> unbox<^T>
        member inline _.For(left: ^T when ^T :> StyleObj, right: unit -> FableObject): ^T =
            !!(!!left @ !!right())
        [<CustomOperation "too">]
        member inline _.toOp(state: FableObject, value: float) =
            "to" ==> value |> add state |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: FableObject, value: float, value2: float) =
            "to" ==> [| value; value2 |] |> add state |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: FableObject, value: string) =
            "to" ==> value |> add state |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: FableObject, value: string, value2: string) =
            "to" ==> [| value; value2 |] |> add state |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: FableObject, value: float, value2: string) =
            "to" ==> (value, value2) |> add state |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: FableObject, value: string, value2: float) =
            "to" ==> (value, value2) |> add state |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: FableObject, value: RelativeTweenValue) =
            "to" ==> value |> add state |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: FableObject, [<InlineIfLambda>] value: FunctionValue<_>) =
            "to" ==> value |> add state |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: FableObject, value: RelativeTweenValue, value2: RelativeTweenValue) =
            "to" ==> [| value; value2 |] |> add state |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: FableObject, [<InlineIfLambda>] value: FunctionValue<_>, [<InlineIfLambda>] value2: FunctionValue<_>) =
            "to" ==> [| value; value2 |] |> add state |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: FableObject, value: float, [<InlineIfLambda>] value2: FunctionValue<_>) =
            "to" ==> (value, value2) |> add state |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: FableObject, [<InlineIfLambda>] value: FunctionValue<_>, value2: float) =
            "to" ==> (value, value2) |> add state |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: FableObject, value: RelativeTweenValue, [<InlineIfLambda>] value2: FunctionValue<_>) =
            "to" ==> (value, value2) |> add state |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: FableObject, [<InlineIfLambda>] value: FunctionValue<_>, value2: RelativeTweenValue) =
            "to" ==> (value, value2) |> add state |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: FableObject, value: RelativeTweenValue, value2: string) =
            "to" ==> (value, value2) |> add state |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: FableObject, value: string, value2: RelativeTweenValue) =
            "to" ==> (value, value2) |> add state |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: FableObject, value: RelativeTweenValue, value2: float) =
            "to" ==> (value, value2) |> add state |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: FableObject, value: float, value2: RelativeTweenValue) =
            "to" ==> (value, value2) |> add state |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: FableObject, [<InlineIfLambda>] value: FunctionValue<_>, value2: string) =
            "to" ==> (value, value2) |> add state |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: FableObject, value: string, [<InlineIfLambda>] value2: FunctionValue<_>) =
            "to" ==> (value, value2) |> add state |> unbox<StyleToObj>
        [<CustomOperation "from">]
        member inline _.fromOp(state: FableObject, value: float) =
            "from" ==> value |> add state |> unbox<StyleFromObj>
        [<CustomOperation "from">]
        member inline _.fromOp(state: FableObject, value: string) =
            "from" ==> value |> add state |> unbox<StyleFromObj>
        [<CustomOperation "from">]
        member inline _.fromOp(state: FableObject, [<InlineIfLambda>] value: FunctionValue<_>) =
            "from" ==> value |> add state |> unbox<StyleFromObj>
        [<CustomOperation "from">]
        member inline _.fromOp(state: FableObject, value: RelativeTweenValue) =
            "from" ==> value |> add state |> unbox<StyleFromObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: ^T when ^T :> StyleAnimationObj, value: float) =
            "to" ==> value |> add (unbox<FableObject> state) |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: ^T when ^T :> StyleAnimationObj, value: float, value2: float) =
            "to" ==> [| value; value2 |] |> add (unbox<FableObject> state) |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: ^T when ^T :> StyleAnimationObj, value: string) =
            "to" ==> value |> add (unbox<FableObject> state) |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: ^T when ^T :> StyleAnimationObj, value: string, value2: string) =
            "to" ==> [| value; value2 |] |> add (unbox<FableObject> state) |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: ^T when ^T :> StyleAnimationObj, value: float, value2: string) =
            "to" ==> (value, value2) |> add (unbox<FableObject> state) |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: ^T when ^T :> StyleAnimationObj, value: string, value2: float) =
            "to" ==> (value, value2) |> add (unbox<FableObject> state) |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: ^T when ^T :> StyleAnimationObj, value: RelativeTweenValue) =
            "to" ==> value |> add (unbox<FableObject> state) |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: ^T when ^T :> StyleAnimationObj, [<InlineIfLambda>] value: FunctionValue<_>) =
            "to" ==> value |> add (unbox<FableObject> state) |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: ^T when ^T :> StyleAnimationObj, value: RelativeTweenValue, value2: RelativeTweenValue) =
            "to" ==> [| value; value2 |] |> add (unbox<FableObject> state) |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: ^T when ^T :> StyleAnimationObj, [<InlineIfLambda>] value: FunctionValue<_>, [<InlineIfLambda>] value2: FunctionValue<_>) =
            "to" ==> [| value; value2 |] |> add (unbox<FableObject> state) |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: ^T when ^T :> StyleAnimationObj, value: float, [<InlineIfLambda>] value2: FunctionValue<_>) =
            "to" ==> (value, value2) |> add (unbox<FableObject> state) |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: ^T when ^T :> StyleAnimationObj, [<InlineIfLambda>] value: FunctionValue<_>, value2: float) =
            "to" ==> (value, value2) |> add (unbox<FableObject> state) |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: ^T when ^T :> StyleAnimationObj, value: RelativeTweenValue, [<InlineIfLambda>] value2: FunctionValue<_>) =
            "to" ==> (value, value2) |> add (unbox<FableObject> state) |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: ^T when ^T :> StyleAnimationObj, [<InlineIfLambda>] value: FunctionValue<_>, value2: RelativeTweenValue) =
            "to" ==> (value, value2) |> add (unbox<FableObject> state) |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: ^T when ^T :> StyleAnimationObj, value: RelativeTweenValue, value2: string) =
            "to" ==> (value, value2) |> add (unbox<FableObject> state) |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: ^T when ^T :> StyleAnimationObj, value: string, value2: RelativeTweenValue) =
            "to" ==> (value, value2) |> add (unbox<FableObject> state) |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: ^T when ^T :> StyleAnimationObj, value: RelativeTweenValue, value2: float) =
            "to" ==> (value, value2) |> add (unbox<FableObject> state) |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: ^T when ^T :> StyleAnimationObj, value: float, value2: RelativeTweenValue) =
            "to" ==> (value, value2) |> add (unbox<FableObject> state) |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: ^T when ^T :> StyleAnimationObj, [<InlineIfLambda>] value: FunctionValue<_>, value2: string) =
            "to" ==> (value, value2) |> add (unbox<FableObject> state) |> unbox<StyleToObj>
        [<CustomOperation "too">]
        member inline _.toOp(state: ^T when ^T :> StyleAnimationObj, value: string, [<InlineIfLambda>] value2: FunctionValue<_>) =
            "to" ==> (value, value2) |> add (unbox<FableObject> state) |> unbox<StyleToObj>
        [<CustomOperation "from">]
        member inline _.fromOp(state: ^T when ^T :> StyleAnimationObj, value: float) =
            "from" ==> value |> add (unbox<FableObject> state) |> unbox<StyleFromObj>
        [<CustomOperation "from">]
        member inline _.fromOp(state: ^T when ^T :> StyleAnimationObj, value: string) =
            "from" ==> value |> add (unbox<FableObject> state) |> unbox<StyleFromObj>
        [<CustomOperation "from">]
        member inline _.fromOp(state: ^T when ^T :> StyleAnimationObj, [<InlineIfLambda>] value: FunctionValue<_>) =
            "from" ==> value |> add (unbox<FableObject> state) |> unbox<StyleFromObj>
        [<CustomOperation "from">]
        member inline _.fromOp(state: ^T when ^T :> StyleAnimationObj, value: RelativeTweenValue) =
            "from" ==> value |> add (unbox<FableObject> state) |> unbox<StyleFromObj>
        [<CustomOperation "delay"; CompilerMessage("A style object value cannot have raw values. Place them in a `too` or `from` field.", 10_001, IsError=true)>]
        member inline _.delayOp(state: StyleValue, value) = 
            ["delay" ==> value; "to" ==> state] |> unbox<FableObject>
        [<CustomOperation "delay">]
        member inline _.delayOp(state: ^T when ^T :> StyleAnimationObj, value: float) =
            "delay" ==> value |> add (unbox<FableObject> state) |> unbox<^T>
        [<CustomOperation "delay">]
        member inline _.delayOp(state: ^T when ^T :> StyleAnimationObj, [<InlineIfLambda>] value: FunctionValue<float>) =
            "delay" ==> value |> add (unbox<FableObject> state) |> unbox<^T>
        [<CustomOperation "delay">]
        member inline _.delayOp(state: StyleObj, value: float) =
            "delay" ==> value |> add (unbox<FableObject> state) |> unbox<StyleAnimationObj>
        [<CustomOperation "delay">]
        member inline _.delayOp(state: StyleObj, [<InlineIfLambda>] value: FunctionValue<float>) =
            "delay" ==> value |> add (unbox<FableObject> state) |> unbox<StyleAnimationObj>
        [<CustomOperation "delay">]
        member inline _.delayOp(state: FableObject, value: float): StyleAnimationObj =
            "delay" ==> value |> add (unbox<FableObject> state) |> unbox
        [<CustomOperation "delay">]
        member inline _.delayOp(state: FableObject, [<InlineIfLambda>] value: FunctionValue<float>): StyleAnimationObj =
            "delay" ==> value |> add (unbox<FableObject> state) |> unbox
        [<CustomOperation "duration"; CompilerMessage("A style object value cannot have raw values. Place them in a `too` or `from` field.", 10_001, IsError=true)>]
        member inline _.durationOp(state: StyleValue, value) =
            ["duration" ==> value; "to" ==> state] |> unbox<FableObject>
        [<CustomOperation "duration">]
        member inline _.durationOp(state: ^T when ^T :> StyleObj, value: float) =
            "duration" ==> value |> add (unbox<FableObject> state) |> unbox<^T>
        [<CustomOperation "duration">]
        member inline _.durationOp(state: ^T when ^T :> StyleObj, [<InlineIfLambda>] value: FunctionValue<float>) =
            "duration" ==> value |> add (unbox<FableObject> state) |> unbox<^T>
        [<CustomOperation "duration">]
        member inline _.durationOp(state: FableObject, value: float) =
            "duration" ==> value |> add (unbox<FableObject> state)
        [<CustomOperation "duration">]
        member inline _.durationOp(state: FableObject, [<InlineIfLambda>] value: FunctionValue<float>) =
            "duration" ==> value |> add (unbox<FableObject> state)
        [<CustomOperation "composition"; CompilerMessage("A style object value cannot have raw values. Place them in a `too` or `from` field.", 10_001, IsError=true)>]
        member inline _.compositionOp(state: StyleValue, value) =
            ["composition" ==> value; "to" ==> state] |> unbox<FableObject>
        [<CustomOperation "composition">]
        member inline _.compositionOp(state: ^T when ^T :> StyleAnimationObj, value: Enums.composition) =
            "composition" ==> value |> add (unbox<FableObject> state) |> unbox<^T>
        [<CustomOperation "composition">]
        member inline _.compositionOp(state: FableObject, value: Enums.composition): StyleAnimationObj =
            "composition" ==> value |> add (unbox<FableObject> state) |> unbox
        [<CustomOperation "modifier"; CompilerMessage("A style object value cannot have raw values. Place them in a `too` or `from` field.", 10_001, IsError=true)>]
        member inline _.modifierOp(state: StyleValue, value: FloatModifier) =
            ["modifier" ==> value; "to" ==> state] |> unbox<FableObject>
        [<CustomOperation "modifier">]
        member inline _.modifierOp(state: ^T, [<InlineIfLambda>] value: FloatModifier) =
            "modifier" ==> value |> add (unbox<FableObject> state) |> unbox<^T>
        [<CustomOperation "ease"; CompilerMessage("A style object value cannot have raw values. Place them in a `too` or `from` field.", 10_001, IsError=true)>]
        member inline _.easeOp(state: StyleValue, value: EasingFun) =
            ["ease" ==> value; "to" ==> state] |> unbox<FableObject>
        [<CustomOperation "ease">]
        member inline _.easeOp(state: ^T, value: EasingFun) =
            "ease" ==> value |> add (unbox<FableObject> state) |> unbox<^T>
        [<CustomOperation "ease">]
        member inline _.easeOp(state: ^T, [<InlineIfLambda>] value: FloatModifier) =
            "ease" ==> value |> add (unbox<FableObject> state) |> unbox<^T>
        [<CustomOperation "unit">]
        member inline _.unitOp(state: FableObject, value: string): StyleAnimatableObj =
            "unit" ==> value |> add state |> unbox
    type ICssStyle with
        member inline this.Value = unbox<string> this
        member inline this.Run(value: StyleValue): string * obj = !!this ==> value
        member inline this.Run(value: StyleValueFunction): string * obj = !!this ==> value
        member inline this.Run(value: FableObject): StyleObj =
            !!(!!this ==> createObj value)
        // member inline this.Run(value: StyleAnimatableObj): StyleAnimatableObj =
        //     !!(!!this ==> createObj !!value)
        member inline this.Run(value: ^T when ^T :> StyleObj): ^T =
            !!(!!this ==> createObj !!value)
        // member inline this.Run(value: StyleAnimationObj): StyleAnimationObj =
        //     !!(!!this ==> createObj !!value)
        // member inline this.Run(value: StyleToObj): StyleToObj =
        //     !!(!!this ==> createObj !!value)
        // member inline this.Run(value: StyleFromObj): StyleFromObj =
        //     !!(!!this ==> createObj !!value)
        member inline this.Run(value: StyleValueList): StyleValueList =
            !!(!!this ==> (!!value |> List.toArray))

    type KeyframeBuilder with
        member inline _.Yield(value: StyleValue): string * obj = !!value
        member inline _.Yield(value: StyleObj): string * obj = !!value
        member inline _.Yield(value: StyleValueFunction): string * obj = !!value
        member inline _.Yield(value: StyleValueList): string * obj = !!value
        member inline _.Run(value: FableObject): KeyframeValue =
            !!createObj value

    type CursorBuilder with
        member inline _.Yield(_: unit): unit = ()
        member inline _.Yield(value: string * obj): string * obj = value
        member inline _.Yield(value: bool) = value
        member inline _.Combine(value: string * obj, value2: string * obj): (string * obj) * (string * obj) = value,value2
        member inline _.Delay(value: unit -> (string * obj)) = value()
        member inline _.Delay(value: unit -> bool) = value()
        member inline _.Delay(value: unit -> ((string * obj) * (string * obj))) = value()
        member inline _.Combine(value: string * obj): string * obj = value
        /// <summary>
        /// <c>onHover</c><br/>
        /// <c>bool -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>onHover</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>string | bool</c></p>
        /// </remarks>
        /// <returns><c>string * obj</c></returns>
        [<CustomOperation "onHover">]
        member inline _.OnHoverOp(_object_builder: unit, value: bool) = "onHover" ==> value
        /// <summary>
        /// <c>onHover</c><br/>
        /// <c>bool -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>onHover</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>string | bool</c></p>
        /// </remarks>
        /// <returns><c>string * obj</c></returns>
        [<CustomOperation "onHover">]
        member inline _.OnHoverOp(_object_builder: string * obj, value: bool) = _object_builder,("onHover" ==> value)
        /// <summary>
        /// <c>onGrab</c><br/>
        /// <c>bool -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>onGrab</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>string | bool</c></p>
        /// </remarks>
        /// <returns><c>string * obj</c></returns>
        [<CustomOperation "onGrab">]
        member inline _.OnGrabOp(_object_builder: unit, value: bool) = "onGrab" ==> value
        /// <summary>
        /// <c>onGrab</c><br/>
        /// <c>bool -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>onGrab</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>string | bool</c></p>
        /// </remarks>
        /// <returns><c>string * obj</c></returns>
        [<CustomOperation "onGrab">]
        member inline _.OnGrabOp(_object_builder: string * obj, value: bool) = _object_builder,("onGrab" ==> value)
        /// <summary>
        /// <c>onHover</c><br/>
        /// <c>string -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>onHover</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>string | bool</c></p>
        /// </remarks>
        /// <returns><c>string * obj</c></returns>
        [<CustomOperation "onHover">]
        member inline _.OnHoverOp(_object_builder: unit, value: string) = "onHover" ==> value
        /// <summary>
        /// <c>onHover</c><br/>
        /// <c>string -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>onHover</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>string | bool</c></p>
        /// </remarks>
        /// <returns><c>string * obj</c></returns>
        [<CustomOperation "onHover">]
        member inline _.OnHoverOp(_object_builder: string * obj, value: string) = _object_builder,("onHover" ==> value)
        /// <summary>
        /// <c>onGrab</c><br/>
        /// <c>string -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>onGrab</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>string | bool</c></p>
        /// </remarks>
        /// <returns><c>string * obj</c></returns>
        [<CustomOperation "onGrab">]
        member inline _.OnGrabOp(_object_builder: unit, value: string) = "onGrab" ==> value
        /// <summary>
        /// <c>onGrab</c><br/>
        /// <c>string -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>onGrab</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>string | bool</c></p>
        /// </remarks>
        /// <returns><c>string * obj</c></returns>
        [<CustomOperation "onGrab">]
        member inline _.OnGrabOp(_object_builder: string * obj, value: string) = _object_builder,("onGrab" ==> value)
        member inline _.Run(_object_run: (string * obj)): DraggableCursor = !!([|_object_run|] |> createObj)
        member inline _.Run(_object_run: ((string * obj) * (string * obj))): DraggableCursor = !!([|_object_run |> fst; _object_run |> snd|] |> createObj)
        member inline _.Run(_object_run: bool): DraggableCursor = !! _object_run

    type AnimatableBuilder with
        member inline _.Yield(value: StyleValue): string * obj = !!value
        member inline _.Yield(value: StyleAnimatableObj): string * obj = !!value
        member inline _.Yield(value: StyleObj): string * obj = !!value
        [<CustomOperation "unit">]
        member inline _.unitOp(state: FableObject, value: string) =
            "unit" ==> value |> add state
        [<CustomOperation "duration">]
        member inline _.durationOp(state: FableObject, value: float) =
            "duration" ==> value |> add state
        [<CustomOperation "duration">]
        member inline _.durationOp(state: FableObject, value: FunctionValue<float>) =
            "duration" ==> value |> add state
        [<CustomOperation "modifier">]
        member inline _.modifierOp(state: FableObject, value: FloatModifier) =
            "modifier" ==> value |> add state
        member inline _.Run(state: FableObject): AnimatableOptions =
            !!createObj state
        member inline _.Run(state: ^T when ^T :> StyleAnimatableObj): AnimatableOptions =
            !!createObj state
    type Bindings.Types.Animatable with
        member inline _.Yield(value: unit): unit = ()
        member inline _.Yield(value: string): StyleNoValue = StyleNoValue value
        member inline _.Yield(value: ICssStyle): StyleNoValue = StyleNoValue !!value
        
        member inline _.Yield(value: float): NoStyleValue = !!value
        member inline _.Yield(value: int): NoStyleValue = !!value
        member inline _.Yield(value: float * float): NoStyleValue = !!value
        member inline _.Yield(value: float * float * float): NoStyleValue = !!value
        member inline _.Yield(value: float * float * float * float): NoStyleValue = !!value
        member inline _.Yield(value: float * float * float * float * float): NoStyleValue = !!value
        member inline _.Yield(value: float * float * float * float * float * float): NoStyleValue = !!value
        member inline _.Yield(value: float * float * float * float * float * float * float): NoStyleValue = !!value
        member inline _.Yield(value: float * float * float * float * float * float * float * float): NoStyleValue = !!value
        member inline _.Yield(value: float * float * float * float * float * float * float * float * float): NoStyleValue = !!value
        member inline _.Yield(value: float * float * float * float * float * float * float * float * float * float): NoStyleValue = !!value
        member inline _.Yield(value: float * float * float * float * float * float * float * float * float * float * float): NoStyleValue = !!value
        member inline _.Yield(value: float[]): NoStyleValue = !!value
        member inline _.Yield(value: float list): NoStyleValue = value |> List.toArray |> unbox
        member inline _.Yield(value: EasingFun): NoStyleWithEase = NoStyleWithEase value
        member inline this.Run(value: StyleNoValue): U2<float, float[]> = emitJsExpr (this,value) "$0[$1]()"
        member inline this.Run(StyleWithValue(prop, value)): Animatable = emitJsExpr (this,prop,value) "$0[$1]($2)"
        member inline this.Run(StyleWithValueDuration(prop, value,duration)): Animatable = emitJsExpr (this,prop,value,duration) "$0[$1]($2,$3)"
        member inline this.Run(StyleWithValueEase(prop, value,ease)): Animatable = emitJsExpr (this,prop,value,ease) "$0[$1]($2, easing = $3)"
        member inline this.Run(StyleWithValueDurationEase(prop, value,duration,ease)): Animatable = emitJsExpr (this,prop,value,duration,ease) "$0[$1]($2,$3,$4)"
        member inline _.Delay(value) = value()
        member inline _.Combine(value, _:unit) = value
        member inline _.Combine(_: unit, value) = value
        member inline _.Combine(NoStyleValue(value), ease: EasingFun) = NoStyleValueEase(value, ease)
        member inline _.Combine(NoStyleWithDuration(duration), ease: EasingFun) = NoStyleWithDurationEase(duration, ease)
        member inline _.Combine(NoStyleValueDuration(value,duration), ease: EasingFun) = NoStyleValueDurationEase(value, duration, ease)
        member inline _.Combine(NoStyleValueDurationEase(value,duration,ease), StyleNoValue(style)) = StyleWithValueDurationEase(style,value,duration,ease)
        member inline _.Combine(NoStyleValue(value), StyleNoValue(prop)) = StyleWithValue(prop,value)
        member inline _.Combine(NoStyleValue(value), StyleWithDuration(props,duration)) = StyleWithValueDuration(props,value,duration)
        member inline _.Combine(NoStyleValue(value), StyleWithEase(prop,ease)) = StyleWithValueEase(prop,value,ease)
        member inline _.Combine(NoStyleValue(value), StyleWithDurationEase(prop,duration,ease)) = StyleWithValueDurationEase(prop,value,duration,ease)
        member inline _.Combine(NoStyleValue(value), NoStyleWithDuration(duration)) = NoStyleValueDuration(value,duration)
        member inline _.Combine(NoStyleValue(value), NoStyleWithEase(ease)) = NoStyleValueEase(value,ease)
        member inline _.Combine(NoStyleValue(value), NoStyleWithDurationEase(duration,ease)) = NoStyleValueDurationEase(value,duration,ease)
        member inline _.Combine(NoStyleWithDuration(duration), StyleNoValue(prop)) = StyleWithDuration(prop,duration)
        member inline _.Combine(NoStyleWithDuration(duration), StyleWithValue(prop,value)) = StyleWithValueDuration(prop,value,duration)
        member inline _.Combine(NoStyleWithDuration(duration), StyleWithEase(prop,ease)) = StyleWithDurationEase(prop,duration,ease)
        member inline _.Combine(NoStyleWithDuration(duration), StyleWithValueEase(prop,value,ease)) = StyleWithValueDurationEase(prop,value,duration,ease)
        member inline _.Combine(NoStyleWithDuration(duration), NoStyleValue(value)) = NoStyleValueDuration(value,duration)
        member inline _.Combine(NoStyleWithDuration(duration), NoStyleWithEase(ease)) = NoStyleWithDurationEase(duration,ease)
        member inline _.Combine(NoStyleWithDuration(duration), NoStyleValueEase(value,ease)) = NoStyleValueDurationEase(value,duration,ease)
        member inline _.Combine(NoStyleWithEase(ease), StyleNoValue(prop)) = StyleWithEase(prop,ease)
        member inline _.Combine(NoStyleWithEase(ease), StyleWithValue(prop,value)) = StyleWithValueEase(prop,value,ease)
        member inline _.Combine(NoStyleWithEase(ease), StyleWithValueDuration(prop,value,duration)) = StyleWithValueDurationEase(prop,value,duration,ease)
        member inline _.Combine(NoStyleWithEase(ease), StyleWithDuration(prop,duration)) = StyleWithDurationEase(prop,duration,ease)
        member inline _.Combine(NoStyleWithEase(ease), NoStyleValue(value)) = NoStyleValueEase(value,ease)
        member inline _.Combine(NoStyleWithEase(ease), NoStyleWithDuration(duration)) = NoStyleWithDurationEase(duration,ease)
        member inline _.Combine(NoStyleWithEase(ease), NoStyleValueDuration(value,duration)) = NoStyleValueDurationEase(value,duration,ease)
        member inline _.Combine(NoStyleWithDurationEase(duration,ease), StyleNoValue(prop)) = StyleWithDurationEase(prop,duration,ease)
        member inline _.Combine(NoStyleWithDurationEase(duration,ease), StyleWithValue(prop,value)) = StyleWithValueDurationEase(prop,value,duration,ease)
        member inline _.Combine(NoStyleWithDurationEase(duration,ease), NoStyleValue(value)) = NoStyleValueDurationEase(value,duration,ease)
        member inline _.Combine(StyleNoValue(prop), ease: EasingFun) = StyleWithEase(prop,ease)
        member inline _.Combine(StyleWithValue(prop,value), ease: EasingFun) = StyleWithValueEase(prop,value,ease)
        member inline _.Combine(StyleWithValueDuration(prop,value,duration), ease: EasingFun) = StyleWithValueDurationEase(prop,value,duration,ease)
        member inline _.Combine(StyleWithDuration(prop,duration), ease: EasingFun) = StyleWithDurationEase(prop,duration,ease)
        member inline _.Combine(StyleNoValue(prop), NoStyleValue(value)) = StyleWithValue(prop,value)
        member inline _.Combine(StyleNoValue(prop), NoStyleWithDuration(duration)) = StyleWithDuration(prop,duration)
        member inline _.Combine(StyleNoValue(prop), NoStyleWithEase(ease)) = StyleWithEase(prop,ease)
        member inline _.Combine(StyleNoValue(prop), NoStyleValueDuration(value,duration)) = StyleWithValueDuration(prop,value,duration)
        member inline _.Combine(StyleNoValue(prop), NoStyleValueEase(value,ease)) = StyleWithValueEase(prop,value,ease)
        member inline _.Combine(StyleNoValue(prop), NoStyleValueDurationEase(value,duration,ease)) = StyleWithValueDurationEase(prop,value,duration,ease)
        member inline _.Combine(StyleWithValue(prop,value), NoStyleWithDuration(duration)) = StyleWithValueDuration(prop,value,duration)
        member inline _.Combine(StyleWithValue(prop,value), NoStyleWithEase(ease)) = StyleWithValueEase(prop,value,ease)
        member inline _.Combine(StyleWithValue(prop,value), NoStyleWithDurationEase(duration,ease)) = StyleWithValueDurationEase(prop,value,duration,ease)
        member inline _.Combine(StyleWithDuration(prop,duration), NoStyleValue(value)) = StyleWithValueDuration(prop,value,duration)
        member inline _.Combine(StyleWithDuration(prop,duration), NoStyleWithEase(ease)) = StyleWithDurationEase(prop,duration,ease)
        member inline _.Combine(StyleWithDuration(prop,duration), NoStyleValueEase(value,ease)) = StyleWithValueDurationEase(prop,value,duration,ease)
        member inline _.Combine(StyleWithEase(prop,ease), NoStyleValue(value)) = StyleWithValueEase(prop,value,ease)
        member inline _.Combine(StyleWithEase(prop,ease), NoStyleWithDuration(duration)) = StyleWithDurationEase(prop,duration,ease)
        member inline _.Combine(StyleWithEase(prop,ease), NoStyleValueDuration(value,duration)) = StyleWithValueDurationEase(prop,value,duration,ease)
        member inline _.Combine(StyleWithDurationEase(prop,duration,ease), NoStyleValue(value)) = StyleWithValueDurationEase(prop,value,duration,ease)
        member inline _.Combine(StyleWithValueDuration(prop,value,duration), NoStyleWithEase(ease)) = StyleWithValueDurationEase(prop,value,duration,ease)
        member inline _.Combine(StyleWithValueEase(prop,value,ease), NoStyleWithDuration(duration)) = StyleWithValueDurationEase(prop,value,duration,ease)
        [<CustomOperation "duration">]
        member inline _.durationOp(StyleNoValue(prop), value: float) = StyleWithDuration(prop,value)
        [<CustomOperation "duration">]
        member inline _.durationOp(StyleWithValue(prop,value), duration: float) = StyleWithValueDuration(prop,value,duration)
        [<CustomOperation "duration">]
        member inline _.durationOp(StyleWithValueEase(prop,value,ease), duration: float) = StyleWithValueDurationEase(prop,value,duration,ease)
        [<CustomOperation "duration">]
        member inline _.durationOp(StyleWithEase(prop,ease), duration: float) = StyleWithDurationEase(prop,duration,ease)
        [<CustomOperation "duration">]
        member inline _.durationOp(NoStyleValue(value), duration: float) = NoStyleValueDuration(value,duration)
        [<CustomOperation "duration">]
        member inline _.durationOp(NoStyleWithEase(ease), duration: float) = NoStyleWithDurationEase(duration,ease)
        [<CustomOperation "duration">]
        member inline _.durationOp(NoStyleValueEase(value,ease), duration: float) = NoStyleValueDurationEase(value,duration,ease)
        [<CustomOperation "ease">]
        member inline _.easeOp(StyleNoValue(prop), value: EasingFun) = StyleWithEase(prop,value)
        [<CustomOperation "ease">]
        member inline _.easeOp(StyleWithValue(prop,value), ease: EasingFun) = StyleWithValueEase(prop,value,ease)
        [<CustomOperation "ease">]
        member inline _.easeOp(StyleWithValueDuration(prop,value,duration), ease: EasingFun) = StyleWithValueDurationEase(prop,value,duration,ease)
        [<CustomOperation "ease">]
        member inline _.easeOp(StyleWithDuration(prop,duration), ease: EasingFun) = StyleWithDurationEase(prop,duration,ease)
        [<CustomOperation "ease">]
        member inline _.easeOp(NoStyleValue(value), ease: EasingFun) = NoStyleValueEase(value,ease)
        [<CustomOperation "ease">]
        member inline _.easeOp(NoStyleWithDuration(duration), ease: EasingFun) = NoStyleWithDurationEase(duration,ease)
        [<CustomOperation "ease">]
        member inline _.easeOp(NoStyleValueDuration(value,duration), ease: EasingFun) = NoStyleValueDurationEase(value,duration,ease)
        [<CustomOperation "ease">]
        member inline _.easeOp(_:unit, value: EasingFun) = NoStyleWithEase value
        member inline _.For(value: ^T, [<InlineIfLambda>] f: ^T -> 'T2): 'T2 = value |> f
        member inline _.For(StyleNoValue(prop), [<InlineIfLambda>] f: unit -> NoStyleWithEase) = StyleWithEase(prop,!!f()) 
        member inline _.For(StyleWithValue(prop,value), [<InlineIfLambda>] f: unit -> NoStyleWithEase) = StyleWithValueEase(prop,value,!!f())
        member inline _.For(StyleWithValueDuration(prop,value,duration), [<InlineIfLambda>] f: unit -> NoStyleWithEase) = StyleWithValueDurationEase(prop,value,duration,!!f())
        member inline _.For(StyleWithDuration(prop,duration), [<InlineIfLambda>] f: unit -> NoStyleWithEase) = StyleWithDurationEase(prop,duration,!!f())
        member inline _.For(NoStyleValue(value), [<InlineIfLambda>] f: unit -> NoStyleWithEase) = NoStyleValueEase(value,!!f())
        member inline _.For(NoStyleWithDuration(duration), [<InlineIfLambda>] f: unit -> NoStyleWithEase) = NoStyleWithDurationEase(duration,!!f())
        member inline _.For(NoStyleValueDuration(value,duration), [<InlineIfLambda>] f: unit -> NoStyleWithEase) = NoStyleValueDurationEase(value,duration,!!f())
        
        member inline this.For(value: StyleNoValue, [<InlineIfLambda>] f: unit -> NoStyleValue) =
            this.Combine(value,f())
        
        member inline _.Run(state: FableObject): AnimatableOptions = createObj state |> unbox
    type Bindings.Types.AnimatableOptions with
        member inline _.Yield(_: unit) = ()
        member inline _.Yield(value: obj) = Target value
        member inline _.Yield(value: obj list) = targets !!value
        member inline _.Yield(value: Targets) = value
        member inline _.Delay(value) = value()
        member inline _.Combine(value) = value()
        member inline this.Run(state: Target<_>) =
            AnimeJs.createAnimatable(!!state, this)
        member inline this.Run(state: Targets) =
            AnimeJs.createAnimatable(!!state, this)

    type DraggableBuilder with
        member inline _.Yield(value: DraggableCursor) = "cursor" ==> value
        [<CustomOperation "trigger">]
        member inline _.triggerOp(state: FableObject, value: obj) = "trigger" ==> value |> add state
        /// <summary>
        /// <c>cursor</c><br/>
        /// <c>bool -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>cursor</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>bool</c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        
        [<CustomOperation "cursor">]
        member inline _.CursorOp(_object_builder: FableObject, value: bool) = ("cursor" ==> value) :: _object_builder
        /// <summary>
        /// <c>x</c><br/>
        /// <c>bool -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>x</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>bool | FSharp.Axis</c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "x">]
        member inline  _.XOp(_object_builder: FableObject, value: bool) = ("x" ==> value) :: _object_builder
        /// <summary>
        /// <c>y</c><br/>
        /// <c>bool -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>y</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>bool | FSharp.Axis</c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "y">]
        member inline _.YOp(_object_builder: FableObject, value: bool) = ("y" ==> value) :: _object_builder
        /// <summary>
        /// <c>x</c><br/>
        /// <c>FSharp.Axis -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>x</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>bool | FSharp.Axis</c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "x">]
        member inline _.XOp(_object_builder: FableObject, value: AxisOptions) = ("x" ==> value) :: _object_builder
        /// <summary>
        /// <c>y</c><br/>
        /// <c>FSharp.Axis -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>y</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>bool | FSharp.Axis</c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "y">]
        member inline _.YOp(_object_builder: FableObject, value: AxisOptions) = ("y" ==> value) :: _object_builder
        /// <summary>
        /// <c>container</c><br/>
        /// <c>Selector -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>container</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>Selector | string | #HTMLElement | Bounds | FunctionValue&lt;Bounds></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "container">]
        member inline _.ContainerOp(_object_builder: FableObject, _object_value: Selector) = ("container" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>container</c><br/>
        /// <c> string -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>container</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>Selector | string | #HTMLElement | Bounds | FunctionValue&lt;Bounds></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "container">]
        member inline _.ContainerOp(_object_builder: FableObject, _object_value: string) = ("container" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>container</c><br/>
        /// <c>#HTMLElement-> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>container</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>Selector | string | #HTMLElement | Bounds | FunctionValue&lt;Bounds></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "container">]
        member inline _.ContainerOp(_object_builder: FableObject, _object_value: #HTMLElement) = ("container" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>container</c><br/>
        /// <c>SBounds -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>container</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>Selector | string | #HTMLElement | Bounds | FunctionValue&lt;Bounds></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "container">]
        member inline _.ContainerOp(_object_builder: FableObject, _object_value: Bounds) = ("container" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>container</c><br/>
        /// <c>FunctionValue&lt;Bounds> -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>container</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>Selector | string | #HTMLElement | Bounds | FunctionValue&lt;Bounds></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "container">]
        member inline _.ContainerOp(_object_builder: FableObject, _object_value: FunctionValue<Bounds>) = ("container" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>containerPadding</c><br/>
        /// <c>float -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>containerPadding</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float> | Bounds | FunctionValue&lt;Bounds></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "containerPadding">]
        member inline _.containerPaddingOp(_object_builder: FableObject, _object_value: float) = ("containerPadding" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>containerPadding</c><br/>
        /// <c>Bounds -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>containerPadding</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float> | Bounds | FunctionValue&lt;Bounds></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "containerPadding">]
        member inline _.containerPaddingOp(_object_builder: FableObject, _object_value: Bounds) = ("containerPadding" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>containerPadding</c><br/>
        /// <c>FunctionValue&lt;Bounds> -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>containerPadding</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float> | Bounds | FunctionValue&lt;Bounds></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "containerPadding">]
        member inline _.containerPaddingOp(_object_builder: FableObject, _object_value: FunctionValue<Bounds>) = ("containerPadding" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>containerPadding</c><br/>
        /// <c>FunctionValue&lt;float> -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>containerPadding</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float> | Bounds | FunctionValue&lt;Bounds></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "containerPadding">]
        member inline _.containerPaddingOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("containerPadding" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>containerFriction</c><br/>
        /// <c>float -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>containerFriction</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float</c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "containerFriction">]
        member inline _.containerFrictionOp(_object_builder: FableObject, _object_value: float) = ("containerFriction" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>releaseMass</c><br/>
        /// <c>float -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>releaseMass</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "releaseMass">]
        member inline _.releaseMassOp(_object_builder: FableObject, _object_value: float) = ("releaseMass" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>releaseMass</c><br/>
        /// <c>FunctionValue&lt;float> -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>releaseMass</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "releaseMass">]
        member inline _.releaseMassOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("releaseMass" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>releaseStiffness</c><br/>
        /// <c>float -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>releaseStiffness</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "releaseStiffness">]
        member inline _.releaseStiffnessOp(_object_builder: FableObject, _object_value: float) = ("releaseStiffness" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>releaseStiffness</c><br/>
        /// <c>FunctionValue&lt;float> -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>releaseStiffness</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "releaseStiffness">]
        member inline _.releaseStiffnessOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("releaseStiffness" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>releaseDamping</c><br/>
        /// <c>float -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>releaseDamping</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "releaseDamping">]
        member inline _.releaseDampingOp(_object_builder: FableObject, _object_value: float) = ("releaseDamping" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>releaseDamping</c><br/>
        /// <c>FunctionValue&lt;float> -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>releaseDamping</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "releaseDamping">]
        member inline _.releaseDampingOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("releaseDamping" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>releaseEase</c><br/>
        /// <c>(float -> float) -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>releaseEase</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float -> float | FunctionValue&lt;float -> float></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "releaseEase">]
        member inline _.releaseEaseOp(_object_builder: FableObject, _object_value: EasingFun) = ("releaseEase" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>releaseEase</c><br/>
        /// <c>FunctionValue&lt;float -> float> -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>releaseEase</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float -> float | FunctionValue&lt;float -> float></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "releaseEase">]
        member inline _.releaseEaseOp(_object_builder: FableObject, _object_value: FunctionValue<float -> float>) = ("releaseEase" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>releaseContainerFriction</c><br/>
        /// <c>float -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>releaseContainerFriction</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "releaseContainerFriction">]
        member inline _.releaseContainerFrictionOp(_object_builder: FableObject, _object_value: float) = ("releaseContainerFriction" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>releaseContainerFriction</c><br/>
        /// <c>FunctionValue&lt;float> -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>releaseContainerFriction</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "releaseContainerFriction">]
        member inline _.releaseContainerFrictionOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("releaseContainerFriction" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>minVelocity</c><br/>
        /// <c>float -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>minVelocity</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "minVelocity">]
        member inline _.minVelocityOp(_object_builder: FableObject, _object_value: float) = ("minVelocity" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>minVelocity</c><br/>
        /// <c>FunctionValue&lt;float> -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>minVelocity</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "minVelocity">]
        member inline _.minVelocityOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("minVelocity" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>maxVelocity</c><br/>
        /// <c>float -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>maxVelocity</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "maxVelocity">]
        member inline _.maxVelocityOp(_object_builder: FableObject, _object_value: float) = ("maxVelocity" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>maxVelocity</c><br/>
        /// <c>FunctionValue&lt;float> -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>maxVelocity</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "maxVelocity">]
        member inline _.maxVelocityOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("maxVelocity" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>velocityMultiplier</c><br/>
        /// <c>float -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>velocityMultiplier</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "velocityMultiplier">]
        member inline _.velocityMultiplierOp(_object_builder: FableObject, _object_value: float) = ("velocityMultiplier" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>velocityMultiplier</c><br/>
        /// <c>FunctionValue&lt;float> -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>velocityMultiplier</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "velocityMultiplier">]
        member inline _.velocityMultiplierOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("velocityMultiplier" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>dragSpeed</c><br/>
        /// <c>float -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>dragSpeed</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "dragSpeed">]
        member inline _.dragSpeedOp(_object_builder: FableObject, _object_value: float) = ("dragSpeed" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>dragSpeed</c><br/>
        /// <c>FunctionValue&lt;float> -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>dragSpeed</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "dragSpeed">]
        member inline _.dragSpeedOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("dragSpeed" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>scrollThreshold</c><br/>
        /// <c>float -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>scrollThreshold</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "scrollThreshold">]
        member inline _.scrollThresholdOp(_object_builder: FableObject, _object_value: float) = ("scrollThreshold" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>scrollThreshold</c><br/>
        /// <c>FunctionValue&lt;float> -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>scrollThreshold</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "scrollThreshold">]
        member inline _.scrollThresholdOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("scrollThreshold" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>scrollSpeed</c><br/>
        /// <c>float -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>scrollSpeed</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "scrollSpeed">]
        member inline _.scrollSpeedOp(_object_builder: FableObject, _object_value: float) = ("scrollSpeed" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>scrollSpeed</c><br/>
        /// <c>FunctionValue&lt;float> -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>scrollSpeed</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float></c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "scrollSpeed">]
        member inline _.scrollSpeedOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("scrollSpeed" ==> _object_value) :: _object_builder
        /// <summary>
        /// <c>snap</c><br/>
        /// <c>float -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>snap</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1 | 2</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&llt;float> | FunctionValue&lt;float * flooat></c></p>
        /// <p><c>float float</c></p>
        /// </remarks>
        /// <returns><c></c>(string * obj) list</returns>
        [<CustomOperation "snap">]
        member inline _.SnapOp(_object_builder: FableObject, value: float) = ("snap" ==> value) :: _object_builder
        /// <summary>
        /// <c>snap</c><br/>
        /// <c>float -> float -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>snap</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1 | 2</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&llt;float> | FunctionValue&lt;float * flooat></c></p>
        /// <p><c>float float</c></p>
        /// </remarks>
        /// <returns><c></c>(string * obj) list</returns>
        [<CustomOperation "snap">]
        member inline _.SnapOp(_object_builder: FableObject, value: float, value2: float) = ("snap" ==> (value, value2)) :: _object_builder
        /// <summary>
        /// <c>snap</c><br/>
        /// <c>FunctionValue&lt;float></c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>snap</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1 | 2</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&llt;float> | FunctionValue&lt;float * flooat></c></p>
        /// <p><c>float float</c></p>
        /// </remarks>
        /// <returns><c></c>(string * obj) list</returns>
        [<CustomOperation "snap">]
        member inline _.SnapOp(_object_builder: FableObject, value: FunctionValue<float>) = ("snap" ==> value) :: _object_builder
        /// <summary>
        /// <c>snap</c><br/>
        /// <c>FunctionValue&lt;float * float></c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>snap</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1 | 2</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&llt;float> | FunctionValue&lt;float * flooat></c></p>
        /// <p><c>float float</c></p>
        /// </remarks>
        /// <returns><c></c>(string * obj) list</returns>
        [<CustomOperation "snap">]
        member inline _.SnapOp(_object_builder: FableObject, value: FunctionValue<float * float>) = ("snap" ==> value) :: _object_builder
        /// <summary>
        /// <c>modifier</c><br/>
        /// <c>(float -> float) -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>modifier</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float -> float</c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "modifier">]
        member inline _.ModifierOp(_object_builder: FableObject, value: FloatModifier) = ("modifier" ==> value) :: _object_builder
        /// <summary>
        /// <c>mapTo</c><br/>
        /// <c>string -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>mapTo</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>string</c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "mapTo">]
        member inline _.MapToOp(_object_builder: FableObject, value: string) = ("mapTo" ==> value) :: _object_builder
        member inline _.Run(_object_builder: FableObject): DraggableOptions = !!createObj !!_object_builder
    type Bindings.Types.DraggableOptions with
        member inline _.Yield(_: unit) = ()
        member inline _.Yield(value: obj) = Target value
        member inline _.Yield(value: obj seq) = targets !!value
        member inline _.Combine(y) = fun () -> y
        member inline _.Delay(value) = value()
        member inline this.Run(state: Target<_>) =
            AnimeJs.createDraggable(!!state, this)
        member inline this.Run(state: Targets) =
            AnimeJs.createDraggable(state, this)
    type axis with
        /// <summary>
        /// <c>snap</c><br/>
        /// <c>float -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>snap</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1 | 2</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float> | FunctionValue&lt;float * flooat></c></p>
        /// <p><c>float float</c></p>
        /// </remarks>
        /// <returns><c></c>(string * obj) list</returns>
        [<CustomOperation "snap">]
        member inline _.SnapOp(_object_builder: FableObject, value: float) = ("snap" ==> value) :: _object_builder
        /// <summary>
        /// <c>snap</c><br/>
        /// <c>float -> float -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>snap</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1 | 2</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float> | FunctionValue&lt;float * flooat></c></p>
        /// <p><c>float float</c></p>
        /// </remarks>
        /// <returns><c></c>(string * obj) list</returns>
        [<CustomOperation "snap">]
        member inline _.SnapOp(_object_builder: FableObject, value: float, value2: float) = ("snap" ==> (value, value2)) :: _object_builder
        /// <summary>
        /// <c>snap</c><br/>
        /// <c>FunctionValue&lt;float></c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>snap</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1 | 2</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float> | FunctionValue&lt;float * flooat></c></p>
        /// <p><c>float float</c></p>
        /// </remarks>
        /// <returns><c></c>(string * obj) list</returns>
        [<CustomOperation "snap">]
        member inline _.SnapOp(_object_builder: FableObject, value: FunctionValue<float>) = ("snap" ==> value) :: _object_builder
        /// <summary>
        /// <c>snap</c><br/>
        /// <c>FunctionValue&lt;float * float></c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>snap</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1 | 2</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float> | FunctionValue&lt;float * flooat></c></p>
        /// <p><c>float float</c></p>
        /// </remarks>
        /// <returns><c></c>(string * obj) list</returns>
        [<CustomOperation "snap">]
        member inline _.SnapOp(_object_builder: FableObject, value: FunctionValue<float * float>) = ("snap" ==> value) :: _object_builder
        /// <summary>
        /// <c>modifier</c><br/>
        /// <c>(float -> float) -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>modifier</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float -> float</c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "modifier">]
        member inline _.ModifierOp(_object_builder: FableObject, value: FloatModifier) = ("modifier" ==> value) :: _object_builder
        /// <summary>
        /// <c>mapTo</c><br/>
        /// <c>string -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>mapTo</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>string</c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "mapTo">]
        member inline _.MapToOp(_object_builder: FableObject, value: string) = ("mapTo" ==> value) :: _object_builder
        member inline _.Yield(value: bool) = value
        member inline this.Run(_object_run: FableObject): string * obj = !!this ==> !!createObj _object_run
        member inline this.Run(_object_run: bool): string * obj = !!this ==> !!_object_run
        
    type AxisBuilder with
        /// <summary>
        /// <c>snap</c><br/>
        /// <c>float -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>snap</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1 | 2</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float> | FunctionValue&lt;float * flooat></c></p>
        /// <p><c>float float</c></p>
        /// </remarks>
        /// <returns><c></c>(string * obj) list</returns>
        [<CustomOperation "snap">]
        member inline _.SnapOp(_object_builder: FableObject, value: float) = ("snap" ==> value) :: _object_builder
        /// <summary>
        /// <c>snap</c><br/>
        /// <c>float -> float -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>snap</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1 | 2</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float> | FunctionValue&lt;float * flooat></c></p>
        /// <p><c>float float</c></p>
        /// </remarks>
        /// <returns><c></c>(string * obj) list</returns>
        [<CustomOperation "snap">]
        member inline _.SnapOp(_object_builder: FableObject, value: float, value2: float) = ("snap" ==> (value, value2)) :: _object_builder
        /// <summary>
        /// <c>snap</c><br/>
        /// <c>FunctionValue&lt;float></c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>snap</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1 | 2</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float> | FunctionValue&lt;float * flooat></c></p>
        /// <p><c>float float</c></p>
        /// </remarks>
        /// <returns><c></c>(string * obj) list</returns>
        [<CustomOperation "snap">]
        member inline _.SnapOp(_object_builder: FableObject, value: FunctionValue<float>) = ("snap" ==> value) :: _object_builder
        /// <summary>
        /// <c>snap</c><br/>
        /// <c>FunctionValue&lt;float * float></c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>snap</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1 | 2</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float | FunctionValue&lt;float> | FunctionValue&lt;float * flooat></c></p>
        /// <p><c>float float</c></p>
        /// </remarks>
        /// <returns><c></c>(string * obj) list</returns>
        [<CustomOperation "snap">]
        member inline _.SnapOp(_object_builder: FableObject, value: FunctionValue<float * float>) = ("snap" ==> value) :: _object_builder
        /// <summary>
        /// <c>modifier</c><br/>
        /// <c>(float -> float) -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>modifier</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>float -> float</c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "modifier">]
        member inline _.ModifierOp(_object_builder: FableObject, value: FloatModifier) = ("modifier" ==> value) :: _object_builder
        /// <summary>
        /// <c>mapTo</c><br/>
        /// <c>string -> ...</c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>mapTo</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>string</c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "mapTo">]
        member inline _.MapToOp(_object_builder: FableObject, value: string) = ("mapTo" ==> value) :: _object_builder
        member inline _.Yield(value: bool) = value
        member inline _.Run(_object_run: FableObject): AxisOptions = !!createObj _object_run
        member inline _.Run(_object_run: bool): AxisOptions = !!_object_run
    type ScrollObserverBuilder with
        /// <summary>
        /// <c>axis</c><br/>
        /// <c> Enums.Axis -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>container</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>Axis</c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "axis">]
        member inline _.axisOp(_object_builder: FableObject, value: axis) = ("axis" ==> value) :: _object_builder
        /// <summary>
        /// <c>container</c><br/>
        /// <c> Selector -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>container</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>Selector | string | #HTMLElement</c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "container">]
        member inline _.containerOp(_object_builder: FableObject, value: Selector) = ("container" ==> value) :: _object_builder
        /// <summary>
        /// <c>container</c><br/>
        /// <c> string -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>container</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>Selector | string | #HTMLElement</c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "container">]
        member inline _.containerOp(_object_builder: FableObject, value: string) = ("container" ==> value) :: _object_builder
        /// <summary>
        /// <c>container</c><br/>
        /// <c> #HTMLElement -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>container</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>Selector | string | #HTMLElement</c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "container">]
        member inline _.containerOp(_object_builder: FableObject, value: #HTMLElement) = ("container" ==> value) :: _object_builder
        /// <summary>
        /// <c>target</c><br/>
        /// <c> Selector -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>target</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>Selector | string | #HTMLElement</c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "target">]
        member inline _.targetOp(_object_builder: FableObject, value: Selector) = ("target" ==> value) :: _object_builder
        /// <summary>
        /// <c>target</c><br/>
        /// <c> string -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>target</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>Selector | string | #HTMLElement</c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "target">]
        member inline _.targetOp(_object_builder: FableObject, value: string) = ("target" ==> value) :: _object_builder
        /// <summary>
        /// <c>target</c><br/>
        /// <c> #HTMLElement -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>target</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>Selector | string | #HTMLElement</c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "target">]
        member inline _.targetOp(_object_builder: FableObject, value: #HTMLElement) = ("target" ==> value) :: _object_builder
        /// <summary>
        /// <c>debug</c><br/>
        /// <c> unit -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>debug</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>0-1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>unit | bool</c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "debug">]
        member inline _.debugOp(_object_builder: FableObject) = ("debug" ==> true) :: _object_builder
        /// <summary>
        /// <c>debug</c><br/>
        /// <c> bool -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>debug</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>0-1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>unit | bool</c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "debug">]
        member inline _.debugOp(_object_builder: FableObject, value: bool) = ("debug" ==> value) :: _object_builder
        /// <summary>
        /// <c>repeat</c><br/>
        /// <c> unit -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>repeat</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>0-1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>unit | bool</c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "repeat">]
        member inline _.repeatOp(_object_builder: FableObject) = ("repeat" ==> true) :: _object_builder
        /// <summary>
        /// <c>repeat</c><br/>
        /// <c> bool -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>repeat</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>0-1</c>
        /// </description> </item>
        /// </list>
        /// <p><c>unit | bool</c></p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "repeat">]
        member inline _.repeatOp(_object_builder: FableObject, value: bool) = ("repeat" ==> value) :: _object_builder
        /// <summary>
        /// <c>sync</c><br/>
        /// <c> unit -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>sync</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>0 | 1 | 2 | 4</c>
        /// </description> </item>
        /// </list>
        /// <p>
        /// 0: <c>unit</c><br/>
        /// 1: <c>bool | string | float | float -> float</c><br/>
        /// 2: <c>string string</c><br/>
        /// 4: <c>string string string string</c><br/>
        /// </p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "sync">]
        member inline _.syncOp(_object_builder: FableObject) = ("sync" ==> true) :: _object_builder
        /// <summary>
        /// <c>sync</c><br/>
        /// <c> bool -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>sync</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>0 | 1 | 2 | 4</c>
        /// </description> </item>
        /// </list>
        /// <p>
        /// 0: <c>unit</c><br/>
        /// 1: <c>bool | string | float | float -> float</c><br/>
        /// 2: <c>string string</c><br/>
        /// 4: <c>string string string string</c><br/>
        /// </p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "sync">]
        member inline _.syncOp(_object_builder: FableObject, value: bool) = ("sync" ==> value) :: _object_builder
        /// <summary>
        /// <c>sync</c><br/>
        /// <c> string -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>sync</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>0 | 1 | 2 | 4</c>
        /// </description> </item>
        /// </list>
        /// <p>
        /// 0: <c>unit</c><br/>
        /// 1: <c>bool | string | float | float -> float</c><br/>
        /// 2: <c>string string</c><br/>
        /// 4: <c>string string string string</c><br/>
        /// </p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "sync">]
        member inline _.syncOp(_object_builder: FableObject, enter: string) = ("sync" ==> enter) :: _object_builder
        /// <summary>
        /// <c>sync</c><br/>
        /// <c> string -> string -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>sync</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>0 | 1 | 2 | 4</c>
        /// </description> </item>
        /// </list>
        /// <p>
        /// 0: <c>unit</c><br/>
        /// 1: <c>bool | string | float | float -> float</c><br/>
        /// 2: <c>string string</c><br/>
        /// 4: <c>string string string string</c><br/>
        /// </p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "sync">]
        member inline _.syncOp(_object_builder: FableObject, enter: string, leave: string) = ("sync" ==> $"{enter} {leave}") :: _object_builder
        /// <summary>
        /// <c>sync</c><br/>
        /// <c> string -> string -> string -> string -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>sync</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>0 | 1 | 2 | 4</c>
        /// </description> </item>
        /// </list>
        /// <p>
        /// 0: <c>unit</c><br/>
        /// 1: <c>bool | string | float | float -> float</c><br/>
        /// 2: <c>string string</c><br/>
        /// 4: <c>string string string string</c><br/>
        /// </p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "sync">]
        member inline _.syncOp(_object_builder: FableObject, enter: string, leave: string, enterBackward: string, leaveBackward: string) = ("sync" ==> $"{enter} {leave} {enterBackward} {leaveBackward}") :: _object_builder
        /// <summary>
        /// <c>sync</c><br/>
        /// <c> float -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>sync</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>0 | 1 | 2 | 4</c>
        /// </description> </item>
        /// </list>
        /// <p>
        /// 0: <c>unit</c><br/>
        /// 1: <c>bool | string | float | float -> float</c><br/>
        /// 2: <c>string string</c><br/>
        /// 4: <c>string string string string</c><br/>
        /// </p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "sync">]
        member inline _.syncOp(_object_builder: FableObject, value: float) = ("sync" ==> value) :: _object_builder
        /// <summary>
        /// <c>sync</c><br/>
        /// <c> (float -> float) -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>sync</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>0 | 1 | 2 | 4</c>
        /// </description> </item>
        /// </list>
        /// <p>
        /// 0: <c>unit</c><br/>
        /// 1: <c>bool | string | float | float -> float</c><br/>
        /// 2: <c>string string</c><br/>
        /// 4: <c>string string string string</c><br/>
        /// </p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "sync">]
        member inline _.syncOp(_object_builder: FableObject, value: float -> float) = ("sync" ==> value) :: _object_builder
        /// <summary>
        /// <c>enter</c><br/>
        /// <c>string -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>enter</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1 | 2</c>
        /// </description> </item>
        /// </list>
        /// <p>
        /// 1: <c>string | ObserverThreshold</c><br/>
        /// 2: <c>(string | ObserverThreshold) (string | ObserverThreshold)</c><br/>
        /// </p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "enter">]
        member inline _.enterOp(_object_builder: FableObject, value: string) = ("enter" ==> value) :: _object_builder
        /// <summary>
        /// <c>enter</c><br/>
        /// <c> ObserverThreshold -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>enter</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1 | 2</c>
        /// </description> </item>
        /// </list>
        /// <p>
        /// 1: <c>string | ObserverThreshold</c><br/>
        /// 2: <c>(string | ObserverThreshold) (string | ObserverThreshold)</c><br/>
        /// </p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "enter">]
        member inline _.enterOp(_object_builder: FableObject, value: observerThreshold) = ("enter" ==> value) :: _object_builder
        /// <summary>
        /// <c>enter</c><br/>
        /// <c> ObserverTreshold -> ObserverThreshold -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>enter</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1 | 2</c>
        /// </description> </item>
        /// </list>
        /// <p>
        /// 1: <c>string | ObserverThreshold</c><br/>
        /// 2: <c>(string | ObserverThreshold) (string | ObserverThreshold)</c><br/>
        /// </p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "enter">]
        member inline _.enterOp(_object_builder: FableObject, value: observerThreshold, target: observerThreshold) = ("enter" ==> {| container = value; target = target |}) :: _object_builder
        /// <summary>
        /// <c>enter</c><br/>
        /// <c> string -> ObserverThreshold -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>enter</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1 | 2</c>
        /// </description> </item>
        /// </list>
        /// <p>
        /// 1: <c>string | ObserverThreshold</c><br/>
        /// 2: <c>(string | ObserverThreshold) (string | ObserverThreshold)</c><br/>
        /// </p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "enter">]
        member inline _.enterOp(_object_builder: FableObject, value: string, target: observerThreshold) = ("enter" ==> {| container = value; target = target |}) :: _object_builder
        /// <summary>
        /// <c>enter</c><br/>
        /// <c> string -> string -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>enter</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1 | 2</c>
        /// </description> </item>
        /// </list>
        /// <p>
        /// 1: <c>string | ObserverThreshold</c><br/>
        /// 2: <c>(string | ObserverThreshold) (string | ObserverThreshold)</c><br/>
        /// </p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "enter">]
        member inline _.enterOp(_object_builder: FableObject, value: string, target: string) = ("enter" ==> {| container = value; target = target |}) :: _object_builder
        /// <summary>
        /// <c>enter</c><br/>
        /// <c> ObserverThreshold -> string -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>enter</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1 | 2</c>
        /// </description> </item>
        /// </list>
        /// <p>
        /// 1: <c>string | ObserverThreshold</c><br/>
        /// 2: <c>(string | ObserverThreshold) (string | ObserverThreshold)</c><br/>
        /// </p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "enter">]
        member inline _.enterOp(_object_builder: FableObject, value: observerThreshold, target: string) = ("enter" ==> {| container = value; target = target |}) :: _object_builder
        /// <summary>
        /// <c>leave</c><br/>
        /// <c> string -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>leave</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1 | 2</c>
        /// </description> </item>
        /// </list>
        /// <p>
        /// 1: <c>string | ObserverThreshold</c><br/>
        /// 2: <c>(string | ObserverThreshold) (string | ObserverThreshold)</c><br/>
        /// </p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "leave">]
        member inline _.leaveOp(_object_builder: FableObject, value: string) = ("leave" ==> value) :: _object_builder
        /// <summary>
        /// <c>leave</c><br/>
        /// <c> ObserverTreshold -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>leave</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1 | 2</c>
        /// </description> </item>
        /// </list>
        /// <p>
        /// 1: <c>string | ObserverThreshold</c><br/>
        /// 2: <c>(string | ObserverThreshold) (string | ObserverThreshold)</c><br/>
        /// </p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "leave">]
        member inline _.leaveOp(_object_builder: FableObject, value: observerThreshold) = ("leave" ==> value) :: _object_builder
        /// <summary>
        /// <c>leave</c><br/>
        /// <c> ObserverTreshold -> ObserverThreshold -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>leave</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1 | 2</c>
        /// </description> </item>
        /// </list>
        /// <p>
        /// 1: <c>string | ObserverThreshold</c><br/>
        /// 2: <c>(string | ObserverThreshold) (string | ObserverThreshold)</c><br/>
        /// </p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "leave">]
        member inline _.leaveOp(_object_builder: FableObject, value: observerThreshold, target: observerThreshold) = ("leave" ==> {| container = value; target = target |}) :: _object_builder
        /// <summary>
        /// <c>leave</c><br/>
        /// <c> string -> ObserverThreshold -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>leave</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1 | 2</c>
        /// </description> </item>
        /// </list>
        /// <p>
        /// 1: <c>string | ObserverThreshold</c><br/>
        /// 2: <c>(string | ObserverThreshold) (string | ObserverThreshold)</c><br/>
        /// </p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "leave">]
        member inline _.leaveOp(_object_builder: FableObject, value: string, target: observerThreshold) = ("leave" ==> {| container = value; target = target |}) :: _object_builder
        /// <summary>
        /// <c>leave</c><br/>
        /// <c> string -> string -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>leave</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1 | 2</c>
        /// </description> </item>
        /// </list>
        /// <p>
        /// 1: <c>string | ObserverThreshold</c><br/>
        /// 2: <c>(string | ObserverThreshold) (string | ObserverThreshold)</c><br/>
        /// </p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "leave">]
        member inline _.leaveOp(_object_builder: FableObject, value: string, target: string) = ("leave" ==> {| container = value; target = target |}) :: _object_builder
        /// <summary>
        /// <c>leave</c><br/>
        /// <c>ObserverTreshold -> string -> ... </c>
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item> <term>
        /// Operation:
        /// </term> <description>
        /// <c>leave</c>
        /// </description> </item>
        /// <item> <term>
        /// Args Num:
        /// </term> <description>
        /// <c>1 | 2</c>
        /// </description> </item>
        /// </list>
        /// <p>
        /// 1: <c>string | ObserverThreshold</c><br/>
        /// 2: <c>(string | ObserverThreshold) (string | ObserverThreshold)</c><br/>
        /// </p>
        /// </remarks>
        /// <returns><c>(string * obj) list</c></returns>
        [<CustomOperation "leave">]
        member inline _.leaveOp(_object_builder: FableObject, value: observerThreshold, target: string) = ("leave" ==> {| container = value; target = target |}) :: _object_builder
        member inline _.Run(_object_run: FableObject): ScrollObserver = AnimeJs.onScroll(createObj _object_run)
    type Engine with
        member inline _.Zero() = ()
        member inline _.Yield(_) = ()
        [<CustomOperation "timeUnit">]
        member inline this.timeUnitOp(_, value: Enums.timeUnit) = this.timeUnit <- value
        [<CustomOperation "precision">]
        member inline this.precisionOp(_, value: int) = this.precision <- value
        [<CustomOperation "speed">]
        member inline this.speedOp(_, value: float) = this.speed <- value
        [<CustomOperation "fps">]
        member inline this.fpsOp(_, value: int) = this.fps <- value
        [<CustomOperation "useDefaultMainLoop">]
        member inline this.useDefaultMainLoopOp(_, value: bool) = this.useDefaultMainLoop <- value
        [<CustomOperation "useDefaultMainLoop">]
        member inline this.useDefaultMainLoopOp(_) = this.useDefaultMainLoop <- true
        [<CustomOperation "pauseOnDocumentHidden">]
        member inline this.pauseOnDocumentHiddenOp(_, value: bool) = this.pauseOnDocumentHidden <- value
        [<CustomOperation "pauseOnDocumentHidden">]
        member inline this.pauseOnDocumentHiddenOp(_) = this.pauseOnDocumentHidden <- true
        [<CustomOperation "playbackEase">]
        member inline this.playbackEaseOp(_, value: float -> float) = this.defaults.playbackEase <- !!value
        [<CustomOperation "playbackRate">]
        member inline this.playbackRateOp(_, value: float) = this.defaults.playbackRate <- value
        [<CustomOperation "frameRate">]
        member inline this.frameRateOp(_, value: float) = this.defaults.frameRate <- !!value
        [<CustomOperation "loop">]
        member inline this.loopOp(_, value: int) = this.defaults.loop <- !!value
        [<CustomOperation "loop">]
        member inline this.loopOp(_, value: bool) = this.defaults.loop <- !^value
        [<CustomOperation "loop">]
        member inline this.loopOp(_) = this.defaults.loop <- !^true
        [<CustomOperation "reversed">]
        member inline this.reversedOp(_) = this.defaults.reversed <- true
        [<CustomOperation "alternate">]
        member inline this.alternateOp(_) = this.defaults.alternate <- true
        [<CustomOperation "autoplay">]
        member inline this.autoplayOp(_) = this.defaults.autoplay <- true
        [<CustomOperation "duration">]
        member inline this.durationOp(_, value: float) = this.defaults.duration <- !^value
        [<CustomOperation "delay">]
        member inline this.delayOp(_, value: float) = this.defaults.delay <- !^value
        [<CustomOperation "composition">]
        member inline this.compositionOp(_, value) = this.defaults.composition <- value
        [<CustomOperation "ease">]
        member inline this.easeOp(_, value: EasingFun) = this.defaults.ease <- !!value
        [<CustomOperation "loopDelay">]
        member inline this.loopDelayOp(_, value: float) = this.defaults.loopDelay <- value
        [<CustomOperation "modifier">]
        member inline this.modifierOp(_, value: float -> float) = this.defaults.modifier <- value
        member inline this.Run(_) = this
    type TimelineBuilder with
        member inline _.Zero(): FableObject = []
        [<CustomOperation "defaults">]
        member inline _.defaultsOp(state: FableObject, value: TimelineOptionsDefaults) =
            "defaults" ==> value |> add state
        member inline _.Run(state: FableObject) = AnimeJs.createTimeline(createObj state)
    type ScopeDefaultOptionsBuilder with
        member inline _.Run(state: FableObject) = unbox<ScopeOptionsDefaults>(createObj state)
    type ScopeBuilder with
        member inline _.Yield(value: ScopeOptionsDefaults): FableObject = [ "defaults" ==> value ]
        [<CustomOperation "root">]
        member inline _.rootOp(state: FableObject, value: string) =
            "root" ==> value |> add state
        [<CustomOperation "root">]
        member inline _.rootOp(state: FableObject, value: #Element) =
            "root" ==> value |> add state
        [<CustomOperation "defaults">]
        member inline _.defaultsOp(state: FableObject, value: obj) =
            "defaults" ==> value |> add state
        [<CustomOperation "mediaQueries">]
        member inline _.mediaQueriesOp(state: FableObject, value: obj) =
            "mediaQueries" ==> value |> add state
        member inline _.Run(state: FableObject) = 
            AnimeJs.createScope(createObj state)
    type Bindings.Types.Scope with
        member inline _.Zero() = ()
        [<CustomOperation "refresh">]
        member inline this.refreshOp(_) = this.refresh()
        [<CustomOperation "revert">]
        member inline this.revertOp(_) = this.revert()
    type TextSplitterBuilder with
        member inline _.Zero() = []
        member inline this.Yield(value: TextSplitterParameter): string * obj =
            unbox value
        [<CustomOperation "debug">]
        member inline this.debugOp(state: FableObject) =
            "debug" ==> true |> add state
        [<CustomOperation "debug">]
        member inline this.debugOp(state: FableObject, value: bool) =
            "debug" ==> value |> add state
        [<CustomOperation "includeSpaces">]
        member inline this.includeSpacesOp(state: FableObject) =
            "includeSpaces" ==> true |> add state
        
        [<CustomOperation "includeSpaces">]
        member inline this.includeSpacesOp(state: FableObject, value: bool) =
            "includeSpaces" ==> value |> add state
        [<CustomOperation "accessible">]
        member inline this.accessibleOp(state: FableObject, value: bool) =
            "accessible" ==> value |> add state
        [<CustomOperation "accessible">]
        member inline this.accessibleOp(state: FableObject) =
            "accessible" ==> true |> add state
        member inline this.Run(state: FableObject) =
            createObj state |> unbox<TextSplitterOptions>
    type SplitParameterBuilder with
        member inline _.Yield(value: bool) = value
        member inline _.Yield(value: string) = value
        [<CustomOperation "class'">]
        member inline _.classOp(state: FableObject, value: string) =
            "class" ==> value |> add state
        [<CustomOperation "wrap">]
        member inline _.wrapOp(state: FableObject, value: bool) =
            "wrap" ==> value |> add state
        [<CustomOperation "wrap">]
        member inline _.wrapOp(state: FableObject) =
            "wrap" ==> true |> add state
        [<CustomOperation "wrap">]
        member inline _.wrapOp(state: FableObject, value: string) =
            "wrap" ==> value |> add state
        [<CustomOperation>]
        member inline _.wrapHidden(state: FableObject) =
            "wrap" ==> "hidden" |> add state
        [<CustomOperation>]
        member inline _.wrapClip(state: FableObject) =
            "wrap" ==> "clip" |> add state
        [<CustomOperation>]
        member inline _.wrapVisible(state: FableObject) =
            "wrap" ==> "visible" |> add state
        [<CustomOperation>]
        member inline _.wrapScroll(state: FableObject) =
            "wrap" ==> "scroll" |> add state
        [<CustomOperation>]
        member inline _.wrapAuto(state: FableObject) =
            "wrap" ==> "auto" |> add state
        [<CustomOperation "clone">]
        member inline _.cloneOp(state: FableObject) =
            "clone" ==> true |> add state
        [<CustomOperation "clone">]
        member inline _.cloneOp(state: FableObject, value: bool) =
            "clone" ==> value |> add state
        [<CustomOperation "clone">]
        member inline _.cloneOp(state: FableObject, value: string) =
            "clone" ==> value |> add state
        [<CustomOperation>]
        member inline _.cloneLeft(state: FableObject) =
            "clone" ==> "left" |> add state
        [<CustomOperation>]
        member inline _.cloneRight(state: FableObject) =
            "clone" ==> "right" |> add state
        [<CustomOperation>]
        member inline _.cloneTop(state: FableObject) =
            "clone" ==> "top" |> add state
        [<CustomOperation>]
        member inline _.cloneBottom(state: FableObject) =
            "clone" ==> "bottom" |> add state
        [<CustomOperation>]
        member inline _.cloneCenter(state: FableObject) =
            "clone" ==> "center" |> add state
        [<CustomOperation "clone">]
        member inline _.cloneOp(state: FableObject, value: cloneText) =
            "clone" ==> value |> add state
        member inline _.Yield(value: cloneText): string * obj =
            "clone" ==> value
        [<CustomOperation "wrap">]
        member inline _.wrapOp(state: FableObject, value: wrapText) =
            "wrap" ==> value |> add state
        member inline _.Yield(value: wrapText): string * obj =
            "wrap" ==> value
    type Bindings.Types.TextSplitterOptions with
        member inline _.Yield(_: unit) = ()
        member inline _.Yield(value: obj) = Target value
        member inline _.Yield(value: obj seq) = targets !!value
        member inline _.Combine(y) = fun () -> y
        member inline _.Delay(value) = value()
        member inline this.Run(state: Target<_>) =
            Text.split(state, this)
        member inline this.Run(state: Targets) =
            Text.split(state, this)

type style = CssStyle
let animate: AnimationBuilder = unbox ()
let timeline: TimelineBuilder = unbox ()
let inline mkStyle (value: string) = unbox<ICssStyle> value
let onScroll: ScrollObserverBuilder = unbox ()
let draggable: DraggableBuilder = unbox ()
let spring: SpringBuilder = unbox ()
let animatable: AnimatableBuilder = unbox ()
let timer: TimerBuilder = unbox ()
let engine: Engine = import "engine" Spec.path
let bounds: BoundsBuilder = unbox ()
let cursor: CursorBuilder = unbox ()
let axisX = AxisX()
let axisY = AxisY()
let axisOptions = unbox<AxisBuilder> ()
let keyframe = KeyframeBuilder()
let tween: StyleArray = StyleArray()
let keyframes = StyleArrayBuilder "keyframes"
type Timeline with
    static member defaults: TimelineDefaultsBuilder = unbox ()
type Bindings.Types.Scope with
    static member defaults: ScopeDefaultOptionsBuilder = unbox ()
let scope: ScopeBuilder = unbox ()
let splitText: TextSplitterBuilder = unbox ()
let lines = TextSplitterParameterBuilder("lines")
let words = TextSplitterParameterBuilder("words")
let chars = TextSplitterParameterBuilder("chars")

module Operators =
    let inline (!<<+=) value: RelativeTimePosition = unbox $"<<+={value}"
    let inline (!<<-=) value: RelativeTimePosition = unbox $"<<-={value}"
    let inline (!<<*=) value: RelativeTimePosition = unbox $"<<*={value}"
    let inline (!<) _: RelativeTimePosition = unbox "<"
    let inline (!<<) _: RelativeTimePosition = unbox "<<"
    let inline (!+=) value: RelativeTweenValue = unbox $"+={value}"
    let inline (!-=) value: RelativeTweenValue = unbox $"-={value}"
    let inline (!*=) value: RelativeTweenValue = unbox $"*={value}"
    let inline timeLabel (value: string): TimeLabel = !!value
    let inline (==<) x y: 'Type = unbox (x,y)
    let inline (!%) value: KeyframePercentValue = !! $"{value}%%"
    let inline (!~) (value: string) = unbox<ICssStyle> value
