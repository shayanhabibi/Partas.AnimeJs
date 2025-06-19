module Partas.AnimeJs.ComputationExpressions

open Fable.Core
open Fable.Core.JsInterop
open Fable.Core.JS
open Partas.AnimeJs.Core
open Browser.Types
open Partas.AnimeJs.CE
type FableObjectBuilder = interface end
type PlaybackObjectBuilder =
    inherit FableObjectBuilder

[<Erase>]
module RelativeOperators =
    let inline (!<<+=) value = unbox<RelativeTimePosition> $"<<+={value}"
    let inline (!<<-=) value = unbox<RelativeTimePosition> $"<<-={value}"
    let inline (!<<*=) value = unbox<RelativeTimePosition> $"<<*={value}"
    let inline (!*=) value = unbox<RelativeTweenValue> $"*={value}"
    let inline (!-=) value = unbox<RelativeTweenValue> $"-={value}"
    let inline (!+=) value = unbox<RelativeTweenValue> $"+={value}"
open RelativeOperators
[<Erase>]
type FableObjectBuildder = FableObjectBuilder of (string * obj) list with
    member inline this.Value: (string * obj) list = !!this

type FableObject = (string * obj) list
[<AutoOpen>]
module FableObjectBuilderExtension =
    type FableObjectBuilder with
        // member inline _.Yield(_: unit): unit = ()
        member inline _.Yield(_: unit): FableObject = []
        member inline _.Yield(_object_value: string * obj) = _object_value
        member inline _.Yield(_object_builder: FableObject) = _object_builder
        member inline _.Combine(_object_unit: unit, _object_value: string * obj) =
            _object_unit
            _object_value
        member inline _.Combine(_object_value: string * obj) =
            _object_value
        member inline _.Combine(_object_value: FableObject) =
            _object_value
        member inline _.Combine(_object_value: string * obj, _object_value2: string * obj) =
            [_object_value; _object_value2]
        member inline _.Combine(_object_builder: FableObject, _object_value: string * obj) = _object_value :: _object_builder
        member inline _.Combine(_object_builder: FableObject, _object_builder2: FableObject) = _object_builder @ _object_builder2
        member inline _.Combine(_object_builder: string * obj, _object_builder2: FableObject) = _object_builder :: _object_builder2
        member inline _.Delay(value) = value()
        member inline _.For(_object_builder: FableObject, _object_builder_action: string * obj -> FableObject) =
            _object_builder |> List.collect _object_builder_action
open System.Runtime.CompilerServices

