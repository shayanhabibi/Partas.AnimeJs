module Partas.AnimeJs.ComputationExpressions

open Fable.Core
open Fable.Core.JsInterop
open Fable.Core.JS
open Partas.AnimeJs.Core
open Browser.Types
open Partas.AnimeJs.CE
/// Alias for a string * obj list
type FableObject = (string * obj) list
/// Implements boiler plate for builder expressions which are creating JS objects
type FableObjectBuilder = interface end
[<AutoOpen>]
module FableObjectBuilderExtension =
    type FableObjectBuilder with
        // Can initialise a sequence with an empty list
        member inline _.Yield(_: unit): FableObject = []
        member inline _.Yield(_object_value: string * obj) = _object_value
        member inline _.Yield(_object_builder: FableObject) = _object_builder
        // hack all the possibilities away. redundancies.
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
        // get rid of it
        member inline _.Delay([<InlineIfLambda>] value) = value()
        // If a custom operation comes after a general key,value pair then the input is string * obj
        member inline _.For(_object_builder: FableObject, _object_builder_action: string * obj -> FableObject) =
            _object_builder |> List.collect _object_builder_action
        // If a custom operation is the first of the sequence/expression, then the input is unit
        member inline _.For(_object_builder: FableObject, _object_builder_action: unit -> FableObject) =
            _object_builder @ (_object_builder_action())

// Forward declaration for Playback computation expression
type PlaybackObjectBuilder =
    inherit FableObjectBuilder

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

open System.Runtime.CompilerServices

/// Style computations implement all the accepted value types for tween/keyframes etc.
/// Monad wrappers are used to make the `to` (too) and `from` mutually exclusive properties.
type Style = CssStyle
and [<Erase>] StyleValue = private StyleValue of obj
and [<Erase>] StyleObj = private StyleObj of obj
and [<Erase>] StyleToObj = private StyleToObj of FableObject
and [<Erase>] StyleFromObj = private StyleFromObj of FableObject
and [<Erase>] StyleArray = private StyleArray of unit
and CssStyle with
    static member inline key: StyleArray = StyleArray ()
and ICssStyle with
    member inline this.Value: string = unbox this
    member inline _.For(_object_builder: FableObject, _object_builder_action: string * obj -> FableObject) =
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
    member inline _.EaseOp(_object_builder: FableObject, _property_value_lambda: float -> float) = _object_builder @ ["ease" ==> _property_value_lambda]
    [<CustomOperation "composition">]
    member inline _.CompositionOp(_object_builder: FableObject, _property_value: Composition) = _object_builder @ ["composition" ==> _property_value]
    [<CustomOperation "modifier">]
    member inline _.ModifierOp(_object_builder: FableObject, _property_value_lambda: FloatModifier) = _object_builder @ ["modifier" ==> _property_value_lambda]
    [<CustomOperation "delay">]
    member inline _.DelayOp(_object_builder: StyleToObj, _property_value: float) = StyleToObj(("delay" ==> _property_value) :: !!_object_builder)
    [<CustomOperation "delay">]
    member inline _.DelayOp(_object_builder: StyleToObj, _property_value_lambda: FunctionValue<float>) = StyleToObj(("delay" ==> _property_value_lambda) :: !!_object_builder)
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: StyleToObj, _property_value: float) = StyleToObj(("duration" ==> _property_value) :: !!_object_builder)
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: StyleToObj, _property_value_lambda: FunctionValue<float>) = StyleToObj(("duration" ==> _property_value_lambda) :: !!_object_builder)
    [<CustomOperation "ease">]
    member inline _.EaseOp(_object_builder: StyleToObj, _property_value_lambda: float -> float) = StyleToObj(("ease" ==> _property_value_lambda) :: !!_object_builder)
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
    member inline _.EaseOp(_object_builder: StyleFromObj, _property_value_lambda: float -> float) = StyleFromObj(("ease" ==> _property_value_lambda) :: !!_object_builder)
    [<CustomOperation "composition">]
    member inline _.CompositionOp(_object_builder: StyleFromObj, _property_value: Composition) = StyleFromObj(("composition" ==> _property_value) :: !!_object_builder)
    [<CustomOperation "modifier">]
    member inline _.ModifierOp(_object_builder: StyleFromObj, _property_value_lambda: FloatModifier) = StyleFromObj(("modifier" ==> _property_value_lambda) :: !!_object_builder)
