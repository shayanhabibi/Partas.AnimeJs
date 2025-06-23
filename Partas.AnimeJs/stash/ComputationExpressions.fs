module Partas.AnimeJs.ComputationExpressions

open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Fable.Core
open Partas.AnimeJs.Core
open Browser.Types

open Fable.Core.JsInterop
open Fable.Core.JS
open Fable.Core.DynamicExtensions
/// Alias for a string * obj list
type FableObject = (string * obj) list
/// Implements boiler plate for builder expressions which are creating JS objects
type FableObjectBuilder = interface end
[<AutoOpen>]
module FableObjectBuilderExtension =
    type FableObjectBuilder with
        // Can initialise a sequence with an empty list
        member inline _.Yield(_: unit): FableObject = []
        // member inline _.Yield(_: unit) = ()
        member inline _.Yield(_object_value: string * obj) = _object_value
        member inline _.Yield(_object_builder: FableObject) = _object_builder
        // hack all the possibilities away. redundancies.
        member inline _.Combine(_object_unit: unit, _object_value: string * obj) =
            [_object_value]
        member inline _.Combine(_object_value: string * obj, _: unit) =
            [_object_value]
        member inline _.Combine(_object_value: FableObject) =
            _object_value
        member inline _.Combine(_object_value: string * obj, _object_value2: string * obj) =
            [_object_value; _object_value2]
        member inline _.Combine(_object_builder: FableObject, _object_value: string * obj) = _object_value :: _object_builder
        member inline _.Combine(_object_builder: FableObject, _object_builder2: FableObject) = _object_builder @ _object_builder2
        member inline _.Combine(_object_builder: string * obj, _object_builder2: FableObject) = _object_builder :: _object_builder2
        // get rid of it
        member inline _.Delay([<InlineIfLambda>] value) = value()
        // If a custom operation comes after a general key,value pair then the input is string * obj
        member inline _.For(_object_builder: FableObject, [<InlineIfLambda>] _object_builder_action: string * obj -> FableObject) =
            _object_builder |> List.collect _object_builder_action
        // If a custom operation is the first of the sequence/expression, then the input is unit
        member inline _.For(_object_builder: FableObject, [<InlineIfLambda>] _object_builder_action: unit -> FableObject) =
            _object_builder @ (_object_builder_action())
        member inline _.For(_object_builder, [<InlineIfLambda>] _object_builder_action) = _object_builder |> _object_builder_action
        member inline _.For(_object_builder, [<InlineIfLambda>] _object_builder_action: unit -> string * obj): FableObject =
            _object_builder_action() :: _object_builder

// Forward declaration for Playback computation expression
type PlaybackObjectBuilder =
    inherit FableObjectBuilder
    inherit EasePropertyComputation
and EasePropertyComputation =
    inherit FableObjectBuilder

type EasingFunc = delegate of float -> float
[<AutoOpen>]
module AutoOpenEasePropertyComputation =
    type EasePropertyComputation with
        member inline _.Yield( [<InlineIfLambda>] value: EasingFunc): FableObject = [ "ease" ==> value ]
        [<CustomOperation "linear">]
        member inline _.linearOpEase(_object_instance: FableObject, ?x1, ?x2, ?x3) =
            ("ease" ==> $"""linear({x1 |> Option.defaultValue 0.} {x2 |> Option.defaultValue "50%"} {x3 |> Option.defaultValue 1.}""") :: _object_instance
        [<CustomOperation "irregular">]
        member inline _.irregularOpEase(_object_instance: FableObject, ?length: float, ?randomness: float) =
            ("ease" ==> Eases.irregular(?length=length,?randomness=randomness))
        [<CustomOperation "steps">]
        member inline _.stepsOpEase(_object_instance: FableObject, ?steps: float, ?fromStart: bool) =
            ("ease" ==> Eases.steps(?steps=steps,?fromStart=fromStart)) :: _object_instance
        [<CustomOperation "cubicBezier">]
        member inline _.cubicBezierOpEase(_object_instance: FableObject, ?mX1: float, ?mY1: float, ?mX2: float, ?mY2: float) =
            ("ease" ==> Eases.cubicBezier(?mX1=mX1,?mX2=mX2,?mY1=mY1,?mY2=mY2)) :: _object_instance
        [<CustomOperation "easeIn">]
        member inline _.easeInOpEase(_object_instance: FableObject, ?power: float) =
            ("ease" ==> Eases.``in``(?power = power)) :: _object_instance
        [<CustomOperation "easeOut">]
        member inline _.easeOutOpEase(_object_instance: FableObject, ?power: float) =
            ("ease" ==> Eases.out(?power=power)) :: _object_instance
        [<CustomOperation "inOut">]
        member inline _.inOutOpEase(_object_instance: FableObject, ?power: float) =
            ("ease" ==> Eases.inOut(?power=power)) :: _object_instance
        [<CustomOperation "inBounce">]
        member inline _.inBounceOpEase(_object_instance: FableObject) =
            ("ease" ==> Eases.inBounce) :: _object_instance
        [<CustomOperation "outBounce">]
        member inline _.outBounceOpEase(_object_instance: FableObject) =
            ("ease" ==> Eases.outBounce) :: _object_instance
        [<CustomOperation "inOutBounce">]
        member inline _.inOutBounceOpEase(_object_instance: FableObject) =
            ("ease" ==> Eases.inOutBounce) :: _object_instance
        [<CustomOperation "inQuad">]
        member inline _.inQuadOpEase(_object_instance: FableObject) =
            ("ease" ==> Eases.inQuad) :: _object_instance
        [<CustomOperation "outQuad">]
        member inline _.outQuadOpEase(_object_instance: FableObject) =
            ("ease" ==> Eases.outQuad) :: _object_instance
        [<CustomOperation "inOutQuad">]
        member inline _.inOutQuadOpEase(_object_instance: FableObject) =
            ("ease" ==> Eases.inOutQuad) :: _object_instance
        [<CustomOperation "inCubic">]
        member inline _.inCubicOpEase(_object_instance: FableObject) =
            ("ease" ==> Eases.inCubic) :: _object_instance
        [<CustomOperation "outCubic">]
        member inline _.outCubicOpEase(_object_instance: FableObject) =
            ("ease" ==> Eases.outCubic) :: _object_instance
        [<CustomOperation "inOutCubic">]
        member inline _.inOutCubicOpEase(_object_instance: FableObject) =
            ("ease" ==> Eases.inOutCubic) :: _object_instance
        [<CustomOperation "inQuart">]
        member inline _.inQuartOpEase(_object_instance: FableObject) =
            ("ease" ==> Eases.inQuart) :: _object_instance
        [<CustomOperation "outQuart">]
        member inline _.outQuartOpEase(_object_instance: FableObject) =
            ("ease" ==> Eases.outQuart) :: _object_instance
        [<CustomOperation "inOutQuart">]
        member inline _.inOutQuartOpEase(_object_instance: FableObject) =
            ("ease" ==> Eases.inOutQuart) :: _object_instance
        [<CustomOperation "inQuint">]
        member inline _.inQuintOpEase(_object_instance: FableObject) =
            ("ease" ==> Eases.inQuint) :: _object_instance
        [<CustomOperation "outQuint">]
        member inline _.outQuintOpEase(_object_instance: FableObject) =
            ("ease" ==> Eases.outQuint) :: _object_instance
        [<CustomOperation "inOutQuint">]
        member inline _.inOutQuintOpEase(_object_instance: FableObject) =
            ("ease" ==> Eases.inOutQuint) :: _object_instance
        [<CustomOperation "inSine">]
        member inline _.inSineOpEase(_object_instance: FableObject) =
            ("ease" ==> Eases.inSine) :: _object_instance
        [<CustomOperation "outSine">]
        member inline _.outSineOpEase(_object_instance: FableObject) =
            ("ease" ==> Eases.outSine) :: _object_instance
        [<CustomOperation "inOutSine">]
        member inline _.inOutSineOpEase(_object_instance: FableObject) =
            ("ease" ==> Eases.inOutSine) :: _object_instance
        [<CustomOperation "inCirc">]
        member inline _.inCircOpEase(_object_instance: FableObject) =
            ("ease" ==> Eases.inCirc) :: _object_instance
        [<CustomOperation "outCirc">]
        member inline _.outCircOpEase(_object_instance: FableObject) =
            ("ease" ==> Eases.outCirc) :: _object_instance
        [<CustomOperation "inOutCirc">]
        member inline _.inOutCircOpEase(_object_instance: FableObject) =
            ("ease" ==> Eases.inOutCirc) :: _object_instance
        [<CustomOperation "inExpo">]
        member inline _.inExpoOpEase(_object_instance: FableObject) =
            ("ease" ==> Eases.inExpo) :: _object_instance
        [<CustomOperation "outExpo">]
        member inline _.outExpoOpEase(_object_instance: FableObject) =
            ("ease" ==> Eases.outExpo) :: _object_instance
        [<CustomOperation "inOutExpo">]
        member inline _.inOutExpoOpEase(_object_instance: FableObject) =
            ("ease" ==> Eases.inOutExpo) :: _object_instance
        [<CustomOperation "inElastic">]
        member inline _.inElasticOpEase(_object_instance: FableObject, ?amplitude: float, ?period: float) =
            ("ease" ==> Eases.inElastic(?amplitude=amplitude,?period=period)) :: _object_instance
        [<CustomOperation "outElastic">]
        member inline _.outElasticOpEase(_object_instance: FableObject, ?amplitude: float, ?period: float) =
            ("ease" ==> Eases.outElastic(?amplitude=amplitude,?period=period)) :: _object_instance
        [<CustomOperation "inOutElastic">]
        member inline _.inOutElasticOpEase(_object_instance: FableObject, ?amplitude: float, ?period: float) =
            ("ease" ==> Eases.inOutElastic(?amplitude=amplitude,?period=period)) :: _object_instance
        
        
/// <summary>
/// Contains implementations of the Relative Time Position operators.
/// All operators are prefixed with <c>!</c>
/// </summary>
/// <example><code>
/// let pos: RelativeTimePosition = !&lt;&lt;+= 5
/// // All operator signatures are 'a -> RelativeTimePosition via:
/// // unbox&lt;RelativeTimePosition> $"&lt;&lt;+={value}"
/// </code></example>
[<Erase>]
module RelativeOperators =
    let inline (!<<+=) value = unbox<RelativeTimePosition> $"<<+={value}"
    let inline (!<<-=) value = unbox<RelativeTimePosition> $"<<-={value}"
    let inline (!<<*=) value = unbox<RelativeTimePosition> $"<<*={value}"
    let inline (!*=) value = unbox<RelativeTweenValue> $"*={value}"
    let inline (!-=) value = unbox<RelativeTweenValue> $"-={value}"
    let inline (!+=) value = unbox<RelativeTweenValue> $"+={value}"


