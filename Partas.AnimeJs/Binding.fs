namespace Partas.AnimeJs.Bindings

open System
open System.Runtime.CompilerServices
open Fable.Core
open Fable.Core.JsInterop
open Browser.Types

#nowarn 3535

module Spec =
    let [<Literal>] path = "animejs"

type EasingFun = float -> float
type FloatModifier = float -> float
type Callback<'T> = 'T -> unit
type ChainMethod<'T> = unit -> 'T
/// <summary>
/// Delegate representation of Value Functions in AnimeJs.
/// </summary>
/// <remarks>
/// This function type is used to conditionally change a property based on
/// the targets index position in an animation. This is automatically done
/// through methods like <c>stagger</c>.
/// <br/>
/// Stagger can therefor be used in other scenarios, so long as you invoke the method
/// with the relevant parameters.
/// </remarks>
/// <param name="target">The target object</param>
/// <param name="index">The objects index in the collection</param>
/// <param name="length">The collection length</param>
type FunctionValue<'Type> = delegate of target: obj * ?index: int * ?length: int -> 'Type
[<Erase; AutoOpen>]
module Enums =
    [<StringEnum; RequireQualifiedAccess>]
    type timeUnit =
        | s
        | ms

    [<StringEnum; RequireQualifiedAccess>]
    type staggerFrom =
        | first
        | center
        | last
        | random


    [<RequireQualifiedAccess>]
    [<StringEnum>]
    type axis =
        | x
        | y
        
    [<StringEnum; RequireQualifiedAccess>]
    type composition =
        /// <summary>
        /// Replace and cancel the current running animation on the property.
        /// </summary>
        | replace
        /// <summary>
        /// Do not replace the running animation. This means the previous animation will
        /// continue running if its duration is longer than the new animation. This mode can
        /// also offer better performance
        /// </summary>
        | none
        /// <summary>
        /// Creates an additive animation and blends its values with the running animation
        /// </summary>
        | blend

    [<StringEnum; RequireQualifiedAccess>]
    type observerThreshold =
        /// Top Y value
        | top
        /// Bottom Y value
        | bottom
        /// Left X value
        | left
        /// Right X value
        | right
        /// Center X or Y value
        | center
        /// Equivalent to Y-Top X-Left
        | start
        /// Equivalent to Y-Bottom X-Right
        | [<CompiledName "end">] end'
        /// Minimum value possible to meet the enter or leave condition
        | min
        /// Maximum value possible to meet the enter or leave condition
        | max
        /// Alias for the 'shorthand' position scheme in animejs
        | [<CompiledName "top bottom">] top_bottom
        | [<CompiledName "bottom top">] bottom_top
        | [<CompiledName "top left">] top_left
        | [<CompiledName "left top">] left_top
        | [<CompiledName "top right">] top_right
        | [<CompiledName "right top">] right_top
        | [<CompiledName "top center">] top_center
        | [<CompiledName "center top">] center_top
        | [<CompiledName "top start">] top_start
        | [<CompiledName "start top">] start_top
        | [<CompiledName "top end">] top_end
        | [<CompiledName "end top">] end_top
        | [<CompiledName "top min">] top_min
        | [<CompiledName "min top">] min_top
        | [<CompiledName "top max">] top_max
        | [<CompiledName "max top">] max_top
        | [<CompiledName "bottom left">] bottom_left
        | [<CompiledName "left bottom">] left_bottom
        | [<CompiledName "bottom right">] bottom_right
        | [<CompiledName "right bottom">] right_bottom
        | [<CompiledName "bottom center">] bottom_center
        | [<CompiledName "center bottom">] center_bottom
        | [<CompiledName "bottom start">] bottom_start
        | [<CompiledName "start bottom">] start_bottom
        | [<CompiledName "bottom end">] bottom_end
        | [<CompiledName "end bottom">] end_bottom
        | [<CompiledName "bottom min">] bottom_min
        | [<CompiledName "min bottom">] min_bottom
        | [<CompiledName "bottom max">] bottom_max
        | [<CompiledName "max bottom">] max_bottom
        | [<CompiledName "left right">] left_right
        | [<CompiledName "right left">] right_left
        | [<CompiledName "left center">] left_center
        | [<CompiledName "center left">] center_left
        | [<CompiledName "left start">] left_start
        | [<CompiledName "start left">] start_left
        | [<CompiledName "left end">] left_end
        | [<CompiledName "end left">] end_left
        | [<CompiledName "left min">] left_min
        | [<CompiledName "min left">] min_left
        | [<CompiledName "left max">] left_max
        | [<CompiledName "max left">] max_left
        | [<CompiledName "right center">] right_center
        | [<CompiledName "center right">] center_right
        | [<CompiledName "right start">] right_start
        | [<CompiledName "start right">] start_right
        | [<CompiledName "right end">] right_end
        | [<CompiledName "end right">] end_right
        | [<CompiledName "right min">] right_min
        | [<CompiledName "min right">] min_right
        | [<CompiledName "right max">] right_max
        | [<CompiledName "max right">] max_right
        | [<CompiledName "center start">] center_start
        | [<CompiledName "start center">] start_center
        | [<CompiledName "center end">] center_end
        | [<CompiledName "end center">] end_center
        | [<CompiledName "center min">] center_min
        | [<CompiledName "min center">] min_center
        | [<CompiledName "center max">] center_max
        | [<CompiledName "max center">] max_center
        | [<CompiledName "start end">] start_end
        | [<CompiledName "end start">] end_start
        | [<CompiledName "start min">] start_min
        | [<CompiledName "min start">] min_start
        | [<CompiledName "start max">] start_max
        | [<CompiledName "max start">] max_start
        | [<CompiledName "end min">] end_min
        | [<CompiledName "min end">] min_end
        | [<CompiledName "end max">] end_max
        | [<CompiledName "max end">] max_end
        | [<CompiledName "min max">] min_max
        | [<CompiledName "max min">] max_min
        static member inline (+) (x: observerThreshold, y: string) =
            $"{x} {y}"
        static member inline (+) (x: string, y: observerThreshold) =
            $"{x} {y}"
        static member inline (+) (x: observerThreshold, y: float) =
            $"{x} {y}"
        static member inline (+) (x: float, y: observerThreshold) =
            $"{x} {y}"
    [<StringEnum; RequireQualifiedAccess>]
    type wrapText =
        | hidden
        | clip
        | visible
        | scroll
        | auto
    [<StringEnum; RequireQualifiedAccess>]
    type cloneText =
        | left
        | right
        | top
        | bottom
        | center

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