type [<Erase>] StyleValue = private StyleValue of obj
and [<Erase>] StyleObj = private StyleObj of obj
and [<Erase>] StyleToObj = private StyleToObj of FableObject
and [<Erase>] StyleFromObj = private StyleFromObj of FableObject
and ICssStyle with
    member inline this.Value: string = unbox this
    member inline _.For(_object_builder: FableObject, _object_builder_action: string * obj -> FableObject) =
        _object_builder |> List.collect _object_builder_action
    member inline _.Yield(_: unit): FableObject = []
    member inline _.Yield(value: ITuple) = StyleValue value
    member inline _.Delay([<InlineIfLambda>] value: unit -> StyleValue) = value()
    member inline _.Yield(value: float) = StyleValue value
    member inline _.Yield(value: string) = StyleValue value
    member inline _.Delay([<InlineIfLambda>] value: unit -> FableObject) = value()
    member inline _.Delay([<InlineIfLambda>] value: unit -> StyleFromObj) = value()
    member inline _.Delay([<InlineIfLambda>] value: unit -> StyleToObj) = value()
    member inline _.Combine(value) = fun () -> value
    [<CustomOperation "from">]
    member inline _.fromOp(_object_builder: FableObject, _property_value: float) = StyleFromObj(("from" ==> _property_value) :: _object_builder)
    [<CustomOperation "from">]
    member inline _.fromOp(_object_builder: FableObject, _property_value: FunctionValue<float>) = StyleFromObj(("from" ==> _property_value) :: _object_builder)
    [<CustomOperation "from">]
    member inline _.fromOp(_object_builder: FableObject, _property_value: string) = StyleFromObj(("from" ==> _property_value) :: _object_builder)
    [<CustomOperation "from">]
    member inline _.fromOp(_object_builder: FableObject, _property_value: RelativeTweenValue) = StyleFromObj(("from" ==> _property_value) :: _object_builder)
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, _property_value: float) = StyleToObj(("to" ==> _property_value) :: _object_builder)
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, _property_value: FunctionValue<float>) = StyleToObj(("to" ==> _property_value) :: _object_builder)
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, _property_value: string) = StyleToObj(("to" ==> _property_value) :: _object_builder)
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, _property_value: RelativeTweenValue) = StyleToObj(("to" ==> _property_value) :: _object_builder)
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, _property_value: RelativeTweenValue, _property_value2: RelativeTweenValue) = StyleToObj(("to" ==> (_property_value, _property_value2)) :: _object_builder)
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, _property_value: float, _property_value2: float) = StyleToObj(("to" ==> (_property_value, _property_value2)) :: _object_builder)
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, _property_value: string, _property_value2: string) = StyleToObj(("to" ==> (_property_value, _property_value2)) :: _object_builder)
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, _property_value: RelativeTweenValue, _property_value2: float) = StyleToObj(("to" ==> (_property_value, _property_value2)) :: _object_builder)
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, _property_value: float, _property_value2: RelativeTweenValue) = StyleToObj(("to" ==> (_property_value, _property_value2)) :: _object_builder)
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, _property_value: FunctionValue<float>, _property_value2: FunctionValue<float>) = StyleToObj(("to" ==> (_property_value, _property_value2)) :: _object_builder)
    [<CustomOperation "delay">]
    member inline _.DelayOp(_object_builder: FableObject, _property_value: float) = _object_builder @ ["delay" ==> _property_value]
    [<CustomOperation "delay">]
    member inline _.DelayOp(_object_builder: FableObject, _property_value_lambda: FunctionValue<float>) = _object_builder @ ["delay" ==> _property_value_lambda]
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: FableObject, _property_value: float) = _object_builder @ ["duration" ==> _property_value]
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: FableObject, _property_value_lambda: FunctionValue<float>) = _object_builder @ ["duration" ==> _property_value_lambda]
    [<CustomOperation "ease">]
    member inline _.EaseOp(_object_builder: FableObject, _property_value_lambda: float -> float) = _object_builder @ ["ease" ==> _property_value_lambda]
    [<CustomOperation "composition">]
    member inline _.CompositionOp(_object_builder: FableObject, _property_value: Composition) = _object_builder @ ["composition" ==> _property_value]
    [<CustomOperation "modifier">]
    member inline _.ModifierOp(_object_builder: FableObject, _property_value_lambda: FloatModifier) = _object_builder @ ["modifier" ==> _property_value_lambda]
    [<CustomOperation "delay">]
    member inline _.DelayOp(_object_builder: StyleToObj, _property_value: float) = StyleToObj(!!_object_builder @ ["delay" ==> _property_value])
    [<CustomOperation "delay">]
    member inline _.DelayOp(_object_builder: StyleToObj, _property_value_lambda: FunctionValue<float>) = StyleToObj(!!_object_builder @ ["delay" ==> _property_value_lambda])
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: StyleToObj, _property_value: float) = StyleToObj(!!_object_builder @ ["duration" ==> _property_value])
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: StyleToObj, _property_value_lambda: FunctionValue<float>) = StyleToObj(!!_object_builder @ ["duration" ==> _property_value_lambda])
    [<CustomOperation "ease">]
    member inline _.EaseOp(_object_builder: StyleToObj, _property_value_lambda: float -> float) = StyleToObj(!!_object_builder @ ["ease" ==> _property_value_lambda])
    [<CustomOperation "composition">]
    member inline _.CompositionOp(_object_builder: StyleToObj, _property_value: Composition) = StyleToObj(!!_object_builder @ ["composition" ==> _property_value])
    [<CustomOperation "modifier">]
    member inline _.ModifierOp(_object_builder: StyleToObj, _property_value_lambda: FloatModifier) = StyleToObj(!!_object_builder @ ["modifier" ==> _property_value_lambda])
    [<CustomOperation "delay">]
    member inline _.DelayOp(_object_builder: StyleFromObj, _property_value: float) = StyleFromObj(!!_object_builder @ ["delay" ==> _property_value])
    [<CustomOperation "delay">]
    member inline _.DelayOp(_object_builder: StyleFromObj, _property_value_lambda: FunctionValue<float>) = StyleFromObj(!!_object_builder @ ["delay" ==> _property_value_lambda])
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: StyleFromObj, _property_value: float) = StyleFromObj(!!_object_builder @ ["duration" ==> _property_value])
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: StyleFromObj, _property_value_lambda: FunctionValue<float>) = StyleFromObj(!!_object_builder @ ["duration" ==> _property_value_lambda])
    [<CustomOperation "ease">]
    member inline _.EaseOp(_object_builder: StyleFromObj, _property_value_lambda: float -> float) = StyleFromObj(!!_object_builder @ ["ease" ==> _property_value_lambda])
    [<CustomOperation "composition">]
    member inline _.CompositionOp(_object_builder: StyleFromObj, _property_value: Composition) = StyleFromObj(!!_object_builder @ ["composition" ==> _property_value])
    [<CustomOperation "modifier">]
    member inline _.ModifierOp(_object_builder: StyleFromObj, _property_value_lambda: FloatModifier) = StyleFromObj(!!_object_builder @ ["modifier" ==> _property_value_lambda])

type Extensions =
    [<Extension>]
    static member inline Run(prop: ICssStyle, runner: StyleValue) =
        !!prop ==> runner
    [<Extension>]
    static member inline Run(prop: ICssStyle, runner: FableObject): StyleObj =
        !!(!!prop ==> createObj runner)
    [<Extension>]
    static member inline Run(prop: ICssStyle, runner: StyleToObj): StyleToObj =
        !!(!!prop ==> createObj !!runner)
    [<Extension>]
    static member inline Run(prop: ICssStyle, runner: StyleFromObj): StyleFromObj =
        !!(!!prop ==> createObj !!runner)

let inline style (value: string) = unbox<ICssStyle> value
let inline (!~) (value: string) = style value 

type Style = CssStyle
let animatablePropertyMutualExclusion = Style.accentColor {
    too 5
    from 5
    delay 5
}
let asdfas = Style.all { 5, "" }

let asdfaasd = !~"porcheese" { 5 }

type TimerCallbackComputation<'T> =
    inherit FableObjectBuilder

