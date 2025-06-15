module Partas.AnimeJs.FSharp

open System.Runtime.CompilerServices
open Browser.Types
open Partas.AnimeJs.Binding
open Fable.Core
open Fable.Core.DynamicExtensions
open Fable.Core.JsInterop
open Partas.AnimeJs.Style
open Partas.AnimeJs.Core
open Partas.Solid.Experimental.U

#nowarn 1182

[<AutoOpen; Erase>]
module internal Internal =
    [<ImportMember(Spec.path)>]
    let engine: Engine = jsNative

type SpringBuilder = {
    mass: float
    stiffness: float
    damping: float
    velocity: float
} with
    member this.toPojo = this |> toPlainJsObj
    member inline this.toSpring: Spring = this.toPojo |> import "createSpring" "animejs"
/// <summary>
/// A building block to make an <c>Ease</c> DU which can then be compiled into an EasingFunction
/// using <c>.ToEasing</c>
/// </summary>
/// <example><code>
/// let builder = AnimationBuilder.TweenProps.init
/// EaseDir.In
/// |> Quad
/// |> _.ToEasing |> builder.WithEase
/// </code></example>
/// <remarks>
/// <c>Ease</c> will implicitly be accepted wherever an <c>EasingFun</c> is accepted.
/// </remarks>
[<RequireQualifiedAccess; StringEnum>]
type EaseDir =
    | In
    | Out
    | InOut
    
/// <summary>
/// To pass a custom easing function to AnimeJS, use the Ease.Function discriminated union
/// </summary>
[<Interface>]
type EasingFun =
    static member inline op_Implicit(other: Ease): EasingFun = other.ToEasing
    
and Ease =
    | DefaultLinear
    | Linear of x1: float * x2: string * x3: float
    | Quad of EaseDir
    | Cubic of EaseDir
    | Quart of EaseDir
    | Quint of EaseDir
    | Sine of EaseDir
    | Circ of EaseDir
    | Expo of EaseDir
    | Bounce of EaseDir
    | Power of EaseDir * power: float
    | Back of EaseDir * overshoot: float
    | Elastic of EaseDir * amplitude: float * period: float
    | Irregular of length: float * randomness: float
    | Steps of steps: float * fromStart: bool
    | CubicBezier of mX1: float * mY1: float * mX2: float * mY2: float
    | Spring of mass: float * stiffness: float * damping: float * velocity: float
    | Function of easingFn: (float -> float)
    [<Import("eases.linear", "animejs")>]
    static member linear (x1: U2<float,string>) (x2: U2<float, string>) (x3: U2<float, string>): EasingFun = jsNative
    [<Import("eases.irregular", "animejs")>]
    static member irregular (length: 'a when 'a : unmanaged) (randomness: 'b when 'b: unmanaged): EasingFun = jsNative
    [<Import("eases.steps", "animejs")>]
    static member steps (steps: 'a when 'a: unmanaged): EasingFun = jsNative
    [<Import("eases.cubicBezier", "animejs")>]
    static member cubicBezier
        (x1: 'a when 'a : unmanaged)
        (x2: 'b when 'b : unmanaged)
        (x3: 'c when 'c : unmanaged)
        (x4: 'd when 'd : unmanaged): EasingFun = jsNative
    [<Import("eases.in", "animejs")>]
    static member in' (power: 'a when 'a: unmanaged) : EasingFun = jsNative
    [<Import("eases.out", "animejs")>]
    static member out (power: 'a when 'a: unmanaged): EasingFun = jsNative
    [<Import("eases.inOut", "animejs")>]
    static member inOut (power: 'a when 'a: unmanaged): EasingFun = jsNative
    [<Import("eases.inBack", "animejs")>]
    static member inBack (overshoot: 'a when 'a : unmanaged) : EasingFun = jsNative
    [<Import("eases.outBack", "animejs")>]
    static member outBack (overshoot: 'a when 'a : unmanaged) : EasingFun = jsNative
    [<Import("eases.inOutBack", "animejs")>]
    static member inOutBack (overshoot: 'a when 'a : unmanaged) : EasingFun = jsNative
    [<Import("eases.inElastic", "animejs")>]
    static member inElastic (amplitude: 'a when 'a: unmanaged) (period: 'b when 'b: unmanaged):EasingFun = jsNative
    [<Import("eases.outElastic", "animejs")>]
    static member outElastic (amplitude: 'a when 'a: unmanaged) (period: 'b when 'b: unmanaged):EasingFun = jsNative
    [<Import("eases.inOutElastic", "animejs")>]
    static member inOutElastic (amplitude: 'a when 'a: unmanaged) (period: 'b when 'b: unmanaged):EasingFun = jsNative
    [<Import("createSpring", "animejs"); ParamObject>]
    static member createSpring (
        ?mass: 'a when 'a : unmanaged
        ,?stiffness: 'b when 'b : unmanaged
        ,?damping: 'c when 'c : unmanaged
        ,?velocity: 'd when 'd : unmanaged
        ): EasingFun = jsNative
    static member inline createSpring (spring: SpringBuilder): EasingFun = (unbox spring.toPojo) |> import "createSpring" "animejs"
    member this.ToEasing: EasingFun =
        match this with
        | DefaultLinear -> unbox "linear"
        | Linear(x1,x2,x3) -> unbox $"linear({x1}, {x2}, {x3})"
        | Quad easeDir -> unbox $"{easeDir}Quad"
        | Cubic easeDir -> unbox $"{easeDir}Cubic"
        | Quart easeDir -> unbox $"{easeDir}Quart"
        | Quint easeDir -> unbox $"{easeDir}Quint"
        | Sine easeDir -> unbox $"{easeDir}Sine"
        | Circ easeDir -> unbox $"{easeDir}Circ"
        | Expo easeDir -> unbox $"{easeDir}Expo"
        | Bounce easeDir -> unbox $"{easeDir}Bounce"
        | Power(easeDir, power) -> unbox $"{easeDir}(p = {power})"
        | Back(easeDir, overshoot) -> unbox $"{easeDir}Back(overshoot = {overshoot})"
        | Elastic(easeDir, amplitude, period) -> unbox $"{easeDir}Elastic(amplitude = {amplitude}, period = {period})"
        | Irregular(length, randomness) -> unbox $"irregular(length = {length}, randomness = {randomness})"
        | Steps(steps, fromStart) -> unbox $"steps(steps = {steps}, fromStart = {fromStart})"
        | CubicBezier(mX1, mY1, mX2, mY2) -> unbox $"cubicBezier({mX1}, {mY1}, {mX2}, {mY2})"
        | Function easingFn -> unbox easingFn
        | Spring(mass, stiffness, damping, velocity) -> Ease.createSpring(mass,stiffness,damping,velocity)
module Ease =
    let power direction power = Power (direction, power)
    let back direction overshoot = Back(direction, overshoot)
    let elasticAmplitude direction amplitude = Elastic(direction, amplitude, 0.3)
    let elasticPeriod direction period = Elastic(direction, 1., period)
    let elastic direction amplitude period = Elastic(direction, amplitude, period)
    let quad = Quad
    let cubic = Cubic
    let quart = Quart
    let quint = Quint
    let sine = Sine
    let circ = Circ