and StyleArray with
    member inline _.For(_object_builder: FableObject, _object_builder_action: string * obj -> FableObject) =
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
    member inline _.toOp(_object_builder: FableObject, _property_value: FunctionValue<float>) = StyleToObj(("to" ==> _property_value) :: _object_builder)
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
    member inline _.toOp(_object_builder: FableObject, _property_value: FunctionValue<float>, _property_value2: FunctionValue<float>) = StyleToObj(("to" ==> (_property_value, _property_value2)) :: _object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/delay">See the docs</a>
    [<CustomOperation "delay">]
    member inline _.DelayOp(_object_builder: FableObject, _property_value: float) = _object_builder @ ["delay" ==> _property_value]
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/delay">See the docs</a>
    [<CustomOperation "delay">]
    member inline _.DelayOp(_object_builder: FableObject, _property_value_lambda: FunctionValue<float>) = _object_builder @ ["delay" ==> _property_value_lambda]
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/duration">See the docs</a>
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: FableObject, _property_value: float) = _object_builder @ ["duration" ==> _property_value]
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/duration">See the docs</a>
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: FableObject, _property_value_lambda: FunctionValue<float>) = _object_builder @ ["duration" ==> _property_value_lambda]
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/ease">See the docs</a>
    [<CustomOperation "ease">]
    member inline _.EaseOp(_object_builder: FableObject, _property_value_lambda: float -> float) = _object_builder @ ["ease" ==> _property_value_lambda]
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/composition">See the docs</a>
    [<CustomOperation "composition">]
    member inline _.CompositionOp(_object_builder: FableObject, _property_value: Composition) = _object_builder @ ["composition" ==> _property_value]
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/modifier">See the docs</a>
    [<CustomOperation "modifier">]
    member inline _.ModifierOp(_object_builder: FableObject, _property_value_lambda: FloatModifier) = _object_builder @ ["modifier" ==> _property_value_lambda]
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/delay">See the docs</a>
    [<CustomOperation "delay">]
    member inline _.DelayOp(_object_builder: StyleToObj, _property_value: float) = StyleToObj(("delay" ==> _property_value) :: !!_object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/delay">See the docs</a>
    [<CustomOperation "delay">]
    member inline _.DelayOp(_object_builder: StyleToObj, _property_value_lambda: FunctionValue<float>) = StyleToObj(("delay" ==> _property_value_lambda) :: !!_object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/duration">See the docs</a>
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: StyleToObj, _property_value: float) = StyleToObj(("duration" ==> _property_value) :: !!_object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/duration">See the docs</a>
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: StyleToObj, _property_value_lambda: FunctionValue<float>) = StyleToObj(("duration" ==> _property_value_lambda) :: !!_object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/ease">See the docs</a>
    [<CustomOperation "ease">]
    member inline _.EaseOp(_object_builder: StyleToObj, _property_value_lambda: float -> float) = StyleToObj(("ease" ==> _property_value_lambda) :: !!_object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/composition">See the docs</a>
    [<CustomOperation "composition">]
    member inline _.CompositionOp(_object_builder: StyleToObj, _property_value: Composition) = StyleToObj(("composition" ==> _property_value) :: !!_object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/modifier">See the docs</a>
    [<CustomOperation "modifier">]
    member inline _.ModifierOp(_object_builder: StyleToObj, _property_value_lambda: FloatModifier) = StyleToObj(("modifier" ==> _property_value_lambda) :: !!_object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/delay">See the docs</a>
    [<CustomOperation "delay">]
    member inline _.DelayOp(_object_builder: StyleFromObj, _property_value: float) = StyleFromObj(("delay" ==> _property_value) :: !!_object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/delay">See the docs</a>
    [<CustomOperation "delay">]
    member inline _.DelayOp(_object_builder: StyleFromObj, _property_value_lambda: FunctionValue<float>) = StyleFromObj(("delay" ==> _property_value_lambda) :: !!_object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/duration">See the docs</a>
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: StyleFromObj, _property_value: float) = StyleFromObj(("duration" ==> _property_value) :: !!_object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/duration">See the docs</a>
    [<CustomOperation "duration">]
    member inline _.DurationOp(_object_builder: StyleFromObj, _property_value_lambda: FunctionValue<float>) = StyleFromObj(("duration" ==> _property_value_lambda) :: !!_object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/ease">See the docs</a>
    [<CustomOperation "ease">]
    member inline _.EaseOp(_object_builder: StyleFromObj, _property_value_lambda: float -> float) = StyleFromObj(("ease" ==> _property_value_lambda) :: !!_object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/composition">See the docs</a>
    [<CustomOperation "composition">]
    member inline _.CompositionOp(_object_builder: StyleFromObj, _property_value: Composition) = StyleFromObj(("composition" ==> _property_value) :: !!_object_builder)
    /// <a href="https://animejs.com/documentation/animation/tween-parameters/modifier">See the docs</a>
    [<CustomOperation "modifier">]
    member inline _.ModifierOp(_object_builder: StyleFromObj, _property_value_lambda: FloatModifier) = StyleFromObj(("modifier" ==> _property_value_lambda) :: !!_object_builder)

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
        member inline _.onBeforeUpdateOp(_object_builder: FableObject, _property_value: Callback<'T>) = ("onBeforeUpdate" ==> _property_value) :: _object_builder
        [<CustomOperation "onRender">]
        member inline _.onRenderOp(_object_builder: FableObject, _property_value: Callback<'T>) = ("onRender" ==> _property_value) :: _object_builder

/// <a href="https://animejs.com/documentation/animation/tween-parameters">Implements Tween Parameters</a>
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

type TweenOptionsComputation =
    inherit FableObjectBuilder

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
type KeyframesValue = interface end
type KeyframeComputation() =
    interface FableObjectBuilder
    interface TweenOptionsComputation
    member inline _.Yield(_: unit): FableObject = []
    member inline _.For(_object_builder: FableObject, _object_builder_action: string * obj -> FableObject) =
        _object_builder |> List.collect _object_builder_action
    member inline _.Yield(_yield_value: string * obj): FableObject = [_yield_value]
    member inline _.Yield(_yield_value: StyleObj): FableObject = [!!_yield_value]
    member inline _.Yield(_yield_value: StyleValue): FableObject = [!!_yield_value]
    member inline _.Delay(value: unit -> FableObject) = value()
    member inline _.Combine(y) = fun () -> y
    member inline _.Run(_object_run: FableObject): KeyframesValue  = !!createObj _object_run

[<AutoOpen>]
module AutoOpenPlaybackObjectBuilder =
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

type AnimatableOptionsComputation() =
    interface FableObjectBuilder
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
    /// <c>ease</c><br/>
    /// <c>(float -> float) -> ...</c>
    /// </summary>
    /// <remarks>
    /// <list type="table">
    /// <item> <term>
    /// Operation:
    /// </term> <description>
    /// <c>ease</c>
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
    [<CustomOperation "ease">]
    member inline _.EaseOp(_object_builder: FableObject, value: float -> float) = ("ease" ==> value) :: _object_builder
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
    member inline _.Run(_object_run: FableObject): Binding.ScrollObserver = createObj _object_run |> unbox |> Binding.Exports.onScroll
    
type StaggerOptionsComputation(value: float) =
    interface FableObjectBuilder
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
    /// <c>float -> ...</c>
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
    /// <p><c>float | TimePosition</c></p>
    /// </remarks>
    /// <returns><c>(string * obj) list</c></returns>
    [<CustomOperation "from">]
    member inline _.fromOp(_object_builder: FableObject, value: float) = ("from" ==> value) :: _object_builder
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
    /// <p><c>float | TimePosition</c></p>
    /// </remarks>
    /// <returns><c>(string * obj) list</c></returns>
    [<CustomOperation "from">]
    member inline _.fromOp(_object_builder: FableObject, value: Binding.TimePosition) = ("from" ==> value) :: _object_builder
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
    /// <c>ease</c><br/>
    /// <c>(float -> float) -> ...</c>
    /// </summary>
    /// <remarks>
    /// <list type="table">
    /// <item> <term>
    /// Operation:
    /// </term> <description>
    /// <c>ease</c>
    /// </description> </item>
    /// <item> <term>
    /// Args Num:
    /// </term> <description>
    /// <c>1</c>
    /// </description> </item>
    /// </list>
    /// <p><c>(float -> float) | EasingFunction</c></p>
    /// </remarks>
    /// <returns><c>(string * obj) list</c></returns>
    [<CustomOperation "ease">]
    member inline _.easeOp(_object_builder: FableObject, value: float -> float) = ("ease" ==> value) :: _object_builder
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
    member inline _.Run(_object_run: FableObject): FunctionValue<float> =
        let stagger value options = import "stagger" Spec.path
        stagger value (createObj _object_run)

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