[<AutoOpen>]
module AutoOpenTimerCallbackComputation =
    type TimerCallbackComputation<'T> with
        [<CustomOperation "onBegin">]
        member inline _.onBeginOp(_object_builder: FableObject, _property_value: Callback<'T>) = ("onBegin" ==> _property_value) :: _object_builder
        [<CustomOperation "onComplete">]
        member inline _.onCompleteOp(_object_builder: FableObject, _property_value: Callback<'T>) = ("onComplete" ==> _property_value) :: _object_builder
        [<CustomOperation "onUpdate">]
        member inline _.onUpdateOp(_object_builder: FableObject, _property_value: Callback<'T>) = ("onUpdate" ==> _property_value) :: _object_builder
        [<CustomOperation "onLoop">]
        member inline _.onLoopOp(_object_builder: FableObject, _property_value: Callback<'T>) = ("onLoop" ==> _property_value) :: _object_builder
        [<CustomOperation "onPause">]
        member inline _.onPauseOp(_object_builder: FableObject, _property_value: Callback<'T>) = ("onPause" ==> _property_value) :: _object_builder
        [<CustomOperation "andThen">]
        member inline _.thenOp(_object_builder: FableObject, _property_value: Callback<'T>) = ("then" ==> _property_value) :: _object_builder


type AnimationCallbackComputation<'T> =
    inherit TimerCallbackComputation<'T>

[<AutoOpen>]
module AutoOpenAnimationCallbackComputation =
    type AnimationCallbackComputation<'T> with
        [<CustomOperation "onBeforeUpdate">]
        member inline _.onBeforeUpdateOp(_object_builder: FableObject, _property_value: Callback<'T>) = ("onBeforeUpdate" ==> _property_value) :: _object_builder
        [<CustomOperation "onRender">]
        member inline _.onRenderOp(_object_builder: FableObject, _property_value: Callback<'T>) = ("onRender" ==> _property_value) :: _object_builder
    

type TweenObjectBuilder() =
    interface FableObjectBuilder
    [<CustomOperation "from">]
    member inline _.fromOp(_object_builder: FableObject, _property_value: float) = ("from" ==> _property_value) :: _object_builder
    [<CustomOperation "from">]
    member inline _.fromOp(_object_builder: FableObject, _property_value: FunctionValue<float>) = ("from" ==> _property_value) :: _object_builder
    [<CustomOperation "from">]
    member inline _.fromOp(_object_builder: FableObject, _property_value: string) = ("from" ==> _property_value) :: _object_builder
    [<CustomOperation "from">]
    member inline _.fromOp(_object_builder: FableObject, _property_value: RelativeTweenValue) = ("from" ==> _property_value) :: _object_builder
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, _property_value: float) = ("to" ==> _property_value) :: _object_builder
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, _property_value: FunctionValue<float>) = ("to" ==> _property_value) :: _object_builder
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, _property_value: string) = ("to" ==> _property_value) :: _object_builder
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, _property_value: RelativeTweenValue) = ("to" ==> _property_value) :: _object_builder
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, _property_value: RelativeTweenValue, _property_value2: RelativeTweenValue) = ("to" ==> (_property_value, _property_value2)) :: _object_builder
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, _property_value: float, _property_value2: float) = ("to" ==> (_property_value, _property_value2)) :: _object_builder
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, _property_value: string, _property_value2: string) = ("to" ==> (_property_value, _property_value2)) :: _object_builder
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, _property_value: RelativeTweenValue, _property_value2: float) = ("to" ==> (_property_value, _property_value2)) :: _object_builder
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, _property_value: float, _property_value2: RelativeTweenValue) = ("to" ==> (_property_value, _property_value2)) :: _object_builder
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, _property_value: FunctionValue<float>, _property_value2: FunctionValue<float>) = ("to" ==> (_property_value, _property_value2)) :: _object_builder
    [<CustomOperation "delay">]
    member inline _.DelayOp(_object_builder: FableObject, _property_value: float) = _object_builder @ ["delay" ==> _property_value]
    [<CustomOperation "delay">]
    member inline _.DelayOp(_object_builder: FableObject, _property_value_lambda: FunctionValue<float>) = _object_builder @ ["delay" ==> _property_value_lambda]
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: FableObject, _property_value: float) = _object_builder @ ["duration" ==> _property_value]
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: FableObject, _property_value_lambda: FunctionValue<float>) = _object_builder @ ["duration" ==> _property_value_lambda]
    [<CustomOperation "ease">]
    member inline _.EaseOp(_object_builder: FableObject, _property_value_lambda: float -> float) = _object_builder @ ["ease" ==> _property_value_lambda]
    [<CustomOperation "composition">]
    member inline _.CompositionOp(_object_builder: FableObject, _property_value: Composition) = _object_builder @ ["composition" ==> _property_value]
    [<CustomOperation "modifier">]
    member inline _.ModifierOp(_object_builder: FableObject, _property_value_lambda: FloatModifier) = _object_builder @ ["modifier" ==> _property_value_lambda]
    member inline _.Run(_object_builder_run: FableObject) = (createObj _object_builder_run)

type TweenOptionsComputation = interface end