type private ITweenParams =
    abstract member Delay: U2<int<ms>, FunctionValue<int<ms>>> option with get
    abstract member Duration: U2<int<ms>, FunctionValue<int<ms>>> option with get
    abstract member Ease: EasingFun option with get
    abstract member Composition: Composition option with get
    abstract member Modifier: FloatModifier option with get

type private ITweenParamsExtensions =
    [<Extension>]
    static member internal toPojo(this: ITweenParams): obj =
        let builder: AnimationOptions  = createEmpty
        this.Delay |> Option.iter (fun value -> builder.delay <- value)
        this.Duration |> Option.iter (fun value -> builder.duration <- value)
        this.Ease |> Option.iter (fun value -> builder.ease <- !!value)
        this.Composition |> Option.iter (fun value -> builder.composition <- value)
        this.Modifier |> Option.iter (fun value -> builder.modifier <- value)
        builder
open type ITweenParamsExtensions

[<AutoOpen>]
module TweenValue =
    type Value =
        | Numeric of U2<int, float>
        | String of string
        | Relative of RelativeTweenValue
        | Function of handler: FunctionValue<U3<int, float, string>>
        member this.unwrap: obj =
            match this with
            | Numeric value -> unbox value
            | String value -> unbox value
            | Relative value -> unbox value
            | Function value -> unbox value
    and Param = {
        Param: ParamKind
        Delay: U2<int<ms>, FunctionValue<int<ms>>> option
        Duration: U2<int<ms>, FunctionValue<int<ms>>> option
        Ease: EasingFun option
        Composition: Composition option
        Modifier: FloatModifier option
    } with
        interface ITweenParams with
            member this.Delay = this.Delay
            member this.Duration = this.Duration
            member this.Ease = this.Ease
            member this.Composition = this.Composition
            member this.Modifier = this.Modifier
        member this.toPojo =
            let builder = this.toPojo()
            match this.Param with
            | To value -> builder["to"] <- value.unwrap
            | ToTuple(value, value1) -> builder["to"] <- [| value.unwrap; value1.unwrap |]
            | From value -> builder["from"] <- value.unwrap
            builder
        
    and ParamKind =
        | To of Value
        | ToTuple of Value * Value
        | From of Value
    and [<Erase>] TweenValue =
        | Param of Param
        | Value of Value
        member this.unwrap =
            match this with
            | Param value -> value.toPojo
            | Value value -> value.unwrap
    and KeyframeParam = {
        Properties: (string * obj) list
        Delay: U2<int<ms>, FunctionValue<int<ms>>> option
        Duration: U2<int<ms>, FunctionValue<int<ms>>> option
        Ease: EasingFun option
        Composition: Composition option
        Modifier: FloatModifier option
    } with
        static member init = {
            Properties = []
            Delay = None
            Duration = None
            Ease = None
            Composition = None
            Modifier = None
        }
        interface ITweenParams with
            member this.Delay = this.Delay
            member this.Duration = this.Duration
            member this.Ease = this.Ease
            member this.Composition = this.Composition
            member this.Modifier = this.Modifier
        member this.toPojo =
            emitJsExpr (
                this.toPojo(),
                this.Properties |> createObj
                ) "{ ...$0, ...$1 }"
            
    and PropertyValue =
        | Keyframes of TweenValue[]
        | Value of TweenValue
        member this.unwrap =
            match this with
            | Keyframes values -> values |> Array.map _.unwrap |> unbox
            | Value value -> value.unwrap

[<Interface>]
type Style<'T> =
    inherit CssStyle<PropertyValue, Style<'T>>
    static member inline translateX(value: PropertyValue): Style<'T> = "translateX" ==>! value
    static member inline translateY (value: PropertyValue): Style<'T> = "translateY" ==>! value
    static member inline translateZ (value: PropertyValue): Style<'T> = "translateZ" ==>! value
    static member inline rotate (value: PropertyValue): Style<'T> = "rotate" ==>! value
    static member inline rotateX (value: PropertyValue): Style<'T> = "rotateX" ==>! value
    static member inline rotateY (value: PropertyValue): Style<'T> = "rotateY" ==>! value
    static member inline rotateZ (value: PropertyValue): Style<'T> = "rotateZ" ==>! value
    static member inline scale (value: PropertyValue): Style<'T> = "scale" ==>! value
    static member inline scaleX (value: PropertyValue): Style<'T> = "scaleX" ==>! value
    static member inline scaleY (value: PropertyValue): Style<'T> = "scaleY" ==>! value
    static member inline scaleZ (value: PropertyValue): Style<'T> = "scaleZ" ==>! value
    static member inline skew (value: PropertyValue): Style<'T> = "skew" ==>! value
    static member inline skewX (value: PropertyValue): Style<'T> = "skewX" ==>! value
    static member inline skewY (value: PropertyValue): Style<'T> = "skewY" ==>! value
    static member inline perspective (value: PropertyValue): Style<'T> = "perspective" ==>! value
    /// <summary>
    /// CAUTION: <ul>
    /// <li>Safe to use with Records</li>
    /// <li>Only use with Classes if you define the property using <c>val mutable</c> or it is in the
    /// primary constructor.</li>
    /// </ul>
    /// </summary>
    /// <param name="mapping"></param>
    /// <param name="value"></param>
    static member inline property<'U> (mapping: 'T -> 'U) (value: PropertyValue): Style<'T> = Experimental.nameofLambda mapping ==>! value
    static member inline variable (name: string) (value: U2<obj, PropertyValue>): Style<'T> = name ==>! value
    static member inline draw (value: PropertyValue): Style<'T> = "draw" ==>! value

module AnimationBuilder =
    /// <summary>
    /// Tween properties that are applied to the whole animation rather than just
    /// a specific part.
    /// </summary>
    type TweenProps = {
        Delay: U2<int<ms>, FunctionValue<int<ms>>> option
        Duration: U2<int<ms>, FunctionValue<int<ms>>> option
        Ease: EasingFun option
        Composition: Composition option
        Modifier: FloatModifier option
    } with
        static member inline init with get() =
            {
                Delay = None
                Duration = None
                Ease = None
                Composition = None
                Modifier = None
            }
        member inline this.WithDelay value = { this with Delay = Some value }
        member inline this.WithDuration value = { this with Duration = value |> Some }
        member inline this.WithEase value = { this with Ease = Some value }
        member inline this.WithComposition value = { this with Composition = Some value }
        member inline this.WithModifier value = { this with Modifier = Some value }
        interface ITweenParams with
            member this.Composition = this.Composition
            member this.Delay = this.Delay
            member this.Duration = this.Duration
            member this.Ease = this.Ease
            member this.Modifier = this.Modifier
        member inline this.toPojo = this.toPojo()
            

