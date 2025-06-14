module Partas.AnimeJs.FSharp

open Partas.AnimeJs.Binding
open Fable.Core
open Fable.Core.JsInterop
open Partas.AnimeJs.Style
open Partas.AnimeJs.Core

#nowarn 1182

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
    
[<Interface>]
type Style<'T> =
    inherit CssStyle<U3<float, string, TweenValue>, Style<'T>>
    static member inline translateX(value: U3<TweenValue, float<px>, string>): Style<'T> = "translateX" ==>! value
    static member inline translateY (value: U3<TweenValue, float<px>, string>): Style<'T> = "translateY" ==>! value
    static member inline translateZ (value: U3<TweenValue, float<px>, string>): Style<'T> = "translateZ" ==>! value
    static member inline rotate (value: U3<TweenValue, float<deg>, string>): Style<'T> = "rotate" ==>! value
    static member inline rotateX (value: U3<TweenValue, float<deg>, string>): Style<'T> = "rotateX" ==>! value
    static member inline rotateY (value: U3<TweenValue, float<deg>, string>): Style<'T> = "rotateY" ==>! value
    static member inline rotateZ (value: U3<TweenValue, float<deg>, string>): Style<'T> = "rotateZ" ==>! value
    static member inline scale (value: U3<TweenValue, float, string>): Style<'T> = "scale" ==>! value
    static member inline scaleX (value: U3<TweenValue, float, string>): Style<'T> = "scaleX" ==>! value
    static member inline scaleY (value: U3<TweenValue, float, string>): Style<'T> = "scaleY" ==>! value
    static member inline scaleZ (value: U3<TweenValue, float, string>): Style<'T> = "scaleZ" ==>! value
    static member inline skew (value: U3<TweenValue, float<deg>, string>): Style<'T> = "skew" ==>! value
    static member inline skewX (value: U3<TweenValue, float<deg>, string>): Style<'T> = "skewX" ==>! value
    static member inline skewY (value: U3<TweenValue, float<deg>, string>): Style<'T> = "skewY" ==>! value
    static member inline perspective (value: U3<TweenValue, float<px>, string>): Style<'T> = "perspective" ==>! value
    /// <summary>
    /// CAUTION: <ul>
    /// <li>Safe to use with Records</li>
    /// <li>Only use with Classes if you define the property using <c>val mutable</c> or it is in the
    /// primary constructor.</li>
    /// </ul>
    /// </summary>
    /// <param name="mapping"></param>
    /// <param name="value"></param>
    static member inline property<'U> (mapping: 'T -> 'U) (value: U2<'U, TweenValue>): Style<'T> = Experimental.nameofLambda mapping ==>! value
    static member inline variable (name: string) (value: U2<obj, TweenValue>): Style<'T> = name ==>! value

module AnimationBuilder =
    type TweenProps = {
        Delay: U2<int<ms>, FunctionValue<int<ms>>> option
        Duration: U2<int<ms>, FunctionValue<int<ms>>> option
        Ease: EasingFun option
        Composition: Composition option
        Modifier: TweenParamModifier option
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
        member this.toPojo =
            [
                if this.Delay.IsSome then "delay" ==> this.Delay
                if this.Duration.IsSome then "duration" ==> this.Duration
                if this.Ease.IsSome then "ease" ==> this.Ease
                if this.Composition.IsSome then "composition" ==> this.Composition
                if this.Modifier.IsSome then "modifier" ==> this.Modifier
            ] |> createObj

type LoopDelay = int<ms> option

[<RequireQualifiedAccess>]
type Loop =
    | Infinite of delay: LoopDelay
    | Count of count: int * delay: LoopDelay
    member this.toPojo =
        let inline loop value = "loop" ==> value
        let inline loopDelay value = "loopDelay" ==> value
        match this with
        | Infinite(None) -> [ loop JS.Infinity ]
        | Infinite(Some value) -> [ loop JS.Infinity; loopDelay value  ]
        | Count(value,None) -> [ loop value ]
        | Count(value, Some delay) -> [ loop value; loopDelay delay ]
        |> createObj

[<Erase>]
type AutoPlay =
    | [<CompiledValue(true)>] Auto
    | [<CompiledValue(false)>] Manual
    | OnScroll of ScrollObserver
    
type PlaybackProps = {
    Loop: Loop option
    Reversed: bool
    Alternating: bool
    AutoPlay: AutoPlay
    FrameRate: int<fps>
    Rate: float
    Ease: EasingFun
} with
    member this.toPojo: obj =
        let loop = this.Loop |> Option.map _.toPojo |> Option.defaultValue createEmpty
        let main = jsOptions<AnimationOptions> (fun o ->
            o.alternate <- this.Alternating
            o.reversed <- this.Reversed
            o.autoplay <- !!this.AutoPlay
            o.playbackRate <- this.Rate
            o.playbackEase <- !!this.Ease
            )
        ignore (loop,main)
        emitJsExpr (loop, main) "{...$0, ...$1}"

type Callbacks<'T> = {
    OnBegin: Callback<'T>
    OnComplete: Callback<'T>
    OnBeforeUpdate: Callback<'T>
    OnUpdate: Callback<'T>
    OnRender: Callback<'T>
    OnLoop: Callback<'T>
    OnPause: Callback<'T>
    Then: Callback<'T> -> JS.Promise<unit>
} with
    static member init: Callbacks<'T> = createEmpty |> unbox
    member this.toPojo: obj =
        jsOptions<AnimationOptions>(fun o ->
            o.onBegin <- !!this.OnBegin
            o.onComplete <- !!this.OnComplete
            o.onBeforeUpdate <- !!this.OnBeforeUpdate
            o.onUpdate <- !!this.OnUpdate
            o.onRender <- !!this.OnRender
            o.onLoop <- !!this.OnLoop
            o.onPause <- !!this.OnPause
            o.``then`` <- !!this.Then
            )
    member inline this.onBegin handler = { this with OnBegin = handler }
    member inline this.onComplete handler = { this with OnComplete = handler }
    member inline this.onBeforeUpdate handler = { this with OnBeforeUpdate = handler }
    member inline this.onUpdate handler = { this with OnUpdate = handler }
    member inline this.onRender handler = { this with OnRender = handler }
    member inline this.onLoop handler = { this with OnLoop = handler }
    member inline this.onPause handler = { this with OnPause = handler }
    member inline this.andThen handler = { this with Then = handler }
    
type AnimationBuilder<'T> = {
    Values: Style<'T> list
    DefaultTweenProps: AnimationBuilder.TweenProps option
    PlaybackProps: PlaybackProps option
    Callbacks: Callbacks<Animation> option
} with
    static member init: AnimationBuilder<'T> = createEmpty 
    member this.toPojo: AnimationOptions =
        let props = this.Values |> unbox |> createObj
        let tweenProps = this.DefaultTweenProps |> Option.map _.toPojo |> Option.defaultValue createEmpty
        let playbackProps = this.PlaybackProps |> Option.map _.toPojo |> Option.defaultValue createEmpty
        let callbacks = this.Callbacks |> Option.map _.toPojo |> Option.defaultValue createEmpty
        ignore (props,tweenProps,playbackProps,callbacks)
        emitJsExpr (props, tweenProps, playbackProps, callbacks) "{...$0, ...$1, ...$2, ...$3}"