[<AutoOpen>]
module AutoOpenTweenOptionsComputation =
    type TweenOptionsComputation with
        [<CustomOperation "delay">]
        member inline _.DelayOp(_object_builder: FableObject, _property_value: float) = _object_builder @ ["delay" ==> _property_value]
        [<CustomOperation "delay">]
        member inline _.DelayOp(_object_builder: FableObject, _property_value_lambda: FunctionValue<float>) = _object_builder @ ["delay" ==> _property_value_lambda]
        [<CustomOperation "duration">]
        member inline _.DurationOp(_object_builder: FableObject, _property_value: float) = _object_builder @ ["duration" ==> _property_value]
        [<CustomOperation "duration">]
        member inline _.DurationOp(_object_builder: FableObject, _property_value_lambda: FunctionValue<float>) = _object_builder @ ["duration" ==> _property_value_lambda]
        [<CustomOperation "ease">]
        member inline _.EaseOp(_object_builder: FableObject, _property_value_lambda: float -> float) = _object_builder @ ["ease" ==> _property_value_lambda]
        [<CustomOperation "composition">]
        member inline _.CompositionOp(_object_builder: FableObject, _property_value: Composition) = _object_builder @ ["composition" ==> _property_value]
        [<CustomOperation "modifier">]
        member inline _.ModifierOp(_object_builder: FableObject, _property_value_lambda: FloatModifier) = _object_builder @ ["modifier" ==> _property_value_lambda]
        
[<AutoOpen>]
module PlaybackObjectBuilder =
    type PlaybackObjectBuilder with
        [<CustomOperation "loop">]
        member inline _.Loop(_object_builder: FableObject, _property_value: bool) = _object_builder @ [("loop" ==> _property_value)]
        [<CustomOperation "loop">]
        member inline _.Loop(_object_builder: FableObject, _property_value: float) = _object_builder @ [("loop" ==> _property_value)]
        [<CustomOperation "loop">]
        member inline _.Loop(_object_builder: FableObject, _property_value: Binding.ScrollObserver) = _object_builder @ [("loop" ==> _property_value)]
        [<CustomOperation "reversed">]
        member inline _.Reversed(_object_builder: FableObject, _property_value: bool) = _object_builder @ ["reversed" ==> _property_value]
        [<CustomOperation "alternate">]
        member inline _.Alternate(_object_builder: FableObject, _property_value: bool) = _object_builder @ ["alternate" ==> _property_value]
        [<CustomOperation "frameRate">]
        member inline _.FrameRate(_object_builder: FableObject, _property_value: int) = _object_builder @ ["frameRate" ==> _property_value]
        [<CustomOperation "ease">]
        member inline _.Ease(_object_builder: FableObject, _property_value_lambda: float -> float) = _object_builder @ ["ease" ==> _property_value_lambda]
        [<CustomOperation "loopDelay">]
        member inline _.LoopDelay(_object_builder: FableObject, _property_value: float) = _object_builder @ ["loopDelay" ==> _property_value]
        [<CustomOperation "loopDelay">]
        member inline _.LoopDelay(_object_builder: FableObject, _property_value: FunctionValue<float>) = _object_builder @ ["loopDelay" ==> _property_value]
        [<CustomOperation "autoplay">]
        member inline _.AutoPlay(_object_builder: FableObject, _property_value: bool) = _object_builder @ ["autoplay" ==> _property_value]
        [<CustomOperation "autoplay">]
        member inline _.AutoPlay(_object_builder: FableObject, _property_value: Binding.ScrollObserver) = _object_builder @ ["autoplay" ==> _property_value]
        
type BoundsComputation() =
    interface FableObjectBuilder
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
let bounds = BoundsComputation()
type SpringComputation() =
    interface FableObjectBuilder
    [<CustomOperation "mass">]
    member inline _.MassOp(_object_builder, _property_value: float) = _object_builder @ [("mass" ==> _property_value)]
    [<CustomOperation "stiffness">]
    member inline _.StiffnessOp(_object_builder, _property_value: float) = _object_builder @ [("stiffness" ==> _property_value)]
    [<CustomOperation "damping">]
    member inline _.DampingOp(_object_builder, _property_value: float) = _object_builder @ [("damping" ==> _property_value)]
    [<CustomOperation "velocity">]
    member inline _.VelocityOp(_object_builder, _property_value: float) = _object_builder @ [("velocity" ==> _property_value)]
    member inline _.Run(_object_builder_run: FableObject): float -> float =
        import "createSpring" "animejs" (_object_builder_run |> createObj)
    member inline _.Yield(x: ICssStyle) = [x.Value]
let spring = SpringComputation()
let tweenOptions = TweenObjectBuilder()

let xxx = tweenOptions {
    modifier
        (spring {
            mass 1.0
            damping 5
        })
    composition Composition.Blend
}

type TimelineObj = FSharp.TimelineObj
type TimelineOptions = interface end

type TimelineOptionsComputation() =
    interface FableObjectBuilder
