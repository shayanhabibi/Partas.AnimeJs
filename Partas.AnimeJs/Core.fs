module Partas.AnimeJs.Core

open Fable.Core
open Fable.Core.JsInterop
open Browser.Types
open Partas.Solid
open Partas.Solid.Experimental.U
open Partas.Solid.Style.Types.DataType

[<Erase>]
module Spec =
    let [<Literal; Erase>] path = "animejs"
    let [<Literal; Erase>] version = "4.0.2"

type internal Noop =
    static member noop: Noop = JS.undefined

[<AutoOpen; Erase>]
module Measurements =
    /// <summary>
    /// Converters:<br/>
    /// - convertSecondsToMs
    /// </summary>
    [<Measure>] type seconds
    /// <summary>
    /// Converters:<br/>
    /// - convertSecondsToMs
    /// </summary>
    [<Measure>] type s = seconds
    
    /// <summary>
    /// No helpers available for this measurement
    /// </summary>
    [<Measure>] type frames
    /// <summary>
    /// No helpers available for this measurement
    /// </summary>
    [<Measure>] type f = frames
    
    /// <summary>
    /// No helpers available for this measurement
    /// </summary>
    [<Measure>] type fps = frames/s
    
    /// <summary>
    /// Converters:<br/>
    /// - convertMsToSeconds
    /// </summary>
    [<Measure>] type milliseconds
    /// <summary>
    /// Converters:<br/>
    /// - convertMsToSeconds
    /// </summary>
    [<Measure>] type ms = milliseconds
    /// <summary>
    /// Render to string with Unit:<br/>
    /// - renderPx
    /// </summary>    
    [<Measure>] type pixels
    /// <summary>
    /// Render to string with Unit:<br/>
    /// - renderPx
    /// </summary>    
    [<Measure>] type px = pixels
    
    /// <summary>
    /// Render to string with Unit:<br/>
    /// - renderPerc
    /// </summary>    
    [<Measure>] type percentage
    /// <summary>
    /// Render to string with Unit:<br/>
    /// - renderPerc
    /// </summary>    
    [<Measure>] type perc = percentage
    
    /// <summary>
    /// Render to string with Unit:<br/>
    /// - renderRem
    /// </summary>    
    [<Measure>] type rem
    
    /// <summary>
    /// Render to string with Unit:<br/>
    /// - renderDeg<br/>
    /// Converters:<br/>
    /// - convertDegToRad
    /// </summary>    
    [<Measure>] type degrees
    /// <summary>
    /// Render to string with Unit:<br/>
    /// - renderDeg<br/>
    /// Converters:<br/>
    /// - convertDegToRad
    /// </summary>    
    [<Measure>] type deg = degrees
    
    /// <summary>
    /// Render to string with Unit:<br/>
    /// - renderRad<br/>
    /// Converters:<br/>
    /// - convertRadToDeg
    /// </summary>    
    [<Measure>] type radians
    /// <summary>
    /// Render to string with Unit:<br/>
    /// - renderRad<br/>
    /// Converters:<br/>
    /// - convertRadToDeg
    /// </summary>    
    [<Measure>] type rad = radians
    
    /// <summary>
    /// Render to string with Unit:<br/>
    /// - renderHex<br/>
    /// </summary>    
    [<Measure>] type colorhex
    /// <summary>
    /// Render to string with Unit:<br/>
    /// - renderHex<br/>
    /// </summary>    
    [<Measure>] type hex = colorhex
    
    [<AutoOpen; Erase>]
    type MeasurementConverters =
        static member msPerSecond: int<ms/s> = 1000<ms/s>
        static member convertSecondsToMs (s: int<s>): int<ms> = s * msPerSecond
        static member convertSecondsToMs (s: float<s>): float<ms> = s * unbox msPerSecond
        static member convertMsToSeconds (ms: int<ms>): int<ms> = (ms / unbox msPerSecond)
        static member convertMsToSeconds (ms: float<ms>): float<ms> = (ms / unbox msPerSecond)
        /// <summary>
        /// Utilises the <c>animejs</c> <c>utils.radToDeg</c> helper
        /// </summary>
        [<Import("utils.radToDeg", Spec.path)>]
        static member convertRadToDeg (rad: float<rad>): float<deg> = jsNative
        /// <summary>
        /// Utilises the <c>animejs</c> <c>utils.radToDeg</c> helper
        /// </summary>
        [<Import("utils.radToDeg", Spec.path)>]
        static member convertRadToDeg (rad: int<rad>): int<deg> = jsNative
        /// <summary>
        /// Utilises the <c>animejs</c> <c>utils.degToRad</c> helper
        /// </summary>
        [<Import("utils.degToRad", Spec.path)>]
        static member convertDegToRad (deg: float<deg>): float<rad> = jsNative
        /// <summary>
        /// Utilises the <c>animejs</c> <c>utils.degToRad</c> helper
        /// </summary>
        [<Import("utils.degToRad", Spec.path)>]
        static member convertDegToRad (deg: int<deg>): int<rad> = jsNative
    
    [<AutoOpen; Erase>]
    type RenderMeasurement =
        /// <summary>
        /// Renders a numerical with the <c>deg</c> measurement to a JS string with the
        /// unit suffix.
        /// </summary>
        /// <returns>"[value]deg"</returns>
        static member inline renderDeg (deg: float<deg>) = $"{deg}deg"
        /// <summary>
        /// Renders a numerical with the <c>deg</c> measurement to a JS string with the
        /// unit suffix.
        /// </summary>
        /// <returns>"[value]deg"</returns>
        static member inline renderDeg (deg: int<deg>) = $"{deg}deg"
        /// <summary>
        /// Renders a numerical with the <c>perc</c> measurement to a JS string with the
        /// unit suffix.
        /// </summary>
        /// <returns>"[value]%"</returns>
        static member inline renderPerc (perc: float<perc>) = $"{perc}%%"
        /// <summary>
        /// Renders a numerical with the <c>perc</c> measurement to a JS string with the
        /// unit suffix.
        /// </summary>
        /// <returns>"[value]%"</returns>
        static member inline renderPerc (perc: int<perc>) = $"{perc}%%"
        /// <summary>
        /// Renders a numerical with the <c>px</c> measurement to a JS string with the
        /// unit suffix.
        /// </summary>
        /// <returns>"[value]px"</returns>
        static member inline renderPx (px: float<px>) = $"{px}px"
        /// <summary>
        /// Renders a numerical with the <c>px</c> measurement to a JS string with the
        /// unit suffix.
        /// </summary>
        /// <returns>"[value]px"</returns>
        static member inline renderPx (px: int<px>) = $"{px}px"
        /// <summary>
        /// Renders a numerical with the <c>rem</c> measurement to a JS string with the
        /// unit suffix.
        /// </summary>
        /// <returns>"[value]rem"</returns>
        static member inline renderRem (rem: int<rem>) = $"{rem}rem"
        /// <summary>
        /// Renders a numerical with the <c>rem</c> measurement to a JS string with the
        /// unit suffix.
        /// </summary>
        /// <returns>"[value]rem"</returns>
        static member inline renderRem (rem: float<rem>) = $"{rem}rem"
        /// <summary>
        /// Renders a numerical with the <c>rad</c> measurement to a JS string with the
        /// unit suffix.
        /// </summary>
        /// <returns>"[value]rad"</returns>
        static member inline renderRad (rad: float<rad>) = $"{rad}rad"
        /// <summary>
        /// Renders a numerical with the <c>rad</c> measurement to a JS string with the
        /// unit suffix.
        /// </summary>
        /// <returns>"[value]rad"</returns>
        static member inline renderRad (rad: int<rad>) = $"{rad}rad"
        /// <summary>
        /// Renders a numerical with the <c>hex</c> measurement to a JS color string with the
        /// <c>#</c> prefix.
        /// </summary>
        /// <returns>"#[value]"</returns>
        static member inline renderHex (hex: int<hex>) = $"#{hex}"