[<RequireQualifiedAccess>]
type Loop =
    | Infinite of delay: int<ms> option
    | Count of count: int * delay: int<ms> option
    | None
    member this.toPojo =
        let inline loop value = "loop" ==> value
        let inline loopDelay value = "loopDelay" ==> value
        match this with
        | Infinite(Option.None) -> [ loop JS.Infinity ]
        | Infinite(Some value) -> [ loop JS.Infinity; loopDelay value  ]
        | Count(value,Option.None) -> [ loop value ]
        | Count(value, Some delay) -> [ loop value; loopDelay delay ]
        | None -> [ loop 0 ]
        |> createObj

[<Erase>]
type AutoPlay =
    | [<CompiledValue(true)>] Auto
    | [<CompiledValue(false)>] Manual
    | OnScroll of ScrollObserver
    member inline this.unwrap = !!this
    
type PlaybackProps = {
    Loop: Loop option
    Reversed: bool option
    Alternating: bool option
    AutoPlay: AutoPlay option
    FrameRate: int<fps> option
    Rate: float option
    Ease: EasingFun option
} with
    static member init = {
        Loop = None; Reversed = None
        Alternating = None; AutoPlay = None
        FrameRate = None
        Rate = None; Ease = None
    }
    member this.toPojo: obj =
        let main = this.Loop |> Option.map _.toPojo |> Option.defaultValue createEmpty
        this.Reversed |> Option.iter (fun value -> main?reversed <- this.Reversed)
        this.Alternating |> Option.iter (fun value -> main?alternate <- this.Alternating)
        this.AutoPlay |> Option.iter (fun value -> main?autoplay <- this.AutoPlay)
        this.FrameRate |> Option.iter (fun value -> main?frameRate <- this.FrameRate)
        this.Rate |> Option.iter (fun value -> main?playbackRate <- this.Rate)
        this.Ease |> Option.iter (fun value -> main?playbackEase <- this.Ease)
        main
    member this.WithReversed value = { this with Reversed = Some value }
    member this.WithLoop value = { this with Loop = Some value }
    member this.WithAlternating value = { this with Alternating = Some value }
    member this.WithAutoPlay value = { this with AutoPlay = Some value }
    member this.WithFrameRate value = { this with FrameRate = Some value }
    member this.WithRate value = { this with Rate = Some value }
    member this.WithEase value = { this with Ease = Some value }

module PlaybackProps =
    let inline withLoop value (builder: PlaybackProps) = builder.WithLoop value
    let inline withReversed value (builder: PlaybackProps) = builder.WithReversed value
    let inline withAlternating value (builder: PlaybackProps) = builder.WithAlternating value
    let inline withAutoPlay value (builder: PlaybackProps) = builder.WithAutoPlay value
    let inline withFrameRate value (builder: PlaybackProps) = builder.WithFrameRate value
    let inline withRate value (builder: PlaybackProps) = builder.WithRate value
    let inline withEase value (builder: PlaybackProps) = builder.WithEase value
    let inline build (builder: PlaybackProps) = builder.toPojo
    
    let loopInfinite = withLoop (Loop.Infinite None)
    let reverse = withReversed true
    let alternate = withAlternating true
    let autoPlay = withAutoPlay AutoPlay.Auto
    let manualPlay = withAutoPlay AutoPlay.Manual
    let scrollPlay observer = withAutoPlay (AutoPlay.OnScroll observer)
    let loopWithDelay value = Some value |> Loop.Infinite |> withLoop
    let loopCount value = (value, None) |> Loop.Count |> withLoop
    let loopCountDelayed count delay = (count, Some delay) |> Loop.Count |> withLoop

[<Erase>]
type private ICallbacks =
    abstract member OnGrab: Callback<obj> option with get
    abstract member OnDrag: Callback<obj> option with get
    abstract member OnRelease: Callback<obj> option with get
    abstract member OnSnap: Callback<obj> option with get
    abstract member OnResize: Callback<obj> option with get
    abstract member OnAfterResize: Callback<obj> option with get
    abstract member OnSettle: Callback<obj> option with get
    abstract member OnBegin: Callback<obj> option with get
    abstract member OnComplete: Callback<obj> option with get
    abstract member OnBeforeUpdate: Callback<obj> option with get
    abstract member OnUpdate: Callback<obj> option with get
    abstract member OnRender: Callback<obj> option with get
    abstract member OnLoop: Callback<obj> option with get
    abstract member OnPause: Callback<obj> option with get
    abstract member Then: (Callback<obj> -> JS.Promise<unit>) option with get

[<Erase>]
type private ICallbacksExtensions =
    [<Extension>]
    static member toPojo(this: ICallbacks): obj =
        let builder: AnimationOptions = createEmpty
        this.OnBegin |> Option.iter (fun value -> builder.onBegin <- value)
        this.OnComplete |> Option.iter (fun value -> builder.onComplete <- value)
        this.OnBeforeUpdate |> Option.iter (fun value -> builder.onBeforeUpdate <- value)
        this.OnUpdate |> Option.iter (fun value -> builder.onUpdate <- value)
        this.OnRender |> Option.iter (fun value -> builder.onRender <- value)
        this.OnLoop |> Option.iter (fun value -> builder.onLoop <- value)
        this.OnPause |> Option.iter (fun value -> builder.onPause <- value)
        this.Then |> Option.iter (fun value -> builder.``then`` <- !!value)
        this.OnGrab |> Option.iter (fun value -> builder?onGrab <- value)
        this.OnDrag |> Option.iter (fun value -> builder?onDrag <- value)
        this.OnRelease |> Option.iter (fun value -> builder?onRelease <- value)
        this.OnSnap |> Option.iter (fun value -> builder?onSnap <- value)
        this.OnResize |> Option.iter (fun value -> builder?onResize <- value)
        this.OnAfterResize |> Option.iter (fun value -> builder?onAfterResize <- value)
        this.OnSettle |> Option.iter (fun value -> builder?onSettle <- value)
        builder

type Callbacks<'T> = {
    OnBegin: Callback<'T> option
    OnComplete: Callback<'T> option
    OnBeforeUpdate: Callback<'T> option
    OnUpdate: Callback<'T> option
    OnRender: Callback<'T> option
    OnLoop: Callback<'T> option
    OnPause: Callback<'T> option
    Then: (Callback<'T> -> JS.Promise<unit>) option
} with
    static member init: Callbacks<'T> = createEmpty |> unbox
    interface ICallbacks with
        member this.OnBegin = this.OnBegin |> unbox
        member this.OnComplete = this.OnComplete |> unbox
        member this.OnBeforeUpdate = this.OnBeforeUpdate |> unbox
        member this.OnUpdate = this.OnUpdate |> unbox
        member this.OnRender = this.OnRender |> unbox
        member this.OnLoop = this.OnLoop |> unbox
        member this.OnPause = this.OnPause |> unbox
        member this.Then = this.Then |> unbox
        member this.OnAfterResize = None
        member this.OnDrag = None
        member this.OnGrab = None
        member this.OnRelease = None
        member this.OnResize = None
        member this.OnSettle = None
        member this.OnSnap = None
    member inline this.toPojo: obj = this.toPojo()
    member inline this.onBegin handler = { this with OnBegin = Some handler }
    member inline this.onComplete handler = { this with OnComplete = Some handler }
    member inline this.onBeforeUpdate handler = { this with OnBeforeUpdate = Some handler }
    member inline this.onUpdate handler = { this with OnUpdate = Some handler }
    member inline this.onRender handler = { this with OnRender = Some handler }
    member inline this.onLoop handler = { this with OnLoop = Some handler }
    member inline this.onPause handler = { this with OnPause = Some handler }
    member inline this.andThen handler = { this with Then = Some handler }