type FSharp.TimelineObj with
// type TimelineComputation(?timeline: TimelineObj) =
    // member inline _.Yield(_: unit) = timeline |> Option.defaultValue (() |> import "createTimeline" Spec.path)
    member inline this.Yield(_: unit) = this
    [<CustomOperation "add">]
    member inline _.AddOp(_object_instance: TimelineObj, _object_method_arg: string, _object_method: obj) =
        _object_instance.add(Selector _object_method_arg, unbox (createObj !!_object_method))
    [<CustomOperation "add">]
    member inline _.AddOp(_object_instance: TimelineObj, _object_method_arg: string, _object_method: obj, value: Binding.TimePosition) =
        _object_instance.add(Selector _object_method_arg, unbox (createObj !!_object_method), !!value)
    [<CustomOperation "restart">]
    member inline _.restartOp(_object_instance: TimelineObj) = _object_instance.restart()
    [<CustomOperation "play">]
    member inline _.playOp(_object_instance: TimelineObj) = _object_instance.play()
    [<CustomOperation "init">]
    member inline _.initOp(_object_instance: TimelineObj) = _object_instance.init()
    [<CustomOperation "reverse">]
    member inline _.reverseOp(_object_instance: TimelineObj) = _object_instance.reverse()
    [<CustomOperation "pause">]
    member inline _.pauseOp(_object_instance: TimelineObj) = _object_instance.pause()
    [<CustomOperation "alternate">]
    member inline _.alternateOp(_object_instance: TimelineObj) = _object_instance.alternate()
    [<CustomOperation "resume">]
    member inline _.resumeOp(_object_instance: TimelineObj) = _object_instance.resume()
    [<CustomOperation "complete">]
    member inline _.completeOp(_object_instance: TimelineObj) = _object_instance.complete()
    [<CustomOperation "cancel">]
    member inline _.cancelOp(_object_instance: TimelineObj) = _object_instance.cancel()
    [<CustomOperation "revert">]
    member inline _.revertOp(_object_instance: TimelineObj) = _object_instance.revert()
    [<CustomOperation "seek">]
    member inline _.seekOp(_object_instance: TimelineObj, time: int, ?muteCallbacks: bool) = _object_instance.seek(time, ?muteCallbacks = muteCallbacks)
    [<CustomOperation "stretch">]
    member inline _.stretchOp(_object_instance: TimelineObj, time: float) = _object_instance.stretch(!!time)
    [<CustomOperation "refresh">]
    member inline _.refreshOp(_object_instance: TimelineObj) = _object_instance.refresh()
    [<CustomOperation "set">]
    member inline _.setOp(_object_instance: TimelineObj, targets: obj, properties: obj, ?position: Binding.TimePosition) = _object_instance.set(!!targets,properties,?position = !!position)
    [<CustomOperation "label">]
    member inline _.label(_object_instance: TimelineObj, label: string, ?position: Binding.TimePosition) = _object_instance.label(!!label, ?position = !!position)
    member inline _.Run(value: TimelineObj) = value
let testTimelineObj: TimelineObj = !! null
// let timeline = TimelineComputation()
let timeline: TimelineObj = !! null
let yyy = timeline {
    init
    add
        "#box"
        (tweenOptions {
            delay 100.0
        })
    restart
    add "#box .paths" []
    resume
}

type AnimatableOptionsComputation() =
    interface FableObjectBuilder
    [<CustomOperation "unit">]
    member inline _.UnitOp(_object_builder: FableObject, value: string) = ("unit", !!value) :: _object_builder
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: FableObject, value: float) = ("duration" ==> value) :: _object_builder
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: FableObject, value: FunctionValue) = ("duration" ==> value) :: _object_builder
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: FableObject, value: string) = ("duration" ==> value) :: _object_builder
    [<CustomOperation "ease">]
    member inline _.EaseOp(_object_builder: FableObject, value: float -> float) = ("ease" ==> value) :: _object_builder
    [<CustomOperation "modifier">]
    member inline _.ModifierOp(_object_builder: FableObject, value: float -> float) = ("modifier" ==> value) :: _object_builder
    member inline _.Run(_object_run: FableObject): Binding.AnimatableOptions = !!(_object_run |> createObj)
let animatableOptions = AnimatableOptionsComputation()

let d = animatableOptions {
    unit "asdf"
}

type CursorOptionsComputation() =
    member inline _.Yield(_: unit): unit = ()
    member inline _.Yield(value: string * obj): string * obj = value
    member inline _.Yield(value: bool) = value
    member inline _.Combine(value: string * obj, value2: string * obj): (string * obj) * (string * obj) = value,value2
    member inline _.Delay(value: unit -> (string * obj)) = value()
    member inline _.Delay(value: unit -> bool) = value()
    member inline _.Delay(value: unit -> ((string * obj) * (string * obj))) = value()
    member inline _.Combine(value: string * obj): string * obj = value
    [<CustomOperation "onHover">]
    member inline _.OnHoverOp(_object_builder: unit, value: bool) = "onHover" ==> value
    [<CustomOperation "onHover">]
    member inline _.OnHoverOp(_object_builder: string * obj, value: bool) = _object_builder,("onHover" ==> value)
    [<CustomOperation "onGrab">]
    member inline _.OnGrabOp(_object_builder: unit, value: bool) = "onGrab" ==> value
    [<CustomOperation "onGrab">]
    member inline _.OnGrabOp(_object_builder: string * obj, value: bool) = _object_builder,("onGrab" ==> value)
    member inline _.Run(_object_run: (string * obj)): DraggableCursor = !!([|_object_run|] |> createObj)
    member inline _.Run(_object_run: ((string * obj) * (string * obj))): DraggableCursor = !!([|_object_run |> fst; _object_run |> snd|] |> createObj)
    member inline _.Run(_object_run: bool): DraggableCursor = DraggableCursor(_object_run)