[<Erase; AutoOpen>]
module Enums =
    [<StringEnum; RequireQualifiedAccess>]
    type TimeUnit =
        | S
        | Ms

    [<StringEnum>]
    type StaggerFrom =
        | First
        | Center
        | Last


    [<RequireQualifiedAccess>]
    [<StringEnum>]
    type Axis =
        | X
        | Y
        
    [<StringEnum; RequireQualifiedAccess>]
    type Composition =
        /// <summary>
        /// Replace and cancel the current running animation on the property.
        /// </summary>
        | Replace
        /// <summary>
        /// Do not replace the running animation. This means the previous animation will
        /// continue running if its duration is longer than the new animation. This mode can
        /// also offer better performance
        /// </summary>
        | None
        /// <summary>
        /// Creates an additive animation and blends its values with the running animation
        /// </summary>
        | Blend

    [<StringEnum>]
    type ObserverThreshold =
        /// Top Y value
        | Top
        /// Bottom Y value
        | Bottom
        /// Left X value
        | Left
        /// Right X value
        | Right
        /// Center X or Y value
        | Center
        /// Equivalent to Y-Top X-Left
        | Start
        /// Equivalent to Y-Bottom X-Right
        | End
        /// Minimum value possible to meet the etner or leave condition
        | Min
        /// Maximum value possible to meet the enter or leave condition
        | Max
        /// Alias for the 'shorthand' position scheme in animejs
        static member inline (+) (x: ObserverThreshold, y: ObserverThreshold) = $"{x} {y}"