[<AutoOpen>]
module rec Types =
    [<Interface>]
    [<Import("utils", "animejs")>]
    type Utils =
        /// Converts the provided targets parameter into an Array of elements, serving as
        /// and alternative to document.querySelectorAll().
        /// When used within a Scope, it uses the Scopes root element instead of document,
        /// effectively calling root.querySelectorAll().
        /// Returns an array of htmlelement, or svgelement, or svggeometryelement
        static abstract ``$``: targets: obj -> NodeList
        /// Returns the current value of a targets property, with optional unit conversion
        /// or removal. If target is an HTMLElement or SVGElement and unit parameter is not
        /// set to false or set a to a valid unit, then returned value is a string. If target
        /// is HTMLElement or SVGElement and unit parameter is false, then returned value is
        /// a number
        static abstract get: targetSelector: obj * propName: string * ?unit: string -> string
        /// Returns the current value of a targets property, with optional unit conversion
        /// or removal. If target is an HTMLElement or SVGElement and unit parameter is not
        /// set to false or set a to a valid unit, then returned value is a string. If target
        /// is HTMLElement or SVGElement and unit parameter is false, then returned value is
        /// a number
        static abstract get: targetSelector: obj * propName: string -> float
        /// Returns the current value of a targets property, with optional unit conversion
        /// or removal. If target is an HTMLElement or SVGElement and unit parameter is not
        /// set to false or set a to a valid unit, then returned value is a string. If target
        /// is HTMLElement or SVGElement and unit parameter is false, then returned value is
        /// a number
        static abstract get: targetSelector: obj * propName: string * unit: bool -> float
        /// Immediately sets one or multiple properties values to one or multiple
        /// targets. It's useful for setting complex values, but for repeatedly
        /// updating the same properties on the same targets, use animatable.
        /// This won't work if you try to set an attribute on a DOM or SVG element
        /// not already defined on the element.
        static abstract set: targets: obj * parameters: obj -> Animation
        /// Removes one or multiple targets from all active animations, a specific
        /// instance or a specific property, cancelling any animation or timeline
        /// referencing the targets if neeeded.
        static abstract remove: targets: obj * ?renderable: U2<Animation, Timeline> * ?propertyName: string -> obj[]
        /// Removes all css inline styles added by the specified instance. Can be
        /// used as a animation or timeline oncomplete callback.
        static abstract cleanInlineStyles: renderable: Animation -> Animation 
        /// Removes all css inline styles added by the specified instance. Can be
        /// used as a animation or timeline oncomplete callback.
        static abstract cleanInlineStyles: renderable: Timeline -> Timeline
        /// Returns a function that recreates a timer, animation or timeline while keeping
        /// track of its current time, allowing to seamlessly update an animations
        /// parameters without breaking the playback state.
        static abstract keepTime: (unit -> Timer) -> (unit -> Timer)
        /// Returns a function that recreates a timer, animation or timeline while keeping
        /// track of its current time, allowing to seamlessly update an animations
        /// parameters without breaking the playback state.
        static abstract keepTime: (unit -> Animation) -> (unit -> Animation)
        /// Returns a function that recreates a timer, animation or timeline while keeping
        /// track of its current time, allowing to seamlessly update an animations
        /// parameters without breaking the playback state.
        static abstract keepTime: (unit -> Timeline) -> (unit -> Timeline)
        /// Returns a random number within a specified range, with an optional third parameter
        /// determining the number of decimal places.
        static abstract random: min: float * max: float * ?decimalLength: int -> float
        /// Returns a random element from the collection.
        static abstract randomPick: collection: 'T[] -> 'T
        /// Mutates an array by randomizing the order of its elements.
        static abstract shuffle: collection: 'T[] -> 'T[]
        /// Executes a callback function in sync with the engine loop
        static abstract sync: callback: (Timer -> unit) -> Timer
        /// Performs a linear interpolation between two values. The closer the amount is
        /// to 1, the closer the result is to the end value.
        static abstract lerp: start: float * ``end``: float * amount: float * ?renderable: obj -> float
        /// Rounds a number to a specified number of decimal places or creates a rounding
        /// function with a predefined decimalLength parameter.
        static abstract round: value: float * decimalLength: int -> float
        /// Rounds a number to a specified number of decimal places or creates a rounding
        /// function with a predefined decimalLength parameter.
        static abstract round: decimalLength: int -> (float -> float)
        /// Restricts a number between the specified min and max values, or creates
        /// a clamping function with predefined min and max values
        static abstract clamp: value: float * min: float * max: float -> float
        /// Restricts a number between the specified min and max values, or creates
        /// a clamping function with predefined min and max values
        static abstract clamp: min: float * max: float -> (float -> float)
        /// Rounds a number to the nearest specified increment or creates a snapping
        /// function with a predefined increment parameter. If an array is provided
        /// as the increment, it selects the closest value from the array.
        static abstract snap: value: float * increment: float -> float
        /// Rounds a number to the nearest specified increment or creates a snapping
        /// function with a predefined increment parameter. If an array is provided
        /// as the increment, it selects the closest value from the array.
        static abstract snap: value: float * increment: float[] -> float
        /// Rounds a number to the nearest specified increment or creates a snapping
        /// function with a predefined increment parameter. If an array is provided
        /// as the increment, it selects the closest value from the array.
        static abstract snap: increment: float -> (float -> float)
        /// Rounds a number to the nearest specified increment or creates a snapping
        /// function with a predefined increment parameter. If an array is provided
        /// as the increment, it selects the closest value from the array.
        static abstract snap: increment: float[] -> (float -> float)
        /// Wraps a number between a range defined with min and max values or creates
        /// a wrapping function with predefined min and max parameters
        static abstract wrap: value: float * min: float * max: float -> float
        /// Wraps a number between a range defined with min and max values or creates
        /// a wrapping function with predefined min and max parameters
        static abstract wrap: min: float * max: float -> (float -> float)
        /// Maps a number from one range to another or creates a mapping function
        /// with predefined ranges parameters
        static abstract mapRange: value: float * fromLow: float * fromHigh: float * toLow: float * toHigh: float -> float
        /// Maps a number from one range to another or creates a mapping function
        /// with predefined ranges parameters
        static abstract mapRange: fromLow: float * fromHigh: float * toLow: float * toHigh: float -> (float -> float)
        /// Interpolates a value between two numbers based on a given progress or creates an interpolation
        /// function with predefined start and end parameters
        static abstract interpolate: start: float * ``end``: float * progress: float -> float
        /// Interpolates a value between two numbers based on a given progress or creates an interpolation
        /// function with predefined start and end parameters
        static abstract interpolate: start: float * ``end``: float -> (float -> float)
        /// Rounds a value to a specified decimal length, pads with zeros if needed, and returns
        /// the result as a string, or creates a rounding and padding function with a predefined
        /// decimal length parameter
        static abstract roundPad: value: U2<float, string> * decimalLength: int -> string
        /// Rounds a value to a specified decimal length, pads with zeros if needed, and returns
        /// the result as a string, or creates a rounding and padding function with a predefined
        /// decimal length parameter
        static abstract roundPad: decimalLength: int -> (float -> string)
        /// Pads a number from the start with a string until the result reaches a given length or creates a padding
        /// function with predefined total length and padstring parameters
        static abstract padStart: value: U2<float, string> * totalLength: int * padString: string -> string
        /// Pads a number from the start with a string until the result reaches a given length or creates a padding
        /// function with predefined total length and padstring parameters
        static abstract padStart: totalLength: int * padString: string -> (U2<float, string> -> string)
        /// Pads a number from the end with a string until the result reaches a given length or creates a
        /// padding function with predefined totallength and padstring parameters
        static abstract padEnd: value: U2<float, string> * totalLength: int * padString: string -> string
        /// Pads a number from the end with a string until the result reaches a given length or creates a
        /// padding function with predefined totallength and padstring parameters
        static abstract padEnd: totalLength: int * padString: string -> (U2<float, string> -> string)
        /// Converts degrees into radians
        static abstract degToRad: degrees: float -> float
        /// Converts radians into degrees
        static abstract radToDeg: radians: float -> float
        
    [<Interface; AllowNullLiteral>]
    type JsTimerCallbacks<'T> =
        abstract onBegin: Callback<'T> with get,set
        abstract onBeforeUpdate: Callback<'T> with get,set
        abstract onUpdate: Callback<'T> with get,set
        abstract onLoop: Callback<'T> with get,set
        abstract onPause: Callback<'T> with get,set
    
    [<Interface; AllowNullLiteral>]
    type TimerCallbacks<'T> =
        abstract onComplete: Callback<'T> with get,set
        abstract ``then``: (Callback<'T> -> JS.Promise<unit>) with get,set
    [<Interface; AllowNullLiteral>]
    type PlaybackParameters =
        abstract delay: float with get,set
        abstract duration: float with get,set
        abstract loop: U2<bool, int> with get,set
        abstract loopDelay: float with get,set
        abstract alternate: bool with get,set
        /// Reverses the iterationTime
        abstract reversed: bool with get,set
        /// No effect when used with a timer that is added to a timeline
        abstract autoplay: U2<bool, ScrollObserver> with get,set
        abstract frameRate: int with get,set
        /// Default is 1.0
        abstract playbackRate: float with get,set
    
    [<Interface; AllowNullLiteral>]
    type PlaybackParametersWithFuncValues =
        abstract member delay: U2<float, FunctionValue<float>> with get,set
        abstract member duration: U2<float, FunctionValue<float>> with get,set
        abstract member loop: U2<bool, float> with get,set
        abstract member alternate: bool with get,set
        abstract member reversed: bool with get,set
        abstract member autoplay: U2<bool, ScrollObserver> with get,set
        abstract member playbackRate: float with get,set
    
    [<Interface; AllowNullLiteral>]
    type JsPlaybackParametersWithFuncValues =
        abstract member loopDelay: U2<float, FunctionValue<float>> with get,set
        abstract member frameRate: int with get,set
        abstract member playbackEase: EasingFun with get,set
    
    [<Interface; AllowNullLiteral>]
    type PlaybackMethods<'T> =
        abstract member play: ChainMethod<'T>
        abstract member reverse: ChainMethod<'T>
        abstract member pause: ChainMethod<'T>
        abstract member restart: ChainMethod<'T>
        abstract member alternate: ChainMethod<'T>
        abstract member resume: ChainMethod<'T>
        abstract member complete: ChainMethod<'T>
        /// Pauses the timer, removes it from the engine's main loop and
        /// frees up the memory
        abstract member cancel: ChainMethod<'T>
        /// <summary>
        /// Cancels the timer, sets its <c>currentTime</c> to <c>0</c> and reverts
        /// the linked <c>onScroll()</c> instance if necessary.<br/><br/> Use <c>.revert()</c>
        /// when you want to completely stop and destroy a timer
        /// </summary>
        abstract member revert: ChainMethod<'T>
    
    [<Interface; AllowNullLiteral>]
    type JsPlaybackMethods<'T> =
        abstract member seek: time: float * ?muteCallbacks: bool -> 'T
        /// <summary>
        /// Changes the total duration of a timer to fit a specific time. The total
        /// duration is equal to the duration of an iteration multiplied with the total
        /// number of iterations. So if a timer has a duration of 1000ms and loops twice
        /// (3 iterations in total) then the total duration is 3000ms.
        /// </summary>
        /// <param name="duration">Duration in ms</param>
        abstract member stretch: duration: float -> 'T
    
    type TimerOptions =
        inherit JsTimerCallbacks<Timer>
        inherit TimerCallbacks<Timer>
        inherit PlaybackParameters
    [<Interface; AllowNullLiteral>]
    type TimerProperties =
        abstract member currentTime: float with get,set
        /// <summary>
        /// Value between <c>0.</c> and <c>1.</c>
        /// </summary>
        abstract member progress: float with get,set
        abstract member speed: float with get,set
        abstract member paused: bool with get,set
        abstract member completed: bool with get,set
    [<Interface; AllowNullLiteral>]
    type JsTimerProperties =
        abstract member id: U2<string, int> with get,set
        abstract member deltaTime: float with get
        abstract member iterationCurrentTime: float with get,set
        abstract member iterationProgress: float with get,set
        abstract member currentIteration: int64 with get,set
        abstract member fps: int with get,set
        abstract member began: bool with get,set
        abstract member reversed: bool with get,set
    type Timer =
        inherit PlaybackMethods<Timer>
        inherit JsPlaybackMethods<Timer>
        inherit TimerProperties
        inherit JsTimerProperties

    module AnimatableProperties =
        [<AllowNullLiteral; Interface>]
        type CssProperties =
            inherit CSSStyleDeclaration
        [<AllowNullLiteral; Interface>]
        type CssTransforms =
            abstract member translateX: U2<float, string> with get,set
            abstract member translateY: U2<float, string> with get,set        
            abstract member translateZ: U2<float, string> with get,set        
            abstract member x: U2<float, string> with get,set
            abstract member y: U2<float, string> with get,set        
            abstract member z: U2<float, string> with get,set
            abstract member rotate: U2<float, string> with get,set
            abstract member rotateX: U2<float, string> with get,set
            abstract member rotateY: U2<float, string> with get,set
            abstract member rotateZ: U2<float, string> with get,set
            abstract member scale: float with get,set
            abstract member scaleX: float with get,set
            abstract member scaleY: float with get,set
            abstract member scaleZ: float with get,set
            abstract member skew: U2<float, string> with get,set
            abstract member skewX: U2<float, string> with get,set
            abstract member skewY: U2<float, string> with get,set
            abstract member perspective: U2<float, string> with get,set            
        type CSS with
            [<Emit("CSS.registerProperty($0)")>]
            static member registerProperty(value: string): unit = jsNative
        [<Erase>]
        type AnimatableProperty =
            | CssTransform of CssTransforms
            | CssProperty of CssProperties



    [<AutoOpen; Erase>]
    module TweenValueTypes =
        [<Interface; AllowNullLiteral>]
        type RelativeValue =
            [<Emit("'+=$0'")>]
            static member Add(value: float): RelativeValue = jsNative
            [<Emit("'+='+$0")>]
            static member Add(value: string): RelativeValue = jsNative
            [<Emit("'-=$0'")>]
            static member Subtract(value: float): RelativeValue = jsNative
            [<Emit("'-='+$0")>]
            static member Subtract(value: string): RelativeValue = jsNative
            [<Emit("'*=$0'")>]
            static member Multiply(value: float): RelativeValue = jsNative
            [<Emit("'*='+$0")>]
            static member Multiply(value: string): RelativeValue = jsNative
        [<Interface; AllowNullLiteral>]
        type ColorValue =
            [<Emit("$0")>]
            static member Hex(value: string): ColorValue = jsNative
            [<Emit("'rgb($0, $1, $2)'")>]
            static member Rgb(r: float, g: float, b: float): ColorValue = jsNative
            [<Emit("'rgb($0, $1, $2, $3)'")>]
            static member Rgba(r: float, g: float, b: float, a: float): ColorValue = jsNative
            [<Emit("'hsl($0, $1, $2)'")>]
            static member Hsl(h: float, s: float, l: float): ColorValue = jsNative
            [<Emit("'hsla($0, $1, $2, $3)'")>]
            static member Hsla(h: float, s: float, l: float, a: float): ColorValue = jsNative
            [<Emit("$0")>]
            static member Named(value: string): ColorValue = jsNative
        [<Interface; AllowNullLiteral>]
        type CssVariable =
            static member cssVar(varName: string): CssVariable = !!FunctionValue(fun target _ _ -> Utils.get(target, varName))
        
        [<Erase>]
        type TweenValueType =
            | ColorValue of ColorValue
            | CssVariable of CssVariable
            | RelativeValue of RelativeValue
            | Number of float
            | String of string
            | Boolean of bool
            static member cssVar(varName: string) = CssVariable <| !!FunctionValue(fun target _ _ -> Utils.get(target, varName))
            [<Emit("$0")>]
            static member Hex(value: string): TweenValueType = jsNative
            [<Emit("'rgb($0, $1, $2)'")>]
            static member Rgb(r: float, g: float, b: float): TweenValueType = jsNative
            [<Emit("'rgb($0, $1, $2, $3)'")>]
            static member Rgba(r: float, g: float, b: float, a: float): TweenValueType = jsNative
            [<Emit("'hsl($0, $1, $2)'")>]
            static member Hsl(h: float, s: float, l: float): TweenValueType = jsNative
            [<Emit("'hsla($0, $1, $2, $3)'")>]
            static member Hsla(h: float, s: float, l: float, a: float): TweenValueType = jsNative
            [<Emit("$0")>]
            static member NamedColor(value: string): TweenValueType = jsNative
            [<Emit("'+=$0'")>]
            static member Add(value: float): TweenValueType = jsNative
            [<Emit("'+='+$0")>]
            static member Add(value: string): TweenValueType = jsNative
            [<Emit("'-=$0'")>]
            static member Subtract(value: float): TweenValueType = jsNative
            [<Emit("'-='+$0")>]
            static member Subtract(value: string): TweenValueType = jsNative
            [<Emit("'*=$0'")>]
            static member Multiply(value: float): TweenValueType = jsNative
            [<Emit("'*='+$0")>]
            static member Multiply(value: string): TweenValueType = jsNative
            
    [<Interface; AllowNullLiteral>]
    type TweenParameters =
        abstract member ``to``: U2<TweenValueType, TweenValueType * TweenValueType> with get,set
        abstract member from: TweenValueType with get,set
        abstract member delay: U2<float, FunctionValue<float>> with get,set
        abstract member duration: U2<float, FunctionValue<float>> with get,set
        abstract member ease: EasingFun with get,set
    module Keyframes =
        type ParameterKeyframe =
            inherit AnimatableProperties.CssProperties
            inherit TweenParameters
        type TweenKeyframe = U2<TweenValueType, ParameterKeyframe>[]
        type PercentageKeyframes = (string * ParameterKeyframe)[]
        [<Erase>]
        type Keyframes =
            | Tween of TweenKeyframe
            | Percentage of PercentageKeyframes
            [<Emit("$0")>]
            member this.boxed: string = jsNative
        
    [<Interface; AllowNullLiteral>]
    type JsTweenParameters =
        inherit TweenParameters
        abstract member composition: composition with get,set
        abstract member modifier: FloatModifier with get,set
    
    
    [<Interface; AllowNullLiteral>]
    type AnimationOptions =
        // inherit AnimatableProperties.CssProperties<
        inherit PlaybackParametersWithFuncValues
        inherit JsPlaybackParametersWithFuncValues
        inherit TimerCallbacks<Animation>
        inherit JsTimerCallbacks<Animation>
        abstract member keyframes: Keyframes.Keyframes[] with get,set
        [<EmitIndexer>]
        abstract member Item: string -> JsTweenParameters with set
    
    [<Interface; AllowNullLiteral>]
    type JsAnimationMethods<'T> =
        abstract member refresh: ChainMethod<'T>
    [<Interface; AllowNullLiteral>]
    type Animation =
        inherit PlaybackMethods<Animation>
        inherit JsPlaybackMethods<Animation>
        inherit TimerProperties
        inherit JsTimerProperties
        inherit JsAnimationMethods<Animation>
        abstract member targets: obj[]
        abstract member duration: float with get,set
    
    module TimePosition =
        [<StringEnum>]
        type RelativePosition =
            | [<CompiledName "<">] AfterLast
            | [<CompiledName "<<">] WithLast
        type RelativeTimePosition =
            static member inline create relativePosition tweenPosition = $"{relativePosition}{tweenPosition}"
        [<Erase>]
        type Label = Label of string
    
    [<Erase>]
    type TimePosition =
        | RelativeTween of TweenValueTypes.RelativeValue
        | RelativePosition of TimePosition.RelativePosition
        | RelativeTimePosition of TimePosition.RelativeTimePosition
        | Absolute of float
        | Label of TimePosition.Label
        | Function of FunctionValue<float>
    
    [<Interface; AllowNullLiteral>]
    type TimelineOptionsDefaults =
        inherit PlaybackParametersWithFuncValues
        inherit JsPlaybackParametersWithFuncValues
        inherit JsTweenParameters
        inherit TweenParameters
        inherit TimerCallbacks<Animation>
        inherit JsTimerCallbacks<Animation>
    
    
    [<Interface; AllowNullLiteral>]
    type TimelineOptions =
        inherit PlaybackParameters
        inherit JsPlaybackParametersWithFuncValues
        inherit TimerCallbacks<Timeline>
        inherit JsTimerCallbacks<Timeline>
        abstract member onRender: Callback<Timeline> with get,set
        abstract member defaults: TimelineOptionsDefaults with get,set
    
    [<Interface; AllowNullLiteral>]
    type Timeline =
        inherit PlaybackMethods<Timeline>
        inherit JsPlaybackMethods<Timeline>
        inherit JsAnimationMethods<Timeline>
        inherit TimerProperties
        inherit JsTimerProperties
        abstract member label: labelName: string * ?position: TimePosition -> Timeline
        abstract member call: callback: (unit -> _) * ?position: TimePosition -> Timeline
        abstract member init: ChainMethod<Timeline>
        
    
    type TimelineExtensions =
        [<Extension; Emit("$0.add($1{{, $2}})")>]
        static member add(this: Timeline, parameters: TimerOptions, ?position: TimePosition): Timeline = jsNative
        [<Extension; Emit("$0.add($1, $2{{, $3}})")>]
        static member add(this: Timeline, targets: obj, parameters: AnimationOptions, ?position: TimePosition): Timeline = jsNative
        [<Extension; Emit("$0.sync($1{{, $2}})")>]
        static member sync(this: Timeline, timer: Timer, ?position: TimePosition): Timeline = jsNative
        [<Extension; Emit("$0.sync($1{{, $2}})")>]
        static member sync(this: Timeline, synced: U3<Animation, Timer, Timeline>, ?position: TimePosition): Timeline = jsNative
        [<Extension; Emit("$0.sync($1{{, $2}})")>]
        static member sync(this: Timeline, synced: WAAPI.Animation, ?position: TimePosition): Timeline = jsNative
        [<Extension; Emit("$0.remove($1)")>]
        static member remove(this: Timeline, targets: obj): Timeline = jsNative
        [<Extension; Emit("$0.remove($1{{, $2}})")>]
        static member remove(this: Timeline, object: Animation, ?position: TimePosition): Timeline = jsNative
        [<Extension; Emit("$0.remove($1{{, $2}})")>]
        static member remove(this: Timeline, object: Timeline, ?position: TimePosition): Timeline = jsNative
        [<Extension; Emit("$0.remove($1{{, $2}})")>]
        static member remove(this: Timeline, object: Timer, ?position: TimePosition): Timeline = jsNative
        [<Extension; Emit("$0.remove($1, $2)")>]
        static member remove(this: Timeline, targets: obj, propertyName: string): Timeline = jsNative
    
    [<Interface; AllowNullLiteral>]
    type AnimatableSettings =
        abstract member unit: string with get,set
        abstract member duration: float with get,set
        abstract member ease: EasingFun with get,set
        abstract member modifier: FloatModifier with get,set
        
    
    [<Interface; AllowNullLiteral>]
    type AnimatableOptions =
        inherit AnimatableProperties.CssProperties (* <AnimatableSettings> *)
        inherit AnimatableSettings
    
    [<Interface; AllowNullLiteral>]
    type Animations =
        [<EmitIndexer>]
        abstract member Item: string -> Animation
    [<Interface; AllowNullLiteral>]
    type Animatable =
        [<Emit("$0[$1]()")>]
        abstract member get: propertyName: string -> U2<float, float[]>
        [<Emit("$0[$1]($2{{, $3}}{{, $4}})")>]
        abstract member set: propertyName: string * value: U2<float, float[]> * ?duration: float * ?easing: EasingFun -> Animatable
        abstract member revert: ChainMethod<Animatable>
        abstract member targets: obj[]
        abstract member animations: Animations

    [<Interface; AllowNullLiteral>]
    type DraggableAxesParameters =
        abstract member x: U2<bool, DraggableAxesParameters> with get,set
        abstract member y: U2<bool, DraggableAxesParameters> with get,set
        abstract member snap: U4<float, float[], FunctionValue<float>, FunctionValue<float[]>> with get,set
        abstract member modifier: FloatModifier with get,set
        abstract member mapTo: string with get,set
    [<Erase>]
    type Bounds = Bounds of top: float * right: float * bottom: float * left: float
    [<Interface; AllowNullLiteral>]
    type DraggableCursor =
        abstract member onHover: string with get,set
        abstract member onGrab: string with get,set
    [<Interface; AllowNullLiteral>]
    type DraggablePos =
        abstract member x: float with get,set
        abstract member y: float with get,set
    [<Erase>]
    type DraggablePosHistory = DraggablePosHistory of x: float * y: float * prevX: float * prevY: float 
    [<Interface; AllowNullLiteral>]
    type DraggableSettings =
        abstract member trigger: U2<string, Element> with get,set
        abstract member container: U4<string, HTMLElement, Bounds, FunctionValue<Bounds>> with get,set
        abstract member containerPadding: U3<float, Bounds, FunctionValue<Bounds>> with get,set
        abstract member containerFriction: U2<float, FunctionValue<float>> with get,set
        abstract member releaseContainerFriction: U2<float, FunctionValue<float>> with get,set
        abstract member releaseMass: float with get,set
        abstract member releaseStiffness: float with get,set
        abstract member releaseDamping: float with get,set
        abstract member velocityMultiplier: U2<float, FunctionValue<float>> with get,set
        abstract member minVelocity: U2<float, FunctionValue<float>> with get,set
        abstract member maxVelocity: U2<float, FunctionValue<float>> with get,set
        abstract member releaseEase: EasingFun with get,set
        abstract member dragSpeed: U2<float, FunctionValue<float>> with get,set
        abstract member scrollThreshold: U2<float, FunctionValue<float>> with get,set
        abstract member scrollSpeed: U2<float, FunctionValue<float>> with get,set
        abstract member cursor: U4<bool, DraggableCursor, FunctionValue<bool>, FunctionValue<DraggableCursor>> with get,set
        
        
        
    [<Interface; AllowNullLiteral>]
    type DraggableCallbacks<'T> =
        abstract member onGrab: Callback<'T> with get,set
        abstract member onDrag: Callback<'T> with get,set
        abstract member onUpdate: Callback<'T> with get,set
        abstract member onRelease: Callback<'T> with get,set
        abstract member onSnap: Callback<'T> with get,set
        abstract member onSettle: Callback<'T> with get,set
        abstract member onResize: Callback<'T> with get,set
        abstract member onAfterResize: Callback<'T> with get,set
    
    [<Interface; AllowNullLiteral>]
    type DraggableOptions =
        inherit DraggableAxesParameters
        inherit DraggableSettings
        inherit DraggableCallbacks<Draggable>
    [<Interface; AllowNullLiteral>]
    type Draggable =
        inherit DraggableSettings
        inherit DraggableCallbacks<Draggable>
        abstract member disable: ChainMethod<Draggable>
        abstract member enable: ChainMethod<Draggable>
        abstract member setX: x: float * ?muteCallback: bool -> Draggable
        abstract member setY: y: float * ?muteCallback: bool -> Draggable
        abstract member animateInView: ?duration: float * ?gap: float * ?ease: EasingFun -> Draggable
        abstract member scrollInView: ?duration: float * ?gap: float * ?ease: EasingFun -> Draggable
        abstract member stop: ChainMethod<Draggable>
        abstract member reset: ChainMethod<Draggable>
        abstract member revert: ChainMethod<Draggable>
        abstract member refresh: ChainMethod<Draggable>
        abstract member ``$container``: HTMLElement with get,set
        abstract member ``$target``: HTMLElement with get,set
        abstract member ``$trigger``: HTMLElement with get,set
        abstract member ``$scrollContainer``: U2<HTMLElement, Window> with get,set
        abstract member x: float with get,set
        abstract member y: float with get,set
        abstract member progressX: float with get,set
        abstract member progressY: float with get,set
        abstract member velocity: float with get
        abstract member angle: float with get
        abstract member xProp: string with get
        abstract member yProp: string with get
        abstract member destX: float with get
        abstract member destY: float with get
        abstract member deltaX: float with get
        abstract member deltaY: float with get
        abstract member enabled: bool with get
        abstract member grabbed: bool with get
        abstract member dragged: bool with get
        abstract member cursor: DraggableCursor with get,set
        abstract member disabled: bool * bool with get
        abstract member ``fixed``: bool with get
        abstract member useWin: bool with get
        abstract member isFinePointer: bool with get
        abstract member initialized: bool with get
        abstract member canScroll: bool with get
        abstract member container: bool with get
        abstract member manual: bool with get
        abstract member released: bool with get
        abstract member updated: bool with get
        abstract member scroll: DraggablePos with get
        abstract member coords: DraggablePosHistory with get
        abstract member snapped: (*x*) float * (*y*) float with get
        abstract member pointer: DraggablePosHistory with get
        abstract member scrollView: (*x*) float * (*y*) float with get
        abstract member dragArea: (*x*) float * (*y*) float * (*width*) float * (*height*) float
        abstract member scrollBounds: Bounds with get
        abstract member targetBounds: Bounds with get
        abstract member window: (*width*) float * (*height*) float with get
        abstract member pointerVelocity: float with get
        abstract member pointerAngle: float with get
        abstract member activeProp: string with get
    
    [<Interface; AllowNullLiteral>]
    type ScrollObserverCallbacks<'T> =
        abstract member onEnter: Callback<'T> with get,set
        abstract member onEnterForward: Callback<'T> with get,set
        abstract member onEnterBackward: Callback<'T> with get,set
        abstract member onLeave: Callback<'T> with get,set
        abstract member onLeaveForward: Callback<'T> with get,set
        abstract member onLeaveBackward: Callback<'T> with get,set
        abstract member onUpdate: Callback<'T> with get,set
        abstract member onSyncComplete: Callback<'T> with get,set
    
    [<Interface; AllowNullLiteral>]
    type ScrollObserverOptions =
        inherit ScrollObserverCallbacks<ScrollObserver>
        abstract member container: U2<string, Element> with get,set
        abstract member target: U2<string, Element> with get,set
        abstract member debug: bool with get,set
        abstract member axis: axis with get,set
        abstract member repeat: bool with get,set
        abstract member enter: string with get,set
        abstract member leave: string with get,set
        abstract member sync: U4<string, float, bool, EasingFun> with get,set
    
    [<Interface; AllowNullLiteral>]
    type ScrollObserver =
        abstract member refresh: ChainMethod<ScrollObserver>
        abstract member revert: ChainMethod<ScrollObserver>
        abstract member link: U3<Animation, Timer, Timeline> -> ScrollObserver
        
        abstract member id: float with get
        abstract member container: obj with get
        abstract member target: HTMLElement
        abstract member linked: U3<Animation, Timer, Timeline>
        abstract member repeat: bool
        abstract member horizontal: bool
        abstract member enter: U2<string, float>
        abstract member leave: U2<string, float> with get,set
        abstract member sync: bool
        abstract member velocity: float
        abstract member backward: bool
        abstract member scroll: float
        abstract member progress: float
        abstract member completed: bool
        abstract member began: bool
        abstract member isInView: bool
        abstract member offset: float
        abstract member offsetStart: float
        abstract member offsetEnd: float
        abstract member distance: float
    
    [<AllowNullLiteral; Interface>]
    type ScopeOptionsDefaults =
        abstract member playbackEase: EasingFun with get,set
        abstract member playbackRate: float with get,set
        abstract member frameRate: int with get,set
        abstract member loop: U2<float, bool> with get,set
        abstract member reversed: bool with get,set
        abstract member alternate: bool with get,set
        abstract member autoplay: bool with get,set
        abstract member duration: U2<float, FunctionValue<float>> with get,set
        abstract member delay: U2<float, FunctionValue<float>> with get,set
        abstract member composition: U2<composition, FunctionValue<composition>> with get,set
        abstract member ease: EasingFun with get,set
        abstract member loopDelay: float with get,set
        abstract member modifier: FloatModifier with get,set
        abstract member onBegin: Callback<obj> with get,set
        abstract member onUpdate: Callback<obj> with get,set
        abstract member onRender: Callback<obj> with get,set
        abstract member onLoop: Callback<obj> with get,set
        abstract member onComplete: Callback<obj> with get,set
    
    [<AllowNullLiteral; Interface>]
    type ScopeOptions =
        abstract member root: U2<string, Element> with get,set
        abstract member mediaQueries: obj with get,set
        abstract member defaults: ScopeOptionsDefaults with get,set
    
    [<AllowNullLiteral; Interface>]
    type ScopeMethods =
        abstract member Item: string -> JsFunc
    
    [<AllowNullLiteral; Interface>]
    type Scope =
        abstract member revert: ChainMethod<Scope>
        abstract member refresh: ChainMethod<Scope>
        abstract member data: obj with get
        abstract member defaults: ScopeOptionsDefaults with get
        abstract member root: U2<Document, HTMLElement>
        abstract member constructors: JsFunc[]
        abstract member revertConstructors: JsFunc[]
        abstract member revertibles: U5<Animatable, Draggable, ScrollObserver, Scope, Animation>[]
        abstract member methods: ScopeMethods
        abstract member matches: obj
        abstract member mediaQueryLists: obj
        abstract member addOnce: (unit -> unit) -> Scope
    [<Erase>]
    type ScopeExtensions =
        [<Emit("$0.add($1)")>]
        static member add(this: Scope, constructor: Scope -> unit): Scope = jsNative
        [<Emit("$0.add($1)")>]
        static member add(this: Scope, constructor: Scope -> (unit -> unit)): Scope = jsNative
        [<Emit("$0.add($1, $2)")>]
        static member add(this: Scope, methodName: string, method: Core.FSharpFunc<_, _>): Scope = jsNative
        [<Emit("$0.add($1, $2)")>]
        static member add(this: Scope, methodName: string, method: System.Func<_>): Scope = jsNative
        [<Emit("$0.add($1, $2)")>]
        static member add(this: Scope, methodName: string, method: System.Func<_, _>): Scope = jsNative
        [<Emit("$0.add($1, $2)")>]
        static member add(this: Scope, methodName: string, method: System.Func<_, _, _>): Scope = jsNative
        [<Emit("$0.add($1, $2)")>]
        static member add(this: Scope, methodName: string, method: System.Func<_, _, _, _>): Scope = jsNative
        [<Emit("$0.add($1, $2)")>]
        static member add(this: Scope, methodName: string, method: System.Func<_, _, _, _, _>): Scope = jsNative
        [<Emit("$0.add($1, $2)")>]
        static member add(this: Scope, methodName: string, method: System.Func<_, _, _, _, _, _>): Scope = jsNative
        [<Emit("$0.add($1, $2)")>]
        static member add(this: Scope, methodName: string, method: System.Func<_, _, _, _, _, _, _>): Scope = jsNative
        [<Emit("$0.add($1, $2)")>]
        static member add(this: Scope, methodName: string, method: System.Func<_, _, _, _, _, _, _, _>): Scope = jsNative
        [<Emit("$0.keepTime($1)")>]
        static member keepTime(this: Scope, constructor: unit -> Timer): Scope = jsNative
        [<Emit("$0.keepTime($1)")>]
        static member keepTime(this: Scope, constructor: unit -> Animation): Scope = jsNative
        [<Emit("$0.keepTime($1)")>]
        static member keepTime(this: Scope, constructor: unit -> Timeline): Scope = jsNative
        
    [<AllowNullLiteral; Interface>]
    type TextSplitter =
        /// Gets the split root element
        abstract ``$target``: HTMLElement with get
        /// Gets the html to split
        abstract html: string with get
        /// Gets if the debug styles are visible or not
        abstract debug: bool with get
        /// Gets if the spaces should be wrapped within the text
        abstract includeSpaces: bool with get
        /// Gets if the accessible clone element should be created
        abstract accessible: bool
        /// Gets the lines of the element
        abstract lines: HTMLElement[]
        /// Gets the words of the element
        abstract words: HTMLElement[]
        /// Gets the chars of the element
        abstract chars: HTMLElement[]
        /// The line html template
        abstract lineTemplate: string
        /// The word html template
        abstract wordTemplate: string
        /// The char html template
        abstract charTemplate: string
        /// Preserves animations and callbacks state between splits when
        /// splitting by lines, and allows reverting all split animations
        /// at once using split.revert()
        abstract addEffect: effect: (TextSplitter -> U4<Animation, Timeline, Timer, unit -> unit>) -> TextSplitter
        abstract revert: unit -> TextSplitter
        /// Manually splits the text again, taking into account any
        /// parameter changes. The properties that can be safely
        /// manually updated are:
        /// Target, html, debug, includeSpaces, accessible, linetemplate,
        /// wordtemplate, chartemplate
        abstract refresh: unit -> TextSplitter
    
    /// Defines the CSS class, wrap behaviour or clone type of a split.
    /// Parameters are configured by passing an object to the lines, words
    /// and chars properties
    [<Interface; AllowNullLiteral>]
    type SplitParameters =
        /// Specifies a custom css class applied to all split elements
        abstract class': string with get,set
        /// Adds an extra wrapper element with the specified CSS overflow
        /// property to all split elements.
        /// One of: 'hidden', 'clip', 'visible', 'scroll', 'auto', true ('clip'), false, null
        abstract wrap: U2<string, bool> with get,set
        /// Clones the split elements in the specified direction by
        /// wrapping the lines, words, or characters within the following
        /// html strcture and setting the top and left CSS properties
        /// accordingly.
        /// Accepts: 'left','top','right','bottom','center',true ('center'), null
        abstract clone: U2<string, bool> with get,set
        
    [<Erase>]
    module HtmlTemplate =
        let [<Literal>] index = "{i}"
        let [<Literal>] value = "{value}"
    [<Erase>]
    type HtmlTemplate =
        /// Use the value and index literals if needed while making
        /// the template
        static member inline create(template: string): HtmlTemplate = !!template 
            
        
    [<AllowNullLiteral; Interface>]
    type TextSplitterOptions =
        /// Defines if and how the lines should be split.
        /// Split elements are accessed via an array returned by the
        /// lines property of a TextSplit instance.
        /// Split wrappers can be configured by passing an object of split parameters
        /// or by passing a custom html template string.
        abstract lines: U4<HtmlTemplate,bool, SplitParameters, string> with get,set
        /// Defines if and how the words should be split.
        /// Split elements are accessed via an array returned by the
        /// words property of a TextSplit instance.
        /// Split wrappers can be configured by passing an object of split parameters
        /// or by passing a custom html template string.
        abstract words: U4<HtmlTemplate, bool, SplitParameters, string> with get,set
        /// Defines if and how the chars should be split.
        /// Split elements are accessed via an array returned by the
        /// chars property of a TextSplit instance.
        /// Split wrappers can be configured by passing an object of split parameters
        /// or by passing a custom html template string.
        abstract chars: U4<HtmlTemplate, bool, SplitParameters, string> with get,set
        /// Toggles debug css styles on the split elements to better visualize
        /// the wrapper elements. Lines are outlined in green,
        /// words in red, characters in blue
        abstract debug: bool with get,set
        /// Defines whether whitespace should be incldued in the split elements.
        abstract includeSpaces: bool with get,set
        /// Creates an accessible cloned element that preserves the structure
        /// of the original split element.
        abstract accessible: bool with get,set
    [<Interface>]
    [<Import("text", "animejs")>]
    type Text =
        static abstract split: target: obj * ?parameters: TextSplitterOptions -> TextSplitter
        
    [<Interface; AllowNullLiteral>]
    type StaggerOptions =
        abstract member start: U2<float, TimePosition> with get,set
        abstract member from: U2<int, staggerFrom> with get,set
        abstract member reversed: bool with get,set
        abstract member ease: EasingFun with get,set
        abstract member grid: float * float with get,set
        abstract member axis: axis with get,set
        abstract member modifier: FloatModifier with get,set
        /// Defines a custom staggering order instead of using the
        /// natural targets order by using an attribute or property
        /// of the targets. Properties or attribute must contain a suite
        /// of number, starting at 0.
        /// A custom total parameter value must be defined if the highest
        /// custom index is lower than the actual total length of staggered targets.
        abstract member ``use``: string with get,set
        abstract member total: int with get,set
    type AnimeJs =
        [<ImportMember(Spec.path)>]
        static abstract stagger: value: float -> FunctionValue<_>
        [<ImportMember(Spec.path)>]
        static abstract stagger: value: (float * float) -> FunctionValue<_>
        [<ImportMember(Spec.path)>]
        static abstract stagger: value: string -> FunctionValue<_>
        [<ImportMember(Spec.path)>]
        static abstract stagger: value: (string * string) -> FunctionValue<_>
        [<ImportMember(Spec.path)>]
        static abstract stagger: value: float * parameters: obj -> FunctionValue<_>
        [<ImportMember(Spec.path)>]
        static abstract stagger: value: (float * float) * parameters: obj -> FunctionValue<_>
        [<ImportMember(Spec.path)>]
        static abstract stagger: value: string * parameters: obj -> FunctionValue<_>
        [<ImportMember(Spec.path)>]
        static abstract stagger: value: (string * string) * parameters: obj -> FunctionValue<_>
        
    [<Interface; AllowNullLiteral>]
    type MotionPath =
        abstract member translateX: obj with get
        abstract member translateY: obj with get
        abstract member rotate: obj with get
        
    [<Import("svg", Spec.path)>]
    type Svg =
        static abstract morphTo: shapeTarget: U4<string, SVGPathElement, SVGPolylineElement, SVGPolygonElement> * precision: float -> string[]
        static abstract createDrawable: target: U5<string, SVGLineElement, SVGPathElement, SVGPolylineElement, SVGRectElement> -> obj[]
        static abstract createMotionPath: path: U2<string, SVGPathElement> -> MotionPath
    [<Import("waapi", Spec.path)>]
    type WAAPI =
        static abstract convertEase: EasingFun -> EasingFun
    module WAAPI =
        [<AllowNullLiteral; Interface>]
        type AnimationOptions =
            inherit AnimatableProperties.CssProperties
            inherit AnimatableProperties.CssTransforms
            inherit TweenParameters
            inherit PlaybackParametersWithFuncValues
            inherit TimerCallbacks<WAAPI.Animation>
        type Animation = interface end
        
[<AllowNullLiteral; Interface>]
type Exports =
    [<Import("createTimer", "animejs")>]
    static member createTimer (parameters: TimerOptions) : Timer = nativeOnly
    [<ImportMember(Spec.path)>]
    static member createTimeline (parameters: TimelineOptions): Timeline = nativeOnly
    [<Import("animate", "animejs")>]
    static member animate (targets: obj, parameters: AnimationOptions) : Animation = nativeOnly
    [<ImportMember "animejs">]
    static member stagger (target: obj, ?parameters: StaggerOptions) : FunctionValue<float> = nativeOnly
    [<Import("createAnimatable", "animejs")>]
    static member createAnimatable (targets: obj, parameters: AnimatableOptions) : Animatable = nativeOnly
    [<Import("createDraggable", "animejs")>]
    static member createDraggable (target: obj, ?parameters: DraggableOptions) : Draggable = nativeOnly
    [<Import("createSpring", "animejs"); ParamObject>]
    static member createSpring (?mass: float, ?stiffness: float, ?damping: float, ?velocity: float) : EasingFun = nativeOnly
    [<Import("createScope", "animejs")>]
    static member createScope (?``params``: ScopeOptions) : Scope = nativeOnly
    [<Import("onScroll", "animejs")>]
    static member inline onScroll (options: ScrollObserverOptions): ScrollObserver = jsNative 