type AxisOptionsComputation() =
    interface FableObjectBuilder
    [<CustomOperation "snap">]
    member inline _.SnapOp(_object_builder: FableObject, value: float) = ("snap" ==> value) :: _object_builder
    [<CustomOperation "snap">]
    member inline _.SnapOp(_object_builder: FableObject, value: float, value2: float) = ("snap" ==> (value, value2)) :: _object_builder
    [<CustomOperation "snap">]
    member inline _.SnapOp(_object_builder: FableObject, value: FunctionValue<float>) = ("snap" ==> value) :: _object_builder
    [<CustomOperation "snap">]
    member inline _.SnapOp(_object_builder: FableObject, value: FunctionValue<float * float>) = ("snap" ==> value) :: _object_builder
    [<CustomOperation "modifier">]
    member inline _.ModifierOp(_object_builder: FableObject, value: FloatModifier) = ("modifier" ==> value) :: _object_builder
    [<CustomOperation "mapTo">]
    member inline _.MapToOp(_object_builder: FableObject, value: string) = ("mapTo" ==> value) :: _object_builder
    member inline _.Yield(value: bool) = value
    member inline _.Run(_object_run: FableObject): FSharp.Axis = !!createObj _object_run
    member inline _.Run(_object_run: bool): FSharp.Axis = !!_object_run
let dragAxis = AxisOptionsComputation()

let dragCursor = CursorOptionsComputation()

let df = dragCursor {
    true
}