module Callbacks =
    let inline onBegin handler (builder: Callbacks<'T>) = builder.onBegin handler
    let inline onComplete handler (builder: Callbacks<'T>) = builder.onComplete handler
    let inline onBeforeUpdate handler (builder: Callbacks<'T>) = builder.onBeforeUpdate handler
    let inline onUpdate handler (builder: Callbacks<'T>) = builder.onUpdate handler
    let inline onRender handler (builder: Callbacks<'T>) = builder.onRender handler
    let inline onLoop handler (builder: Callbacks<'T>) = builder.onLoop handler
    let inline onPause handler (builder: Callbacks<'T>) = builder.onPause handler
    let inline andThen handler (builder: Callbacks<'T>) = builder.andThen handler
    
    let inline build (builder: Callbacks<'T>) = builder.toPojo

/// <summary>
/// Interface to the AnimeJS Engine.
/// </summary>
[<Erase>]
module Global =
    /// <summary>
    /// Retrieve the Engine using the static member <c>.get</c>.
    /// You can pattern match against, or change this, and then commit it to
    /// the global engine using <c>.commit()</c>. <br/>In doing this, we always
    /// have an F# pattern matchable set of defaults to map against.<br/>
    /// The methods of the <c>engine</c> are employed as static methods.
    /// </summary>
    type Engine = {
        Duration: U2<int<ms>, FunctionValue<int<ms>>>
        Delay: U2<int<ms>, FunctionValue<int<ms>>>
        TimeUnit: TimeUnit
        Speed: float
        Fps: int<fps>
        Precision: int
        PauseOnDocumentHidden: bool
        UseDefaultMainLoop: bool
        PlaybackEase: EasingFun
        PlaybackRate: float
        Loop: Loop
        Alternate: bool
        Reversed: bool
        AutoPlay: bool
        OnBegin: Callback<obj>
        OnUpdate: Callback<obj>
        OnRender: Callback<obj>
        OnLoop: Callback<obj>
        OnComplete: Callback<obj>
        OnPause: Callback<obj>
        Ease: Ease
        Composition: Composition
        Modifier: FloatModifier option
    }
    let mutable private _engine = {
        Delay = !^0<ms>
        Duration = !!JS.Infinity
        TimeUnit = TimeUnit.Ms
        Speed = 1.
        Fps = 120<fps>
        Precision = 4
        PauseOnDocumentHidden = true
        UseDefaultMainLoop = engine.useDefaultMainLoop
        PlaybackEase = !!engine.playbackEase
        PlaybackRate = engine.playbackRate
        Loop = Loop.Count(0,Some 0<ms>)
        Alternate = false
        Reversed = false
        AutoPlay = true
        OnBegin = fun _ -> ()
        OnComplete = fun _ -> ()
        OnUpdate = fun _ -> ()
        OnRender = fun _ -> ()
        OnLoop = fun _ -> ()
        OnPause = fun _ -> ()
        Ease = Ease.Power(EaseDir.Out, 2.)
        Composition = Composition.Replace
        Modifier = None
    }
    
    let inline private ifDifferentThenSome comparator input = if input = comparator then None else Some input
    let inline private mapEngines mapping engine = _engine |> mapping,engine |> mapping
    let inline private mapToOption mapping engine = engine |> mapEngines mapping ||> ifDifferentThenSome
    let private iterOption mapping engineMap localEngine =
        localEngine
        |> mapToOption mapping
        |> Option.iter (fun value -> (engine |> engineMap) <- value)
    let private saveCallbacks localEngine =
        engine.onBegin <- localEngine.OnBegin
        engine.onComplete <- localEngine.OnComplete
        engine.onUpdate <- localEngine.OnUpdate
        engine.onRender <- localEngine.OnRender
        engine.onLoop <- localEngine.OnLoop
        engine.onPause <- localEngine.OnPause
        engine.modifier <- !!localEngine.Modifier
    type Engine with
        static member get with get() = _engine
        static member update() = engine.update() |> ignore
        static member pause() = engine.pause() |> ignore
        static member resume() = engine.resume() |> ignore
        member this.commit() =
            let inline (>->) x y = x |> y; x
            this
            >-> iterOption (_.Duration >> unbox) _.duration
            >-> iterOption (_.Delay >> unbox) _.delay
            >-> iterOption _.TimeUnit _.timeUnit
            >-> iterOption _.Alternate _.alternate
            >-> iterOption _.AutoPlay _.autoplay
            >-> iterOption _.Composition _.composition
            >-> iterOption (_.Ease >> _.ToEasing >> unbox) _.ease
            >-> iterOption _.Fps _.fps
            >-> iterOption _.Precision _.precision
            >-> iterOption _.PauseOnDocumentHidden _.pauseOnDocumentHidden
            >-> iterOption _.Speed _.speed
            >-> iterOption (_.PlaybackEase >> unbox) _.playbackEase
            >-> iterOption _.PlaybackRate _.playbackRate
            >-> iterOption _.Reversed _.reversed
            >-> iterOption _.UseDefaultMainLoop _.useDefaultMainLoop
            >-> (mapToOption _.Loop >> Option.iter (
                fun value ->
                    match value with
                    | Loop.Infinite (Some value) ->
                        engine.loop <- true
                        engine.loopDelay <- !!value
                    | Loop.Infinite _ ->
                        engine.loop <- true
                        engine.loopDelay <- 0.
                    | Loop.Count(count, delay) ->
                        engine.loop <- float count
                        engine.loopDelay <- delay |> Option.map unbox |> Option.defaultValue 0.
                    | Loop.None ->
                        engine.loop <- false
                        engine.loopDelay <- 0.
                ) )
            >-> saveCallbacks
            |> fun local -> _engine <- local
            
            
            


type AnimationBuilder<'T> = {
    Values: Style<'T> list
    Keyframes: KeyframeParam list option
    DefaultTweenProps: AnimationBuilder.TweenProps option
    PlaybackProps: PlaybackProps option
    Callbacks: Callbacks<Animation> option
} with
    static member init: AnimationBuilder<'T> = {
        Values = []; DefaultTweenProps = None
        Keyframes = None
        PlaybackProps = None
        Callbacks = None
    }
    member this.WithDefaultTweenProps value = { this with DefaultTweenProps = Some value }
    member this.WithPlaybackProps value = { this with PlaybackProps = Some value }
    member this.WithCallbacks value = { this with Callbacks = Some value }
    member this.WithValues value = { this with Values = value }
    member this.WithKeyframes values = { this with Keyframes = Some values }
    member this.toPojo: AnimationOptions =
        let props = this.Values |> unbox |> createObj
        let tweenProps = this.DefaultTweenProps |> Option.map _.toPojo |> Option.defaultValue createEmpty
        let playbackProps = this.PlaybackProps |> Option.map _.toPojo |> Option.defaultValue createEmpty
        let callbacks = this.Callbacks |> Option.map _.toPojo |> Option.defaultValue createEmpty
        this.Keyframes |> Option.map (List.map _.toPojo >> List.toArray) |> Option.iter (fun value -> props["keyframes"] <- value)
        emitJsExpr (props, tweenProps, playbackProps, callbacks) "{...$0, ...$1, ...$2, ...$3}"
    member inline this.animate (targets: Targets)=
        animate(targets, this.toPojo)
module Animation =
    module Style =
        let inline unboxValues (builder: AnimationBuilder<_>) = builder.Values |> unbox<(string * PropertyValue) list>
        let inline applyToValues func builder = { builder with Values = builder |> unboxValues |> func }
        let map (mapping: string * PropertyValue -> string * PropertyValue) = applyToValues (List.map mapping >> unbox) 
        let filter (predicate: string * PropertyValue -> bool) = applyToValues (List.filter predicate >> unbox)
        let choose (predicate: string * PropertyValue -> (string * PropertyValue) option) = applyToValues (List.choose predicate >> unbox)
        
        let take (predicate: string * PropertyValue -> bool) builder =
            let result,remaining = builder |> unboxValues |> List.partition predicate
            result,{ builder with Values = remaining |> unbox }
type TimerCallbacks = {
    OnBegin: Callback<Timer> option
    OnComplete: Callback<Timer> option
    OnUpdate: Callback<Timer> option
    OnLoop: Callback<Timer> option
    OnPause: Callback<Timer> option
    Then: (Callback<Timer> -> JS.Promise<unit>) option
} with
    interface ICallbacks with
        member this.OnBeforeUpdate = None
        member this.OnBegin = !!this.OnBegin
        member this.OnComplete = !!this.OnComplete
        member this.OnLoop = !!this.OnLoop
        member this.OnPause = !!this.OnPause
        member this.OnRender = None
        member this.OnUpdate = !!this.OnUpdate
        member this.Then = !!this.Then
        member this.OnAfterResize = None
        member this.OnDrag = None
        member this.OnGrab = None
        member this.OnRelease = None
        member this.OnResize = None
        member this.OnSettle = None
        member this.OnSnap = None
    member inline this.toPojo = this.toPojo()
    member inline this.WithOnBegin value = { this with OnBegin = Some value }
    member inline this.WithOnComplete value = { this with OnComplete = Some value }
    member inline this.WithOnUpdate value = { this with OnUpdate = Some value }
    member inline this.WithOnLoop value = { this with OnLoop = Some value }
    member inline this.WithOnPause value = { this with OnPause = Some value }
    member inline this.WithThen value = { this with Then = Some value }
    

type TimerBuilder =
    {
        Delay: int<ms>
        Duration: int<ms>
        Loop: Loop
        Alternate: bool
        Reversed: bool
        AutoPlay: AutoPlay
        FrameRate: int<fps>
        PlaybackRate: float
        Callbacks: TimerCallbacks
    } with
    static member init =
        let defaults = Global.Engine.get
        {
            Delay = defaults.Delay |> function U2.Case1 value -> value | _ -> 0<ms>
            Duration = defaults.Duration |> function U2.Case1 value -> value | _ -> !!infinity 
            Loop = defaults.Loop
            Alternate = defaults.Alternate
            Reversed = defaults.Reversed
            AutoPlay = if defaults.AutoPlay then AutoPlay.Auto else AutoPlay.Manual
            FrameRate = defaults.Fps
            PlaybackRate = defaults.PlaybackRate
            Callbacks = {
                OnBegin = None
                OnComplete = None
                OnUpdate = None
                OnLoop = None
                OnPause = None
                Then = None
            }
        }
    member this.toPojo with get() =
        let loop = this.Loop.toPojo
        let builder = jsOptions<TimerOptions>(fun builder ->
            builder.delay <- this.Delay
            builder.duration <- this.Duration
            builder.alternate <- this.Alternate
            builder.reversed <- this.Reversed
            builder.autoplay <- this.AutoPlay.unwrap
            builder.frameRate <- this.FrameRate
            builder.playbackRate <- this.PlaybackRate
            )
        let callbacks = this.Callbacks.toPojo
        emitJsExpr (loop, builder, callbacks) "{...$0, ...$1, ...$2}"
    member inline this.createTimer() = createTimer(this.toPojo)

[<Erase>]
type TimeLabel [<Emit("$0")>] (label: string) =
    static member inline create label = TimeLabel(label) 

[<Erase>]
type TimePosition =
    static member inline Absolute (value: int<ms>): TimePosition = value |> unbox
    static member inline Addition (value: int<ms>): TimePosition = $"+={value}" |> unbox
    static member inline Subtraction (value: int<ms>): TimePosition = $"-={value}" |> unbox
    static member inline Multiplier (value: float): TimePosition = $"*={value}" |> unbox
    [<Emit "'<'">]
    static member AfterPrevious: TimePosition = jsNative
    [<Emit "'<<'">]
    static member WithPrevious : TimePosition = jsNative
    static member inline AdditionAfterPrevious value : TimePosition = $"<+={value}" |> unbox
    static member inline SubtractionAfterPrevious value : TimePosition = $"<-={value}" |> unbox
    static member inline MultiplierAfterPrevious value : TimePosition = $"<*={value}" |> unbox
    static member inline AdditionWithPrevious value : TimePosition = $"<<+={value}" |> unbox
    static member inline SubtractionWithPrevious value : TimePosition = $"<<-={value}" |> unbox
    static member inline MultiplierWithPrevious value : TimePosition = $"<<*={value}" |> unbox
    static member inline Label (label: TimeLabel): TimePosition = label |> unbox
    static member inline FunctionValue (handler: FunctionValue<_>): TimePosition = handler |> unbox
    [<Import("stagger", Spec.path); ParamObject(1)>]
    static member Stagger(value: int<ms>, start: TimePosition, ?from: int<ms>, ?reversed: bool, ?ease: EasingFun, ?grid: int<ms> * int<ms>): TimePosition = jsNative
    [<Import("stagger", Spec.path); ParamObject(1)>]
    static member Stagger(value: int<ms>, ?start: int<ms>, ?from: int<ms>, ?reversed: bool, ?ease: EasingFun, ?grid: int<ms> * int<ms>): TimePosition = jsNative

type LabelMap =
    [<EmitIndexer>]
    abstract member Item: TimeLabel -> TimePosition with get,set

[<Erase>]
type Timeline =
    [<EmitMethod("add")>]
    member this._add([<ParamCollection>] values): Timeline= jsNative
    member inline this.add(targets: Targets, animationBuilder: AnimationBuilder<_>, ?position: TimePosition): Timeline = this._add(targets, animationBuilder.toPojo, position)
    member inline this.add(timerParameters: TimerBuilder, ?position: TimePosition) = this._add(timerParameters.toPojo, position)
    member this.sync(synced: Timer, ?position: TimePosition): Timeline = jsNative
    member this.sync(synced: Animation, ?position: TimePosition): Timeline = jsNative
    member this.sync(synced: Timeline, ?position: TimePosition): Timeline = jsNative
    member this.call(handler: Callback<unit>, ?position: TimePosition): Timeline = jsNative
    member this.label(label: TimeLabel, ?position: TimePosition): Timeline = jsNative
    member this.set(target: Targets, properties: obj, ?position: TimePosition): Timeline = jsNative
    member inline this.set(target: Targets, listPropValues: (string * obj) list, ?position: TimePosition): Timeline = this.set(target, createObj listPropValues, ?position = position)
    member this.remove(targets: Targets): Timeline = jsNative
    member this.remove(targets: Targets, propertyName: string): Timeline = jsNative
    member inline this.remove(targets: #CSSStyleDeclaration, propertyMap: #CSSStyleDeclaration -> string): Timeline =
        this.remove(!!targets, Experimental.nameofLambda propertyMap)
    member inline this.remove(targets: #CSSStyleDeclaration[], propertyMap: #CSSStyleDeclaration -> string): Timeline =
        this.remove(!!targets, Experimental.nameofLambda propertyMap)
    member this.remove(object: Animation, ?position: TimePosition): Timeline = jsNative
    member this.remove(object: Timer, ?position: TimePosition): Timeline = jsNative
    member this.remove(object: Timeline, ?position: TimePosition): Timeline = jsNative
    member this.init(): Timeline = jsNative
    member this.play(): Timeline = jsNative
    member this.reverse(): Timeline = jsNative
    member this.pause(): Timeline = jsNative
    member this.restart(): Timeline = jsNative
    member this.alternate(): Timeline = jsNative
    member this.resume(): Timeline = jsNative
    member this.complete(): Timeline = jsNative
    member this.cancel(): Timeline = jsNative
    member this.revert(): Timeline = jsNative
    member this.seek(time: int<ms>, ?muteCallbacks: bool): Timeline = jsNative
    member this.stretch(duration: int<ms>): Timeline = jsNative
    member this.refresh(): Timeline = jsNative
    
    [<Emit("$0")>]
    member this.endChain: unit = jsNative
    member this.duration: int<ms> = JS.undefined
    member this.targets: Targets = JS.undefined
    member this.began
        with get(): bool = false
        and set(value: bool) = ()
    member this.completed
        with get(): bool = JS.undefined
        and set(value: bool) = ()
    member this.currentIteration
        with get(): int = JS.undefined
        and set(value: int) = ()
    member this.currentTime
        with get(): float<ms> = JS.undefined
        and set(value: float<ms>) = ()
    member this.deltaTime
        with get(): float<ms> = JS.undefined
        and set(value: float<ms>) = ()
    member this.fps
        with get(): int<fps> = JS.undefined
        and set(value: int<fps>) = ()
    member this.id
        with get(): obj = JS.undefined
        and set(value: obj) = ()
    member this.iterationCurrentTime
        with get(): float<ms> = JS.undefined
        and set(value: float<ms>) = ()
    member this.iterationProgress
        with get(): float = JS.undefined
        and set(value: float) = ()
    member this.labels
        with get(): LabelMap = JS.undefined
        and set(value: LabelMap) = ()
    member this.paused
        with get(): bool = JS.undefined
        and set(value: bool) = ()
    member this.progress
        with get(): float = JS.undefined
        and set(value: float) = ()
    member this.reversed
        with get(): bool = JS.undefined
        and set(value: bool) = ()
    member this.speed
        with get(): float = JS.undefined
        and set(value: float) = ()

type TimelineBuilder = {
    ChildDefaults: TimerBuilder
    PlaybackProps: PlaybackProps
    Delay: int<ms>
    Callbacks: Callbacks<Timeline>
} with
    static member init = {
        ChildDefaults = TimerBuilder.init
        PlaybackProps = PlaybackProps.init
        Delay = 0<ms>
        Callbacks = Callbacks.init
    }
    member this.toPojo with get() =
        let playback = this.PlaybackProps.toPojo
        let callbacks = this.Callbacks.toPojo
        let builder: TimelineOptions = emitJsExpr (playback, callbacks) "{ ...$0, ...$1 }"
        builder.delay <- this.Delay
        builder.defaults <- !!this.ChildDefaults.toPojo
    
    member inline this.createTimeline(): Timeline =
        this.toPojo |> import "createTimeline" Spec.path

type AnimatableSettings<'Value> = {
    Unit: string
    Duration: U2<int<ms>, FunctionValue<int<ms>>>
    Ease: EasingFun
    Modifier: ('Value -> 'Value)
} with
    static member init= ()
and [<Erase>] PropertyKeyAnimValue<'T> = private PropertyKeyArgType of string * AnimatableSettings<obj> with
    member inline this.Key = unbox<string * obj>(this) |> fst
    member inline this.Matches (key: 'T -> 'Value): bool = Experimental.nameofLambda key = this.Key
    member inline this.Matches(key: PropertyKey<'T, 'Value>): bool = !!key = this.Key
    member inline this.Matches(keyValue: PropertyKeyValue<'T>): bool = keyValue.Key = this.Key
    member inline this.Value (key: 'T -> 'Value) =
        if this.Matches key then
            unbox<string * AnimatableSettings<'Value>>(this) |> snd |> Some
        else None
    member inline this.Value(key: PropertyKey<'T, 'Value>) =
        if this.Matches key then
            unbox<string * AnimatableSettings<'Value>>(this) |> snd |> Some
        else None
    static member inline op_Implicit(other: string * obj): PropertyKeyAnimValue<'T> = PropertyKeyArgType !!other
    static member inline op_Implicit(other: string * AnimatableSettings<_>): PropertyKeyAnimValue<'T> = PropertyKeyArgType !!other
and [<Erase>] PropertyKey<'T, 'Value> = private PropertyKey of string with
    member inline this.Key = unbox<string> this
    static member inline op_Equals(x: PropertyKey<'T, 'Value>, y: string) = !!x = y
    static member inline op_Equals(x: PropertyKey<'T, 'Value>, y: 'T -> 'Value) = Experimental.nameofLambda y = !!x
    static member inline op_MinusMinusGreater(x: PropertyKey<'T, 'Value>, y: 'Value) = (!!x ==> y) |> unbox<PropertyKeyValue<'T>>
    static member inline op_MinusMinusGreater(x: PropertyKey<'T, 'Value>, y: AnimatableSettings<'Value>) = unbox<PropertyKeyAnimValue<'T>>(x,y)
/// <summary>
/// Erased union for an objects key value pair. 
/// </summary>
and [<Erase>] PropertyKeyValue<'T> = private PropertyKeyValue of string * obj with
    /// <summary>
    /// Retrieves the property name/key
    /// </summary>
    member inline this.Key = unbox<string * obj>(this) |> fst
    /// <summary>
    /// Tests if this key value pair matches the property defined by the access path provided
    /// </summary>
    /// <param name="key">Property key represented by the access path of the object to the property</param>
    member inline this.Matches (key: 'T -> 'Value): bool = Experimental.nameofLambda key = this.Key
    /// <summary>
    /// Retrieves the value of the property if it matches the key mapping provided (as it infers the type of the
    /// value and simultaneously tests if it's the requested property)
    /// </summary>
    /// <param name="key"></param>
    member inline this.Value (key: 'T -> 'Value) =
        if this.Matches(key)
        then Some (this |> unbox<string * 'Value> |> snd)
        else None
    static member inline makeKey(handler: 'T -> 'Value): PropertyKey<'T, 'Value> = Experimental.nameofLambda handler |> unbox
    static member inline make(handler: 'T -> 'Value): PropertyKey<'T, 'Value> = Experimental.nameofLambda handler |> unbox

type Animatable<'T> =
    [<Emit("$0[$1]()")>]
    member this.get (value: string): U2<float, float[]> = jsNative
    member inline this.get (handler: 'T -> obj): U2<float, float[]> = Experimental.nameofLambda handler |> this.get
    member inline this.get (handler: CSSStyleDeclaration -> string): U2<float, float[]> = Experimental.nameofLambda handler |> this.get
    [<Emit("$0[$1]($2, ...[$3, $4])")>]
    member this.set (value: string, setValue: U2<float, float[]>, ?duration: int<ms>, ?easing: EasingFun): Animatable<'T> = jsNative
    member inline this.set (handler: 'T -> obj, setValue: U2<float, float[]>, ?duration: int<ms>, ?easing: EasingFun): Animatable<'T> = this.set(Experimental.nameofLambda handler, setValue, ?duration=duration, ?easing=easing)
    member inline this.set (handler: CSSStyleDeclaration -> string, setValue: U2<float, float[]>, ?duration: int<ms>, ?easing: EasingFun): Animatable<'T> = this.set(Experimental.nameofLambda handler, setValue, ?duration=duration, ?easing=easing)
    member this.revert(): Animatable<'T> = JS.undefined
    [<EmitProperty "targets">]
    member this.targets: Target[] = jsNative
    [<EmitProperty "animations">]
    member this.animations: Animation[] = jsNative
type AnimatableBuilder<'T> = {
    Properties: PropertyKeyAnimValue<'T> list
    Unit: string option
    Duration: U2<int<ms>, FunctionValue<int<ms>>> option
    Ease: EasingFun option
    Modifier: FloatModifier option
} with
    static member init: AnimatableBuilder<'T> = {
        Properties = []
        Unit = None
        Duration = None
        Ease = None
        Modifier = None
    }
    member this.toPojo with get() =
        let properties: AnimatableOptions = !!this.Properties |> !!createObj
        this.Unit |> Option.iter (fun value -> properties.unit <- value)
        this.Duration |> Option.iter (fun value -> properties.duration <- value)
        this.Ease |> Option.iter (fun value -> properties.ease <- !!value)
        this.Modifier |> Option.iter (fun value -> properties.modifier <- value)
        properties
    member inline this.createAnimatable (targets: 'T)=
        createAnimatable(!!targets, this.toPojo)
    member inline this.createAnimatable (targets: 'T[])=
        createAnimatable(!!targets, this.toPojo)
    member inline this.createAnimatable (targets: Targets)=
        createAnimatable(targets, this.toPojo)
type AnimatableBuilder = AnimatableBuilder<obj>

module AnimatableBuilder =
    let inline addProperty ([<InlineIfLambda>] handler: 'T -> 'Value) value (builder: AnimatableBuilder<'T>) =
        {
            builder with
                Properties = (
                    PropertyKeyValue.make handler --> value
                ) :: builder.Properties
        }
    let inline addGeneric ([<InlineIfLambda>] handler: CSSStyleDeclaration -> _) value (builder: AnimatableBuilder<'T>) = {
        builder with
            Properties = (
                PropertyKeyValue.make handler --> value
            ) :: builder.Properties
    }
    let inline withProperties values (builder: AnimatableBuilder<'T>) = {
        builder with Properties = values
    }
    let inline withUnit value (builder: AnimatableBuilder<'T>) = { builder with Unit = value }
    let inline withDuration value (builder: AnimatableBuilder<'T>) = { builder with Duration = value }
    let inline withEase value (builder: AnimatableBuilder<'T>) = { builder with Ease = value }
    let inline withModifier value (builder: AnimatableBuilder<'T>) = { builder with Modifier = value }

module Draggable =

    [<Erase>]
    type Snap =
        | Absolute of int<px>
        | Array of int<px>[]
        | Function of FunctionValue<U2<int<px>, int<px>[]>>
        member inline this.unwrap = unbox<int<px>> this

    type AxisConfig = {
        Snap: Snap option
        Modifier: FloatModifier option
        Mapping: string option
    } with
        static member init with get() = {
            Snap = None
            Modifier = None
            Mapping = None
        }
        member this.toPojo =
            let builder = createEmpty<DraggableOptions>
            this.Snap |> Option.iter (fun value -> builder.snap <- !!value)
            this.Modifier |> Option.iter (fun value -> builder.modifier <- value)
            this.Mapping |> Option.iter (fun value -> builder.mapTo <- value)
            builder
            
    type ContainerConfig = {
        Container: U4<Selector, HTMLElement, DraggableBounds, FunctionValue<DraggableBounds>> option
        Padding: U3<float<px>, DraggableBounds, FunctionValue<DraggableBounds>>
        Friction: float
    } with
        static member init with get() = {
            Container = None
            Padding = !^0.<px>
            Friction = 0.8
        }
        member this.toPojo with get() =
            let builder = jsOptions<DraggableOptions> (fun builder ->
                builder.containerPadding <- !!this.Padding
                builder.containerFriction <- this.Friction
                )
            this.Container |> Option.iter (fun value -> builder.container <- !!value)
            builder
        member inline this.WithContainer (value: Selector) = { this with Container = Some !^value } 
        member inline this.WithContainer (value: #HTMLElement) = { this with Container = Some !!value } 
        member inline this.WithContainer (value: DraggableBounds) = { this with Container = Some !^value } 
        member inline this.WithContainer (value: FunctionValue<DraggableBounds>) = { this with Container = Some !^value }
        member inline this.WithPadding (value: float) = { this with Padding = !!value }
        member inline this.WithPadding (value: int) = { this with Padding = !!value }
        member inline this.WithPadding (value: DraggableBounds) = { this with Padding = !^value }
        member inline this.WithPadding (value: FunctionValue<DraggableBounds>) = { this with Padding = !^value }
        member inline this.WithFriction value = { this with Friction = value }
    type ReleaseConfig = {
        ContainerFriction: U2<float, FunctionValue<float>> option
        Mass: U2<float, FunctionValue<float>>
        Stiffness: U2<float, FunctionValue<float>>
        Damping: U2<float, FunctionValue<float>>
        Ease: Ease
    } with
        static member init with get() = {
            ContainerFriction = None
            Mass = !^1.
            Stiffness = !^80.
            Damping = !^10.
            Ease = EaseDir.Out |> Ease.Quint
        }
        member this.toPojo with get() =
            let builder = jsOptions<DraggableOptions> (fun builder ->
                    builder.releaseMass <- !!this.Mass
                    builder.releaseStiffness <- !!this.Stiffness
                    builder.releaseDamping <- !!this.Damping
                    builder.releaseEase <- !!this.Ease.ToEasing
                )
            this.ContainerFriction |> Option.iter (fun value -> builder.releaseContainerFriction <- !!value)
            builder
    
    type VelocityConfig = {
        Min: U2<float, FunctionValue<float>>
        Max: U2<float, FunctionValue<float>>
        Multiplier: U2<float, FunctionValue<float>>
        DragSpeed: U2<float, FunctionValue<float>>
    } with
        static member init with get() = {
            Min = !^0.
            Max = !^50.
            Multiplier = !^1.
            DragSpeed = !^1.
        }
        member this.toPojo with get() =
            jsOptions<DraggableOptions>(fun builder ->
                builder.minVelocity <- this.Min |> unbox
                builder.maxVelocity <- this.Max |> unbox
                builder.velocityMultiplier <- this.Multiplier |> unbox
                builder.dragSpeed <- this.DragSpeed |> unbox
                )
    type ScrollConfig = {
        Threshold: U2<float, FunctionValue<float>>
        Speed: U2<float, FunctionValue<float>>
    } with
        static member init with get() = {
            Threshold = !^20.
            Speed = !^1.5
        }
        member this.toPojo with get() =
            jsOptions<DraggableOptions>(fun builder ->
                builder.scrollSpeed <- !!this.Speed
                builder.scrollThreshold <- !!this.Threshold)
            
    
    type Cursor =
        | Disabled
        | OnHover of string
        | OnGrab of string
        | OnValues of onHover: string * onGrab: string
        | Function of (unit -> Cursor)
        member inline this.unwrap =
            match this with
            | Disabled -> box false
            | OnValues(onHover,onGrab) ->
                createObj [
                    "onHover" ==> onHover
                    "onGrab" ==> onGrab
                ]
            | OnHover onHover -> createObj [ "onHover" ==> onHover ]
            | OnGrab onGrab -> createObj [ "onGrab" ==> onGrab ]
            | Function value -> box value
    type AxisParameter =
        | Enabled
        | Disabled
        | Config of AxisConfig
        member inline this.unwrap =
            match this with
            | Enabled -> box true
            | Disabled -> box false
            | Config config -> config.toPojo

    type Callbacks = {
        OnGrab: Callback<Draggable> option
        OnDrag: Callback<Draggable> option
        OnRelease: Callback<Draggable> option
        OnSnap: Callback<Draggable> option
        OnResize: Callback<Draggable> option
        OnAfterResize: Callback<Draggable> option
        OnSettle: Callback<Draggable> option
        OnUpdate: Callback<Draggable> option
    } with
        static member init with get() = {
            OnGrab = None
            OnDrag = None
            OnRelease = None
            OnSnap = None
            OnResize = None
            OnAfterResize = None
            OnSettle = None
            OnUpdate = None
        }
        member this.WithOnDrag handler = { this with OnDrag = Some handler }
        member this.WithOnGrab handler = { this with OnGrab = Some handler }
        member this.WithOnRelease handler = { this with OnRelease = Some handler }
        member this.WithOnSnap handler = { this with OnSnap = Some handler }
        member this.WithOnResize handler = { this with OnResize = Some handler }
        member this.WithOnAfterResize handler = { this with OnAfterResize = Some handler }
        member this.WithOnSettle handler = { this with OnSettle = Some handler }
        member this.WithOnUpdate handler = { this with OnUpdate = Some handler }
        interface ICallbacks with
            member this.OnAfterResize = !!this.OnAfterResize
            member this.OnBeforeUpdate = None
            member this.OnBegin = None
            member this.OnComplete = None
            member this.OnDrag = !!this.OnDrag
            member this.OnGrab = !!this.OnGrab
            member this.OnLoop = None
            member this.OnPause = None
            member this.OnRelease = !!this.OnRelease
            member this.OnRender = None
            member this.OnResize = !!this.OnResize
            member this.OnSettle = !!this.OnSettle
            member this.OnSnap = !!this.OnSnap
            member this.OnUpdate = !!this.OnUpdate
            member this.Then = None
        member this.toPojo with get() = this.toPojo()

type DraggableBuilder = {
    X: Draggable.AxisParameter option
    Y: Draggable.AxisParameter option
    AxisDefaults: Draggable.AxisConfig option
    ContainerConfig: Draggable.ContainerConfig
    ReleaseConfig: Draggable.ReleaseConfig
    VelocityConfig: Draggable.VelocityConfig
    Callbacks: Draggable.Callbacks
    ScrollConfig: Draggable.ScrollConfig
    Cursor: Draggable.Cursor
} with
    static member init with get() = {
        X = None; Y = None; AxisDefaults = None
        ContainerConfig = Draggable.ContainerConfig.init
        ReleaseConfig = Draggable.ReleaseConfig.init
        VelocityConfig = Draggable.VelocityConfig.init
        Callbacks = Draggable.Callbacks.init
        ScrollConfig = Draggable.ScrollConfig.init
        Cursor = Draggable.Cursor.OnValues("grab","grabbing")
    }
    member this.toPojo with get() =
        let container = this.ContainerConfig.toPojo
        let release = this.ReleaseConfig.toPojo
        let velocity = this.VelocityConfig.toPojo
        let callbacks = this.Callbacks.toPojo
        let scroll = this.ScrollConfig.toPojo
        let defaults = this.AxisDefaults |> Option.map _.toPojo |> Option.defaultValue createEmpty
        let builder: DraggableOptions =
            emitJsExpr (
                container,
                release,
                velocity,
                callbacks,
                scroll,
                defaults
            ) "{ ...$0, ...$1, ...$2, ...$3, ...$4, ...$5 }"
        builder?cursor <- this.Cursor.unwrap
        this.X |> Option.iter (fun value -> builder.x <- !!value.unwrap )
        this.Y |> Option.iter (fun value -> builder.y <- !!value.unwrap)
        builder
    member inline this.createDraggable (targets: Targets): Draggable =
        let inline createDraggable(targets, options) = import "createDraggable" Spec.path
        createDraggable(targets,this.toPojo)
    
[<Erase; AutoOpen>]
type Exports =
    [<ImportMember(Spec.path)>]
    static member cleanInlineStyles(renderable: 'T): 'T = jsNative
    [<ImportMember(Spec.path)>]
    static member random(min: 'a when 'a: unmanaged,max: 'b when 'b:unmanaged,?decimalLength:'c when 'c:unmanaged): float = jsNative
    [<ImportMember(Spec.path)>]
    [<Import("randomPick", "animejs")>]
    static member randomPick (items: string): char = nativeOnly
    [<Import("randomPick", "animejs")>]
    static member randomPick (items: 'T[]): 'T = nativeOnly
    [<Import("shuffle", "animejs")>]
    static member shuffle (items: 'T[]): 'T[] = nativeOnly