type [<Erase;System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
    PercentKeyframe = PercentKeyframe of string with
        interface FableObjectBuilder
        interface EasePropertyComputation

[<Erase>]
module Operators =
    let (!<<+=) = RelativeOperators.op_BangLessLessPlusEquals
    let (!<<-=) = RelativeOperators.op_BangLessLessMinusEquals
    let (!<<*=) = RelativeOperators.op_BangLessLessMultiplyEquals
    let (!*=) = RelativeOperators.op_BangMultiplyEquals
    let (!-=) = RelativeOperators.op_BangMinusEquals
    let (!+=) = RelativeOperators.op_BangPlusEquals
    let inline (!%%) value = PercentKeyframe $"{value}%%"

[<AutoOpen>]
module AutoOpenCssStyle =
    type ICssStyle =
        inherit EasePropertyComputation
    type CssStyle = interface end

/// Style computations implement all the accepted value types for tween/keyframes etc.
/// Monad wrappers are used to make the `to` (too) and `from` mutually exclusive properties.
type Style = CssStyle
and [<Erase; System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>] StyleValue = StyleValue of obj
and [<Erase; System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>] StyleObj = StyleObj of obj
and [<Erase; System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>] StyleToObj = StyleToObj of FableObject
and [<Erase; System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>] StyleFromObj = StyleFromObj of FableObject
and [<Erase; System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>] StyleArray = StyleArray of unit
and CssStyle with
    static member inline key: StyleArray = StyleArray ()
and ICssStyle with
    member inline this.Value: string = unbox this
    member inline _.For(_object_builder: FableObject, [<InlineIfLambda>] _object_builder_action: string * obj -> FableObject) =
        _object_builder |> List.collect _object_builder_action
    member inline _.Yield(_: unit): FableObject = []
    member inline _.Yield(value: ITuple) = StyleValue value
    member inline _.Yield(value: float) = StyleValue value
    member inline _.Yield(value: string) = StyleValue value
    member inline _.Yield(value: RelativeTweenValue) = StyleValue value
    member inline _.Delay([<InlineIfLambda>] value: unit -> StyleValue) = value()
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
    member inline _.EaseOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value_lambda: float -> float) = _object_builder @ ["ease" ==> _property_value_lambda]
    [<CustomOperation "composition">]
    member inline _.CompositionOp(_object_builder: FableObject, _property_value: Composition) = _object_builder @ ["composition" ==> _property_value]
    [<CustomOperation "modifier">]
    member inline _.ModifierOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value_lambda: FloatModifier) = _object_builder @ ["modifier" ==> _property_value_lambda]
    [<CustomOperation "delay">]
    member inline _.DelayOp(_object_builder: StyleToObj, _property_value: float) = StyleToObj(("delay" ==> _property_value) :: !!_object_builder)
    [<CustomOperation "delay">]
    member inline _.DelayOp(_object_builder: StyleToObj, _property_value_lambda: FunctionValue<float>) = StyleToObj(("delay" ==> _property_value_lambda) :: !!_object_builder)
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: StyleToObj, _property_value: float) = StyleToObj(("duration" ==> _property_value) :: !!_object_builder)
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: StyleToObj, _property_value_lambda: FunctionValue<float>) = StyleToObj(("duration" ==> _property_value_lambda) :: !!_object_builder)
    [<CustomOperation "ease">]
    member inline _.EaseOp(_object_builder: StyleToObj, [<InlineIfLambda>] _property_value_lambda: float -> float) = StyleToObj(("ease" ==> _property_value_lambda) :: !!_object_builder)
    [<CustomOperation "composition">]
    member inline _.CompositionOp(_object_builder: StyleToObj, _property_value: Composition) = StyleToObj(("composition" ==> _property_value) :: !!_object_builder)
    [<CustomOperation "modifier">]
    member inline _.ModifierOp(_object_builder: StyleToObj, _property_value_lambda: FloatModifier) = StyleToObj(("modifier" ==> _property_value_lambda) :: !!_object_builder)
    [<CustomOperation "delay">]
    member inline _.DelayOp(_object_builder: StyleFromObj, _property_value: float) = StyleFromObj(("delay" ==> _property_value) :: !!_object_builder)
    [<CustomOperation "delay">]
    member inline _.DelayOp(_object_builder: StyleFromObj, _property_value_lambda: FunctionValue<float>) = StyleFromObj(("delay" ==> _property_value_lambda) :: !!_object_builder)
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: StyleFromObj, _property_value: float) = StyleFromObj(("duration" ==> _property_value) :: !!_object_builder)
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: StyleFromObj, _property_value_lambda: FunctionValue<float>) = StyleFromObj(("duration" ==> _property_value_lambda) :: !!_object_builder)
    [<CustomOperation "ease">]
    member inline _.EaseOp(_object_builder: StyleFromObj, [<InlineIfLambda>] _property_value_lambda: float -> float) = StyleFromObj(("ease" ==> _property_value_lambda) :: !!_object_builder)
    [<CustomOperation "composition">]
    member inline _.CompositionOp(_object_builder: StyleFromObj, _property_value: Composition) = StyleFromObj(("composition" ==> _property_value) :: !!_object_builder)
    [<CustomOperation "modifier">]
    member inline _.ModifierOp(_object_builder: StyleFromObj, [<InlineIfLambda>] _property_value_lambda: FloatModifier) = StyleFromObj(("modifier" ==> _property_value_lambda) :: !!_object_builder)
    [<CustomOperation "irregular">]
    member inline _.irregularOpEase(
        _object_instance: StyleToObj,
 (*10*) length: float): StyleToObj =
        !!(("ease" ==> (FSharp.Ease.irregular length 1)) :: !!_object_instance)
    [<CustomOperation "irregular">]
    member inline _.irregularOpEase(
        _object_instance: StyleToObj,
 (*10*) length: float,
  (*1*) randomness: float): StyleToObj =
        !!(("ease" ==> (FSharp.Ease.irregular length randomness)) :: !!_object_instance)
    [<CustomOperation "linear">]
    member inline _.linearOpEase(_object_instance: StyleToObj, x1: float, x2: float, x3: float): StyleToObj =
        !!(("ease" ==> (FSharp.Ease.linear !^x1 !^x2 !^x3)) :: !!_object_instance)
    [<CustomOperation "steps">]
    member inline _.stepsOpEase(_object_instance: StyleToObj, steps: float): StyleToObj =
        !!(("ease" ==> (FSharp.Ease.steps steps)) :: !!_object_instance)
    [<CustomOperation "cubicBezier">]
    member inline _.cubicBezierOpEase(_object_instance: StyleToObj, x1,x2,x3,x4): StyleToObj =
        !!(("ease" ==> (FSharp.Ease.cubicBezier x1 x2 x3 x4)) :: !!_object_instance)
    [<CustomOperation "easeIn">]
    member inline _.easeInOpEase(
        _object_instance: StyleToObj): StyleToObj =
        !!(("ease" ==> (FSharp.Ease.in' 1.675)) :: !!_object_instance)
    [<CustomOperation "easeOut">]
    member inline _.easeOutOpEase(
        _object_instance: StyleToObj,
        ?power): StyleToObj =
        !!(("ease" ==> (FSharp.Ease.out (power |> Option.defaultValue 1.675))) :: !!_object_instance)
    [<CustomOperation "inOut">]
    member inline _.inOutOpEase(
        _object_instance: StyleToObj, ?power): StyleToObj =
        !!(("ease" ==> (FSharp.Ease.inOut (power |> Option.defaultValue 1.675))) :: !!_object_instance)
    [<CustomOperation "inBack">]
    member inline _.inBackOpEase(
        _object_instance: StyleToObj, power): StyleToObj =
        !!(("ease" ==> (FSharp.Ease.inBack (power |> Option.defaultValue 1.70158))) :: !!_object_instance)
    [<CustomOperation "outBack">]
    member inline _.outBackOpEase(
        _object_instance: StyleToObj, power): StyleToObj =
        !!(("ease" ==> (FSharp.Ease.outBack (power |> Option.defaultValue 1.70158))) :: !!_object_instance)
    [<CustomOperation "inOutBack">]
    member inline _.inOutBackOpEase(
        _object_instance: StyleToObj, power): StyleToObj =
        !!(("ease" ==> (FSharp.Ease.inOutBack (power |> Option.defaultValue 1.70158))) :: !!_object_instance)
    [<CustomOperation "inElastic">]
    member inline _.inElasticOpEase(
        _object_instance: StyleToObj, ?amplitude, ?period): StyleToObj =
        !!(("ease" ==> (FSharp.Ease.inElastic (amplitude |> Option.defaultValue 1) (period |> Option.defaultValue 0.3))) :: !!_object_instance)
    [<CustomOperation "outElastic">]
    member inline _.outElasticOpEase(
        _object_instance: StyleToObj, ?amplitude, ?period): StyleToObj =
        !!(("ease" ==> (FSharp.Ease.outElastic (amplitude |> Option.defaultValue 1) (period |> Option.defaultValue 0.3))) :: !!_object_instance)
    [<CustomOperation "inOutElastic">]
    member inline _.inOutElasticOpEase(_object_instance: StyleToObj, ?amplitude, ?period): StyleToObj =
        !!(("ease" ==> (FSharp.Ease.inOutElastic (amplitude |> Option.defaultValue 1) (period |> Option.defaultValue 0.3))) :: !!_object_instance)
    [<CustomOperation "irregular">]
    member inline _.irregularOpEase(
        _object_instance: StyleFromObj,
 (*10*) length: float): StyleFromObj =
        !!(("ease" ==> (FSharp.Ease.irregular length 1)) :: !!_object_instance)
    [<CustomOperation "irregular">]
    member inline _.irregularOpEase(
        _object_instance: StyleFromObj,
 (*10*) length: float,
  (*1*) randomness: float): StyleFromObj =
        !!(("ease" ==> (FSharp.Ease.irregular length randomness)) :: !!_object_instance)
    [<CustomOperation "linear">]
    member inline _.linearOpEase(_object_instance: StyleFromObj, x1: float, x2: float, x3: float): StyleFromObj =
        !!(("ease" ==> (FSharp.Ease.linear !^x1 !^x2 !^x3)) :: !!_object_instance)
    [<CustomOperation "steps">]
    member inline _.stepsOpEase(_object_instance: StyleFromObj, steps: float): StyleFromObj =
        !!(("ease" ==> (FSharp.Ease.steps steps)) :: !!_object_instance)
    [<CustomOperation "cubicBezier">]
    member inline _.cubicBezierOpEase(_object_instance: StyleFromObj, x1,x2,x3,x4): StyleFromObj =
        !!(("ease" ==> (FSharp.Ease.cubicBezier x1 x2 x3 x4)) :: !!_object_instance)
    [<CustomOperation "easeIn">]
    member inline _.easeInOpEase(
        _object_instance: StyleFromObj): StyleFromObj =
        !!(("ease" ==> (FSharp.Ease.in' 1.675)) :: !!_object_instance)
    [<CustomOperation "easeOut">]
    member inline _.easeOutOpEase(
        _object_instance: StyleFromObj,
        [<DefaultParameterValue 1.675; Optional>] power): StyleFromObj =
        !!(("ease" ==> (FSharp.Ease.out power)) :: !!_object_instance)
    [<CustomOperation "inOut">]
    member inline _.inOutOpEase(
        _object_instance: StyleFromObj,
        [<DefaultParameterValue 1.675; Optional>] power): StyleFromObj =
        !!(("ease" ==> (FSharp.Ease.inOut power)) :: !!_object_instance)
    [<CustomOperation "inBack">]
    member inline _.inBackOpEase(
        _object_instance: StyleFromObj,
        [<DefaultParameterValue 1.70158; Optional>] power): StyleFromObj =
        !!(("ease" ==> (FSharp.Ease.inBack power)) :: !!_object_instance)
    [<CustomOperation "outBack">]
    member inline _.outBackOpEase(
        _object_instance: StyleFromObj,
        [<DefaultParameterValue 1.70158; Optional>] power): StyleFromObj =
        !!(("ease" ==> (FSharp.Ease.outBack power)) :: !!_object_instance)
    [<CustomOperation "inOutBack">]
    member inline _.inOutBackOpEase(
        _object_instance: StyleFromObj,
        [<DefaultParameterValue 1.70158; Optional>] power): StyleFromObj =
        !!(("ease" ==> (FSharp.Ease.inOutBack power)) :: !!_object_instance)
    [<CustomOperation "inElastic">]
    member inline _.inElasticOpEase(
        _object_instance: StyleFromObj,
        [<DefaultParameterValue 1; Optional>] amplitude,
        [<DefaultParameterValue 0.3; Optional>] period): StyleFromObj =
        !!(("ease" ==> (FSharp.Ease.inElastic amplitude period)) :: !!_object_instance)
    [<CustomOperation "outElastic">]
    member inline _.outElasticOpEase(
        _object_instance: StyleFromObj,
        [<DefaultParameterValue 1; Optional>] amplitude,
        [<DefaultParameterValue 0.3; Optional>] period): StyleFromObj =
        !!(("ease" ==> (FSharp.Ease.outElastic amplitude period)) :: !!_object_instance)
    [<CustomOperation "inOutElastic">]
    member inline _.inOutElasticOpEase(_object_instance: StyleFromObj,
                                       [<DefaultParameterValue 1; Optional>] amplitude,
                                       [<DefaultParameterValue(0.3); Optional>] period): StyleFromObj =
        !!(("ease" ==> (FSharp.Ease.inOutElastic amplitude period)) :: !!_object_instance)
and StyleArray with
    [<CustomOperation "irregular">]
    member inline _.irregularOpEase(
        _object_instance: StyleToObj,
 (*10*) length: float): StyleToObj =
        !!(("ease" ==> (FSharp.Ease.irregular length 1)) :: !!_object_instance)
    [<CustomOperation "irregular">]
    member inline _.irregularOpEase(
        _object_instance: StyleToObj,
 (*10*) length: float,
  (*1*) randomness: float): StyleToObj =
        !!(("ease" ==> (FSharp.Ease.irregular length randomness)) :: !!_object_instance)
    [<CustomOperation "linear">]
    member inline _.linearOpEase(_object_instance: StyleToObj, x1: float, x2: float, x3: float): StyleToObj =
        !!(("ease" ==> (FSharp.Ease.linear !^x1 !^x2 !^x3)) :: !!_object_instance)
    [<CustomOperation "steps">]
    member inline _.stepsOpEase(_object_instance: StyleToObj, steps: float): StyleToObj =
        !!(("ease" ==> (FSharp.Ease.steps steps)) :: !!_object_instance)
    [<CustomOperation "cubicBezier">]
    member inline _.cubicBezierOpEase(_object_instance: StyleToObj, x1,x2,x3,x4): StyleToObj =
        !!(("ease" ==> (FSharp.Ease.cubicBezier x1 x2 x3 x4)) :: !!_object_instance)
    [<CustomOperation "easeIn">]
    member inline _.easeInOpEase(
        _object_instance: StyleToObj): StyleToObj =
        !!(("ease" ==> (FSharp.Ease.in' 1.675)) :: !!_object_instance)
    [<CustomOperation "easeOut">]
    member inline _.easeOutOpEase(
        _object_instance: StyleToObj, ?power): StyleToObj =
        !!(("ease" ==> (FSharp.Ease.out (power |> Option.defaultValue 1.675))) :: !!_object_instance)
    [<CustomOperation "inOut">]
    member inline _.inOutOpEase(
        _object_instance: StyleToObj, ?power): StyleToObj =
        !!(("ease" ==> (FSharp.Ease.inOut (power |> Option.defaultValue 1.675))) :: !!_object_instance)
    [<CustomOperation "inBack">]
    member inline _.inBackOpEase(
        _object_instance: StyleToObj, ?power): StyleToObj =
        !!(("ease" ==> (FSharp.Ease.inBack (power |> Option.defaultValue 1.70158))) :: !!_object_instance)
    [<CustomOperation "outBack">]
    member inline _.outBackOpEase(
        _object_instance: StyleToObj, ?power): StyleToObj =
        !!(("ease" ==> (FSharp.Ease.outBack (power |> Option.defaultValue 1.70158))) :: !!_object_instance)
    [<CustomOperation "inOutBack">]
    member inline _.inOutBackOpEase(
        _object_instance: StyleToObj, ?power): StyleToObj =
        !!(("ease" ==> (FSharp.Ease.inOutBack (power |> Option.defaultValue 1.70158))) :: !!_object_instance)
    [<CustomOperation "inElastic">]
    member inline _.inElasticOpEase(
        _object_instance: StyleToObj, ?amplitude, ?period): StyleToObj =
        !!(("ease" ==> (FSharp.Ease.inElastic (amplitude |> Option.defaultValue 1) (period |> Option.defaultValue 0.3))) :: !!_object_instance)
    [<CustomOperation "outElastic">]
    member inline _.outElasticOpEase(
        _object_instance: StyleToObj, ?amplitude, ?period): StyleToObj =
        !!(("ease" ==> (FSharp.Ease.outElastic (amplitude |> Option.defaultValue 1) (period |> Option.defaultValue 0.3))) :: !!_object_instance)
    [<CustomOperation "inOutElastic">]
    member inline _.inOutElasticOpEase(_object_instance: StyleToObj, ?amplitude, ?period): StyleToObj =
        !!(("ease" ==> (FSharp.Ease.inOutElastic (amplitude |> Option.defaultValue 1) (period |> Option.defaultValue 0.3))) :: !!_object_instance)
    [<CustomOperation "irregular">]
    member inline _.irregularOpEase(
        _object_instance: StyleFromObj,
 (*10*) length: float): StyleFromObj =
        !!(("ease" ==> (FSharp.Ease.irregular length 1)) :: !!_object_instance)
    [<CustomOperation "irregular">]
    member inline _.irregularOpEase(
        _object_instance: StyleFromObj,
 (*10*) length: float,
  (*1*) randomness: float): StyleFromObj =
        !!(("ease" ==> (FSharp.Ease.irregular length randomness)) :: !!_object_instance)
    [<CustomOperation "linear">]
    member inline _.linearOpEase(_object_instance: StyleFromObj, x1: float, x2: float, x3: float): StyleFromObj =
        !!(("ease" ==> (FSharp.Ease.linear !^x1 !^x2 !^x3)) :: !!_object_instance)
    [<CustomOperation "steps">]
    member inline _.stepsOpEase(_object_instance: StyleFromObj, steps: float): StyleFromObj =
        !!(("ease" ==> (FSharp.Ease.steps steps)) :: !!_object_instance)
    [<CustomOperation "cubicBezier">]
    member inline _.cubicBezierOpEase(_object_instance: StyleFromObj, x1,x2,x3,x4): StyleFromObj =
        !!(("ease" ==> (FSharp.Ease.cubicBezier x1 x2 x3 x4)) :: !!_object_instance)
    [<CustomOperation "easeIn">]
    member inline _.easeInOpEase(
        _object_instance: StyleFromObj): StyleFromObj =
        !!(("ease" ==> (FSharp.Ease.in' 1.675)) :: !!_object_instance)
    [<CustomOperation "easeOut">]
    member inline _.easeOutOpEase(
        _object_instance: StyleFromObj, ?power): StyleFromObj =
        !!(("ease" ==> (FSharp.Ease.out ( power |> Option.defaultValue 1.675))) :: !!_object_instance)
    [<CustomOperation "inOut">]
    member inline _.inOutOpEase(
        _object_instance: StyleFromObj, ?power): StyleFromObj =
        !!(("ease" ==> (FSharp.Ease.inOut ( power |> Option.defaultValue 1.675))) :: !!_object_instance)
    [<CustomOperation "inBack">]
    member inline _.inBackOpEase(
        _object_instance: StyleFromObj, ?power): StyleFromObj =
        !!(("ease" ==> (FSharp.Ease.inBack (power |> Option.defaultValue 1.70158))) :: !!_object_instance)
    [<CustomOperation "outBack">]
    member inline _.outBackOpEase(
        _object_instance: StyleFromObj, ?power): StyleFromObj =
        !!(("ease" ==> (FSharp.Ease.outBack (power |> Option.defaultValue 1.70158))) :: !!_object_instance)
    [<CustomOperation "inOutBack">]
    member inline _.inOutBackOpEase(
        _object_instance: StyleFromObj, ?power): StyleFromObj =
        !!(("ease" ==> (FSharp.Ease.inOutBack (power |> Option.defaultValue 1.70158))) :: !!_object_instance)
    [<CustomOperation "inElastic">]
    member inline _.inElasticOpEase(
        _object_instance: StyleFromObj, ?amplitude, ?period): StyleFromObj =
        !!(("ease" ==> (FSharp.Ease.inElastic (amplitude |> Option.defaultValue 1) (period |> Option.defaultValue 0.3))) :: !!_object_instance)
    [<CustomOperation "outElastic">]
    member inline _.outElasticOpEase(
        _object_instance: StyleFromObj, ?amplitude, ?period): StyleFromObj =
        !!(("ease" ==> (FSharp.Ease.outElastic (amplitude |> Option.defaultValue 1) (period |> Option.defaultValue 0.3))) :: !!_object_instance)
    [<CustomOperation "inOutElastic">]
    member inline _.inOutElasticOpEase(_object_instance: StyleFromObj, ?amplitude, ?period): StyleFromObj =
        !!(("ease" ==> (FSharp.Ease.inOutElastic (amplitude |> Option.defaultValue 1) (period |> Option.defaultValue 0.3))) :: !!_object_instance)
    member inline _.For(_object_builder: FableObject, [<InlineIfLambda>] _object_builder_action: string * obj -> FableObject) =
        _object_builder |> List.collect _object_builder_action
    member inline _.Yield(_: unit): FableObject = []
    member inline _.Yield(value: ITuple) = StyleValue value
    member inline _.Delay([<InlineIfLambda>] value: unit -> StyleValue) = value()
    member inline _.Yield(value: float) = StyleValue value
    member inline _.Yield(value: string) = StyleValue value
    member inline _.Yield(value: RelativeTweenValue) = StyleValue value
    member inline _.Delay([<InlineIfLambda>] value: unit -> FableObject) = value()
    member inline _.Delay([<InlineIfLambda>] value: unit -> StyleFromObj) = value()
    member inline _.Delay([<InlineIfLambda>] value: unit -> StyleToObj) = value()
    member inline _.Combine(value) = fun () -> value
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/from">See the docs</a>
    [<CustomOperation "from">]
    member inline _.fromOp(_object_builder: FableObject, _property_value: float) = StyleFromObj(("from" ==> _property_value) :: _object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/from">See the docs</a>
    [<CustomOperation "from">]
    member inline _.fromOp(_object_builder: FableObject, _property_value: FunctionValue<float>) = StyleFromObj(("from" ==> _property_value) :: _object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/from">See the docs</a>
    [<CustomOperation "from">]
    member inline _.fromOp(_object_builder: FableObject, _property_value: string) = StyleFromObj(("from" ==> _property_value) :: _object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/from">See the docs</a>
    [<CustomOperation "from">]
    member inline _.fromOp(_object_builder: FableObject, _property_value: RelativeTweenValue) = StyleFromObj(("from" ==> _property_value) :: _object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/to">See the docs</a>
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, _property_value: float) = StyleToObj(("to" ==> _property_value) :: _object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/to">See the docs</a>
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value: FunctionValue<float>) = StyleToObj(("to" ==> _property_value) :: _object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/to">See the docs</a>
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, _property_value: string) = StyleToObj(("to" ==> _property_value) :: _object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/to">See the docs</a>
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, _property_value: RelativeTweenValue) = StyleToObj(("to" ==> _property_value) :: _object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/to">See the docs</a>
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, _property_value: RelativeTweenValue, _property_value2: RelativeTweenValue) = StyleToObj(("to" ==> (_property_value, _property_value2)) :: _object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/to">See the docs</a>
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, _property_value: float, _property_value2: float) = StyleToObj(("to" ==> (_property_value, _property_value2)) :: _object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/to">See the docs</a>
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, _property_value: string, _property_value2: string) = StyleToObj(("to" ==> (_property_value, _property_value2)) :: _object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/to">See the docs</a>
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, _property_value: RelativeTweenValue, _property_value2: float) = StyleToObj(("to" ==> (_property_value, _property_value2)) :: _object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/to">See the docs</a>
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, _property_value: float, _property_value2: RelativeTweenValue) = StyleToObj(("to" ==> (_property_value, _property_value2)) :: _object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/to">See the docs</a>
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value: FunctionValue<float>, [<InlineIfLambda>] _property_value2: FunctionValue<float>) = StyleToObj(("to" ==> (_property_value, _property_value2)) :: _object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/delay">See the docs</a>
    [<CustomOperation "delay">]
    member inline _.DelayOp(_object_builder: FableObject, _property_value: float) = _object_builder @ ["delay" ==> _property_value]
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/delay">See the docs</a>
    [<CustomOperation "delay">]
    member inline _.DelayOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value_lambda: FunctionValue<float>) = _object_builder @ ["delay" ==> _property_value_lambda]
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/duration">See the docs</a>
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: FableObject, _property_value: float) = _object_builder @ ["duration" ==> _property_value]
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/duration">See the docs</a>
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value_lambda: FunctionValue<float>) = _object_builder @ ["duration" ==> _property_value_lambda]
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/ease">See the docs</a>
    [<CustomOperation "ease">]
    member inline _.EaseOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value_lambda: float -> float) = _object_builder @ ["ease" ==> _property_value_lambda]
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/composition">See the docs</a>
    [<CustomOperation "composition">]
    member inline _.CompositionOp(_object_builder: FableObject, _property_value: Composition) = _object_builder @ ["composition" ==> _property_value]
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/modifier">See the docs</a>
    [<CustomOperation "modifier">]
    member inline _.ModifierOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value_lambda: FloatModifier) = _object_builder @ ["modifier" ==> _property_value_lambda]
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/delay">See the docs</a>
    [<CustomOperation "delay">]
    member inline _.DelayOp(_object_builder: StyleToObj, _property_value: float) = StyleToObj(("delay" ==> _property_value) :: !!_object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/delay">See the docs</a>
    [<CustomOperation "delay">]
    member inline _.DelayOp(_object_builder: StyleToObj, [<InlineIfLambda>] _property_value_lambda: FunctionValue<float>) = StyleToObj(("delay" ==> _property_value_lambda) :: !!_object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/duration">See the docs</a>
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: StyleToObj, _property_value: float) = StyleToObj(("duration" ==> _property_value) :: !!_object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/duration">See the docs</a>
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: StyleToObj, [<InlineIfLambda>] _property_value_lambda: FunctionValue<float>) = StyleToObj(("duration" ==> _property_value_lambda) :: !!_object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/ease">See the docs</a>
    [<CustomOperation "ease">]
    member inline _.EaseOp(_object_builder: StyleToObj, [<InlineIfLambda>] _property_value_lambda: float -> float) = StyleToObj(("ease" ==> _property_value_lambda) :: !!_object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/composition">See the docs</a>
    [<CustomOperation "composition">]
    member inline _.CompositionOp(_object_builder: StyleToObj, _property_value: Composition) = StyleToObj(("composition" ==> _property_value) :: !!_object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/modifier">See the docs</a>
    [<CustomOperation "modifier">]
    member inline _.ModifierOp(_object_builder: StyleToObj, [<InlineIfLambda>] _property_value_lambda: FloatModifier) = StyleToObj(("modifier" ==> _property_value_lambda) :: !!_object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/delay">See the docs</a>
    [<CustomOperation "delay">]
    member inline _.DelayOp(_object_builder: StyleFromObj, _property_value: float) = StyleFromObj(("delay" ==> _property_value) :: !!_object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/delay">See the docs</a>
    [<CustomOperation "delay">]
    member inline _.DelayOp(_object_builder: StyleFromObj, [<InlineIfLambda>] _property_value_lambda: FunctionValue<float>) = StyleFromObj(("delay" ==> _property_value_lambda) :: !!_object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/duration">See the docs</a>
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: StyleFromObj, _property_value: float) = StyleFromObj(("duration" ==> _property_value) :: !!_object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/duration">See the docs</a>
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: StyleFromObj, [<InlineIfLambda>] _property_value_lambda: FunctionValue<float>) = StyleFromObj(("duration" ==> _property_value_lambda) :: !!_object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/ease">See the docs</a>
    [<CustomOperation "ease">]
    member inline _.EaseOp(_object_builder: StyleFromObj, [<InlineIfLambda>] _property_value_lambda: float -> float) = StyleFromObj(("ease" ==> _property_value_lambda) :: !!_object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/composition">See the docs</a>
    [<CustomOperation "composition">]
    member inline _.CompositionOp(_object_builder: StyleFromObj, _property_value: Composition) = StyleFromObj(("composition" ==> _property_value) :: !!_object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/modifier">See the docs</a>
    [<CustomOperation "modifier">]
    member inline _.ModifierOp(_object_builder: StyleFromObj, [<InlineIfLambda>] _property_value_lambda: FloatModifier) = StyleFromObj(("modifier" ==> _property_value_lambda) :: !!_object_builder)

[<AutoOpen>]
type AutoOpenExtensions =
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
    [<Extension>]
    static member inline Run(_: StyleArray, runner: StyleValue): StyleValue = runner
    [<Extension>]
    static member inline Run(prop: StyleArray, runner: FableObject): StyleObj = !!createObj !!runner
    [<Extension>]
    static member inline Run(prop: StyleArray, runner: StyleToObj): StyleToObj = !!createObj !!runner
    [<Extension>]
    static member inline Run(prop: StyleArray, runner: StyleFromObj): StyleFromObj = !!createObj !!runner

// Forward interface declaration
// Implementation below
type TimerCallbackComputation<'T> =
    inherit FableObjectBuilder

[<AutoOpen>]
module AutoOpenTimerCallbackComputation =
    // Generic type param because multiple options use the same callbacks but return their
    // own type as the param
    type TimerCallbackComputation<'T> with
        [<CustomOperation "onBegin">]
        member inline _.onBeginOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value: Callback<'T>) = ("onBegin" ==> _property_value) :: _object_builder
        [<CustomOperation "onComplete">]
        member inline _.onCompleteOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value: Callback<'T>) = ("onComplete" ==> _property_value) :: _object_builder
        [<CustomOperation "onUpdate">]
        member inline _.onUpdateOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value: Callback<'T>) = ("onUpdate" ==> _property_value) :: _object_builder
        [<CustomOperation "onLoop">]
        member inline _.onLoopOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value: Callback<'T>) = ("onLoop" ==> _property_value) :: _object_builder
        [<CustomOperation "onPause">]
        member inline _.onPauseOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value: Callback<'T>) = ("onPause" ==> _property_value) :: _object_builder
        [<CustomOperation "andThen">]
        member inline _.thenOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value: Callback<'T>) = ("then" ==> _property_value) :: _object_builder

// Forward interface declaration
// implementation below
type AnimationCallbackComputation<'T> =
    inherit TimerCallbackComputation<'T>

[<AutoOpen>]
module AutoOpenAnimationCallbackComputation =
    // Generic type param because multiple types may inherit these callbacks and
    // only differ in the type that is provided as the parameter
    type AnimationCallbackComputation<'T> with
        [<CustomOperation "onBeforeUpdate">]
        member inline _.onBeforeUpdateOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value: Callback<'T>) = ("onBeforeUpdate" ==> _property_value) :: _object_builder
        [<CustomOperation "onRender">]
        member inline _.onRenderOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value: Callback<'T>) = ("onRender" ==> _property_value) :: _object_builder

// Forward interface declaration
// implementation below
type ScrollObserverCallbackComputation<'T> =
    interface end

[<AutoOpen>]
module AutoOpenScrollObserverCallbackComputation =
    type ScrollObserverCallbackComputation<'T> with
        [<CustomOperation "onEnter">]
        member inline _.onEnterOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value: Callback<'T>): FableObject =
            ("onEnter" ==> _property_value) :: _object_builder
        [<CustomOperation "onEnterForward">]
        member inline _.onEnterForwardOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value: Callback<'T>): FableObject =
            ("onEnterForward" ==> _property_value) :: _object_builder
        [<CustomOperation "onEnterBackward">]
        member inline _.onEnterBackwardOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value: Callback<'T>): FableObject =
            ("onEnterBackward" ==> _property_value) :: _object_builder
        [<CustomOperation "onLeave">]
        member inline _.onLeaveOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value: Callback<'T>): FableObject =
            ("onLeave" ==> _property_value) :: _object_builder
        [<CustomOperation "onLeaveForward">]
        member inline _.onLeaveForwardOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value: Callback<'T>): FableObject =
            ("onLeaveForward" ==> _property_value) :: _object_builder
        [<CustomOperation "onLeaveBackward">]
        member inline _.onLeaveBackwardOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value: Callback<'T>): FableObject =
            ("onLeaveBackward" ==> _property_value) :: _object_builder
        [<CustomOperation "onUpdate">]
        member inline _.onUpdateOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value: Callback<'T>): FableObject =
            ("onUpdate" ==> _property_value) :: _object_builder
        [<CustomOperation "onSyncComplete">]
        member inline _.onSyncCompleteOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value: Callback<'T>): FableObject =
            ("onSyncComplete" ==> _property_value) :: _object_builder

/// <a href="https://animejs.com/documentation/animation/tween-parameters">Implements Tween Parameters</a>
type TweenObjectBuilder() =
    interface FableObjectBuilder
    interface EasePropertyComputation
    [<CustomOperation "from">]
    member inline _.fromOp(_object_builder: FableObject, _property_value: float) = ("from" ==> _property_value) :: _object_builder
    [<CustomOperation "from">]
    member inline _.fromOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value: FunctionValue<float>) = ("from" ==> _property_value) :: _object_builder
    [<CustomOperation "from">]
    member inline _.fromOp(_object_builder: FableObject, _property_value: string) = ("from" ==> _property_value) :: _object_builder
    [<CustomOperation "from">]
    member inline _.fromOp(_object_builder: FableObject, _property_value: RelativeTweenValue) = ("from" ==> _property_value) :: _object_builder
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, _property_value: float) = ("to" ==> _property_value) :: _object_builder
    [<CustomOperation "too">]
    member inline _.toOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value: FunctionValue<float>) = ("to" ==> _property_value) :: _object_builder
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
    member inline _.toOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value: FunctionValue<float>, [<InlineIfLambda>] _property_value2: FunctionValue<float>) = ("to" ==> (_property_value, _property_value2)) :: _object_builder
    [<CustomOperation "delay">]
    member inline _.DelayOp(_object_builder: FableObject, _property_value: float) = _object_builder @ ["delay" ==> _property_value]
    [<CustomOperation "delay">]
    member inline _.DelayOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value_lambda: FunctionValue<float>) = _object_builder @ ["delay" ==> _property_value_lambda]
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: FableObject, _property_value: float) = _object_builder @ ["duration" ==> _property_value]
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: FableObject, [<InlineIfLambda>]  _property_value_lambda: FunctionValue<float>) = _object_builder @ ["duration" ==> _property_value_lambda]
    [<CustomOperation "composition">]
    member inline _.CompositionOp(_object_builder: FableObject, _property_value: Composition) = _object_builder @ ["composition" ==> _property_value]
    [<CustomOperation "modifier">]
    member inline _.ModifierOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value_lambda: FloatModifier) = _object_builder @ ["modifier" ==> _property_value_lambda]
    member inline _.Run(_object_builder_run: FableObject) = (createObj _object_builder_run)

type TweenOptionsComputation =
    inherit FableObjectBuilder
    inherit EasePropertyComputation

[<AutoOpen>]
module AutoOpenTweenOptionsComputation =
    type TweenOptionsComputation with
        [<CustomOperation "delay">]
        member inline _.DelayOp(_object_builder: FableObject, _property_value: float) = _object_builder @ ["delay" ==> _property_value]
        [<CustomOperation "delay">]
        member inline _.DelayOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value_lambda: FunctionValue<float>) = _object_builder @ ["delay" ==> _property_value_lambda]
        [<CustomOperation "duration">]
        member inline _.DurationOp(_object_builder: FableObject, _property_value: float) = _object_builder @ ["duration" ==> _property_value]
        [<CustomOperation "duration">]
        member inline _.DurationOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value_lambda: FunctionValue<float>) = _object_builder @ ["duration" ==> _property_value_lambda]
        [<CustomOperation "composition">]
        member inline _.CompositionOp(_object_builder: FableObject, _property_value: Composition) = _object_builder @ ["composition" ==> _property_value]
        [<CustomOperation "modifier">]
        member inline _.ModifierOp(_object_builder: FableObject, [<InlineIfLambda>] _property_value_lambda: FloatModifier) = _object_builder @ ["modifier" ==> _property_value_lambda]
        [<CustomOperation "delay">]
        member inline _.DelayOp(_object_builder: string * obj, _property_value: float) = [_object_builder; "delay" ==> _property_value]
        [<CustomOperation "delay">]
        member inline _.DelayOp(_object_builder: string * obj, [<InlineIfLambda>] _property_value_lambda: FunctionValue<float>) = [_object_builder; "delay" ==> _property_value_lambda]
        [<CustomOperation "duration">]
        member inline _.DurationOp(_object_builder: string * obj, _property_value: float) = [_object_builder; "duration" ==> _property_value]
        [<CustomOperation "duration">]
        member inline _.DurationOp(_object_builder: string * obj, [<InlineIfLambda>] _property_value_lambda: FunctionValue<float>) = [_object_builder; "duration" ==> _property_value_lambda]
        [<CustomOperation "composition">]
        member inline _.CompositionOp(_object_builder: string * obj, _property_value: Composition) = [_object_builder; "composition" ==> _property_value]
        [<CustomOperation "modifier">]
        member inline _.ModifierOp(_object_builder: string * obj, [<InlineIfLambda>] _property_value_lambda: FloatModifier) = [_object_builder; "modifier" ==> _property_value_lambda]
type KeyframesValue = interface end
type KeyframesPercentKeyValue = interface end
type KeyframeComputation() =
    interface FableObjectBuilder
    interface TweenOptionsComputation
    member inline _.Yield(_: unit): FableObject = []
    member inline _.For(_object_builder: FableObject, [<InlineIfLambda>] _object_builder_action: string * obj -> FableObject) =
        _object_builder |> List.collect _object_builder_action
    member inline _.Yield(_yield_value: string * obj): FableObject = [_yield_value]
    member inline _.Yield(_yield_value: StyleObj): FableObject = [!!_yield_value]
    member inline _.Yield(_yield_value: StyleValue): FableObject = [!!_yield_value]
    member inline _.Delay([<InlineIfLambda>] value: unit -> FableObject) = value()
    member inline _.Combine(y) = fun () -> y
    member inline _.Run(_object_run: FableObject): KeyframesValue  = !!createObj _object_run

type PercentKeyframe with
    member inline _.Yield(_yield_value: string * obj): FableObject = [ !!_yield_value ]
    member inline this.Run(_object_run: FableObject): KeyframesPercentKeyValue = !!(!!this ==> !!createObj _object_run)


type TimelineObj = FSharp.TimelineObj
type TimelineOptions = interface end
type TimerOptionsBaseComputation() =
    interface FableObjectBuilder
    interface PlaybackObjectBuilder
    interface TimerCallbackComputation<Binding.Timer>
type TimerOptionsComputation() =
    inherit TimerOptionsBaseComputation()
    member inline _.Run(_object_run: FableObject): Binding.TimerOptions = _object_run |> !!createObj
type TimerOptionsCreateComputation() =
    inherit TimerOptionsBaseComputation()
    member inline _.Run (_object_run: FableObject): Binding.Timer = _object_run |> !!createObj |> Binding.Export.createTimer
type Binding.Timer with
    member inline this.Yield(_: unit) = ()
    member inline this.Zero() = ()
    [<CustomOperation "currentTime">]
    member inline this.currentTimeOp(_,value: float) = this.currentTime <- !!value
    [<CustomOperation "iterationCurrentTime">]
    member inline this.iterationCurrentTimeOp(_,value: float) = this.iterationCurrentTime <- !!value
    [<CustomOperation "progress">]
    member inline this.progressOp(_,value: float) = this.progress <- !!value
    [<CustomOperation "iterationProgress">]
    member inline this.iterationProgressOp(_,value: float) = this.iterationProgress <- !!value
    [<CustomOperation "currentIteration">]
    member inline this.currentIterationOp(_,value: float) = this.currentIteration <- !!value
    [<CustomOperation "speed">]
    member inline this.speedOp(_,value: float) = this.speed <- !!value
    [<CustomOperation "fps">]
    member inline this.fpsOp(_,value: float) = this.fps <- !!value
    [<CustomOperation "paused">]
    member inline this.pausedOp(_,value: bool) = this.paused <- !!value
    [<CustomOperation "began">]
    member inline this.beganOp(_,value: bool) = this.began <- !!value
    [<CustomOperation "completed">]
    member inline this.completedOp(_,value: bool) = this.completed <- !!value
    [<CustomOperation "reversed">]
    member inline this.reversedOp(_,value: bool) = this.reversed <- !!value
    [<CustomOperation "play">]
    member inline this.playOp(_) = this.play()
    [<CustomOperation "reverse">]
    member inline this.reverseOp(_) = this.reverse()
    [<CustomOperation "pause">]
    member inline this.pauseOp(_) = this.pause()
    [<CustomOperation "restart">]
    member inline this.restartOp(_) = this.restart()
    [<CustomOperation "alternate">]
    member inline this.alternateOp(_) = this.alternate()
    [<CustomOperation "resume">]
    member inline this.resumeOp(_) = this.resume()
    [<CustomOperation "complete">]
    member inline this.completeOp(_) = this.complete()
    [<CustomOperation "cancel">]
    member inline this.cancelOp(_) = this.cancel()
    [<CustomOperation "revert">]
    member inline this.revertOp(_) = this.revert()
    [<CustomOperation "seek">]
    member inline this.seekOp(_, value: float, ?muteCallbacks: bool) = this.seek(!!value, ?muteCallbacks = muteCallbacks)
    [<CustomOperation "stretch">]
    member inline this.stretchOp(_, value: float) = this.stretch(!!value)
    [<CustomOperation "andThen">]
    member inline this.andThenOp(_, value: Binding.Timer -> JS.Promise<_>) = this?``then``(value)
    member inline this.Run(_) = this 

type TimelineOptionsComputation() =
    interface FableObjectBuilder
    interface PlaybackObjectBuilder
    interface AnimationCallbackComputation<FSharp.TimelineObj>
    member inline _.Yield (value: Binding.TimerOptions) = "defaults" ==> value
    /// <summary>
    /// <c>defaults</c><br/>
    /// <c>Binding.TimerOptions -> ...</c>
    /// </summary>
    /// <remarks>
    /// <list type="table">
    /// <item> <term>
    /// Operation:
    /// </term> <description>
    /// <c>defaults</c>
    /// </description> </item>
    /// <item> <term>
    /// Args Num:
    /// </term> <description>
    /// <c>1</c>
    /// </description> </item>
    /// </list>
    /// <p><c>Binding.TimerOptions</c></p>
    /// </remarks>
    /// <returns><c>(string * obj) list</c></returns>
    [<CustomOperation "defaults">]
    member inline _.defaultsOp(_object_builder: FableObject, value: Binding.TimerOptions): FableObject = ("defaults" ==> value) :: !!_object_builder
    member inline _.Run (_object_run: FableObject): TimelineObj = _object_run |> createObj |> import "createTimeline" Spec.path
type FSharp.TimelineObj with
// type TimelineComputation(?timeline: TimelineObj) =
    // member inline _.Yield(_: unit) = timeline |> Option.defaultValue (() |> import "createTimeline" Spec.path)
    member inline this.Yield(_: unit) = ()
    member inline this.Zero() = ()
    [<CustomOperation "add">]
    member inline this.AddOp(_, _object_method_arg: string, _object_method: obj) =
        this._add(Selector _object_method_arg, unbox (_object_method)) |> ignore
    [<CustomOperation "add">]
    member inline this.AddOp(_, _object_method_arg: string, _object_method: obj, value: Binding.TimePosition) =
        this._add(Selector _object_method_arg, unbox (_object_method), !!value) |> ignore
    [<CustomOperation "add">]
    member inline this.AddOp(_, _object_method_arg: string, _object_method: obj, value: RelativeTimePosition) =
        this._add(Selector _object_method_arg, unbox (_object_method), !!value) |> ignore
    [<CustomOperation "add">]
    member inline this.AddOp(_, _object_method_arg: string, _object_method: obj, value: RelativeTweenValue) =
        this._add(Selector _object_method_arg, unbox (_object_method), !!value) |> ignore
    [<CustomOperation "restart">]
    member inline this.restartOp(_) = this.restart() |> ignore
    [<CustomOperation "play">]
    member inline this.playOp(_) = this.play() |> ignore
    [<CustomOperation "init">]
    member inline this.initOp(_) = this.init() |> ignore
    [<CustomOperation "reverse">]
    member inline this.reverseOp(_) = this.reverse() |> ignore
    [<CustomOperation "pause">]
    member inline this.pauseOp(_) = this.pause() |> ignore
    [<CustomOperation "alternate">]
    member inline this.alternateOp(_) = this.alternate() |> ignore
    [<CustomOperation "resume">]
    member inline this.resumeOp(_) = this.resume() |> ignore
    [<CustomOperation "complete">]
    member inline this.completeOp(_) = this.complete() |> ignore
    [<CustomOperation "cancel">]
    member inline this.cancelOp(_) = this.cancel() |> ignore
    [<CustomOperation "revert">]
    member inline this.revertOp(_) = this.revert() |> ignore
    [<CustomOperation "seek">]
    member inline this.seekOp(_, time: int, ?muteCallbacks: bool) = this.seek(time, ?muteCallbacks = muteCallbacks) |> ignore
    [<CustomOperation "stretch">]
    member inline this.stretchOp(_, time: float) = this.stretch(!!time) |> ignore
    [<CustomOperation "refresh">]
    member inline this.refreshOp(_) = this.refresh() |> ignore
    [<CustomOperation "set">]
    member inline this.setOp(_, targets: obj, properties: obj, ?position: Binding.TimePosition) = this.set(!!targets,properties,?position = !!position) |> ignore
    [<CustomOperation "set">]
    member inline this.setOp(_, targets: obj, properties: obj, ?position: RelativeTimePosition) = this.set(!!targets,properties,?position = !!position) |> ignore
    [<CustomOperation "set">]
    member inline this.setOp(_, targets: obj, properties: obj, ?position: RelativeTweenValue) = this.set(!!targets,properties,?position = !!position) |> ignore
    [<CustomOperation "set">]
    member inline this.setOp(_, targets: obj, properties: obj) = this.set(!!targets,properties) |> ignore
    [<CustomOperation "label">]
    member inline this.label(_, label: string, ?position: Binding.TimePosition) = this.label(!!label, ?position = !!position) |> ignore
    [<CustomOperation "label">]
    member inline this.label(_, label: string, ?position: RelativeTimePosition) = this.label(!!label, ?position = !!position) |> ignore
    [<CustomOperation "label">]
    member inline this.label(_, label: string, ?position: RelativeTweenValue) = this.label(!!label, ?position = !!position) |> ignore
    [<CustomOperation "label">]
    member inline this.label(_, label: string) = this.label(!!label) |> ignore
    member inline this.Run(_) = this

type AnimatableOptionsComputation() =
    interface FableObjectBuilder
    interface EasePropertyComputation
    /// <summary>
    /// <c>unit</c><br/>
    /// <c>string -> ...</c>
    /// </summary>
    /// <remarks>
    /// <list type="table">
    /// <item> <term>
    /// Operation:
    /// </term> <description>
    /// <c>unit</c>
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
    [<CustomOperation "unit">]
    member inline _.UnitOp(_object_builder: FableObject, value: string) = ("unit", !!value) :: _object_builder
    /// <summary>
    /// <c>duration</c><br/>
    /// <c>float -> ...</c>
    /// </summary>
    /// <remarks>
    /// <list type="table">
    /// <item> <term>
    /// Operation:
    /// </term> <description>
    /// <c>duration</c>
    /// </description> </item>
    /// <item> <term>
    /// Args Num:
    /// </term> <description>
    /// <c>1</c>
    /// </description> </item>
    /// </list>
    /// <p><c>string | FunctionValue&lt;float> | FunctionValue&lt;RelativeTweenValue> | RelativeTweenValue</c></p>
    /// </remarks>
    /// <returns><c>(string * obj) list</c></returns>
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: FableObject, value: float) = ("duration" ==> value) :: _object_builder
    /// <summary>
    /// <c>duration</c><br/>
    /// <c>FunctionValue&lt;float> -> ...</c>
    /// </summary>
    /// <remarks>
    /// <list type="table">
    /// <item> <term>
    /// Operation:
    /// </term> <description>
    /// <c>duration</c>
    /// </description> </item>
    /// <item> <term>
    /// Args Num:
    /// </term> <description>
    /// <c>1</c>
    /// </description> </item>
    /// </list>
    /// <p><c>string | FunctionValue&lt;float> | FunctionValue&lt;RelativeTweenValue> | RelativeTweenValue</c></p>
    /// </remarks>
    /// <returns><c>(string * obj) list</c></returns>
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: FableObject, value: FunctionValue<float>) = ("duration" ==> value) :: _object_builder
    /// <summary>
    /// <c>duration</c><br/>
    /// <c>FunctionValue&lt;RelativeTweenValue> -> ...</c>
    /// </summary>
    /// <remarks>
    /// <list type="table">
    /// <item> <term>
    /// Operation:
    /// </term> <description>
    /// <c>duration</c>
    /// </description> </item>
    /// <item> <term>
    /// Args Num:
    /// </term> <description>
    /// <c>1</c>
    /// </description> </item>
    /// </list>
    /// <p><c>string | FunctionValue&lt;float> | FunctionValue&lt;RelativeTweenValue> | RelativeTweenValue</c></p>
    /// </remarks>
    /// <returns><c>(string * obj) list</c></returns>
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: FableObject, value: FunctionValue<RelativeTweenValue>) = ("duration" ==> value) :: _object_builder
    /// <summary>
    /// <c>duration</c><br/>
    /// <c>RelativeTweenValue -> ...</c>
    /// </summary>
    /// <remarks>
    /// <list type="table">
    /// <item> <term>
    /// Operation:
    /// </term> <description>
    /// <c>duration</c>
    /// </description> </item>
    /// <item> <term>
    /// Args Num:
    /// </term> <description>
    /// <c>1</c>
    /// </description> </item>
    /// </list>
    /// <p><c>string | FunctionValue&lt;float> | FunctionValue&lt;RelativeTweenValue> | RelativeTweenValue</c></p>
    /// </remarks>
    /// <returns><c>(string * obj) list</c></returns>
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: FableObject, value: RelativeTweenValue) = ("duration" ==> value) :: _object_builder
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
    member inline _.ModifierOp(_object_builder: FableObject, value: float -> float) = ("modifier" ==> value) :: _object_builder
    member inline _.Run(_object_run: FableObject): Binding.AnimatableOptions = !!(_object_run |> createObj)
    
type Binding.AnimatableOptions with
    member inline _.Yield(value: unit): unit = value
    member inline _.Combine(value: unit -> Targets): Targets = value()
    member inline _.Yield(value: Selector): Targets = !!value
    member inline _.Yield(value: string): Targets = !!value
    member inline _.Yield(value: NodeList): Targets = !!value
    member inline _.Yield(value: #Node): Targets = !!value
    member inline _.Delay(value) = value()

type [<Erase; System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>] StyleNoValue = StyleNoValue of string
and [<Erase; System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>] StyleWithValue = StyleWithValue of prop: string * value: obj
and [<Erase; System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>] StyleWithValueDuration = StyleWithValueDuration of prop: string * value: obj * duration: float
and [<Erase; System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>] StyleWithDuration = StyleWithDuration of prop: string * duration: float
and [<Erase; System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>] StyleWithEase = StyleWithEase of prop: string * ease: (float -> float)
and [<Erase; System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>] StyleWithDurationEase = StyleWithDurationEase of prop: string * duration: float * ease: (float -> float)
and [<Erase; System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>] StyleWithValueEase = StyleWithValueEase of prop: string * value: obj * ease: (float -> float)
and [<Erase; System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>] StyleWithValueDurationEase = StyleWithValueDurationEase of prop: string * value: obj * duration: float * ease: (float -> float)
and Binding.Animatable with
    member inline _.Yield(value: unit): unit = ()
    member inline _.Yield(value: string): StyleNoValue = StyleNoValue value
    member inline _.Yield(value: ICssStyle): StyleNoValue = !!value
    
    member inline _.Yield(value: float): float = value
    member inline _.Yield(value: int): int = value
    member inline _.Yield(value: float * float): ITuple = value
    member inline _.Yield(value: float * float * float): ITuple = value
    member inline _.Yield(value: float * float * float * float): ITuple = value
    member inline _.Yield(value: float * float * float * float * float): ITuple = value
    member inline _.Yield(value: float * float * float * float * float * float): ITuple = value
    member inline _.Yield(value: float * float * float * float * float * float * float): ITuple = value
    member inline _.Yield(value: float * float * float * float * float * float * float * float): ITuple = value
    member inline _.Yield(value: float * float * float * float * float * float * float * float * float): ITuple = value
    member inline _.Yield(value: float * float * float * float * float * float * float * float * float * float): ITuple = value
    member inline _.Yield(value: float * float * float * float * float * float * float * float * float * float * float): ITuple = value
    member inline _.Yield(value: float[]): float[] = value
    member inline _.Yield(value: float list): float[] = value |> List.toArray
    member inline this.Run(value: StyleNoValue): U2<float, float[]> = emitJsExpr (this,value) "$0[$1]()"
    member inline this.Run(StyleWithValue(prop, value)): Binding.Animatable = emitJsExpr (this,prop,value) "$0[$1]($2)"
    member inline this.Run(StyleWithValueDuration(prop, value,duration)): Binding.Animatable = emitJsExpr (this,prop,value,duration) "$0[$1]($2,$3)"
    member inline this.Run(StyleWithValueEase(prop, value,ease)): Binding.Animatable = emitJsExpr (this,prop,value,ease) "$0[$1]($2, easing = $3)"
    member inline this.Run(StyleWithValueDurationEase(prop, value,duration,ease)): Binding.Animatable = emitJsExpr (this,prop,value,duration,ease) "$0[$1]($2,$3,$4)"
    member inline _.Delay(value) = value()
    member inline _.Combine(StyleNoValue(prop), value) =
        StyleWithValue(prop,value) 
    member inline _.Combine(StyleWithDuration(prop,duration), value) =
        StyleWithValueDuration(prop,value,duration) 
    member inline _.Combine(StyleWithEase(prop,ease), value) =
        StyleWithValueEase(prop,value,ease)
    member inline _.Combine(StyleWithDurationEase(prop,duration,ease), value) =
        StyleWithValueDurationEase(prop,value,duration,ease)
    member inline _.Combine(value, _:unit) = value
    [<CustomOperation "duration">]
    member inline _.durationOp(StyleNoValue(prop), value: float) = StyleWithDuration(prop,value)
    [<CustomOperation "duration">]
    member inline _.durationOp(StyleWithValue(prop,value), duration: float) = StyleWithValueDuration(prop,value,duration)
    [<CustomOperation "duration">]
    member inline _.durationOp(StyleWithValueEase(prop,value,ease), duration: float) = StyleWithValueDurationEase(prop,value,duration,ease)
    [<CustomOperation "duration">]
    member inline _.durationOp(StyleWithEase(prop,ease), duration: float) = StyleWithDurationEase(prop,duration,ease)
    [<CustomOperation "ease">]
    member inline _.easeOp(StyleNoValue(prop), value: float -> float) = StyleWithEase(prop,value)
    [<CustomOperation "ease">]
    member inline _.easeOp(StyleWithValue(prop,value), ease: float -> float) = StyleWithValueEase(prop,value,ease)
    [<CustomOperation "ease">]
    member inline _.easeOp(StyleWithValueDuration(prop,value,duration), ease: float -> float) = StyleWithValueDurationEase(prop,value,duration,ease)
    [<CustomOperation "ease">]
    member inline _.easeOp(StyleWithDuration(prop,duration), ease: float -> float) = StyleWithDurationEase(prop,duration,ease)
    member inline _.For(_object_instance: StyleWithValueDuration, _object_builder_action: StyleWithValueDuration -> StyleWithValueDurationEase) = _object_instance |> _object_builder_action

[<AutoOpen>]
type AutoOpenAnimatableOptionsExtensions =
    [<Extension>]
    static member inline Run(options: Binding.AnimatableOptions, value: Targets): Binding.Animatable =
        Binding.Export.createAnimatable(value,options)

type CursorOptionsComputation() =
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
    member inline _.Run(_object_run: bool): DraggableCursor = DraggableCursor(_object_run)

type AxisOptionsComputation() =
    interface FableObjectBuilder
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
    member inline _.Yield(value: bool) = value
    member inline _.Run(_object_run: FableObject): FSharp.Axis = !!createObj _object_run
    member inline _.Run(_object_run: bool): FSharp.Axis = !!_object_run

type DraggableOptionsComputation() =
    interface FableObjectBuilder
    member _.Yield(value: DraggableCursor) = "cursor" ==> value
    member _.Yield(value: Axis * FSharp.Axis): string * obj = !!value
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
    member _.CursorOp(_object_builder: FableObject, value: bool) = ("cursor" ==> value) :: _object_builder
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
    member _.XOp(_object_builder: FableObject, value: bool) = ("x" ==> value) :: _object_builder
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
    member _.YOp(_object_builder: FableObject, value: bool) = ("y" ==> value) :: _object_builder
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
    member _.XOp(_object_builder: FableObject, value: FSharp.Axis) = ("x" ==> value) :: _object_builder
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
    member _.YOp(_object_builder: FableObject, value: FSharp.Axis) = ("y" ==> value) :: _object_builder
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
    member _.ContainerOp(_object_builder: FableObject, _object_value: Selector) = ("container" ==> _object_value) :: _object_builder
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
    member _.ContainerOp(_object_builder: FableObject, _object_value: string) = ("container" ==> _object_value) :: _object_builder
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
    member _.ContainerOp(_object_builder: FableObject, _object_value: #HTMLElement) = ("container" ==> _object_value) :: _object_builder
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
    member _.ContainerOp(_object_builder: FableObject, _object_value: Bounds) = ("container" ==> _object_value) :: _object_builder
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
    member _.ContainerOp(_object_builder: FableObject, _object_value: FunctionValue<Bounds>) = ("container" ==> _object_value) :: _object_builder
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
    member _.containerPaddingOp(_object_builder: FableObject, _object_value: float) = ("containerPadding" ==> _object_value) :: _object_builder
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
    member _.containerPaddingOp(_object_builder: FableObject, _object_value: Bounds) = ("containerPadding" ==> _object_value) :: _object_builder
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
    member _.containerPaddingOp(_object_builder: FableObject, _object_value: FunctionValue<Bounds>) = ("containerPadding" ==> _object_value) :: _object_builder
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
    member _.containerPaddingOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("containerPadding" ==> _object_value) :: _object_builder
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
    member _.containerFrictionOp(_object_builder: FableObject, _object_value: float) = ("containerFriction" ==> _object_value) :: _object_builder
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
    member _.releaseMassOp(_object_builder: FableObject, _object_value: float) = ("releaseMass" ==> _object_value) :: _object_builder
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
    member _.releaseMassOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("releaseMass" ==> _object_value) :: _object_builder
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
    member _.releaseStiffnessOp(_object_builder: FableObject, _object_value: float) = ("releaseStiffness" ==> _object_value) :: _object_builder
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
    member _.releaseStiffnessOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("releaseStiffness" ==> _object_value) :: _object_builder
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
    member _.releaseDampingOp(_object_builder: FableObject, _object_value: float) = ("releaseDamping" ==> _object_value) :: _object_builder
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
    member _.releaseDampingOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("releaseDamping" ==> _object_value) :: _object_builder
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
    member _.releaseEaseOp(_object_builder: FableObject, _object_value: float -> float) = ("releaseEase" ==> _object_value) :: _object_builder
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
    member _.releaseEaseOp(_object_builder: FableObject, _object_value: FunctionValue<float -> float>) = ("releaseEase" ==> _object_value) :: _object_builder
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
    member _.releaseContainerFrictionOp(_object_builder: FableObject, _object_value: float) = ("releaseContainerFriction" ==> _object_value) :: _object_builder
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
    member _.releaseContainerFrictionOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("releaseContainerFriction" ==> _object_value) :: _object_builder
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
    member _.minVelocityOp(_object_builder: FableObject, _object_value: float) = ("minVelocity" ==> _object_value) :: _object_builder
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
    member _.minVelocityOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("minVelocity" ==> _object_value) :: _object_builder
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
    member _.maxVelocityOp(_object_builder: FableObject, _object_value: float) = ("maxVelocity" ==> _object_value) :: _object_builder
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
    member _.maxVelocityOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("maxVelocity" ==> _object_value) :: _object_builder
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
    member _.velocityMultiplierOp(_object_builder: FableObject, _object_value: float) = ("velocityMultiplier" ==> _object_value) :: _object_builder
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
    member _.velocityMultiplierOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("velocityMultiplier" ==> _object_value) :: _object_builder
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
    member _.dragSpeedOp(_object_builder: FableObject, _object_value: float) = ("dragSpeed" ==> _object_value) :: _object_builder
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
    member _.dragSpeedOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("dragSpeed" ==> _object_value) :: _object_builder
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
    member _.scrollThresholdOp(_object_builder: FableObject, _object_value: float) = ("scrollThreshold" ==> _object_value) :: _object_builder
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
    member _.scrollThresholdOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("scrollThreshold" ==> _object_value) :: _object_builder
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
    member _.scrollSpeedOp(_object_builder: FableObject, _object_value: float) = ("scrollSpeed" ==> _object_value) :: _object_builder
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
    member _.scrollSpeedOp(_object_builder: FableObject, _object_value: FunctionValue<float>) = ("scrollSpeed" ==> _object_value) :: _object_builder
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
    member inline _.Run(_object_builder: FableObject): Binding.DraggableOptions = !!createObj !!_object_builder
type Binding.DraggableOptions with
    member inline _.Yield(value: unit): unit = value
    member inline _.Combine(value: unit -> Targets): Targets = value()
    member inline _.Yield(value: Selector): Targets = !!value
    member inline _.Yield(value: string): Targets = !!value
    member inline _.Yield(value: NodeList): Targets = !!value
    member inline _.Yield(value: #Node): Targets = !!value
    member inline _.Delay(value) = value()
[<AutoOpen; Erase>]
type AutoOpenDraggableOptionsExtensions =
    [<Extension>]
    static member inline Run(options: Binding.DraggableOptions, value: Targets): Binding.Draggable =
        Binding.Exports.createDraggable(value,options)

type ScrollObserverOptionsComputation() =
    interface FableObjectBuilder
    interface ScrollObserverCallbackComputation<Binding.ScrollObserver>
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
    member inline _.axisOp(_object_builder: FableObject, value: Axis) = ("axis" ==> value) :: _object_builder
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
    member inline _.enterOp(_object_builder: FableObject, value: ObserverThreshold) = ("enter" ==> value) :: _object_builder
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
    member inline _.enterOp(_object_builder: FableObject, value: ObserverThreshold, target: ObserverThreshold) = ("enter" ==> {| container = value; target = target |}) :: _object_builder
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
    member inline _.enterOp(_object_builder: FableObject, value: string, target: ObserverThreshold) = ("enter" ==> {| container = value; target = target |}) :: _object_builder
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
    member inline _.enterOp(_object_builder: FableObject, value: ObserverThreshold, target: string) = ("enter" ==> {| container = value; target = target |}) :: _object_builder
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
    member inline _.leaveOp(_object_builder: FableObject, value: ObserverThreshold) = ("leave" ==> value) :: _object_builder
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
    member inline _.leaveOp(_object_builder: FableObject, value: ObserverThreshold, target: ObserverThreshold) = ("leave" ==> {| container = value; target = target |}) :: _object_builder
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
    member inline _.leaveOp(_object_builder: FableObject, value: string, target: ObserverThreshold) = ("leave" ==> {| container = value; target = target |}) :: _object_builder
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
    member inline _.leaveOp(_object_builder: FableObject, value: ObserverThreshold, target: string) = ("leave" ==> {| container = value; target = target |}) :: _object_builder
    member inline _.Run(_object_run: FableObject): Binding.ScrollObserver = Binding.Exports.onScroll(!!createObj _object_run)
    
type StaggerOptionsComputation(value: float) =
    interface FableObjectBuilder
    interface EasePropertyComputation
    /// <summary>
    /// <c>start</c><br/>
    /// <c>float -> ...</c>
    /// </summary>
    /// <remarks>
    /// <list type="table">
    /// <item> <term>
    /// Operation:
    /// </term> <description>
    /// <c>start</c>
    /// </description> </item>
    /// <item> <term>
    /// Args Num:
    /// </term> <description>
    /// <c>1</c>
    /// </description> </item>
    /// </list>
    /// <p><c>float | TimePosition</c></p>
    /// </remarks>
    /// <returns><c>(string * obj) list</c></returns>
    [<CustomOperation "start">]
    member inline _.startOp(_object_builder: FableObject, value: float) = ("start" ==> value) :: _object_builder
    /// <summary>
    /// <c>start</c><br/>
    /// <c>TimePosition -> ...</c>
    /// </summary>
    /// <remarks>
    /// <list type="table">
    /// <item> <term>
    /// Operation:
    /// </term> <description>
    /// <c>start</c>
    /// </description> </item>
    /// <item> <term>
    /// Args Num:
    /// </term> <description>
    /// <c>1</c>
    /// </description> </item>
    /// </list>
    /// <p><c>float | TimePosition</c></p>
    /// </remarks>
    /// <returns><c>(string * obj) list</c></returns>
    [<CustomOperation "start">]
    member inline _.startOp(_object_builder: FableObject, value: Binding.TimePosition) = ("start" ==> value) :: _object_builder
    /// <summary>
    /// <c>from</c><br/>
    /// <c>TimePosition -> ...</c>
    /// </summary>
    /// <remarks>
    /// <list type="table">
    /// <item> <term>
    /// Operation:
    /// </term> <description>
    /// <c>from</c>
    /// </description> </item>
    /// <item> <term>
    /// Args Num:
    /// </term> <description>
    /// <c>1</c>
    /// </description> </item>
    /// </list>
    /// <p><c>StaggerFrom</c></p>
    /// </remarks>
    /// <returns><c>(string * obj) list</c></returns>
    [<CustomOperation "from">]
    member inline _.fromOp(_object_builder: FableObject, value: StaggerFrom) = ("from" ==> value) :: _object_builder
    member inline _.Yield(value: StaggerFrom): FableObject = ["from" ==> value]
    /// <summary>
    /// <c>reversed</c><br/>
    /// <c>bool -> ...</c>
    /// </summary>
    /// <remarks>
    /// <list type="table">
    /// <item> <term>
    /// Operation:
    /// </term> <description>
    /// <c>reversed</c>
    /// </description> </item>
    /// <item> <term>
    /// Args Num:
    /// </term> <description>
    /// <c>0-1</c>
    /// </description> </item>
    /// </list>
    /// <p><c>bool | unit</c></p>
    /// </remarks>
    /// <returns><c>(string * obj) list</c></returns>
    [<CustomOperation "reversed">]
    member inline _.reversedOp(_object_builder: FableObject, value: bool) = ("reversed" ==> value) :: _object_builder
    /// <summary>
    /// <c>reversed</c><br/>
    /// <c>unit -> ...</c>
    /// </summary>
    /// <remarks>
    /// <list type="table">
    /// <item> <term>
    /// Operation:
    /// </term> <description>
    /// <c>reversed</c>
    /// </description> </item>
    /// <item> <term>
    /// Args Num:
    /// </term> <description>
    /// <c>0-1</c>
    /// </description> </item>
    /// </list>
    /// <p><c>bool | unit</c></p>
    /// </remarks>
    /// <returns><c>(string * obj) list</c></returns>
    [<CustomOperation "reversed">]
    member inline _.reversedOp(_object_builder: FableObject) = ("reversed" ==> true) :: _object_builder
    /// <summary>
    /// <c>axis</c><br/>
    /// <c>Axis -> ...</c>
    /// </summary>
    /// <remarks>
    /// <list type="table">
    /// <item> <term>
    /// Operation:
    /// </term> <description>
    /// <c>axis</c>
    /// </description> </item>
    /// <item> <term>
    /// Args Num:
    /// </term> <description>
    /// <c>1</c>
    /// </description> </item>
    /// </list>
    /// <p><c>Args</c></p>
    /// </remarks>
    /// <returns><c>(string * obj) list</c></returns>
    [<CustomOperation "axis">]
    member inline _.axisOp(_object_builder: FableObject, value: Axis) = ("axis" ==> value) :: _object_builder
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
    member inline _.modifierOp(_object_builder: FableObject, value: FloatModifier) = ("modifier" ==> value) :: _object_builder
    /// <summary>
    /// <c>grid</c><br/>
    /// <c>float -> float -> ...</c>
    /// </summary>
    /// <remarks>
    /// <list type="table">
    /// <item> <term>
    /// Operation:
    /// </term> <description>
    /// <c>grid</c>
    /// </description> </item>
    /// <item> <term>
    /// Args Num:
    /// </term> <description>
    /// <c>2</c>
    /// </description> </item>
    /// </list>
    /// <p><c>float float</c></p>
    /// </remarks>
    /// <returns><c>(string * obj) list</c></returns>
    [<CustomOperation "grid">]
    member inline _.gridOp(_object_builder: FableObject, value: float, value2: float) = ("grid" ==> (value, value2)) :: _object_builder
    member _.Run(_object_run: FableObject): FunctionValue<float> =
        !!Binding.Export.stagger(value, !!(createObj _object_run))

type Binding.ScrollObserver with
    member inline this.Yield(_: unit) = ()
    member inline this.Zero() = ()
    member inline _.Delay(value) = value()
    member inline _.Combine(value) = fun () -> value
    [<CustomOperation "link">]
    member inline this.linkOp(_, _object_value: Binding.Animation) =
        this.link(!^_object_value) |> ignore
    [<CustomOperation "refresh">]
    member inline this.refreshOp(_) =
        this.refresh() |> ignore
    [<CustomOperation "revert">]
    member inline this.revertOp(_) =
        this.revert() |> ignore
    member inline this.Run(_) = this
    

type AnimationOptionsComputation() =
    interface TweenOptionsComputation
    interface FableObjectBuilder
    interface AnimationCallbackComputation<Binding.Animation>
    interface PlaybackObjectBuilder
    interface EasePropertyComputation
    member inline _.Yield(value: string * obj) = [value]
    member inline _.Yield(value: StyleObj): string * obj = !!value
    member inline _.Yield(value: StyleValue): string * obj = !!value
    member inline _.Yield(value: StyleToObj): string * obj = !!value
    member inline _.Yield(value: StyleFromObj): string * obj = !!value
    [<CustomOperation "keyframes">]
    member inline _.keyframesOp(_object_instance: FableObject, value: KeyframesValue list)=
        ("keyframes" ==> (value |> List.toArray)) :: _object_instance
    [<CustomOperation "keyframes">]
    member inline _.keyframesOp(_object_instance: FableObject, value: KeyframesPercentKeyValue list) =
        ("keyframes" ==> createObj !!value) :: _object_instance
    member inline _.Run(_object_instance: FableObject): Binding.AnimationOptions = !!createObj _object_instance
type Binding.AnimationOptions with
    member inline this.Yield(value: unit) = this
    member inline _.Combine(y) = fun () -> y
    member inline _.Delay(value) = value()
    member inline _.Yield(value: string): Targets = !!value
    member inline _.Yield(value: Selector): Targets = !!value
    member inline _.Yield(value: #HTMLElement): Targets = !!value
    member inline _.Yield(value: NodeList): Targets = !!value
    member inline _.Yield(value: #HTMLElement array): Targets = !!value
    member inline this.Run(value: Targets): Binding.Animation = Binding.Export.animate(value,this)

type Binding.Animation with
    member inline this.Yield(_: unit) = ()
    member inline this.Zero() = ()
    member inline _.Delay(value) = value()
    [<CustomOperation "play">]
    member inline this.playOp(_) = this.play() |> ignore
    [<CustomOperation "reverse">]
    member inline this.reverseOp(_) = this.reverse() |> ignore
    [<CustomOperation "pause">]
    member inline this.pauseOp(_) = this.pause() |> ignore
    [<CustomOperation "restart">]
    member inline this.restartOp(_) = this.restart() |> ignore
    [<CustomOperation "alternate">]
    member inline this.alternateOp(_) = this.alternate() |> ignore
    [<CustomOperation "resume">]
    member inline this.resumeOp(_) = this.resume() |> ignore
    [<CustomOperation "complete">]
    member inline this.completeOp(_) = this.complete() |> ignore
    [<CustomOperation "cancel">]
    member inline this.cancelOp(_) = this.cancel() |> ignore
    [<CustomOperation "revert">]
    member inline this.revertOp(_) = this.revert() |> ignore
    [<CustomOperation "seek">]
    member inline this.seekOp(_, time: float, ?muteCallbacks: bool) = this.seek(!!time,?muteCallbacks=muteCallbacks) |> ignore
    [<CustomOperation "stretch">]
    member inline this.stretchOp(_, value: float) = this.stretch(!!value) |> ignore
    [<CustomOperation "id">]
    member inline this.idOp(_, value: string) = this.id <- !!value
    member inline this.Run(_) = this
    [<CustomOperation "🔄">]
    member inline this.RestartOp(_) = this.restart() |> ignore
    [<CustomOperation "( •_•)>⌐■-■">]
    member inline this.PauseOp(_) = this.pause() |> ignore
    [<CustomOperation "(⌐■_■)">]
    member inline this.PlayOp(_) = this.play() |> ignore

[<AutoOpen>]
type AutoOpenAnimationExtensions =
    [<Extension>]
    static member inline Run(this: Binding.AnimationOptions, value: Targets): Binding.Animation =
        Binding.Export.animate(value, this)

type Binding.Engine with
    member inline _.Zero() = ()
    member inline _.Yield(_) = ()
    [<CustomOperation "timeUnit">]
    member inline this.timeUnitOp(_, value: Enums.TimeUnit) = this.timeUnit <- value
    [<CustomOperation "precision">]
    member inline this.precisionOp(_, value: int) = this?precision <- value
    [<CustomOperation "speed">]
    member inline this.speedOp(_, value: float) = this?speed <- value
    [<CustomOperation "fps">]
    member inline this.fpsOp(_, value: int) = this?fps <- value
    [<CustomOperation "useDefaultMainLoop">]
    member inline this.useDefaultMainLoopOp(_, value: bool) = this?useDefaultMainLoop <- value
    [<CustomOperation "useDefaultMainLoop">]
    member inline this.useDefaultMainLoopOp(_) = this.useDefaultMainLoop <- true
    [<CustomOperation "pauseOnDocumentHidden">]
    member inline this.pauseOnDocumentHiddenOp(_, value: bool) = this?pauseOnDocumentHidden <- value
    [<CustomOperation "pauseOnDocumentHidden">]
    member inline this.pauseOnDocumentHiddenOp(_) = this.pauseOnDocumentHidden <- true
    [<CustomOperation "playbackEase">]
    member inline this.playbackEaseOp(_, value: float -> float) = this?defaults?playbackEase <- !!value
    [<CustomOperation "playbackRate">]
    member inline this.playbackRateOp(_, value: float) = this?defaults?playbackRate <- !!value
    [<CustomOperation "frameRate">]
    member inline this.frameRateOp(_, value: float) = this?defaults?frameRate <- !!value
    [<CustomOperation "loop">]
    member inline this.loopOp(_, value: int) = this?defaults?loop <- !!value
    [<CustomOperation "loop">]
    member inline this.loopOp(_, value: bool) = this?defaults?loop <- !!value
    [<CustomOperation "loop">]
    member inline this.loopOp(_) = this?defaults?loop <- true
    [<CustomOperation "reversed">]
    member inline this.reversedOp(_) = this?defaults?reversed <- true
    [<CustomOperation "alternate">]
    member inline this.alternateOp(_) = this?defaults?alternate <- true
    [<CustomOperation "autoplay">]
    member inline this.autoplayOp(_) = this?defaults?autoplay <- true
    [<CustomOperation "duration">]
    member inline this.durationOp(_, value: float) = this?defaults?duration <- !!value
    [<CustomOperation "delay">]
    member inline this.delayOp(_, value: float) = this?defaults?delay <- !!value
    [<CustomOperation "composition">]
    member inline this.compositionOp(_, value) = this?defaults?composition <- value
    [<CustomOperation "ease">]
    member inline this.easeOp(_, value: float -> float) = this?defaults?ease <- !!value
    [<CustomOperation "loopDelay">]
    member inline this.loopDelayOp(_, value: float) = this?defaults?loopDelay <- !!value
    [<CustomOperation "modifier">]
    member inline this.modifierOp(_, value: float -> float) = this?defaults?modifier <- !!value
    member inline this.Run(_) = this

let onScroll = ScrollObserverOptionsComputation()
let stagger value = StaggerOptionsComputation(value)
/// <summary>
/// Computation Expression to create a draggable in AnimeJs.<br/>
/// Includes a run signature that implements <c>createDraggable</c> on the
/// completed options object
/// </summary>
/// <example><code>
/// let draggableOptions = draggable { x (dragAxis { true }) }
/// let drag = draggableOptions { ".sphere" }
/// // ...
/// let drag = draggable { x (dragAxis { true }) } { ".sphere" }
/// </code></example>
let draggable = DraggableOptionsComputation()
/// <summary>
/// Helper computation expression for DraggableOptions.<br/>
/// Shorthand for the "x" and "y" operations
/// </summary>
let dragAxis = AxisOptionsComputation()
/// <summary>
/// Helper computation expression for DraggableOptions.<br/>
/// Shorthand for "cursor" operation
/// </summary>
let dragCursor = CursorOptionsComputation()
let animatable = AnimatableOptionsComputation()
let timeline = TimelineOptionsComputation()
let spring = SpringComputation()
let tweenParams = TweenObjectBuilder()
let bounds = BoundsComputation()
let keyframe = KeyframeComputation()
let inline style (value: string) = unbox<ICssStyle> value
let inline (!~) (value: string) = style value
let timerOptions = TimerOptionsComputation()
let timer = TimerOptionsCreateComputation()
let animate = AnimationOptionsComputation()
