module Partas.AnimeJs.FSharp

open System.Runtime.CompilerServices
open Browser.Types
open Partas.AnimeJs.Binding
open Fable.Core
open Fable.Core.JsInterop
open Partas.AnimeJs.Style
open Partas.AnimeJs.Core
open Partas.Solid.Experimental.U

#nowarn 1182

[<AutoOpen; Erase>]
module internal Internal =
    [<ImportMember(Spec.path)>]
    let engine: Engine = jsNative
module UnsafeOperators =
    let inline (==<) x y: 'T = unbox (x,y)
open UnsafeOperators

/// <summary>
/// Access to AnimeJs methods and utils
/// </summary>
[<Erase>]
type AnimeJs = interface end

(*
Distinguish interface values from properties.
Properties are valid in list form.
Values are valid only as a parameter
*)

/// <summary>
/// Allows access to the spring properties
/// </summary>
[<Erase>]
type ISpringProp = interface end
/// <summary>
/// Signifies a key value pair that is accepted as an animatable prop.<br/>
/// Use AnimatedProp.make to create generic ones
/// </summary>
type [<Erase; Interface>] IAnimatedProp<'T> =
    inherit ICssStyle<'T>
    // static member inline op_Implicit(from: IAnimatedProp): #IAnimatedProp<'T> = !!from
    static member inline op_Implicit(from: string * 'T): #IAnimatedProp<'T> = !!from
    // static member inline op_Implicit(from: string * obj): #IAnimatedProp<string> = !!from
type [<Erase; Interface>] IAnimatedPropObj<'PropType> =
    inherit ICssStyleObj<'PropType>
[<Erase; Interface>]
type IAnimatedProp =
    inherit IAnimatedProp<string>
    inherit ICssStyle


[<Erase; Interface>]
type Style<'T> =
    inherit CssStyle<'T>
    static member inline translateX(value: 'T): #IAnimatedProp<'T> = "translateX" ==< value
    static member inline translateY (value: 'T): #IAnimatedProp<'T> = "translateY" ==< value
    static member inline translateZ (value: 'T): #IAnimatedProp<'T> = "translateZ" ==< value
    static member inline rotate (value: 'T): #IAnimatedProp<'T> = "rotate" ==< value
    static member inline rotateX (value: 'T): #IAnimatedProp<'T> = "rotateX" ==< value
    static member inline rotateY (value: 'T): #IAnimatedProp<'T> = "rotateY" ==< value
    static member inline rotateZ (value: 'T): #IAnimatedProp<'T> = "rotateZ" ==< value
    static member inline scale (value: 'T): #IAnimatedProp<'T> = "scale" ==< value
    static member inline scaleX (value: 'T): #IAnimatedProp<'T> = "scaleX" ==< value
    static member inline scaleY (value: 'T): #IAnimatedProp<'T> = "scaleY" ==< value
    static member inline scaleZ (value: 'T): #IAnimatedProp<'T> = "scaleZ" ==< value
    static member inline skew (value: 'T): #IAnimatedProp<'T> = "skew" ==< value
    static member inline skewX (value: 'T): #IAnimatedProp<'T> = "skewX" ==< value
    static member inline skewY (value: 'T): #IAnimatedProp<'T> = "skewY" ==< value
    static member inline perspective (value: 'T): #IAnimatedProp<'T> = "perspective" ==< value
    static member inline draw(value: 'T): #IAnimatedProp<'T> = "draw" ==< value

[<Interface>]
type StyleObj<'PropType> =
    inherit CssStyleObj<'PropType>
    static member inline translateX(value: 'PropType list): #IAnimatedPropObj<'PropType> = "translateX" ==< createObj !!value
    static member inline translateY (value: 'PropType list): #IAnimatedPropObj<'PropType> = "translateY" ==< createObj !!value
    static member inline translateZ (value: 'PropType list): #IAnimatedPropObj<'PropType> = "translateZ" ==< createObj !!value
    static member inline rotate (value: 'PropType list): #IAnimatedPropObj<'PropType> = "rotate" ==< createObj !!value
    static member inline rotateX (value: 'PropType list): #IAnimatedPropObj<'PropType> = "rotateX" ==< createObj !!value
    static member inline rotateY (value: 'PropType list): #IAnimatedPropObj<'PropType> = "rotateY" ==< createObj !!value
    static member inline rotateZ (value: 'PropType list): #IAnimatedPropObj<'PropType> = "rotateZ" ==< createObj !!value
    static member inline scale (value: 'PropType list): #IAnimatedPropObj<'PropType> = "scale" ==< createObj !!value
    static member inline scaleX (value: 'PropType list): #IAnimatedPropObj<'PropType> = "scaleX" ==< createObj !!value
    static member inline scaleY (value: 'PropType list): #IAnimatedPropObj<'PropType> = "scaleY" ==< createObj !!value
    static member inline scaleZ (value: 'PropType list): #IAnimatedPropObj<'PropType> = "scaleZ" ==< createObj !!value
    static member inline skew (value: 'PropType list): #IAnimatedPropObj<'PropType> = "skew" ==< createObj !!value
    static member inline skewX (value: 'PropType list): #IAnimatedPropObj<'PropType> = "skewX" ==< createObj !!value
    static member inline skewY (value: 'PropType list): #IAnimatedPropObj<'PropType> = "skewY" ==< createObj !!value
    static member inline perspective (value: 'PropType list): #IAnimatedPropObj<'PropType> = "perspective" ==< createObj !!value
    static member inline draw(value: 'PropType list): #IAnimatedPropObj<'PropType> = "draw" ==< createObj !!value

[<Interface>]
type Style =
    inherit CssStyle
    static member inline translateX(value: int): #IAnimatedProp = "translateX" ==< value
    static member inline translateX(value: float): #IAnimatedProp = "translateX" ==< value
    static member inline translateX(value: string): #IAnimatedProp = "translateX" ==< value
    static member inline translateY (value: int): #IAnimatedProp = "translateY" ==< value
    static member inline translateY (value: float): #IAnimatedProp = "translateY" ==< value
    static member inline translateY (value: string): #IAnimatedProp = "translateY" ==< value
    static member inline translateZ (value: int): #IAnimatedProp = "translateZ" ==< value
    static member inline translateZ (value: float): #IAnimatedProp = "translateZ" ==< value
    static member inline translateZ (value: string): #IAnimatedProp = "translateZ" ==< value
    static member inline rotate (value: int): #IAnimatedProp = "rotate" ==< value
    static member inline rotate (value: float): #IAnimatedProp = "rotate" ==< value
    static member inline rotate (value: string): #IAnimatedProp = "rotate" ==< value
    static member inline rotateX (value: int): #IAnimatedProp = "rotateX" ==< value
    static member inline rotateX (value: float): #IAnimatedProp = "rotateX" ==< value
    static member inline rotateX (value: string): #IAnimatedProp = "rotateX" ==< value
    static member inline rotateY (value: int): #IAnimatedProp = "rotateY" ==< value
    static member inline rotateY (value: float): #IAnimatedProp = "rotateY" ==< value
    static member inline rotateY (value: string): #IAnimatedProp = "rotateY" ==< value
    static member inline rotateZ (value: int): #IAnimatedProp = "rotateZ" ==< value
    static member inline rotateZ (value: float): #IAnimatedProp = "rotateZ" ==< value
    static member inline rotateZ (value: string): #IAnimatedProp = "rotateZ" ==< value
    static member inline scale (value: int): #IAnimatedProp = "scale" ==< value
    static member inline scale (value: float): #IAnimatedProp = "scale" ==< value
    static member inline scale (value: string): #IAnimatedProp = "scale" ==< value
    static member inline scaleX (value: int): #IAnimatedProp = "scaleX" ==< value
    static member inline scaleX (value: float): #IAnimatedProp = "scaleX" ==< value
    static member inline scaleX (value: string): #IAnimatedProp = "scaleX" ==< value
    static member inline scaleY (value: int): #IAnimatedProp = "scaleY" ==< value
    static member inline scaleY (value: float): #IAnimatedProp = "scaleY" ==< value
    static member inline scaleY (value: string): #IAnimatedProp = "scaleY" ==< value
    static member inline scaleZ (value: int): #IAnimatedProp = "scaleZ" ==< value
    static member inline scaleZ (value: float): #IAnimatedProp = "scaleZ" ==< value
    static member inline scaleZ (value: string): #IAnimatedProp = "scaleZ" ==< value
    static member inline skew (value: int): #IAnimatedProp = "skew" ==< value
    static member inline skew (value: float): #IAnimatedProp = "skew" ==< value
    static member inline skew (value: string): #IAnimatedProp = "skew" ==< value
    static member inline skewX (value: int): #IAnimatedProp = "skewX" ==< value
    static member inline skewX (value: float): #IAnimatedProp = "skewX" ==< value
    static member inline skewX (value: string): #IAnimatedProp = "skewX" ==< value
    static member inline skewY (value: int): #IAnimatedProp = "skewY" ==< value
    static member inline skewY (value: float): #IAnimatedProp = "skewY" ==< value
    static member inline skewY (value: string): #IAnimatedProp = "skewY" ==< value
    static member inline perspective (value: int): #IAnimatedProp = "perspective" ==< value
    static member inline perspective (value: float): #IAnimatedProp = "perspective" ==< value
    static member inline perspective (value: string): #IAnimatedProp = "perspective" ==< value
    static member inline draw(value: float): #IAnimatedProp = "draw" ==< $"{value}"
    static member inline draw(value: float * float): #IAnimatedProp = "draw" ==< $"{fst value} {snd value}"
    static member inline draw(value: string * float): #IAnimatedProp = "draw" ==< $"{fst value} {snd value}"
    static member inline draw(value: string * string): #IAnimatedProp = "draw" ==< $"{fst value} {snd value}"
    static member inline draw(value: string): #IAnimatedProp = "draw" ==< value

[<Interface>]
type AnimatedProp =
    static member inline make (key: string, value: 'T): #IAnimatedProp<'T> = !!(key ==> value)
    static member inline make (key: string, value: obj): #IAnimatedProp<string> = !!(key ==> value)

[<Erase>]
type Spring =
    static member inline mass (value: float): #ISpringProp = nameof Spring.mass ==< value
    static member inline stiffness (value: float): #ISpringProp = nameof Spring.stiffness ==< value
    static member inline damping (value: float): #ISpringProp = nameof Spring.damping ==< value
    static member inline velocity (value: float): #ISpringProp = nameof Spring.damping ==< value

type AnimeJs with
    static member inline createSpring (values: ISpringProp list): Spring = (createObj !!values) |> import "createSpring" "animejs"

    
/// <summary>
/// To pass a custom easing function to AnimeJS, use the Ease.Function discriminated union
/// </summary>
[<Interface>]
type EasingFun =
    static member inline op_Implicit(other: Ease): EasingFun = !!other
    static member inline op_Implicit(other: FloatModifier): EasingFun = !!other
    static member inline from(easingFn: (float -> float)): EasingFun = unbox easingFn
    
and [<RequireQualifiedAccess>]
    [<StringEnum>]
     Ease =
    | Linear
    | ``Linear(x1, x2 25%, x3)``
    | In
    | Out
    | InOut
    | InQuad
    | OutQuad
    | InOutQuad
    | InCubic
    | OutCubic
    | InOutCubic
    | InQuart
    | OutQuart
    | InOutQuart
    | InQuint
    | OutQuint
    | InOutQuint
    | InSine
    | OutSine
    | InOutSine
    | InCirc
    | OutCirc
    | InOutCirc
    | InExpo
    | OutExpo
    | InOutExpo
    | InBounce
    | OutBounce
    | InOutBounce
    | InBack
    | OutBack
    | InOutBack
    | InElastic
    | OutElastic
    | InOutElastic
    | Irregular
    | CubicBezier
    | Steps
    | [<CompiledName("in(p = 1.675)")>] ``In(p = 1_675)``
    | [<CompiledName("out(p = 1.675)")>] ``Out(p = 1_675)``
    | [<CompiledName("inOut(p = 1.675)")>] ``InOut(p = 1_675)``
    | [<CompiledName("inBack(overshoot = 1.70158)")>] ``InBack(overshoot = 1_70158)``
    | [<CompiledName("outBack(overshoot = 1.70158)")>] ``OutBack(overshoot = 1_70158)``
    | [<CompiledName("inOutBack(overshoot = 1.70158)")>] ``InOutBack(overshoot = 1_70158)``
    | [<CompiledName("inElastic(amplitude = 1, period = .3)")>] ``InElastic(amplitude = 1, period = _3)``
    | [<CompiledName("outElastic(amplitude = 1, period = .3)")>] ``OutElastic(amplitude = 1, period = _3)``
    | [<CompiledName("inOutElastic(amplitude = 1, period = .3)")>] ``InOutElastic(amplitude = 1, period = _3)``
    | ``Irregular(length = 10, randomness = 1)``
    | ``CubicBezier(x1, y1, x2, y2)``
    | ``Steps(steps = 10)``
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
    static member inline createSpring (spring: ISpringProp list): EasingFun = spring |> AnimeJs.createSpring |> unbox
/// <summary>
/// Indicates a type accepts TweenProp members
/// </summary>
[<Interface>]
type ITweenProp = interface end

[<Interface>]
type TweenProp =
    static member inline delay(delay: int): #ITweenProp = nameof delay ==< delay
    static member inline delay(delay: FunctionValue<int>): #ITweenProp = nameof delay ==< delay
    static member inline delay(delay: FunctionValue): #ITweenProp = nameof delay ==< delay
    static member inline duration(duration: int): #ITweenProp = nameof duration ==< duration
    static member inline duration(duration: FunctionValue<int>): #ITweenProp = nameof duration ==< duration
    static member inline duration(duration: FunctionValue): #ITweenProp = nameof duration ==< duration
    static member inline ease(ease: EasingFun): #ITweenProp = nameof TweenProp.ease ==< ease
    static member inline composition(value: Composition): #ITweenProp = nameof TweenProp.composition ==< value
    static member inline modifier(value: FloatModifier): #ITweenProp = nameof TweenProp.modifier ==< value

/// <summary>
/// Indicates that RelativeTweenValue helpers are valid values
/// </summary>
[<Interface>]
type IRelativeTweenValue = interface end
/// <summary>
/// Indicates that TweenValue helpers are valid values
/// </summary>
[<Interface>]
type ITweenValue = inherit IRelativeTweenValue

[<Interface>]
type RelativeTweenValue =
    static member inline addition (value: int): #IRelativeTweenValue = !! $"+={value}"
    static member inline addition (value: float): #IRelativeTweenValue = !! $"+={value}"
    static member inline subtraction (value: int): #IRelativeTweenValue = !! $"-={value}"
    static member inline subtraction (value: float): #IRelativeTweenValue = !! $"-={value}"
    static member inline multiplier (value: int): #IRelativeTweenValue = !! $"*={value}"
    static member inline multiplier (value: float): #IRelativeTweenValue = !! $"*={value}"

[<Interface>]
type TweenValue =
    inherit RelativeTweenValue
/// <summary>
/// Indicates that TweenProp and ToTweenValue helpers produce valid values
/// </summary>
[<Interface>]
type IToTweenProp =
    inherit ITweenProp
/// <summary>
/// Indicates that TweenProp and FromTweenValue helpers produce valid values
/// </summary>
[<Interface>]
type IFromTweenProp =
    inherit ITweenProp

[<Interface>]
type ToTween =
    inherit TweenProp
    static member inline to'(value: ITweenValue): #IToTweenProp = "to" ==< value
    static member inline to'(value: ITweenValue * ITweenValue): #IToTweenProp = "to" ==< value
    static member inline to'(value: int * float): #IToTweenProp = "to" ==< value
    static member inline to'(value: float * float): #IToTweenProp = "to" ==< value
    static member inline to'(value: float * int): #IToTweenProp = "to" ==< value
    static member inline to'(value: int * int): #IToTweenProp = "to" ==< value
    static member inline to'(value: string): #IToTweenProp = "to" ==< value
    static member inline to'(value: string * string): #IToTweenProp = "to" ==< value
    static member inline to'(value: FunctionValue<float>): #IToTweenProp = "to" ==< value
    static member inline to'(value: FunctionValue<float> * FunctionValue<float>): #IToTweenProp = "to" ==< value
    
[<Interface>]
type FromTween =
    inherit TweenProp
    static member inline from(value: ITweenValue): #IFromTweenProp = "from" ==< value
    static member inline from(value: int): #IFromTweenProp = "from" ==< value
    static member inline from(value: float): #IFromTweenProp = "from" ==< value
    static member inline from(value: string): #IFromTweenProp = "from" ==< value

[<Interface>]
type IKeyframeProp =
    inherit IToTweenProp
    inherit IFromTweenProp
[<Interface>]
type IKeyframeValueProp =
    inherit IAnimatedProp
    inherit IAnimatedProp<string[]>
    inherit IAnimatedProp<int[]>
    inherit IAnimatedProp<float[]>
    inherit ITweenProp

[<Interface>]
type Keyframe =
    inherit ToTween
    inherit FromTween

[<Erase>]
module Keyframe =
    [<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
    type Style_ = Style
    
    [<Interface>]
    type Style =
        inherit Style<string[]>
        inherit Style<int[]>
        inherit Style<float[]>
        inherit Style_

[<Interface>]
type IKeyframeValue =
    inherit ITweenValue
    
[<Interface>]
type KeyframeValue =
    inherit TweenValue
    static member inline create (values: IKeyframeValueProp list): #IKeyframeValue = !!createObj !!values
    static member inline create (value: int): #IKeyframeValue = !!value
    static member inline create (value: float): #IKeyframeValue = !!value
    static member inline create (value: string): #IKeyframeValue = !!value

[<Interface>]
type IPlaybackProp = interface end

[<Interface>]
type IStaggerProp = interface end
[<Interface>]
type ITimerCallbackProp<'T> = interface end
[<Interface>]
type IAnimationCallbackProp<'T> =
    inherit ITimerCallbackProp<'T>
[<Interface>]
type TimerCallbacks<'T> =
    static member inline onBegin (callback: Callback<'T>): #ITimerCallbackProp<'T> = "onBegin" ==< callback
    static member inline onComplete (callback: Callback<'T>): #ITimerCallbackProp<'T> = "onComplete" ==< callback
    static member inline onUpdate (callback: Callback<'T>): #ITimerCallbackProp<'T> = "onUpdate" ==< callback
    static member inline onLoop (callback: Callback<'T>): #ITimerCallbackProp<'T> = "onLoop" ==< callback
    static member inline onPause (callback: Callback<'T>): #ITimerCallbackProp<'T> = "onPause" ==< callback
    static member inline andThen (callback: Callback<'T> -> JS.Promise<unit>): #ITimerCallbackProp<'T> = "then" ==< callback
    
[<Interface>]
type AnimationCallbacks<'T> =
    inherit TimerCallbacks<'T>
    static member inline onBeforeUpdate (callback: Callback<'T>): #IAnimationCallbackProp<'T> = "onBeforeUpdate" ==< callback
    static member inline onRender (callback: Callback<'T>): #IAnimationCallbackProp<'T> = "onRender" ==< callback
    

[<Interface>]
type Playback =
    static member inline loop (value: bool): #IPlaybackProp = "loop" ==< value
    static member inline loop (count: int): #IPlaybackProp = "loop" ==< count
    static member inline loop (onScroll: ScrollObserver): #IPlaybackProp = "loop" ==< onScroll
    static member inline reversed (value: bool): #IPlaybackProp = "reversed" ==< value
    static member inline alternate (value: bool): #IPlaybackProp = "alternate" ==< value
    static member inline frameRate (value: int): #IPlaybackProp = "frameRate" ==< value
    static member inline ease (value: EasingFun): #IPlaybackProp = "ease" ==< value
    static member inline ease (value: Ease): #IPlaybackProp = "ease" ==< value
    static member inline loopDelay (value: float): #IPlaybackProp = "loopDelay" ==< value
    static member inline loopDelay (value: int): #IPlaybackProp = "loopDelay" ==< value
    static member inline loopDelay (stagger: Stagger): #IPlaybackProp = "loopDelay" ==< stagger
    static member inline loopDelay (stagger: IStaggerProp list): #IPlaybackProp = "loopDelay" ==< (createObj !!stagger)

[<Interface>]
type ITimerProp =
    inherit ITweenProp
    inherit IPlaybackProp
    inherit ITimerCallbackProp<Timer>

[<Interface>]
type Timer =
    inherit TweenProp
    inherit Playback
    inherit TimerCallbacks<Binding.Timer>

type AnimeJs with
    static member inline createTimer(timer: ITimerProp list): Binding.Timer = createTimer(!!createObj timer)

[<Interface>]
type IAnimationProp =
    inherit IAnimationCallbackProp<Animation>
    inherit IAnimatedPropObj<IKeyframeProp>
    inherit IAnimatedProp<string[]>
    inherit IAnimatedProp<int[]>
    inherit IAnimatedProp<float[]>
    inherit IAnimatedProp
    inherit IPlaybackProp
    inherit ITweenProp

[<Erase>]
type TimeLabel [<Emit("$0")>] (label: string) =
    static member inline create label = TimeLabel(label)

[<Interface>]
type ITimePosition = interface end

[<Interface>]
type TimePosition =
    static member inline addition (value: int): #ITimePosition = $"+={value}" |> unbox
    static member inline subtraction (value: int): #ITimePosition = $"-={value}" |> unbox
    static member inline multiplier (value: float): #ITimePosition = $"*={value}" |> unbox
    [<Emit "'<'">]
    static member inline afterPrevious(): #ITimePosition = jsNative
    [<Emit "'<<'">]
    static member inline withPrevious() : #ITimePosition = jsNative
    static member inline additionAfterPrevious value : #ITimePosition = $"<+={value}" |> unbox
    static member inline subtractionAfterPrevious value : #ITimePosition = $"<-={value}" |> unbox
    static member inline multiplierAfterPrevious value : #ITimePosition = $"<*={value}" |> unbox
    static member inline additionWithPrevious value : #ITimePosition = $"<<+={value}" |> unbox
    static member inline subtractionWithPrevious value : #ITimePosition = $"<<-={value}" |> unbox
    static member inline multiplierWithPrevious value : #ITimePosition = $"<<*={value}" |> unbox
    static member inline label (label: TimeLabel): #ITimePosition = label |> unbox
    static member inline functionValue (handler: FunctionValue<_>): #ITimePosition = handler |> unbox
    [<Import("stagger", Spec.path); ParamObject(1)>]
    static member stagger(value: int, start: ITimePosition, ?from: int, ?reversed: bool, ?ease: EasingFun, ?grid: int * int): #ITimePosition = jsNative
    [<Import("stagger", Spec.path); ParamObject(1)>]
    static member stagger(value: int, ?start: int, ?from: int, ?reversed: bool, ?ease: EasingFun, ?grid: int * int): TimePosition = jsNative


[<Interface>]
type Animation =
    inherit AnimationCallbacks<Animation>
    // inherit StyleObj<IKeyframeProp>
    // inherit Style<string[]>
    // inherit Style
    inherit Playback
    inherit TweenProp
    static member inline keyframes (value: IKeyframeValue list): #IAnimationProp = "keyframes" ==< List.toArray value

[<Erase>]
module Animation =
    [<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
    type Style_ = Style
    [<Interface>]
    type Style =
        inherit StyleObj<IKeyframeProp>
        inherit Style<string[]>
        inherit Style<float[]>
        inherit Style<int[]>
        inherit Style_
type Exports with
    [<Import("animate", "animejs")>]
    static member animate (targets: Targets, parameters: AnimationOptions) : Binding.Animation = nativeOnly
type AnimeJs with
    static member inline animate targets (options: IAnimationProp list): Binding.Animation =
        Exports.animate(targets,!!createObj options)

type TimeLabelMap =
    [<EmitIndexer>]
    abstract member Item: TimeLabel -> ITimePosition with get,set

type TimelineObj =

    [<EmitMethod("add")>]
    member this._add([<ParamCollection>] values): TimelineObj= jsNative
    member inline this.add(targets: Targets, animationBuilder: IAnimationProp list, ?position: ITimePosition): TimelineObj = this._add(targets, createObj !!animationBuilder, position)
    member inline this.add(timerParameters: ITimerProp list, ?position: ITimePosition) = this._add(createObj !!timerParameters, position)
    member this.sync(synced: Binding.Timer, ?position: ITimePosition): TimelineObj = jsNative
    member this.sync(synced: Binding.Animation, ?position: ITimePosition): TimelineObj = jsNative
    member this.sync(synced: TimelineObj, ?position: ITimePosition): TimelineObj = jsNative
    member this.call(handler: Callback<unit>, ?position: ITimePosition): TimelineObj = jsNative
    member this.label(label: TimeLabel, ?position: ITimePosition): TimelineObj = jsNative
    member this.set(target: Targets, properties: obj, ?position: ITimePosition): TimelineObj = jsNative
    member inline this.set(target: Targets, listPropValues: (string * obj) list, ?position: ITimePosition): TimelineObj = this.set(target, createObj listPropValues, ?position = position)
    member this.remove(targets: Targets): TimelineObj = jsNative
    member this.remove(targets: Targets, propertyName: string): TimelineObj = jsNative
    member inline this.remove(targets: #CSSStyleDeclaration, propertyMap: #CSSStyleDeclaration -> string): TimelineObj =
        this.remove(!!targets, Experimental.nameofLambda propertyMap)
    member inline this.remove(targets: #CSSStyleDeclaration[], propertyMap: #CSSStyleDeclaration -> string): TimelineObj =
        this.remove(!!targets, Experimental.nameofLambda propertyMap)
    member this.remove(object: Binding.Animation, ?position: ITimePosition): TimelineObj = jsNative
    member this.remove(object: Binding.Timer, ?position: ITimePosition): TimelineObj = jsNative
    member this.remove(object: TimelineObj, ?position: ITimePosition): TimelineObj = jsNative
    member this.init(): TimelineObj = jsNative
    member this.play(): TimelineObj = jsNative
    member this.reverse(): TimelineObj = jsNative
    member this.pause(): TimelineObj = jsNative
    member this.restart(): TimelineObj = jsNative
    member this.alternate(): TimelineObj = jsNative
    member this.resume(): TimelineObj = jsNative
    member this.complete(): TimelineObj = jsNative
    member this.cancel(): TimelineObj = jsNative
    member this.revert(): TimelineObj = jsNative
    member this.seek(time: int, ?muteCallbacks: bool): TimelineObj = jsNative
    member this.stretch(duration: int): TimelineObj = jsNative
    member this.refresh(): TimelineObj = jsNative
    
    [<Emit("$0")>]
    member this.endChain: unit = jsNative
    member this.duration: int = JS.undefined
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
        with get(): float = JS.undefined
        and set(value: float) = ()
    member this.deltaTime
        with get(): float = JS.undefined
        and set(value: float) = ()
    member this.fps
        with get(): int<fps> = JS.undefined
        and set(value: int<fps>) = ()
    member this.id
        with get(): obj = JS.undefined
        and set(value: obj) = ()
    member this.iterationCurrentTime
        with get(): float = JS.undefined
        and set(value: float) = ()
    member this.iterationProgress
        with get(): float = JS.undefined
        and set(value: float) = ()
    member this.labels
        with get(): TimeLabelMap = JS.undefined
        and set(value: TimeLabelMap) = ()
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


[<Interface>]
type ITimelineProp =
    inherit IPlaybackProp
    inherit IAnimationCallbackProp<TimelineObj>
    inherit ITimerCallbackProp<TimelineObj>

[<Interface>]
type Timeline =
    inherit Playback
    inherit AnimationCallbacks<TimelineObj>
    inherit TimerCallbacks<TimelineObj>
    static member inline defaults (timer: ITimerProp list): #ITimelineProp = "defaults" ==< (createObj !!timer)
    
type AnimeJs with
    static member inline createTimeline (timeline: ITimelineProp list): TimelineObj =
        createObj !!timeline |> import "createTimeline" Spec.path

[<Interface>]
type IAnimatableValue = interface end
[<Interface>]
type IAnimatableProp =
    inherit IAnimatedPropObj<IAnimatableValue>
    inherit IAnimatableValue

[<Interface>]
type AnimatableValue =
    static member inline unit (value: string): #IAnimatableValue = "unit" ==< value
    static member inline duration (value: int): #IAnimatableValue = "duration" ==< value
    static member inline duration (value: FunctionValue<_>): #IAnimatableValue = "duration" ==< value
    static member inline duration (value: string): #IAnimatableValue = "duration" ==< value
    static member inline ease (value: Ease): #IAnimatableValue = "ease" ==< value
    static member inline ease (value: EasingFun): #IAnimatableValue = "ease" ==< value
    static member inline modifier (value: FloatModifier): #IAnimatableValue = "modifier" ==< value

type Binding.Animatable with
    [<Emit("$0[$1]()")>]
    member this.get (value: string): U2<float, float[]> = jsNative
    member inline this.get (handler: 'T -> obj): U2<float, float[]> = Experimental.nameofLambda handler |> this.get
    member inline this.get (handler: CSSStyleDeclaration -> string): U2<float, float[]> = Experimental.nameofLambda handler |> this.get
    [<Emit("$0[$1]($2, ...[$3, $4])")>]
    member this.set (value: string, setValue: U2<float, float[]>, ?duration: int, ?easing: EasingFun): Binding.Animatable = jsNative
    member inline this.set (handler: 'T -> obj, setValue: U2<float, float[]>, ?duration: int, ?easing: EasingFun): Binding.Animatable = this.set(Experimental.nameofLambda handler, setValue, ?duration=duration, ?easing=easing)
    member inline this.set (handler: CSSStyleDeclaration -> string, setValue: U2<float, float[]>, ?duration: int, ?easing: EasingFun): Binding.Animatable = this.set(Experimental.nameofLambda handler, setValue, ?duration=duration, ?easing=easing)
    member this.revert(): Binding.Animatable = JS.undefined
    [<EmitProperty "targets">]
    member this.targets: Target[] = jsNative
    [<EmitProperty "animations">]
    member this.animations: Binding.Animation[] = jsNative

[<Interface>]
type Animatable =
    inherit AnimatableValue
    inherit AnimatedProp
    inherit StyleObj<IAnimatableValue>

type AnimeJs with
    static member inline createAnimatable targets (options: IAnimatableProp list): Binding.Animatable =
        createAnimatable(!!targets, !!createObj !!options)
        
[<Interface>]
type IAxisProp = interface end
[<Interface>]
type IDraggableCallbackProp<'T> = interface end
[<Interface>]
type IDraggableProp =
    inherit IAxisProp
    inherit IDraggableCallbackProp<Draggable>
[<Interface>]
type ICursorProp = interface end
type ICursorValue = interface end

[<Interface>]
type DraggableCallbacks<'T> =
    static member inline onGrab (value: Callback<'T>): #IDraggableCallbackProp<'T> = "onGrab" ==< value
    static member inline onDrag (value: Callback<'T>): #IDraggableCallbackProp<'T> = "onDrag" ==< value
    static member inline onRelease (value: Callback<'T>): #IDraggableCallbackProp<'T> = "onRelease" ==< value
    static member inline onSnap (value: Callback<'T>): #IDraggableCallbackProp<'T> = "onSnap" ==< value
    static member inline onResize (value: Callback<'T>): #IDraggableCallbackProp<'T> = "onResize" ==< value
    static member inline onAfterResize (value: Callback<'T>): #IDraggableCallbackProp<'T> = "onAfterResize" ==< value
    static member inline onSettle (value: Callback<'T>): #IDraggableCallbackProp<'T> = "onSettle" ==< value
    static member inline onUpdate (value: Callback<'T>): #IDraggableCallbackProp<'T> = "onUpdate" ==< value

[<Interface>]
type Cursor =
    static member inline onHover (value: string): #ICursorProp = "onHover" ==< value
    static member inline onGrab (value: string): #ICursorProp = "onGrab" ==< value

[<Interface>]
type Axis =
    static member inline snap (value: int): #IAxisProp = "snap" ==< value
    static member inline snap (value: int[]): #IAxisProp = "snap" ==< value
    static member inline snap (value: FunctionValue<int>): #IAxisProp = "snap" ==< value
    static member inline snap (value: FunctionValue<int[]>): #IAxisProp = "snap" ==< value
    static member inline modifier (value: FloatModifier): #IAxisProp = "modifier" ==< value
    static member inline mapTo (value: string): #IAxisProp = "mapTo" ==< value

[<Interface>]
type Draggable =
    inherit Axis
    inherit DraggableCallbacks<Binding.Draggable>
    static member inline cursor (value: bool): #IDraggableProp = "cursor" ==< value
    static member inline cursor (value: ICursorProp list): #IDraggableProp = "cursor" ==< createObj !!value
    static member inline cursor (value: unit -> ICursorProp list): #IDraggableProp = "cursor" ==< (value >> !!createObj)
    static member inline x (value: bool): #IDraggableProp = "x" ==< value
    static member inline x (value: IAxisProp list): #IDraggableProp = "x" ==< createObj !!value
    static member inline y (value: bool): #IDraggableProp = "y" ==< value
    static member inline y (value: IAxisProp list): #IDraggableProp = "y" ==< createObj !!value
    static member inline container (value: Selector): #IDraggableProp = "container" ==< value
    static member inline container (value: #HTMLElement): #IDraggableProp = "container" ==< value
    static member inline container (?top: int,?right: int,?bottom: int,?left: int): #IDraggableProp = "container" ==< (top,right,bottom,left)
    static member inline container (value: FunctionValue<DraggableBounds>): #IDraggableProp = "container" ==< value
    static member inline container (value: DraggableBounds): #IDraggableProp = "container" ==< value
    static member inline containerPadding (value: float): #IDraggableProp = "containerPadding" ==< value
    static member inline containerPadding (value: int): #IDraggableProp = "containerPadding" ==< value
    static member inline containerPadding (?top: int,?right: int,?bottom: int,?left: int): #IDraggableProp = "containerPadding" ==< (top,right,bottom,left)
    static member inline containerPadding (value: DraggableBounds): #IDraggableProp = "containerPadding" ==< value
    static member inline containerPadding (value: FunctionValue<DraggableBounds>): #IDraggableProp = "containerPadding" ==< value
    static member inline containerFriction (value: int): #IDraggableProp = "containerFriction" ==< value
    static member inline containerFriction (value: float): #IDraggableProp = "containerFriction" ==< value
    static member inline releaseMass (value: int): #IDraggableProp = "releaseMass" ==< value
    static member inline releaseMass (value: float): #IDraggableProp = "releaseMass" ==< value
    static member inline releaseMass (value: FunctionValue<float>): #IDraggableProp = "releaseMass" ==< value
    static member inline releaseMass (value: FunctionValue<int>): #IDraggableProp = "releaseMass" ==< value
    static member inline releaseStiffness (value: int): #IDraggableProp = "releaseStiffness" ==< value
    static member inline releaseStiffness (value: float): #IDraggableProp = "releaseStiffness" ==< value
    static member inline releaseStiffness (value: FunctionValue<float>): #IDraggableProp = "releaseStiffness" ==< value
    static member inline releaseStiffness (value: FunctionValue<int>): #IDraggableProp = "releaseStiffness" ==< value
    static member inline releaseDamping (value: int): #IDraggableProp = "releaseDamping" ==< value
    static member inline releaseDamping (value: float): #IDraggableProp = "releaseDamping" ==< value
    static member inline releaseDamping (value: FunctionValue<float>): #IDraggableProp = "releaseDamping" ==< value
    static member inline releaseDamping (value: FunctionValue<int>): #IDraggableProp = "releaseDamping" ==< value
    static member inline releaseEase (value: int): #IDraggableProp = "releaseEase" ==< value
    static member inline releaseEase (value: float): #IDraggableProp = "releaseEase" ==< value
    static member inline releaseEase (value: FunctionValue<float>): #IDraggableProp = "releaseEase" ==< value
    static member inline releaseEase (value: FunctionValue<int>): #IDraggableProp = "releaseEase" ==< value
    static member inline releaseContainerFriction (value: int): #IDraggableProp = "releaseContainerFriction" ==< value
    static member inline releaseContainerFriction (value: float): #IDraggableProp = "releaseContainerFriction" ==< value
    static member inline releaseContainerFriction (value: FunctionValue<float>): #IDraggableProp = "releaseContainerFriction" ==< value
    static member inline releaseContainerFriction (value: FunctionValue<int>): #IDraggableProp = "releaseContainerFriction" ==< value
    static member inline minVelocity (value: int): #IDraggableProp = "minVelocity" ==< value
    static member inline minVelocity (value: float): #IDraggableProp = "minVelocity" ==< value
    static member inline minVelocity (value: FunctionValue<float>): #IDraggableProp = "minVelocity" ==< value
    static member inline minVelocity (value: FunctionValue<int>): #IDraggableProp = "minVelocity" ==< value
    static member inline maxVelocity (value: int): #IDraggableProp = "maxVelocity" ==< value
    static member inline maxVelocity (value: float): #IDraggableProp = "maxVelocity" ==< value
    static member inline maxVelocity (value: FunctionValue<float>): #IDraggableProp = "maxVelocity" ==< value
    static member inline maxVelocity (value: FunctionValue<int>): #IDraggableProp = "maxVelocity" ==< value
    static member inline velocityMultiplier (value: int): #IDraggableProp = "velocityMultiplier" ==< value
    static member inline velocityMultiplier (value: float): #IDraggableProp = "velocityMultiplier" ==< value
    static member inline velocityMultiplier (value: FunctionValue<float>): #IDraggableProp = "velocityMultiplier" ==< value
    static member inline velocityMultiplier (value: FunctionValue<int>): #IDraggableProp = "velocityMultiplier" ==< value
    static member inline dragSpeed (value: int): #IDraggableProp = "dragSpeed" ==< value
    static member inline dragSpeed (value: float): #IDraggableProp = "dragSpeed" ==< value
    static member inline dragSpeed (value: FunctionValue<float>): #IDraggableProp = "dragSpeed" ==< value
    static member inline dragSpeed (value: FunctionValue<int>): #IDraggableProp = "dragSpeed" ==< value
    static member inline scrollThreshold (value: int): #IDraggableProp = "scrollThreshold" ==< value
    static member inline scrollThreshold (value: float): #IDraggableProp = "scrollThreshold" ==< value
    static member inline scrollThreshold (value: FunctionValue<float>): #IDraggableProp = "scrollThreshold" ==< value
    static member inline scrollThreshold (value: FunctionValue<int>): #IDraggableProp = "scrollThreshold" ==< value
    static member inline scrollSpeed (value: int): #IDraggableProp = "scrollSpeed" ==< value
    static member inline scrollSpeed (value: float): #IDraggableProp = "scrollSpeed" ==< value
    static member inline scrollSpeed (value: FunctionValue<float>): #IDraggableProp = "scrollSpeed" ==< value
    static member inline scrollSpeed (value: FunctionValue<int>): #IDraggableProp = "scrollSpeed" ==< value

type AnimeJs with
    static member inline createDraggable targets (options: IDraggableProp list): Binding.Draggable =
        Exports.createDraggable(targets, !!createObj !!options)

[<Interface>]
type IScrollObserverCallbackProp<'T> = interface end
[<Interface>]
type IScrollObserverProp =
    inherit IScrollObserverCallbackProp<ScrollObserver>

[<Interface>]
type ScrollObserverCallbacks<'T> =
    static member inline onEnter (value: Callback<'T>): #IScrollObserverCallbackProp<'T> = "onEnter" ==< value
    static member inline onEnterForward (value: Callback<'T>): #IScrollObserverCallbackProp<'T> = "onEnterForward" ==< value
    static member inline onEnterBackward (value: Callback<'T>): #IScrollObserverCallbackProp<'T> = "onEnterBackward" ==< value
    static member inline onLeave (value: Callback<'T>): #IScrollObserverCallbackProp<'T> = "onLeave" ==< value
    static member inline onLeaveForward (value: Callback<'T>): #IScrollObserverCallbackProp<'T> = "onLeaveForward" ==< value
    static member inline onLeaveBackward (value: Callback<'T>): #IScrollObserverCallbackProp<'T> = "onLeaveBackward" ==< value
    static member inline onUpdate (value: Callback<'T>): #IScrollObserverCallbackProp<'T> = "onUpdate" ==< value
    static member inline onSyncComplete (value: Callback<'T>): #IScrollObserverCallbackProp<'T> = "onSyncComplete" ==< value

type Playback with
    static member inline loop (onScroll: IScrollObserverProp list): #IPlaybackProp = "loop" ==< Exports.onScroll !!(createObj !!onScroll)

[<Interface>]
type ScrollObserver =
    inherit ScrollObserverCallbacks<Binding.ScrollObserver>
    static member inline axis (value: Enums.Axis): #IScrollObserverProp = "axis" ==< value
    static member inline container (value: Selector): #IScrollObserverProp = "container" ==< value
    static member inline container (value: #HTMLElement): #IScrollObserverProp = "container" ==< value
    static member inline target (value: Selector): #IScrollObserverProp = "target" ==< value
    static member inline target (value: #HTMLElement): #IScrollObserverProp = "target" ==< value
    static member inline debug (): #IScrollObserverProp = "debug" ==< true
    static member inline debug (value: bool): #IScrollObserverProp = "debug" ==< false
    static member inline repeat (value: bool): #IScrollObserverProp = "repeat" ==< value
    static member inline repeat (): #IScrollObserverProp = "repeat" ==< true
    static member inline sync (value: bool): #IScrollObserverProp = "sync" ==< value
    static member inline sync (enter: string): #IScrollObserverProp = "sync" ==< enter
    static member inline sync (enter: string, leave: string): #IScrollObserverProp = "sync" ==< $"{enter} {leave}"
    static member inline sync (enter: string, leave: string, enterBackward: string, leaveBackward: string): #IScrollObserverProp =
        "sync" ==< $"{enter} {leave} {enterBackward} {leaveBackward}"
    static member inline sync (smooth: float): #IScrollObserverProp = "sync" ==< smooth
    static member inline sync (ease: Ease): #IScrollObserverProp = "sync" ==< ease
    static member inline sync (ease: EasingFun): #IScrollObserverProp = "sync" ==< ease
    static member inline enter (value: string): #IScrollObserverProp = "enter" ==< value
    static member inline enter (value: Enums.ObserverThreshold): #IScrollObserverProp = "enter" ==< value
    static member inline enter (container: Enums.ObserverThreshold, target: Enums.ObserverThreshold): #IScrollObserverProp = "enter" ==< {| container = container; target = target |}
    static member inline enter (container: Enums.ObserverThreshold, target: string): #IScrollObserverProp = "enter" ==< {| container = container; target = target |}
    static member inline enter (container: string, target: Enums.ObserverThreshold): #IScrollObserverProp = "enter" ==< {| container = container; target = target |}
    static member inline enter (container: string, target: string): #IScrollObserverProp = "enter" ==< {| container = container; target = target |}
    static member inline leave (value: string): #IScrollObserverProp = "leave" ==< value
    static member inline leave (value: Enums.ObserverThreshold): #IScrollObserverProp = "leave" ==< value
    static member inline leave (container: Enums.ObserverThreshold, target: Enums.ObserverThreshold): #IScrollObserverProp = "leave" ==< {| container = container; target = target |}
    static member inline leave (container: Enums.ObserverThreshold, target: string): #IScrollObserverProp = "leave" ==< {| container = container; target = target |}
    static member inline leave (container: string, target: Enums.ObserverThreshold): #IScrollObserverProp = "leave" ==< {| container = container; target = target |}
    static member inline leave (container: string, target: string): #IScrollObserverProp = "leave" ==< {| container = container; target = target |}

[<Interface>]
type Stagger =
    static member inline start (value: float): #IStaggerProp = "start" ==< value
    static member inline start (value: ITimePosition): #IStaggerProp = "start" ==< value
    static member inline from (value: float): #IStaggerProp = "from" ==< value
    static member inline from (value: StaggerFrom): #IStaggerProp = "from" ==< value
    static member inline reversed (value: bool): #IStaggerProp = "reversed" ==< value
    static member inline ease (value: Ease): #IStaggerProp = "ease" ==< value
    static member inline ease (value: EasingFun): #IStaggerProp = "ease" ==< value
    static member inline grid (value: int * int): #IStaggerProp = "grid" ==< value
    static member inline axis (value: Enums.Axis): #IStaggerProp = "axis" ==< value
    static member inline modifier (value: FloatModifier): #IStaggerProp = "modifier" ==< value
    

type AnimeJs with
    static member inline onScroll (options: IScrollObserverProp list): Binding.ScrollObserver = Exports.onScroll(!!createObj !!options)
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
    [<Import("svg.createDrawable", Spec.path)>]
    static member createDrawable(selector: SVGElement, ?start: float, ?``end``: float): SVGElementInstanceList = jsNative
    [<Import("svg.createDrawable", Spec.path)>]
    static member createDrawable(selector: Selector, ?start: float, ?``end``: float): SVGElementInstanceList = jsNative
    [<Import("svg.morphTo", Spec.path)>]
    static member morphTo (path2: SVGElement, ?precision: float) : FunctionValue<_> = nativeOnly
    [<Import("svg.morphTo", Spec.path)>]
    static member morphTo (path2: Selector, ?precision: float) : FunctionValue<_> = nativeOnly
    [<Import("svg.createMotionPath", Spec.path)>]
    static member createMotionPath (path: SVGElement) : MotionPath = nativeOnly
    [<Import("svg.morphTo", Spec.path)>]
    static member createMotionPath (path: Selector) : MotionPath = nativeOnly
    [<ImportMember(Spec.path)>]
    static member stagger (target: int): FunctionValue<int> = jsNative
    [<ImportMember(Spec.path)>]
    static member stagger (target: float): FunctionValue<float> = jsNative
    static member inline stagger (target: int, options: IStaggerProp list): FunctionValue<int> =
        !!stagger(!!target, !!createObj !!options)
    static member inline stagger (target: float, options: IStaggerProp list): FunctionValue<float> =
        !!stagger(!!target, !!createObj !!options)

type Utils =
    inherit Binding.Utils