type DraggableOptionsComputation() =
    interface FableObjectBuilder
    member _.Yield(value: DraggableCursor) = "cursor" ==> value
    member _.Yield(value: Axis * FSharp.Axis): string * obj = !!value
    [<CustomOperation "cursor">]
    member _.CursorOp(_object_builder: FableObject, value: bool) = ("cursor" ==> value) :: _object_builder
    [<CustomOperation "x">]
    member _.XOp(_object_builder: FableObject, value: bool) = ("x" ==> value) :: _object_builder
    [<CustomOperation "y">]
    member _.YOp(_object_builder: FableObject, value: bool) = ("y" ==> value) :: _object_builder
    [<CustomOperation "x">]
    member _.XOp(_object_builder: FableObject, value: FSharp.Axis) = ("x" ==> value) :: _object_builder
    [<CustomOperation "y">]
    member _.YOp(_object_builder: FableObject, value: FSharp.Axis) = ("y" ==> value) :: _object_builder
    // member _.Run(value: string * obj): Binding.DraggableOptions = !!createObj [value]
    [<CustomOperation "container">]
    member _.ContainerOp(_object_builder: FableObject, _object_value: Selector) = ("container" ==> _object_value) :: _object_builder
    [<CustomOperation "container">]
    member _.ContainerOp(_object_builder: FableObject, _object_value: string) = ("container" ==> _object_value) :: _object_builder
    [<CustomOperation "container">]
    member _.ContainerOp(_object_builder: FableObject, _object_value: #HTMLElement) = ("container" ==> _object_value) :: _object_builder
    [<CustomOperation "container">]
    member _.ContainerOp(_object_builder: FableObject, _object_value: Bounds) = ("container" ==> _object_value) :: _object_builder
    [<CustomOperation "container">]
    member _.ContainerOp(_object_builder: FableObject, _object_value: FunctionValue<Bounds>) = ("container" ==> _object_value) :: _object_builder
    [<CustomOperation "containerPadding">]
    member _.containerPaddingOp(_object_builder: FableObject, _object_value: float) = ("containerPadding" ==> _object_value) :: _object_builder
    [<CustomOperation "containerPadding">]
    member _.containerPaddingOp(_object_builder: FableObject, _object_value: Bounds) = ("containerPadding" ==> _object_value) :: _object_builder
    [<CustomOperation "containerPadding">]
    member _.containerPaddingOp(_object_builder: FableObject, _object_value: FunctionValue<Bounds>) = ("containerPadding" ==> _object_value) :: _object_builder
    [<CustomOperation "containerPadding">]
    member _.containerPaddingOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("containerPadding" ==> _object_value) :: _object_builder
    [<CustomOperation "containerFriction">]
    member _.containerFrictionOp(_object_builder: FableObject, _object_value: float) = ("containerFriction" ==> _object_value) :: _object_builder
    [<CustomOperation "releaseMass">]
    member _.releaseMassOp(_object_builder: FableObject, _object_value: float) = ("releaseMass" ==> _object_value) :: _object_builder
    [<CustomOperation "releaseMass">]
    member _.releaseMassOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("releaseMass" ==> _object_value) :: _object_builder
    [<CustomOperation "releaseStiffness">]
    member _.releaseStiffnessOp(_object_builder: FableObject, _object_value: float) = ("releaseStiffness" ==> _object_value) :: _object_builder
    [<CustomOperation "releaseStiffness">]
    member _.releaseStiffnessOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("releaseStiffness" ==> _object_value) :: _object_builder
    [<CustomOperation "releaseDamping">]
    member _.releaseDampingOp(_object_builder: FableObject, _object_value: float) = ("releaseDamping" ==> _object_value) :: _object_builder
    [<CustomOperation "releaseDamping">]
    member _.releaseDampingOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("releaseDamping" ==> _object_value) :: _object_builder
    [<CustomOperation "releaseEase">]
    member _.releaseEaseOp(_object_builder: FableObject, _object_value: float -> float) = ("releaseEase" ==> _object_value) :: _object_builder
    [<CustomOperation "releaseEase">]
    member _.releaseEaseOp(_object_builder: FableObject, _object_value: FunctionValue<float -> float>) = ("releaseEase" ==> _object_value) :: _object_builder
    [<CustomOperation "releaseContainerFriction">]
    member _.releaseContainerFrictionOp(_object_builder: FableObject, _object_value: float) = ("releaseContainerFriction" ==> _object_value) :: _object_builder
    [<CustomOperation "releaseContainerFriction">]
    member _.releaseContainerFrictionOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("releaseContainerFriction" ==> _object_value) :: _object_builder
    [<CustomOperation "minVelocity">]
    member _.minVelocityOp(_object_builder: FableObject, _object_value: float) = ("minVelocity" ==> _object_value) :: _object_builder
    [<CustomOperation "minVelocity">]
    member _.minVelocityOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("minVelocity" ==> _object_value) :: _object_builder
    [<CustomOperation "maxVelocity">]
    member _.maxVelocityOp(_object_builder: FableObject, _object_value: float) = ("maxVelocity" ==> _object_value) :: _object_builder
    [<CustomOperation "maxVelocity">]
    member _.maxVelocityOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("maxVelocity" ==> _object_value) :: _object_builder
    [<CustomOperation "velocityMultiplier">]
    member _.velocityMultiplierOp(_object_builder: FableObject, _object_value: float) = ("velocityMultiplier" ==> _object_value) :: _object_builder
    [<CustomOperation "velocityMultiplier">]
    member _.velocityMultiplierOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("velocityMultiplier" ==> _object_value) :: _object_builder
    [<CustomOperation "dragSpeed">]
    member _.dragSpeedOp(_object_builder: FableObject, _object_value: float) = ("dragSpeed" ==> _object_value) :: _object_builder
    [<CustomOperation "dragSpeed">]
    member _.dragSpeedOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("dragSpeed" ==> _object_value) :: _object_builder
    [<CustomOperation "scrollThreshold">]
    member _.scrollThresholdOp(_object_builder: FableObject, _object_value: float) = ("scrollThreshold" ==> _object_value) :: _object_builder
    [<CustomOperation "scrollThreshold">]
    member _.scrollThresholdOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("scrollThreshold" ==> _object_value) :: _object_builder
    [<CustomOperation "scrollSpeed">]
    member _.scrollSpeedOp(_object_builder: FableObject, _object_value: float) = ("scrollSpeed" ==> _object_value) :: _object_builder
    [<CustomOperation "scrollSpeed">]
    member _.scrollSpeedOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("scrollSpeed" ==> _object_value) :: _object_builder
    [<CustomOperation "snap">]
    member inline _.SnapOp(_object_builder: FableObject, value: float) = ("snap" ==> value) :: _object_builder
    [<CustomOperation "snap">]
    member inline _.SnapOp(_object_builder: FableObject, value: float, value2: float) = ("snap" ==> (value, value2)) :: _object_builder
    [<CustomOperation "snap">]
    member inline _.SnapOp(_object_builder: FableObject, value: FunctionValue<float>) = ("snap" ==> value) :: _object_builder
    [<CustomOperation "snap">]
    member inline _.SnapOp(_object_builder: FableObject, value: FunctionValue<float * float>) = ("snap" ==> value) :: _object_builder
    [<CustomOperation "modifier">]
    member inline _.ModifierOp(_object_builder: FableObject, value: FloatModifier) = ("modifier" ==> value) :: _object_builder
    [<CustomOperation "mapTo">]
    member inline _.MapToOp(_object_builder: FableObject, value: string) = ("mapTo" ==> value) :: _object_builder
let draggableOptions = DraggableOptionsComputation()

let f = draggableOptions {
    dragCursor { onGrab false }
    x (dragAxis { true })
    container (bounds { top 5 })
    
}


type ScrollObserverOptionsComputation() =
    interface FableObjectBuilder
    [<CustomOperation "axis">]
    member inline _.axisOp(_object_builder: FableObject, value: Axis) = ("axis" ==> value) :: _object_builder
    [<CustomOperation "container">]
    member inline _.containerOp(_object_builder: FableObject, value: Selector) = ("container" ==> value) :: _object_builder
    [<CustomOperation "container">]
    member inline _.containerOp(_object_builder: FableObject, value: string) = ("container" ==> value) :: _object_builder
    [<CustomOperation "container">]
    member inline _.containerOp(_object_builder: FableObject, value: #HTMLElement) = ("container" ==> value) :: _object_builder
    [<CustomOperation "target">]
    member inline _.targetOp(_object_builder: FableObject, value: Selector) = ("target" ==> value) :: _object_builder
    [<CustomOperation "target">]
    member inline _.targetOp(_object_builder: FableObject, value: string) = ("target" ==> value) :: _object_builder
    [<CustomOperation "target">]
    member inline _.targetOp(_object_builder: FableObject, value: #HTMLElement) = ("target" ==> value) :: _object_builder
    [<CustomOperation "debug">]
    member inline _.debugOp(_object_builder: FableObject) = ("debug" ==> true) :: _object_builder
    [<CustomOperation "debug">]
    member inline _.debugOp(_object_builder: FableObject, value: bool) = ("debug" ==> value) :: _object_builder
    [<CustomOperation "repeat">]
    member inline _.repeatOp(_object_builder: FableObject) = ("repeat" ==> true) :: _object_builder
    [<CustomOperation "repeat">]
    member inline _.repeatOp(_object_builder: FableObject, value: bool) = ("repeat" ==> value) :: _object_builder
    [<CustomOperation "sync">]
    member inline _.syncOp(_object_builder: FableObject) = ("sync" ==> true) :: _object_builder
    [<CustomOperation "sync">]
    member inline _.syncOp(_object_builder: FableObject, value: bool) = ("sync" ==> value) :: _object_builder
    [<CustomOperation "sync">]
    member inline _.syncOp(_object_builder: FableObject, enter: string) = ("sync" ==> enter) :: _object_builder
    [<CustomOperation "sync">]
    member inline _.syncOp(_object_builder: FableObject, enter: string, leave: string) = ("sync" ==> $"{enter} {leave}") :: _object_builder
    [<CustomOperation "sync">]
    member inline _.syncOp(_object_builder: FableObject, enter: string, leave: string, enterBackward: string, leaveBackward: string) = ("sync" ==> $"{enter} {leave} {enterBackward} {leaveBackward}") :: _object_builder
    [<CustomOperation "sync">]
    member inline _.syncOp(_object_builder: FableObject, value: float) = ("sync" ==> value) :: _object_builder
    [<CustomOperation "sync">]
    member inline _.syncOp(_object_builder: FableObject, value: float -> float) = ("sync" ==> value) :: _object_builder
    [<CustomOperation "enter">]
    member inline _.enterOp(_object_builder: FableObject, value: string) = ("enter" ==> value) :: _object_builder
    [<CustomOperation "enter">]
    member inline _.enterOp(_object_builder: FableObject, value: ObserverThreshold) = ("enter" ==> value) :: _object_builder
    [<CustomOperation "enter">]
    member inline _.enterOp(_object_builder: FableObject, value: ObserverThreshold, target: ObserverThreshold) = ("enter" ==> {| container = value; target = target |}) :: _object_builder
    [<CustomOperation "enter">]
    member inline _.enterOp(_object_builder: FableObject, value: string, target: ObserverThreshold) = ("enter" ==> {| container = value; target = target |}) :: _object_builder
    [<CustomOperation "enter">]
    member inline _.enterOp(_object_builder: FableObject, value: string, target: string) = ("enter" ==> {| container = value; target = target |}) :: _object_builder
    [<CustomOperation "enter">]
    member inline _.enterOp(_object_builder: FableObject, value: ObserverThreshold, target: string) = ("enter" ==> {| container = value; target = target |}) :: _object_builder
    [<CustomOperation "leave">]
    member inline _.leaveOp(_object_builder: FableObject, value: string) = ("leave" ==> value) :: _object_builder
    [<CustomOperation "leave">]
    member inline _.leaveOp(_object_builder: FableObject, value: ObserverThreshold) = ("leave" ==> value) :: _object_builder
    [<CustomOperation "leave">]
    member inline _.leaveOp(_object_builder: FableObject, value: ObserverThreshold, target: ObserverThreshold) = ("leave" ==> {| container = value; target = target |}) :: _object_builder
    [<CustomOperation "leave">]
    member inline _.leaveOp(_object_builder: FableObject, value: string, target: ObserverThreshold) = ("leave" ==> {| container = value; target = target |}) :: _object_builder
    [<CustomOperation "leave">]
    member inline _.leaveOp(_object_builder: FableObject, value: string, target: string) = ("leave" ==> {| container = value; target = target |}) :: _object_builder
    [<CustomOperation "leave">]
    member inline _.leaveOp(_object_builder: FableObject, value: ObserverThreshold, target: string) = ("leave" ==> {| container = value; target = target |}) :: _object_builder
    member inline _.Run(_object_run: FableObject): Binding.ScrollObserver = createObj _object_run |> unbox |> Binding.Exports.onScroll
    
let onScroll = ScrollObserverOptionsComputation()
let sdfasfas = onScroll {
    debug
    target ""
}

type StaggerOptionsComputation(value: float) =
    interface FableObjectBuilder
    [<CustomOperation "start">]
    member inline _.startOp(_object_builder: FableObject, value: float) = ("start" ==> value) :: _object_builder
    [<CustomOperation "start">]
    member inline _.startOp(_object_builder: FableObject, value: Binding.TimePosition) = ("start" ==> value) :: _object_builder
    [<CustomOperation "from">]
    member inline _.fromOp(_object_builder: FableObject, value: float) = ("from" ==> value) :: _object_builder
    [<CustomOperation "from">]
    member inline _.fromOp(_object_builder: FableObject, value: Binding.TimePosition) = ("from" ==> value) :: _object_builder
    [<CustomOperation "reversed">]
    member inline _.reversedOp(_object_builder: FableObject, value: bool) = ("reversed" ==> value) :: _object_builder
    [<CustomOperation "reversed">]
    member inline _.reversedOp(_object_builder: FableObject) = ("reversed" ==> true) :: _object_builder
    [<CustomOperation "ease">]
    member inline _.easeOp(_object_builder: FableObject, value: float -> float) = ("ease" ==> value) :: _object_builder
    [<CustomOperation "axis">]
    member inline _.axisOp(_object_builder: FableObject, value: Axis) = ("axis" ==> value) :: _object_builder
    [<CustomOperation "modifier">]
    member inline _.modifierOp(_object_builder: FableObject, value: FloatModifier) = ("modifier" ==> value) :: _object_builder
    [<CustomOperation "grid">]
    member inline _.gridOp(_object_builder: FableObject, value: float, value2: float) = ("grid" ==> (value, value2)) :: _object_builder
    member inline _.Run(_object_run: FableObject): FunctionValue<float> =
        let stagger value options = import "stagger" Spec.path
        stagger value (createObj _object_run)

let stagger value = StaggerOptionsComputation(value)

let asddf = stagger (100) {}