[<AutoOpen; Erase>]
module Types =
    type Callback<'T> = 'T -> unit
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
        member inline this.find with get(): NodeList = this |> JsInterop.import "utils.$" Spec.path
    /// <summary>
    /// Target can either be a selector, an element, a list of elements, or a record/pojo
    /// </summary>
    and Target = U6<Selector, NodeList, SVGGElement, HTMLElement, SVGElement, obj>
    and Targets<'T> = 'T[]
    [<Erase>]
    type FunctionValue<'T> = delegate of target: Target * ?index: int * ?length: int -> 'T
    type FloatModifier = float -> float
    /// <summary>
    /// Acceptable shape of type that can have stagger applied
    /// </summary>
    type Staggerable = U6<string, float, int, int * int, float * float, string * string>
    /// <summary>
    /// Is an acceptable parameter for some Draggable options to modify the bounds of the draggable.<br/>
    /// Can use the <c>Bounds</c> record with the <c>.toTuple</c> member. Can also create a record using
    /// <c>Bounds.fromTuple</c>.
    /// </summary>
    [<Erase>]
    type DraggableBounds = DraggableBounds of top: float<px> * right: float<px> * bottom: float<px> * left: float<px> 
    /// <summary>
    /// Used when modifying the cursor appearance/style in draggables
    /// </summary>
    [<Global>]
    type DraggableCursor private (noop: unit -> unit) =
        [<Emit("$0")>]
        new(_: bool) = DraggableCursor(fun () -> ())
        [<ParamObject; Emit("$0")>]
        new(?onHover: string, ?onGrab: string) = DraggableCursor(fun () -> ())
        [<Emit("$0")>]
        new(_:unit -> DraggableCursor) = DraggableCursor(fun () -> ())
    /// <summary>
    /// Returned and set in some properties. Can use the <c>Coordinate</c> record with
    /// <c>.toPojo</c>. Can also create a record using <c>Coordinate.fromPojo</c> 
    /// </summary>
    [<Erase; AllowNullLiteral>]
    type JSCoordinatePojo =
        abstract member x: float<px> with get,set
        abstract member y: float<px> with get,set
    /// <summary>
    /// Returned and set in some properties. Can use the <c>Coordinate</c> record with
    /// <c>.toTuple</c>. Can also create a record using <c>Coordinate.fromTuple</c>
    /// </summary>
    [<Erase>]
    type JSCoordinate = JSCoordinate of x: float<px> * y: float<px>
    /// <summary>
    /// Returned and set in some properties. Can use the <c>CoordinateHistory</c> record with
    /// <c>.toTuple</c>. Can also create a record using <c>CoordinateHistory.fromTuple</c>
    /// </summary>
    [<Erase>]
    type JSCoordinateHistory = JSCoordinateHistory of x: float<px> * y: float<px> * prevX: float<px> * prevY: float<px> 
    
    /// FSharp wrapping type for use with the Draggable and others. Can compile to/from tuples and pojos using
    /// the static methods.
    type CoordinateHistory = {
        x: float<px>
        y: float<px>
        prevX: float<px>
        prevY: float<px>
    } with
        member inline this.toPojo = this |> toPlainJsObj
        member inline this.toTuple = JSCoordinateHistory (this.x,this.y,this.prevX,this.prevY)
        static member inline fromTuple (JSCoordinateHistory (x,y,prevX,prevY)) = { x = x; y = y; prevX = prevX; prevY = prevY }
        static member inline unsafeFromTuple value =
            value |> unbox<JSCoordinateHistory> |> CoordinateHistory.fromTuple

    /// FSharp wrapping type for use with the Draggable and others. Can compile to/from tuples and pojos using
    /// the static methods.
    type Coordinate = {
        x: float<px>
        y: float<px>
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
        static member inline fromTuple (JSCoordinate (x: float<px> , y: float<px>)) = { x = x; y = y }
        static member inline unsafeFromTuple (value: obj): Coordinate = {
            x = fst !!value
            y = snd !!value
        }
    
    /// FSharp wrapping type for use with the Draggable. Can compile to/from JSDraggableBounds
    type Bounds = {
        top: float<px>
        right: float<px>
        bottom: float<px>
        left: float<px>
    } with
        member inline this.toTuple: DraggableBounds = DraggableBounds(this.top,this.right,this.bottom,this.left)
        static member inline fromTuple (DraggableBounds (top,right,bottom,left)) = { top = top; right = right; bottom = bottom; left = left }
        static member inline unsafeFromTuple value = !!value |> Bounds.fromTuple

    /// <summary>
    /// Tween Value operators for type based composition.<br/> Available operators: <code>
    /// !+= [ float | int | string ] // "+={value}"
    /// !-= [ float | int | string ] // "-={value}"
    /// !*= [ float | int | string ] // "*={value}"
    /// </code>
    /// </summary>
    [<AutoOpen; Erase>]
    type RelativeTweenValue =
        /// <summary>
        /// Helper to render a numeric with the prefix operator (anything following the <c>!</c>)
        /// </summary>
        static member inline (!+=) (value: int): RelativeTweenValue = unbox $"+={value}"
        /// <summary>
        /// Helper to render a numeric with the prefix operator (anything following the <c>!</c>)
        /// </summary>
        static member inline (!+=) (value: float): RelativeTweenValue = unbox $"+={value}"
        /// <summary>
        /// Helper to render a numeric with the prefix operator (anything following the <c>!</c>)
        /// </summary>
        static member inline (!+=) (value: string): RelativeTweenValue = unbox $"+={value}"
        /// <summary>
        /// Helper to render a numeric with the prefix operator (anything following the <c>!</c>)
        /// </summary>
        static member inline (!-=) (value: int): RelativeTweenValue = unbox $"-={value}"
        /// <summary>
        /// Helper to render a numeric with the prefix operator (anything following the <c>!</c>)
        /// </summary>
        static member inline (!-=) (value: float): RelativeTweenValue = unbox $"-={value}"
        /// <summary>
        /// Helper to render a numeric with the prefix operator (anything following the <c>!</c>)
        /// </summary>
        static member inline (!-=) (value: string): RelativeTweenValue = unbox $"-={value}"
        /// <summary>
        /// Helper to render a numeric with the prefix operator (anything following the <c>!</c>)
        /// </summary>
        static member inline (!*=) (value: int): RelativeTweenValue = unbox $"*={value}"
        /// <summary>
        /// Helper to render a numeric with the prefix operator (anything following the <c>!</c>)
        /// </summary>
        static member inline (!*=) (value: float): RelativeTweenValue = unbox $"*={value}"
        /// <summary>
        /// Helper to render a numeric with the prefix operator (anything following the <c>!</c>)
        /// </summary>
        static member inline (!*=) (value: string): RelativeTweenValue = unbox $"*={value}"

    [<AutoOpen; Erase>]
    type RelativeTimePosition =
        /// <summary>
        /// Compiles the <c>"&lt;"</c> enum in JS.
        /// </summary>
        static member inline (!<): RelativeTimePosition = unbox "<"
        /// <summary>
        /// Compiles the <c>"&lt;&lt;"</c> enum in JS.
        /// </summary>
        static member inline (!<<): RelativeTimePosition = unbox "<<"
        /// <summary>
        /// Helper to render a numeric with the prefix operator (anything following the <c>!</c>)
        /// </summary>
        static member inline (!<<+=) value: RelativeTimePosition = unbox $"<<+={value}"
        /// <summary>
        /// Helper to render a numeric with the prefix operator (anything following the <c>!</c>)
        /// </summary>
        static member inline (!<<-=) value: RelativeTimePosition = unbox $"<<-={value}"
        /// <summary>
        /// Helper to render a numeric with the prefix operator (anything following the <c>!</c>)
        /// </summary>
        static member inline (!<<*=) value: RelativeTimePosition = unbox $"<<*={value}"

    /// <summary>
    /// Used in the ScrollObserver to set the threshold for animations to active/stop.
    /// </summary>
    type ScrollObserverThreshold private (noop: char) =
        [<Emit("$0")>]
        new(_:int<px>) = ScrollObserverThreshold(' ')
        [<Emit("$0")>]
        new(_:float<px>) = ScrollObserverThreshold(' ')
        [<Emit("$0")>]
        new(_: ObserverThreshold) = ScrollObserverThreshold(' ')
        [<Emit("$0")>]
        new(_: string) = ScrollObserverThreshold(' ')
        [<Emit("$0")>]
        new(_: RelativeTweenValue) = ScrollObserverThreshold(' ')
        [<Emit("$0"); ParamObject>]
        new(target: U5<string, int, float, ObserverThreshold, RelativeTweenValue>, container: U5<string, int, float, ObserverThreshold, RelativeTweenValue>) = ScrollObserverThreshold(' ')

    /// <summary>
    /// Provides stronger typing
    /// </summary>
    [<Global>]
    type TweenValue private (v: Noop) =
        [<Emit("$0")>]
        new(_: int) = TweenValue(Noop.noop)
        [<Emit("$0")>]
        new(_: float) = TweenValue(Noop.noop)
        [<Emit("$0")>]
        new(_: RelativeTweenValue) = TweenValue(Noop.noop)
        [<Emit("$0")>]
        new(_: FunctionValue<int>) = TweenValue(Noop.noop)
        [<Emit("$0")>]
        new(_: FunctionValue<float>) = TweenValue(Noop.noop)
        [<Emit("$0")>]
        new(_: FunctionValue<string>) = TweenValue(Noop.noop)
        [<ParamObject; Emit("$0")>]
        new(
            ?``to``: TweenValue,
            ?``from``: TweenValue,
            ?delay: U2<int<ms>, FunctionValue<int<ms>>>,
            ?duration: U2<int<ms>, FunctionValue<int<ms>>>,
            ?ease: EasingFunction,
            ?composition: Composition,
            ?modifier: FloatModifier
            ) = TweenValue(Noop.noop)
        [<Emit("$0")>]
        new(_: TweenValue * TweenValue) = TweenValue(Noop.noop)
        // TODO - Duration based keyframes
        // [<Emit("$0")>]
        // new() = TweenValue(Noop.noop)
        // TODO - Percentage based keyframes
        // [<Emit("$0")>]
        // new(_: string * TweenValue) = TweenValue(Noop.noop)
