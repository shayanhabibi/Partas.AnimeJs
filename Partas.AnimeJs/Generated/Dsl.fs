// Generated from Xantham declarations and the reviewed DSL policy.
namespace Partas.AnimeJs

open Fable.Core
open Fable.Core.JsInterop
open Fable.Core.JS

type AnimationContext = private AnimationContext of unit
type TimerContext = private TimerContext of unit
type TimelineContext = private TimelineContext of unit
type StaggerContext = private StaggerContext of unit
type SpringContext = private SpringContext of unit
type AnimatableContext = private AnimatableContext of unit
type DraggableContext = private DraggableContext of unit
type ScrollContext = private ScrollContext of unit
type ScopeContext = private ScopeContext of unit
type TextSplitterContext = private TextSplitterContext of unit
type DefaultsContext = private DefaultsContext of unit
type DurationKeyframeContext = private DurationKeyframeContext of unit
type PercentageKeyframeContext = private PercentageKeyframeContext of unit
type DragAxisContext = private DragAxisContext of unit
type DragCursorContext = private DragCursorContext of unit
type DragThresholdContext = private DragThresholdContext of unit
type AnimatablePropertyContext = private AnimatablePropertyContext of unit
type ScrollThresholdContext = private ScrollThresholdContext of unit
type SplitTemplateContext = private SplitTemplateContext of unit
type PercentageStop = private PercentageStop of string * Options<PercentageKeyframeContext>

[<AutoOpen>]
module Builders =
    let ``animation`` = OptionsBuilder<AnimationContext>()
    let ``timer`` = OptionsBuilder<TimerContext>()
    let ``timeline`` = OptionsBuilder<TimelineContext>()
    let ``stagger`` = OptionsBuilder<StaggerContext>()
    let ``spring`` = OptionsBuilder<SpringContext>()
    let ``animatable`` = OptionsBuilder<AnimatableContext>()
    let ``draggable`` = OptionsBuilder<DraggableContext>()
    let ``scroll`` = OptionsBuilder<ScrollContext>()
    let ``scope`` = OptionsBuilder<ScopeContext>()
    let ``textSplitter`` = OptionsBuilder<TextSplitterContext>()
    let ``defaults`` = OptionsBuilder<DefaultsContext>()
    let ``durationKeyframe`` = OptionsBuilder<DurationKeyframeContext>()
    let ``percentageKeyframe`` = OptionsBuilder<PercentageKeyframeContext>()
    let ``dragAxis`` = OptionsBuilder<DragAxisContext>()
    let ``dragCursor`` = OptionsBuilder<DragCursorContext>()
    let ``dragThreshold`` = OptionsBuilder<DragThresholdContext>()
    let ``animatableProperty`` = OptionsBuilder<AnimatablePropertyContext>()
    let ``scrollThreshold`` = OptionsBuilder<ScrollThresholdContext>()
    let ``splitTemplate`` = OptionsBuilder<SplitTemplateContext>()

[<AbstractClass; Sealed>]
type ``Animation`` private () =
    static member ``id`` (value: U2<string, float>) : Field<AnimationContext> = Field.create<AnimationContext> "id" (box (value))
    static member ``id`` (value: string) : Field<AnimationContext> = Field.create<AnimationContext> "id" (box (U2.Case1 value))
    static member ``id`` (value: float) : Field<AnimationContext> = Field.create<AnimationContext> "id" (box (U2.Case2 value))
    static member ``duration`` (value: U6<string, float, (float -> float), Raw.DurationKeyframes.Item.Duration, Raw.Spring, Raw.TweakRegister>) : Field<AnimationContext> = Field.create<AnimationContext> "duration" (box (value))
    static member ``duration`` (value: string) : Field<AnimationContext> = Field.create<AnimationContext> "duration" (box (U6.Case1 value))
    static member ``duration`` (value: float) : Field<AnimationContext> = Field.create<AnimationContext> "duration" (box (U6.Case2 value))
    static member ``duration`` (value: (float -> float)) : Field<AnimationContext> = Field.create<AnimationContext> "duration" (box (U6.Case3 value))
    static member ``duration`` (value: Raw.DurationKeyframes.Item.Duration) : Field<AnimationContext> = Field.create<AnimationContext> "duration" (box (U6.Case4 value))
    static member ``duration`` (value: Raw.Spring) : Field<AnimationContext> = Field.create<AnimationContext> "duration" (box (U6.Case5 value))
    static member ``duration`` (value: Raw.TweakRegister) : Field<AnimationContext> = Field.create<AnimationContext> "duration" (box (U6.Case6 value))
    static member ``delay`` (value: U6<string, float, (float -> float), Raw.DurationKeyframes.Item.Duration, Raw.Spring, Raw.TweakRegister>) : Field<AnimationContext> = Field.create<AnimationContext> "delay" (box (value))
    static member ``delay`` (value: string) : Field<AnimationContext> = Field.create<AnimationContext> "delay" (box (U6.Case1 value))
    static member ``delay`` (value: float) : Field<AnimationContext> = Field.create<AnimationContext> "delay" (box (U6.Case2 value))
    static member ``delay`` (value: (float -> float)) : Field<AnimationContext> = Field.create<AnimationContext> "delay" (box (U6.Case3 value))
    static member ``delay`` (value: Raw.DurationKeyframes.Item.Duration) : Field<AnimationContext> = Field.create<AnimationContext> "delay" (box (U6.Case4 value))
    static member ``delay`` (value: Raw.Spring) : Field<AnimationContext> = Field.create<AnimationContext> "delay" (box (U6.Case5 value))
    static member ``delay`` (value: Raw.TweakRegister) : Field<AnimationContext> = Field.create<AnimationContext> "delay" (box (U6.Case6 value))
    static member ``loopDelay`` (value: float) : Field<AnimationContext> = Field.create<AnimationContext> "loopDelay" (box (value))
    static member ``reversed`` (value: bool) : Field<AnimationContext> = Field.create<AnimationContext> "reversed" (box (value))
    static member ``alternate`` (value: bool) : Field<AnimationContext> = Field.create<AnimationContext> "alternate" (box (value))
    static member ``loop`` (value: U2<float, bool>) : Field<AnimationContext> = Field.create<AnimationContext> "loop" (box (value))
    static member ``loop`` (value: float) : Field<AnimationContext> = Field.create<AnimationContext> "loop" (box (U2.Case1 value))
    static member ``loop`` (value: bool) : Field<AnimationContext> = Field.create<AnimationContext> "loop" (box (U2.Case2 value))
    static member ``autoplay`` (value: U2<bool, Raw.ScrollObserver>) : Field<AnimationContext> = Field.create<AnimationContext> "autoplay" (box (value))
    static member ``autoplay`` (value: bool) : Field<AnimationContext> = Field.create<AnimationContext> "autoplay" (box (U2.Case1 value))
    static member ``autoplay`` (value: Raw.ScrollObserver) : Field<AnimationContext> = Field.create<AnimationContext> "autoplay" (box (U2.Case2 value))
    static member ``frameRate`` (value: float) : Field<AnimationContext> = Field.create<AnimationContext> "frameRate" (box (value))
    static member ``playbackRate`` (value: float) : Field<AnimationContext> = Field.create<AnimationContext> "playbackRate" (box (value))
    static member ``priority`` (value: float) : Field<AnimationContext> = Field.create<AnimationContext> "priority" (box (value))
    static member ``keyframes`` (value: U2<(Raw.DurationKeyframes.Item)[], Fable.Core.JS.JS.Record<string, Raw.PercentageKeyframes.Item>>) : Field<AnimationContext> = Field.create<AnimationContext> "keyframes" (box (value))
    static member ``keyframes`` (value: (Raw.DurationKeyframes.Item)[]) : Field<AnimationContext> = Field.create<AnimationContext> "keyframes" (box (U2.Case1 value))
    static member ``keyframes`` (value: Fable.Core.JS.JS.Record<string, Raw.PercentageKeyframes.Item>) : Field<AnimationContext> = Field.create<AnimationContext> "keyframes" (box (U2.Case2 value))
    static member ``playbackEase`` (value: U4<string, (float -> float), Raw.Spring, Raw.TweakRegister>) : Field<AnimationContext> = Field.create<AnimationContext> "playbackEase" (box (value))
    static member ``playbackEase`` (value: string) : Field<AnimationContext> = Field.create<AnimationContext> "playbackEase" (box (U4.Case1 value))
    static member ``playbackEase`` (value: (float -> float)) : Field<AnimationContext> = Field.create<AnimationContext> "playbackEase" (box (U4.Case2 value))
    static member ``playbackEase`` (value: Raw.Spring) : Field<AnimationContext> = Field.create<AnimationContext> "playbackEase" (box (U4.Case3 value))
    static member ``playbackEase`` (value: Raw.TweakRegister) : Field<AnimationContext> = Field.create<AnimationContext> "playbackEase" (box (U4.Case4 value))
    static member ``ease`` (value: U5<string, (float -> float), Raw.DurationKeyframes.Item.Duration, Raw.Spring, Raw.TweakRegister>) : Field<AnimationContext> = Field.create<AnimationContext> "ease" (box (value))
    static member ``ease`` (value: string) : Field<AnimationContext> = Field.create<AnimationContext> "ease" (box (U5.Case1 value))
    static member ``ease`` (value: (float -> float)) : Field<AnimationContext> = Field.create<AnimationContext> "ease" (box (U5.Case2 value))
    static member ``ease`` (value: Raw.DurationKeyframes.Item.Duration) : Field<AnimationContext> = Field.create<AnimationContext> "ease" (box (U5.Case3 value))
    static member ``ease`` (value: Raw.Spring) : Field<AnimationContext> = Field.create<AnimationContext> "ease" (box (U5.Case4 value))
    static member ``ease`` (value: Raw.TweakRegister) : Field<AnimationContext> = Field.create<AnimationContext> "ease" (box (U5.Case5 value))
    static member ``modifier`` (value: (float -> U2<string, float>)) : Field<AnimationContext> = Field.create<AnimationContext> "modifier" (box (value))
    static member ``composition`` (value: U2<float, string>) : Field<AnimationContext> = Field.create<AnimationContext> "composition" (box (value))
    static member ``composition`` (value: float) : Field<AnimationContext> = Field.create<AnimationContext> "composition" (box (U2.Case1 value))
    static member ``composition`` (value: string) : Field<AnimationContext> = Field.create<AnimationContext> "composition" (box (U2.Case2 value))
    static member ``onBeginValue`` (value: (Raw.JSAnimation -> obj)) : Field<AnimationContext> = Field.create<AnimationContext> "onBegin" (box (value))
    static member ``onBegin`` (value: (Raw.JSAnimation -> unit)) : Field<AnimationContext> =
        let callback (handle: Raw.JSAnimation) : obj = value handle; box ()
        Field.create<AnimationContext> "onBegin" (box (callback))
    static member ``onBeforeUpdateValue`` (value: (Raw.JSAnimation -> obj)) : Field<AnimationContext> = Field.create<AnimationContext> "onBeforeUpdate" (box (value))
    static member ``onBeforeUpdate`` (value: (Raw.JSAnimation -> unit)) : Field<AnimationContext> =
        let callback (handle: Raw.JSAnimation) : obj = value handle; box ()
        Field.create<AnimationContext> "onBeforeUpdate" (box (callback))
    static member ``onUpdateValue`` (value: (Raw.JSAnimation -> obj)) : Field<AnimationContext> = Field.create<AnimationContext> "onUpdate" (box (value))
    static member ``onUpdate`` (value: (Raw.JSAnimation -> unit)) : Field<AnimationContext> =
        let callback (handle: Raw.JSAnimation) : obj = value handle; box ()
        Field.create<AnimationContext> "onUpdate" (box (callback))
    static member ``onLoopValue`` (value: (Raw.JSAnimation -> obj)) : Field<AnimationContext> = Field.create<AnimationContext> "onLoop" (box (value))
    static member ``onLoop`` (value: (Raw.JSAnimation -> unit)) : Field<AnimationContext> =
        let callback (handle: Raw.JSAnimation) : obj = value handle; box ()
        Field.create<AnimationContext> "onLoop" (box (callback))
    static member ``onPauseValue`` (value: (Raw.JSAnimation -> obj)) : Field<AnimationContext> = Field.create<AnimationContext> "onPause" (box (value))
    static member ``onPause`` (value: (Raw.JSAnimation -> unit)) : Field<AnimationContext> =
        let callback (handle: Raw.JSAnimation) : obj = value handle; box ()
        Field.create<AnimationContext> "onPause" (box (callback))
    static member ``onCompleteValue`` (value: (Raw.JSAnimation -> obj)) : Field<AnimationContext> = Field.create<AnimationContext> "onComplete" (box (value))
    static member ``onComplete`` (value: (Raw.JSAnimation -> unit)) : Field<AnimationContext> =
        let callback (handle: Raw.JSAnimation) : obj = value handle; box ()
        Field.create<AnimationContext> "onComplete" (box (callback))
    static member ``onRenderValue`` (value: (Raw.JSAnimation -> obj)) : Field<AnimationContext> = Field.create<AnimationContext> "onRender" (box (value))
    static member ``onRender`` (value: (Raw.JSAnimation -> unit)) : Field<AnimationContext> =
        let callback (handle: Raw.JSAnimation) : obj = value handle; box ()
        Field.create<AnimationContext> "onRender" (box (callback))
    static member parameters (options: Options<AnimationContext>) : Raw.AnimationParams = Options.toObject options |> unbox
    /// Animate a dynamic object property. The key does not prove target-property existence.
    static member property (key: string) (value: Raw.TweenOptions) : Field<AnimationContext> =
        if System.String.IsNullOrWhiteSpace key || List.contains key ["alternate"; "autoplay"; "composition"; "delay"; "duration"; "ease"; "frameRate"; "id"; "keyframes"; "loop"; "loopDelay"; "modifier"; "onBeforeUpdate"; "onBegin"; "onComplete"; "onLoop"; "onPause"; "onRender"; "onUpdate"; "playbackEase"; "playbackRate"; "priority"; "reversed"; "__proto__"; "constructor"; "prototype"] then invalidArg "key" "Custom keys must be nonempty and cannot name an option."
        Field.create<AnimationContext> key (box value)

[<AbstractClass; Sealed>]
type ``Timer`` private () =
    static member ``id`` (value: U2<string, float>) : Field<TimerContext> = Field.create<TimerContext> "id" (box (value))
    static member ``id`` (value: string) : Field<TimerContext> = Field.create<TimerContext> "id" (box (U2.Case1 value))
    static member ``id`` (value: float) : Field<TimerContext> = Field.create<TimerContext> "id" (box (U2.Case2 value))
    static member ``duration`` (value: U6<string, float, (float -> float), Raw.DurationKeyframes.Item.Duration, Raw.Spring, Raw.TweakRegister>) : Field<TimerContext> = Field.create<TimerContext> "duration" (box (value))
    static member ``duration`` (value: string) : Field<TimerContext> = Field.create<TimerContext> "duration" (box (U6.Case1 value))
    static member ``duration`` (value: float) : Field<TimerContext> = Field.create<TimerContext> "duration" (box (U6.Case2 value))
    static member ``duration`` (value: (float -> float)) : Field<TimerContext> = Field.create<TimerContext> "duration" (box (U6.Case3 value))
    static member ``duration`` (value: Raw.DurationKeyframes.Item.Duration) : Field<TimerContext> = Field.create<TimerContext> "duration" (box (U6.Case4 value))
    static member ``duration`` (value: Raw.Spring) : Field<TimerContext> = Field.create<TimerContext> "duration" (box (U6.Case5 value))
    static member ``duration`` (value: Raw.TweakRegister) : Field<TimerContext> = Field.create<TimerContext> "duration" (box (U6.Case6 value))
    static member ``delay`` (value: U6<string, float, (float -> float), Raw.DurationKeyframes.Item.Duration, Raw.Spring, Raw.TweakRegister>) : Field<TimerContext> = Field.create<TimerContext> "delay" (box (value))
    static member ``delay`` (value: string) : Field<TimerContext> = Field.create<TimerContext> "delay" (box (U6.Case1 value))
    static member ``delay`` (value: float) : Field<TimerContext> = Field.create<TimerContext> "delay" (box (U6.Case2 value))
    static member ``delay`` (value: (float -> float)) : Field<TimerContext> = Field.create<TimerContext> "delay" (box (U6.Case3 value))
    static member ``delay`` (value: Raw.DurationKeyframes.Item.Duration) : Field<TimerContext> = Field.create<TimerContext> "delay" (box (U6.Case4 value))
    static member ``delay`` (value: Raw.Spring) : Field<TimerContext> = Field.create<TimerContext> "delay" (box (U6.Case5 value))
    static member ``delay`` (value: Raw.TweakRegister) : Field<TimerContext> = Field.create<TimerContext> "delay" (box (U6.Case6 value))
    static member ``loopDelay`` (value: float) : Field<TimerContext> = Field.create<TimerContext> "loopDelay" (box (value))
    static member ``reversed`` (value: bool) : Field<TimerContext> = Field.create<TimerContext> "reversed" (box (value))
    static member ``alternate`` (value: bool) : Field<TimerContext> = Field.create<TimerContext> "alternate" (box (value))
    static member ``loop`` (value: U2<float, bool>) : Field<TimerContext> = Field.create<TimerContext> "loop" (box (value))
    static member ``loop`` (value: float) : Field<TimerContext> = Field.create<TimerContext> "loop" (box (U2.Case1 value))
    static member ``loop`` (value: bool) : Field<TimerContext> = Field.create<TimerContext> "loop" (box (U2.Case2 value))
    static member ``autoplay`` (value: U2<bool, Raw.ScrollObserver>) : Field<TimerContext> = Field.create<TimerContext> "autoplay" (box (value))
    static member ``autoplay`` (value: bool) : Field<TimerContext> = Field.create<TimerContext> "autoplay" (box (U2.Case1 value))
    static member ``autoplay`` (value: Raw.ScrollObserver) : Field<TimerContext> = Field.create<TimerContext> "autoplay" (box (U2.Case2 value))
    static member ``frameRate`` (value: float) : Field<TimerContext> = Field.create<TimerContext> "frameRate" (box (value))
    static member ``playbackRate`` (value: float) : Field<TimerContext> = Field.create<TimerContext> "playbackRate" (box (value))
    static member ``priority`` (value: float) : Field<TimerContext> = Field.create<TimerContext> "priority" (box (value))
    static member ``onBeginValue`` (value: (Raw.Timer -> obj)) : Field<TimerContext> = Field.create<TimerContext> "onBegin" (box (value))
    static member ``onBegin`` (value: (Raw.Timer -> unit)) : Field<TimerContext> =
        let callback (handle: Raw.Timer) : obj = value handle; box ()
        Field.create<TimerContext> "onBegin" (box (callback))
    static member ``onBeforeUpdateValue`` (value: (Raw.Timer -> obj)) : Field<TimerContext> = Field.create<TimerContext> "onBeforeUpdate" (box (value))
    static member ``onBeforeUpdate`` (value: (Raw.Timer -> unit)) : Field<TimerContext> =
        let callback (handle: Raw.Timer) : obj = value handle; box ()
        Field.create<TimerContext> "onBeforeUpdate" (box (callback))
    static member ``onUpdateValue`` (value: (Raw.Timer -> obj)) : Field<TimerContext> = Field.create<TimerContext> "onUpdate" (box (value))
    static member ``onUpdate`` (value: (Raw.Timer -> unit)) : Field<TimerContext> =
        let callback (handle: Raw.Timer) : obj = value handle; box ()
        Field.create<TimerContext> "onUpdate" (box (callback))
    static member ``onLoopValue`` (value: (Raw.Timer -> obj)) : Field<TimerContext> = Field.create<TimerContext> "onLoop" (box (value))
    static member ``onLoop`` (value: (Raw.Timer -> unit)) : Field<TimerContext> =
        let callback (handle: Raw.Timer) : obj = value handle; box ()
        Field.create<TimerContext> "onLoop" (box (callback))
    static member ``onPauseValue`` (value: (Raw.Timer -> obj)) : Field<TimerContext> = Field.create<TimerContext> "onPause" (box (value))
    static member ``onPause`` (value: (Raw.Timer -> unit)) : Field<TimerContext> =
        let callback (handle: Raw.Timer) : obj = value handle; box ()
        Field.create<TimerContext> "onPause" (box (callback))
    static member ``onCompleteValue`` (value: (Raw.Timer -> obj)) : Field<TimerContext> = Field.create<TimerContext> "onComplete" (box (value))
    static member ``onComplete`` (value: (Raw.Timer -> unit)) : Field<TimerContext> =
        let callback (handle: Raw.Timer) : obj = value handle; box ()
        Field.create<TimerContext> "onComplete" (box (callback))
    static member parameters (options: Options<TimerContext>) : Raw.TimerParams = Options.toObject options |> unbox

[<AbstractClass; Sealed>]
type ``Timeline`` private () =
    static member ``id`` (value: U2<string, float>) : Field<TimelineContext> = Field.create<TimelineContext> "id" (box (value))
    static member ``id`` (value: string) : Field<TimelineContext> = Field.create<TimelineContext> "id" (box (U2.Case1 value))
    static member ``id`` (value: float) : Field<TimelineContext> = Field.create<TimelineContext> "id" (box (U2.Case2 value))
    static member ``duration`` (value: U6<string, float, (float -> float), Raw.DurationKeyframes.Item.Duration, Raw.Spring, Raw.TweakRegister>) : Field<TimelineContext> = Field.create<TimelineContext> "duration" (box (value))
    static member ``duration`` (value: string) : Field<TimelineContext> = Field.create<TimelineContext> "duration" (box (U6.Case1 value))
    static member ``duration`` (value: float) : Field<TimelineContext> = Field.create<TimelineContext> "duration" (box (U6.Case2 value))
    static member ``duration`` (value: (float -> float)) : Field<TimelineContext> = Field.create<TimelineContext> "duration" (box (U6.Case3 value))
    static member ``duration`` (value: Raw.DurationKeyframes.Item.Duration) : Field<TimelineContext> = Field.create<TimelineContext> "duration" (box (U6.Case4 value))
    static member ``duration`` (value: Raw.Spring) : Field<TimelineContext> = Field.create<TimelineContext> "duration" (box (U6.Case5 value))
    static member ``duration`` (value: Raw.TweakRegister) : Field<TimelineContext> = Field.create<TimelineContext> "duration" (box (U6.Case6 value))
    static member ``delay`` (value: U6<string, float, (float -> float), Raw.DurationKeyframes.Item.Duration, Raw.Spring, Raw.TweakRegister>) : Field<TimelineContext> = Field.create<TimelineContext> "delay" (box (value))
    static member ``delay`` (value: string) : Field<TimelineContext> = Field.create<TimelineContext> "delay" (box (U6.Case1 value))
    static member ``delay`` (value: float) : Field<TimelineContext> = Field.create<TimelineContext> "delay" (box (U6.Case2 value))
    static member ``delay`` (value: (float -> float)) : Field<TimelineContext> = Field.create<TimelineContext> "delay" (box (U6.Case3 value))
    static member ``delay`` (value: Raw.DurationKeyframes.Item.Duration) : Field<TimelineContext> = Field.create<TimelineContext> "delay" (box (U6.Case4 value))
    static member ``delay`` (value: Raw.Spring) : Field<TimelineContext> = Field.create<TimelineContext> "delay" (box (U6.Case5 value))
    static member ``delay`` (value: Raw.TweakRegister) : Field<TimelineContext> = Field.create<TimelineContext> "delay" (box (U6.Case6 value))
    static member ``loopDelay`` (value: float) : Field<TimelineContext> = Field.create<TimelineContext> "loopDelay" (box (value))
    static member ``reversed`` (value: bool) : Field<TimelineContext> = Field.create<TimelineContext> "reversed" (box (value))
    static member ``alternate`` (value: bool) : Field<TimelineContext> = Field.create<TimelineContext> "alternate" (box (value))
    static member ``loop`` (value: U2<float, bool>) : Field<TimelineContext> = Field.create<TimelineContext> "loop" (box (value))
    static member ``loop`` (value: float) : Field<TimelineContext> = Field.create<TimelineContext> "loop" (box (U2.Case1 value))
    static member ``loop`` (value: bool) : Field<TimelineContext> = Field.create<TimelineContext> "loop" (box (U2.Case2 value))
    static member ``autoplay`` (value: U2<bool, Raw.ScrollObserver>) : Field<TimelineContext> = Field.create<TimelineContext> "autoplay" (box (value))
    static member ``autoplay`` (value: bool) : Field<TimelineContext> = Field.create<TimelineContext> "autoplay" (box (U2.Case1 value))
    static member ``autoplay`` (value: Raw.ScrollObserver) : Field<TimelineContext> = Field.create<TimelineContext> "autoplay" (box (U2.Case2 value))
    static member ``frameRate`` (value: float) : Field<TimelineContext> = Field.create<TimelineContext> "frameRate" (box (value))
    static member ``playbackRate`` (value: float) : Field<TimelineContext> = Field.create<TimelineContext> "playbackRate" (box (value))
    static member ``priority`` (value: float) : Field<TimelineContext> = Field.create<TimelineContext> "priority" (box (value))
    static member ``defaults`` (value: Raw.DefaultsParams) : Field<TimelineContext> = Field.create<TimelineContext> "defaults" (box (value))
    static member ``playbackEase`` (value: U4<string, (float -> float), Raw.Spring, Raw.TweakRegister>) : Field<TimelineContext> = Field.create<TimelineContext> "playbackEase" (box (value))
    static member ``playbackEase`` (value: string) : Field<TimelineContext> = Field.create<TimelineContext> "playbackEase" (box (U4.Case1 value))
    static member ``playbackEase`` (value: (float -> float)) : Field<TimelineContext> = Field.create<TimelineContext> "playbackEase" (box (U4.Case2 value))
    static member ``playbackEase`` (value: Raw.Spring) : Field<TimelineContext> = Field.create<TimelineContext> "playbackEase" (box (U4.Case3 value))
    static member ``playbackEase`` (value: Raw.TweakRegister) : Field<TimelineContext> = Field.create<TimelineContext> "playbackEase" (box (U4.Case4 value))
    static member ``composition`` (value: bool) : Field<TimelineContext> = Field.create<TimelineContext> "composition" (box (value))
    static member ``onBeginValue`` (value: (Raw.Timeline -> obj)) : Field<TimelineContext> = Field.create<TimelineContext> "onBegin" (box (value))
    static member ``onBegin`` (value: (Raw.Timeline -> unit)) : Field<TimelineContext> =
        let callback (handle: Raw.Timeline) : obj = value handle; box ()
        Field.create<TimelineContext> "onBegin" (box (callback))
    static member ``onBeforeUpdateValue`` (value: (Raw.Timeline -> obj)) : Field<TimelineContext> = Field.create<TimelineContext> "onBeforeUpdate" (box (value))
    static member ``onBeforeUpdate`` (value: (Raw.Timeline -> unit)) : Field<TimelineContext> =
        let callback (handle: Raw.Timeline) : obj = value handle; box ()
        Field.create<TimelineContext> "onBeforeUpdate" (box (callback))
    static member ``onUpdateValue`` (value: (Raw.Timeline -> obj)) : Field<TimelineContext> = Field.create<TimelineContext> "onUpdate" (box (value))
    static member ``onUpdate`` (value: (Raw.Timeline -> unit)) : Field<TimelineContext> =
        let callback (handle: Raw.Timeline) : obj = value handle; box ()
        Field.create<TimelineContext> "onUpdate" (box (callback))
    static member ``onLoopValue`` (value: (Raw.Timeline -> obj)) : Field<TimelineContext> = Field.create<TimelineContext> "onLoop" (box (value))
    static member ``onLoop`` (value: (Raw.Timeline -> unit)) : Field<TimelineContext> =
        let callback (handle: Raw.Timeline) : obj = value handle; box ()
        Field.create<TimelineContext> "onLoop" (box (callback))
    static member ``onPauseValue`` (value: (Raw.Timeline -> obj)) : Field<TimelineContext> = Field.create<TimelineContext> "onPause" (box (value))
    static member ``onPause`` (value: (Raw.Timeline -> unit)) : Field<TimelineContext> =
        let callback (handle: Raw.Timeline) : obj = value handle; box ()
        Field.create<TimelineContext> "onPause" (box (callback))
    static member ``onCompleteValue`` (value: (Raw.Timeline -> obj)) : Field<TimelineContext> = Field.create<TimelineContext> "onComplete" (box (value))
    static member ``onComplete`` (value: (Raw.Timeline -> unit)) : Field<TimelineContext> =
        let callback (handle: Raw.Timeline) : obj = value handle; box ()
        Field.create<TimelineContext> "onComplete" (box (callback))
    static member ``onRenderValue`` (value: (Raw.Timeline -> obj)) : Field<TimelineContext> = Field.create<TimelineContext> "onRender" (box (value))
    static member ``onRender`` (value: (Raw.Timeline -> unit)) : Field<TimelineContext> =
        let callback (handle: Raw.Timeline) : obj = value handle; box ()
        Field.create<TimelineContext> "onRender" (box (callback))
    static member parameters (options: Options<TimelineContext>) : Raw.TimelineParams = Options.toObject options |> unbox

[<AbstractClass; Sealed>]
type ``Stagger`` private () =
    static member ``start`` (value: U2<string, float>) : Field<StaggerContext> = Field.create<StaggerContext> "start" (box (value))
    static member ``start`` (value: string) : Field<StaggerContext> = Field.create<StaggerContext> "start" (box (U2.Case1 value))
    static member ``start`` (value: float) : Field<StaggerContext> = Field.create<StaggerContext> "start" (box (U2.Case2 value))
    static member ``from`` (value: U3<float, string, (float)[]>) : Field<StaggerContext> = Field.create<StaggerContext> "from" (box (value))
    static member ``from`` (value: float) : Field<StaggerContext> = Field.create<StaggerContext> "from" (box (U3.Case1 value))
    static member ``from`` (value: string) : Field<StaggerContext> = Field.create<StaggerContext> "from" (box (U3.Case2 value))
    static member ``from`` (value: (float)[]) : Field<StaggerContext> = Field.create<StaggerContext> "from" (box (U3.Case3 value))
    static member ``reversed`` (value: bool) : Field<StaggerContext> = Field.create<StaggerContext> "reversed" (box (value))
    static member ``grid`` (value: U2<bool, (float)[]>) : Field<StaggerContext> = Field.create<StaggerContext> "grid" (box (value))
    static member ``grid`` (value: bool) : Field<StaggerContext> = Field.create<StaggerContext> "grid" (box (U2.Case1 value))
    static member ``grid`` (value: (float)[]) : Field<StaggerContext> = Field.create<StaggerContext> "grid" (box (U2.Case2 value))
    static member ``axis`` (value: Raw.StaggerParams.Axis) : Field<StaggerContext> = Field.create<StaggerContext> "axis" (box (value))
    static member ``use`` (value: U2<string, Raw.Method>) : Field<StaggerContext> = Field.create<StaggerContext> "use" (box (value))
    static member ``use`` (value: string) : Field<StaggerContext> = Field.create<StaggerContext> "use" (box (U2.Case1 value))
    static member ``use`` (value: Raw.Method) : Field<StaggerContext> = Field.create<StaggerContext> "use" (box (U2.Case2 value))
    static member ``total`` (value: float) : Field<StaggerContext> = Field.create<StaggerContext> "total" (box (value))
    static member ``ease`` (value: U4<string, (float -> float), Raw.Spring, Raw.TweakRegister>) : Field<StaggerContext> = Field.create<StaggerContext> "ease" (box (value))
    static member ``ease`` (value: string) : Field<StaggerContext> = Field.create<StaggerContext> "ease" (box (U4.Case1 value))
    static member ``ease`` (value: (float -> float)) : Field<StaggerContext> = Field.create<StaggerContext> "ease" (box (U4.Case2 value))
    static member ``ease`` (value: Raw.Spring) : Field<StaggerContext> = Field.create<StaggerContext> "ease" (box (U4.Case3 value))
    static member ``ease`` (value: Raw.TweakRegister) : Field<StaggerContext> = Field.create<StaggerContext> "ease" (box (U4.Case4 value))
    static member ``modifier`` (value: (float -> U2<string, float>)) : Field<StaggerContext> = Field.create<StaggerContext> "modifier" (box (value))
    /// Additive uniform noise on the computed stagger value. Number form gives flat `+/-jitter`; tuple form ramps the magnitude `start -&gt; end` across the from/axis/grid ordering and respects `ease`.
    static member ``jitter`` (value: U2<float, (float * float)>) : Field<StaggerContext> = Field.create<StaggerContext> "jitter" (box (value))
    static member ``jitter`` (value: float) : Field<StaggerContext> = Field.create<StaggerContext> "jitter" (box (U2.Case1 value))
    static member ``jitter`` (value: (float * float)) : Field<StaggerContext> = Field.create<StaggerContext> "jitter" (box (U2.Case2 value))
    /// Seed for jitter draws and `from: &apos;random&apos;` shuffling. `false` (default) uses Math.random. `true` seeds with `0`. A number is used directly as the seed.
    static member ``seed`` (value: U2<float, bool>) : Field<StaggerContext> = Field.create<StaggerContext> "seed" (box (value))
    static member ``seed`` (value: float) : Field<StaggerContext> = Field.create<StaggerContext> "seed" (box (U2.Case1 value))
    static member ``seed`` (value: bool) : Field<StaggerContext> = Field.create<StaggerContext> "seed" (box (U2.Case2 value))
    static member parameters (options: Options<StaggerContext>) : Raw.StaggerParams = Options.toObject options |> unbox

[<AbstractClass; Sealed>]
type ``Spring`` private () =
    /// - Mass, default 1
    static member ``mass`` (value: float) : Field<SpringContext> = Field.create<SpringContext> "mass" (box (value))
    /// - Stiffness, default 100
    static member ``stiffness`` (value: float) : Field<SpringContext> = Field.create<SpringContext> "stiffness" (box (value))
    /// - Damping, default 10
    static member ``damping`` (value: float) : Field<SpringContext> = Field.create<SpringContext> "damping" (box (value))
    /// - Initial velocity, default 0
    static member ``velocity`` (value: float) : Field<SpringContext> = Field.create<SpringContext> "velocity" (box (value))
    /// - Initial bounce, default 0
    static member ``bounce`` (value: float) : Field<SpringContext> = Field.create<SpringContext> "bounce" (box (value))
    /// - The perceived duration, default 0
    static member ``duration`` (value: float) : Field<SpringContext> = Field.create<SpringContext> "duration" (box (value))
    /// - Callback function called when the spring currentTime hits the perceived duration
    static member ``onCompleteValue`` (value: (Raw.JSAnimation -> obj)) : Field<SpringContext> = Field.create<SpringContext> "onComplete" (box (value))
    static member ``onComplete`` (value: (Raw.JSAnimation -> unit)) : Field<SpringContext> =
        let callback (handle: Raw.JSAnimation) : obj = value handle; box ()
        Field.create<SpringContext> "onComplete" (box (callback))
    static member parameters (options: Options<SpringContext>) : Raw.SpringParams = Options.toObject options |> unbox

[<AbstractClass; Sealed>]
type ``Animatable`` private () =
    static member ``unit`` (value: string) : Field<AnimatableContext> = Field.create<AnimatableContext> "unit" (box (value))
    static member ``duration`` (value: U6<string, float, (float -> float), Raw.DurationKeyframes.Item.Duration, Raw.Spring, Raw.TweakRegister>) : Field<AnimatableContext> = Field.create<AnimatableContext> "duration" (box (value))
    static member ``duration`` (value: string) : Field<AnimatableContext> = Field.create<AnimatableContext> "duration" (box (U6.Case1 value))
    static member ``duration`` (value: float) : Field<AnimatableContext> = Field.create<AnimatableContext> "duration" (box (U6.Case2 value))
    static member ``duration`` (value: (float -> float)) : Field<AnimatableContext> = Field.create<AnimatableContext> "duration" (box (U6.Case3 value))
    static member ``duration`` (value: Raw.DurationKeyframes.Item.Duration) : Field<AnimatableContext> = Field.create<AnimatableContext> "duration" (box (U6.Case4 value))
    static member ``duration`` (value: Raw.Spring) : Field<AnimatableContext> = Field.create<AnimatableContext> "duration" (box (U6.Case5 value))
    static member ``duration`` (value: Raw.TweakRegister) : Field<AnimatableContext> = Field.create<AnimatableContext> "duration" (box (U6.Case6 value))
    static member ``ease`` (value: U4<string, (float -> float), Raw.Spring, Raw.TweakRegister>) : Field<AnimatableContext> = Field.create<AnimatableContext> "ease" (box (value))
    static member ``ease`` (value: string) : Field<AnimatableContext> = Field.create<AnimatableContext> "ease" (box (U4.Case1 value))
    static member ``ease`` (value: (float -> float)) : Field<AnimatableContext> = Field.create<AnimatableContext> "ease" (box (U4.Case2 value))
    static member ``ease`` (value: Raw.Spring) : Field<AnimatableContext> = Field.create<AnimatableContext> "ease" (box (U4.Case3 value))
    static member ``ease`` (value: Raw.TweakRegister) : Field<AnimatableContext> = Field.create<AnimatableContext> "ease" (box (U4.Case4 value))
    static member ``modifier`` (value: (float -> U2<string, float>)) : Field<AnimatableContext> = Field.create<AnimatableContext> "modifier" (box (value))
    static member ``composition`` (value: U2<float, string>) : Field<AnimatableContext> = Field.create<AnimatableContext> "composition" (box (value))
    static member ``composition`` (value: float) : Field<AnimatableContext> = Field.create<AnimatableContext> "composition" (box (U2.Case1 value))
    static member ``composition`` (value: string) : Field<AnimatableContext> = Field.create<AnimatableContext> "composition" (box (U2.Case2 value))
    static member ``onBeginValue`` (value: (Raw.JSAnimation -> obj)) : Field<AnimatableContext> = Field.create<AnimatableContext> "onBegin" (box (value))
    static member ``onBegin`` (value: (Raw.JSAnimation -> unit)) : Field<AnimatableContext> =
        let callback (handle: Raw.JSAnimation) : obj = value handle; box ()
        Field.create<AnimatableContext> "onBegin" (box (callback))
    static member ``onBeforeUpdateValue`` (value: (Raw.JSAnimation -> obj)) : Field<AnimatableContext> = Field.create<AnimatableContext> "onBeforeUpdate" (box (value))
    static member ``onBeforeUpdate`` (value: (Raw.JSAnimation -> unit)) : Field<AnimatableContext> =
        let callback (handle: Raw.JSAnimation) : obj = value handle; box ()
        Field.create<AnimatableContext> "onBeforeUpdate" (box (callback))
    static member ``onUpdateValue`` (value: (Raw.JSAnimation -> obj)) : Field<AnimatableContext> = Field.create<AnimatableContext> "onUpdate" (box (value))
    static member ``onUpdate`` (value: (Raw.JSAnimation -> unit)) : Field<AnimatableContext> =
        let callback (handle: Raw.JSAnimation) : obj = value handle; box ()
        Field.create<AnimatableContext> "onUpdate" (box (callback))
    static member ``onLoopValue`` (value: (Raw.JSAnimation -> obj)) : Field<AnimatableContext> = Field.create<AnimatableContext> "onLoop" (box (value))
    static member ``onLoop`` (value: (Raw.JSAnimation -> unit)) : Field<AnimatableContext> =
        let callback (handle: Raw.JSAnimation) : obj = value handle; box ()
        Field.create<AnimatableContext> "onLoop" (box (callback))
    static member ``onPauseValue`` (value: (Raw.JSAnimation -> obj)) : Field<AnimatableContext> = Field.create<AnimatableContext> "onPause" (box (value))
    static member ``onPause`` (value: (Raw.JSAnimation -> unit)) : Field<AnimatableContext> =
        let callback (handle: Raw.JSAnimation) : obj = value handle; box ()
        Field.create<AnimatableContext> "onPause" (box (callback))
    static member ``onCompleteValue`` (value: (Raw.JSAnimation -> obj)) : Field<AnimatableContext> = Field.create<AnimatableContext> "onComplete" (box (value))
    static member ``onComplete`` (value: (Raw.JSAnimation -> unit)) : Field<AnimatableContext> =
        let callback (handle: Raw.JSAnimation) : obj = value handle; box ()
        Field.create<AnimatableContext> "onComplete" (box (callback))
    static member ``onRenderValue`` (value: (Raw.JSAnimation -> obj)) : Field<AnimatableContext> = Field.create<AnimatableContext> "onRender" (box (value))
    static member ``onRender`` (value: (Raw.JSAnimation -> unit)) : Field<AnimatableContext> =
        let callback (handle: Raw.JSAnimation) : obj = value handle; box ()
        Field.create<AnimatableContext> "onRender" (box (callback))
    static member parameters (options: Options<AnimatableContext>) : Raw.AnimatableParams = Options.toObject options |> unbox
    static member property (key: string, value: U9<string, float, Raw.AnimatablePropertyParamsOptions, (float -> float), Raw.DurationKeyframes.Item.Duration, Raw.Spring, Raw.TweakRegister, (float -> U2<string, float>), (Raw.JSAnimation -> obj)>) : Field<AnimatableContext> =
        if System.String.IsNullOrWhiteSpace key || List.contains key ["unit"; "duration"; "ease"; "modifier"; "composition"; "onBegin"; "onBeforeUpdate"; "onUpdate"; "onLoop"; "onPause"; "onComplete"; "onRender"; "__proto__"; "constructor"; "prototype"] then invalidArg "key" "Custom keys must be nonempty and cannot name an option."
        Field.create<AnimatableContext> key (box (value))
    static member property (key: string, value: string) : Field<AnimatableContext> =
        if System.String.IsNullOrWhiteSpace key || List.contains key ["unit"; "duration"; "ease"; "modifier"; "composition"; "onBegin"; "onBeforeUpdate"; "onUpdate"; "onLoop"; "onPause"; "onComplete"; "onRender"; "__proto__"; "constructor"; "prototype"] then invalidArg "key" "Custom keys must be nonempty and cannot name an option."
        Field.create<AnimatableContext> key (box (U9.Case1 (value)))
    static member property (key: string, value: float) : Field<AnimatableContext> =
        if System.String.IsNullOrWhiteSpace key || List.contains key ["unit"; "duration"; "ease"; "modifier"; "composition"; "onBegin"; "onBeforeUpdate"; "onUpdate"; "onLoop"; "onPause"; "onComplete"; "onRender"; "__proto__"; "constructor"; "prototype"] then invalidArg "key" "Custom keys must be nonempty and cannot name an option."
        Field.create<AnimatableContext> key (box (U9.Case2 (value)))
    static member property (key: string, value: Raw.AnimatablePropertyParamsOptions) : Field<AnimatableContext> =
        if System.String.IsNullOrWhiteSpace key || List.contains key ["unit"; "duration"; "ease"; "modifier"; "composition"; "onBegin"; "onBeforeUpdate"; "onUpdate"; "onLoop"; "onPause"; "onComplete"; "onRender"; "__proto__"; "constructor"; "prototype"] then invalidArg "key" "Custom keys must be nonempty and cannot name an option."
        Field.create<AnimatableContext> key (box (U9.Case3 (value)))
    static member property (key: string, value: (float -> float)) : Field<AnimatableContext> =
        if System.String.IsNullOrWhiteSpace key || List.contains key ["unit"; "duration"; "ease"; "modifier"; "composition"; "onBegin"; "onBeforeUpdate"; "onUpdate"; "onLoop"; "onPause"; "onComplete"; "onRender"; "__proto__"; "constructor"; "prototype"] then invalidArg "key" "Custom keys must be nonempty and cannot name an option."
        Field.create<AnimatableContext> key (box (U9.Case4 (value)))
    static member property (key: string, value: Raw.DurationKeyframes.Item.Duration) : Field<AnimatableContext> =
        if System.String.IsNullOrWhiteSpace key || List.contains key ["unit"; "duration"; "ease"; "modifier"; "composition"; "onBegin"; "onBeforeUpdate"; "onUpdate"; "onLoop"; "onPause"; "onComplete"; "onRender"; "__proto__"; "constructor"; "prototype"] then invalidArg "key" "Custom keys must be nonempty and cannot name an option."
        Field.create<AnimatableContext> key (box (U9.Case5 (value)))
    static member property (key: string, value: Raw.Spring) : Field<AnimatableContext> =
        if System.String.IsNullOrWhiteSpace key || List.contains key ["unit"; "duration"; "ease"; "modifier"; "composition"; "onBegin"; "onBeforeUpdate"; "onUpdate"; "onLoop"; "onPause"; "onComplete"; "onRender"; "__proto__"; "constructor"; "prototype"] then invalidArg "key" "Custom keys must be nonempty and cannot name an option."
        Field.create<AnimatableContext> key (box (U9.Case6 (value)))
    static member property (key: string, value: Raw.TweakRegister) : Field<AnimatableContext> =
        if System.String.IsNullOrWhiteSpace key || List.contains key ["unit"; "duration"; "ease"; "modifier"; "composition"; "onBegin"; "onBeforeUpdate"; "onUpdate"; "onLoop"; "onPause"; "onComplete"; "onRender"; "__proto__"; "constructor"; "prototype"] then invalidArg "key" "Custom keys must be nonempty and cannot name an option."
        Field.create<AnimatableContext> key (box (U9.Case7 (value)))
    static member property (key: string, value: (float -> U2<string, float>)) : Field<AnimatableContext> =
        if System.String.IsNullOrWhiteSpace key || List.contains key ["unit"; "duration"; "ease"; "modifier"; "composition"; "onBegin"; "onBeforeUpdate"; "onUpdate"; "onLoop"; "onPause"; "onComplete"; "onRender"; "__proto__"; "constructor"; "prototype"] then invalidArg "key" "Custom keys must be nonempty and cannot name an option."
        Field.create<AnimatableContext> key (box (U9.Case8 (value)))
    static member property (key: string, value: (Raw.JSAnimation -> obj)) : Field<AnimatableContext> =
        if System.String.IsNullOrWhiteSpace key || List.contains key ["unit"; "duration"; "ease"; "modifier"; "composition"; "onBegin"; "onBeforeUpdate"; "onUpdate"; "onLoop"; "onPause"; "onComplete"; "onRender"; "__proto__"; "constructor"; "prototype"] then invalidArg "key" "Custom keys must be nonempty and cannot name an option."
        Field.create<AnimatableContext> key (box (U9.Case9 (value)))

[<AbstractClass; Sealed>]
type ``Draggable`` private () =
    static member ``trigger`` (value: U4<string, Fable.Core.TS.Dom.HTMLElement, Fable.Core.TS.Dom.NodeList, Fable.Core.TS.Dom.SVGElement>) : Field<DraggableContext> = Field.create<DraggableContext> "trigger" (box (value))
    static member ``trigger`` (value: string) : Field<DraggableContext> = Field.create<DraggableContext> "trigger" (box (U4.Case1 value))
    static member ``trigger`` (value: Fable.Core.TS.Dom.HTMLElement) : Field<DraggableContext> = Field.create<DraggableContext> "trigger" (box (U4.Case2 value))
    static member ``trigger`` (value: Fable.Core.TS.Dom.NodeList) : Field<DraggableContext> = Field.create<DraggableContext> "trigger" (box (U4.Case3 value))
    static member ``trigger`` (value: Fable.Core.TS.Dom.SVGElement) : Field<DraggableContext> = Field.create<DraggableContext> "trigger" (box (U4.Case4 value))
    static member ``container`` (value: U6<string, (float)[], Fable.Core.TS.Dom.HTMLElement, Fable.Core.TS.Dom.NodeList, Fable.Core.TS.Dom.SVGElement, (Raw.Draggable -> U5<string, (float)[], Fable.Core.TS.Dom.HTMLElement, Fable.Core.TS.Dom.NodeList, Fable.Core.TS.Dom.SVGElement>)>) : Field<DraggableContext> = Field.create<DraggableContext> "container" (box (value))
    static member ``container`` (value: string) : Field<DraggableContext> = Field.create<DraggableContext> "container" (box (U6.Case1 value))
    static member ``container`` (value: (float)[]) : Field<DraggableContext> = Field.create<DraggableContext> "container" (box (U6.Case2 value))
    static member ``container`` (value: Fable.Core.TS.Dom.HTMLElement) : Field<DraggableContext> = Field.create<DraggableContext> "container" (box (U6.Case3 value))
    static member ``container`` (value: Fable.Core.TS.Dom.NodeList) : Field<DraggableContext> = Field.create<DraggableContext> "container" (box (U6.Case4 value))
    static member ``container`` (value: Fable.Core.TS.Dom.SVGElement) : Field<DraggableContext> = Field.create<DraggableContext> "container" (box (U6.Case5 value))
    static member ``container`` (value: (Raw.Draggable -> U5<string, (float)[], Fable.Core.TS.Dom.HTMLElement, Fable.Core.TS.Dom.NodeList, Fable.Core.TS.Dom.SVGElement>)) : Field<DraggableContext> = Field.create<DraggableContext> "container" (box (U6.Case6 value))
    static member ``x`` (value: U2<bool, Raw.DraggableAxisParam>) : Field<DraggableContext> = Field.create<DraggableContext> "x" (box (value))
    static member ``x`` (value: bool) : Field<DraggableContext> = Field.create<DraggableContext> "x" (box (U2.Case1 value))
    static member ``x`` (value: Raw.DraggableAxisParam) : Field<DraggableContext> = Field.create<DraggableContext> "x" (box (U2.Case2 value))
    static member ``y`` (value: U2<bool, Raw.DraggableAxisParam>) : Field<DraggableContext> = Field.create<DraggableContext> "y" (box (value))
    static member ``y`` (value: bool) : Field<DraggableContext> = Field.create<DraggableContext> "y" (box (U2.Case1 value))
    static member ``y`` (value: Raw.DraggableAxisParam) : Field<DraggableContext> = Field.create<DraggableContext> "y" (box (U2.Case2 value))
    static member ``modifier`` (value: (float -> U2<string, float>)) : Field<DraggableContext> = Field.create<DraggableContext> "modifier" (box (value))
    static member ``snap`` (value: U3<float, (float)[], (Raw.Draggable -> U2<float, (float)[]>)>) : Field<DraggableContext> = Field.create<DraggableContext> "snap" (box (value))
    static member ``snap`` (value: float) : Field<DraggableContext> = Field.create<DraggableContext> "snap" (box (U3.Case1 value))
    static member ``snap`` (value: (float)[]) : Field<DraggableContext> = Field.create<DraggableContext> "snap" (box (U3.Case2 value))
    static member ``snap`` (value: (Raw.Draggable -> U2<float, (float)[]>)) : Field<DraggableContext> = Field.create<DraggableContext> "snap" (box (U3.Case3 value))
    static member ``containerPadding`` (value: U3<float, (float)[], (Raw.Draggable -> U2<float, (float)[]>)>) : Field<DraggableContext> = Field.create<DraggableContext> "containerPadding" (box (value))
    static member ``containerPadding`` (value: float) : Field<DraggableContext> = Field.create<DraggableContext> "containerPadding" (box (U3.Case1 value))
    static member ``containerPadding`` (value: (float)[]) : Field<DraggableContext> = Field.create<DraggableContext> "containerPadding" (box (U3.Case2 value))
    static member ``containerPadding`` (value: (Raw.Draggable -> U2<float, (float)[]>)) : Field<DraggableContext> = Field.create<DraggableContext> "containerPadding" (box (U3.Case3 value))
    static member ``containerFriction`` (value: U2<float, (Raw.Draggable -> float)>) : Field<DraggableContext> = Field.create<DraggableContext> "containerFriction" (box (value))
    static member ``containerFriction`` (value: float) : Field<DraggableContext> = Field.create<DraggableContext> "containerFriction" (box (U2.Case1 value))
    static member ``containerFriction`` (value: (Raw.Draggable -> float)) : Field<DraggableContext> = Field.create<DraggableContext> "containerFriction" (box (U2.Case2 value))
    static member ``releaseContainerFriction`` (value: U2<float, (Raw.Draggable -> float)>) : Field<DraggableContext> = Field.create<DraggableContext> "releaseContainerFriction" (box (value))
    static member ``releaseContainerFriction`` (value: float) : Field<DraggableContext> = Field.create<DraggableContext> "releaseContainerFriction" (box (U2.Case1 value))
    static member ``releaseContainerFriction`` (value: (Raw.Draggable -> float)) : Field<DraggableContext> = Field.create<DraggableContext> "releaseContainerFriction" (box (U2.Case2 value))
    static member ``dragSpeed`` (value: U2<float, (Raw.Draggable -> float)>) : Field<DraggableContext> = Field.create<DraggableContext> "dragSpeed" (box (value))
    static member ``dragSpeed`` (value: float) : Field<DraggableContext> = Field.create<DraggableContext> "dragSpeed" (box (U2.Case1 value))
    static member ``dragSpeed`` (value: (Raw.Draggable -> float)) : Field<DraggableContext> = Field.create<DraggableContext> "dragSpeed" (box (U2.Case2 value))
    static member ``dragThreshold`` (value: U3<float, Raw.DraggableDragThresholdParams, (Raw.Draggable -> U2<float, Raw.DraggableDragThresholdParams>)>) : Field<DraggableContext> = Field.create<DraggableContext> "dragThreshold" (box (value))
    static member ``dragThreshold`` (value: float) : Field<DraggableContext> = Field.create<DraggableContext> "dragThreshold" (box (U3.Case1 value))
    static member ``dragThreshold`` (value: Raw.DraggableDragThresholdParams) : Field<DraggableContext> = Field.create<DraggableContext> "dragThreshold" (box (U3.Case2 value))
    static member ``dragThreshold`` (value: (Raw.Draggable -> U2<float, Raw.DraggableDragThresholdParams>)) : Field<DraggableContext> = Field.create<DraggableContext> "dragThreshold" (box (U3.Case3 value))
    static member ``scrollSpeed`` (value: U2<float, (Raw.Draggable -> float)>) : Field<DraggableContext> = Field.create<DraggableContext> "scrollSpeed" (box (value))
    static member ``scrollSpeed`` (value: float) : Field<DraggableContext> = Field.create<DraggableContext> "scrollSpeed" (box (U2.Case1 value))
    static member ``scrollSpeed`` (value: (Raw.Draggable -> float)) : Field<DraggableContext> = Field.create<DraggableContext> "scrollSpeed" (box (U2.Case2 value))
    static member ``scrollThreshold`` (value: U2<float, (Raw.Draggable -> float)>) : Field<DraggableContext> = Field.create<DraggableContext> "scrollThreshold" (box (value))
    static member ``scrollThreshold`` (value: float) : Field<DraggableContext> = Field.create<DraggableContext> "scrollThreshold" (box (U2.Case1 value))
    static member ``scrollThreshold`` (value: (Raw.Draggable -> float)) : Field<DraggableContext> = Field.create<DraggableContext> "scrollThreshold" (box (U2.Case2 value))
    static member ``minVelocity`` (value: U2<float, (Raw.Draggable -> float)>) : Field<DraggableContext> = Field.create<DraggableContext> "minVelocity" (box (value))
    static member ``minVelocity`` (value: float) : Field<DraggableContext> = Field.create<DraggableContext> "minVelocity" (box (U2.Case1 value))
    static member ``minVelocity`` (value: (Raw.Draggable -> float)) : Field<DraggableContext> = Field.create<DraggableContext> "minVelocity" (box (U2.Case2 value))
    static member ``maxVelocity`` (value: U2<float, (Raw.Draggable -> float)>) : Field<DraggableContext> = Field.create<DraggableContext> "maxVelocity" (box (value))
    static member ``maxVelocity`` (value: float) : Field<DraggableContext> = Field.create<DraggableContext> "maxVelocity" (box (U2.Case1 value))
    static member ``maxVelocity`` (value: (Raw.Draggable -> float)) : Field<DraggableContext> = Field.create<DraggableContext> "maxVelocity" (box (U2.Case2 value))
    static member ``velocityMultiplier`` (value: U2<float, (Raw.Draggable -> float)>) : Field<DraggableContext> = Field.create<DraggableContext> "velocityMultiplier" (box (value))
    static member ``velocityMultiplier`` (value: float) : Field<DraggableContext> = Field.create<DraggableContext> "velocityMultiplier" (box (U2.Case1 value))
    static member ``velocityMultiplier`` (value: (Raw.Draggable -> float)) : Field<DraggableContext> = Field.create<DraggableContext> "velocityMultiplier" (box (U2.Case2 value))
    static member ``releaseMass`` (value: float) : Field<DraggableContext> = Field.create<DraggableContext> "releaseMass" (box (value))
    static member ``releaseStiffness`` (value: float) : Field<DraggableContext> = Field.create<DraggableContext> "releaseStiffness" (box (value))
    static member ``releaseDamping`` (value: float) : Field<DraggableContext> = Field.create<DraggableContext> "releaseDamping" (box (value))
    static member ``releaseEase`` (value: U4<string, (float -> float), Raw.Spring, Raw.TweakRegister>) : Field<DraggableContext> = Field.create<DraggableContext> "releaseEase" (box (value))
    static member ``releaseEase`` (value: string) : Field<DraggableContext> = Field.create<DraggableContext> "releaseEase" (box (U4.Case1 value))
    static member ``releaseEase`` (value: (float -> float)) : Field<DraggableContext> = Field.create<DraggableContext> "releaseEase" (box (U4.Case2 value))
    static member ``releaseEase`` (value: Raw.Spring) : Field<DraggableContext> = Field.create<DraggableContext> "releaseEase" (box (U4.Case3 value))
    static member ``releaseEase`` (value: Raw.TweakRegister) : Field<DraggableContext> = Field.create<DraggableContext> "releaseEase" (box (U4.Case4 value))
    static member ``cursor`` (value: U3<bool, Raw.DraggableCursorParams, (Raw.Draggable -> U2<bool, Raw.DraggableCursorParams>)>) : Field<DraggableContext> = Field.create<DraggableContext> "cursor" (box (value))
    static member ``cursor`` (value: bool) : Field<DraggableContext> = Field.create<DraggableContext> "cursor" (box (U3.Case1 value))
    static member ``cursor`` (value: Raw.DraggableCursorParams) : Field<DraggableContext> = Field.create<DraggableContext> "cursor" (box (U3.Case2 value))
    static member ``cursor`` (value: (Raw.Draggable -> U2<bool, Raw.DraggableCursorParams>)) : Field<DraggableContext> = Field.create<DraggableContext> "cursor" (box (U3.Case3 value))
    static member ``onGrabValue`` (value: (Raw.Draggable -> obj)) : Field<DraggableContext> = Field.create<DraggableContext> "onGrab" (box (value))
    static member ``onGrab`` (value: (Raw.Draggable -> unit)) : Field<DraggableContext> =
        let callback (handle: Raw.Draggable) : obj = value handle; box ()
        Field.create<DraggableContext> "onGrab" (box (callback))
    static member ``onDragValue`` (value: (Raw.Draggable -> obj)) : Field<DraggableContext> = Field.create<DraggableContext> "onDrag" (box (value))
    static member ``onDrag`` (value: (Raw.Draggable -> unit)) : Field<DraggableContext> =
        let callback (handle: Raw.Draggable) : obj = value handle; box ()
        Field.create<DraggableContext> "onDrag" (box (callback))
    static member ``onReleaseValue`` (value: (Raw.Draggable -> obj)) : Field<DraggableContext> = Field.create<DraggableContext> "onRelease" (box (value))
    static member ``onRelease`` (value: (Raw.Draggable -> unit)) : Field<DraggableContext> =
        let callback (handle: Raw.Draggable) : obj = value handle; box ()
        Field.create<DraggableContext> "onRelease" (box (callback))
    static member ``onUpdateValue`` (value: (Raw.Draggable -> obj)) : Field<DraggableContext> = Field.create<DraggableContext> "onUpdate" (box (value))
    static member ``onUpdate`` (value: (Raw.Draggable -> unit)) : Field<DraggableContext> =
        let callback (handle: Raw.Draggable) : obj = value handle; box ()
        Field.create<DraggableContext> "onUpdate" (box (callback))
    static member ``onSettleValue`` (value: (Raw.Draggable -> obj)) : Field<DraggableContext> = Field.create<DraggableContext> "onSettle" (box (value))
    static member ``onSettle`` (value: (Raw.Draggable -> unit)) : Field<DraggableContext> =
        let callback (handle: Raw.Draggable) : obj = value handle; box ()
        Field.create<DraggableContext> "onSettle" (box (callback))
    static member ``onSnapValue`` (value: (Raw.Draggable -> obj)) : Field<DraggableContext> = Field.create<DraggableContext> "onSnap" (box (value))
    static member ``onSnap`` (value: (Raw.Draggable -> unit)) : Field<DraggableContext> =
        let callback (handle: Raw.Draggable) : obj = value handle; box ()
        Field.create<DraggableContext> "onSnap" (box (callback))
    static member ``onResizeValue`` (value: (Raw.Draggable -> obj)) : Field<DraggableContext> = Field.create<DraggableContext> "onResize" (box (value))
    static member ``onResize`` (value: (Raw.Draggable -> unit)) : Field<DraggableContext> =
        let callback (handle: Raw.Draggable) : obj = value handle; box ()
        Field.create<DraggableContext> "onResize" (box (callback))
    static member ``onAfterResizeValue`` (value: (Raw.Draggable -> obj)) : Field<DraggableContext> = Field.create<DraggableContext> "onAfterResize" (box (value))
    static member ``onAfterResize`` (value: (Raw.Draggable -> unit)) : Field<DraggableContext> =
        let callback (handle: Raw.Draggable) : obj = value handle; box ()
        Field.create<DraggableContext> "onAfterResize" (box (callback))
    static member parameters (options: Options<DraggableContext>) : Raw.DraggableParams = Options.toObject options |> unbox

[<AbstractClass; Sealed>]
type ``Scroll`` private () =
    static member ``id`` (value: U2<string, float>) : Field<ScrollContext> = Field.create<ScrollContext> "id" (box (value))
    static member ``id`` (value: string) : Field<ScrollContext> = Field.create<ScrollContext> "id" (box (U2.Case1 value))
    static member ``id`` (value: float) : Field<ScrollContext> = Field.create<ScrollContext> "id" (box (U2.Case2 value))
    static member ``sync`` (value: U6<string, float, bool, (float -> float), Raw.Spring, Raw.TweakRegister>) : Field<ScrollContext> = Field.create<ScrollContext> "sync" (box (value))
    static member ``sync`` (value: string) : Field<ScrollContext> = Field.create<ScrollContext> "sync" (box (U6.Case1 value))
    static member ``sync`` (value: float) : Field<ScrollContext> = Field.create<ScrollContext> "sync" (box (U6.Case2 value))
    static member ``sync`` (value: bool) : Field<ScrollContext> = Field.create<ScrollContext> "sync" (box (U6.Case3 value))
    static member ``sync`` (value: (float -> float)) : Field<ScrollContext> = Field.create<ScrollContext> "sync" (box (U6.Case4 value))
    static member ``sync`` (value: Raw.Spring) : Field<ScrollContext> = Field.create<ScrollContext> "sync" (box (U6.Case5 value))
    static member ``sync`` (value: Raw.TweakRegister) : Field<ScrollContext> = Field.create<ScrollContext> "sync" (box (U6.Case6 value))
    static member ``container`` (value: U6<string, (U5<string, Fable.Core.TS.Dom.HTMLElement, Fable.Core.JS.JS.Record<string, obj>, Fable.Core.TS.Dom.NodeList, Fable.Core.TS.Dom.SVGElement>)[], Fable.Core.TS.Dom.HTMLElement, Fable.Core.JS.JS.Record<string, obj>, Fable.Core.TS.Dom.NodeList, Fable.Core.TS.Dom.SVGElement>) : Field<ScrollContext> = Field.create<ScrollContext> "container" (box (value))
    static member ``container`` (value: string) : Field<ScrollContext> = Field.create<ScrollContext> "container" (box (U6.Case1 value))
    static member ``container`` (value: (U5<string, Fable.Core.TS.Dom.HTMLElement, Fable.Core.JS.JS.Record<string, obj>, Fable.Core.TS.Dom.NodeList, Fable.Core.TS.Dom.SVGElement>)[]) : Field<ScrollContext> = Field.create<ScrollContext> "container" (box (U6.Case2 value))
    static member ``container`` (value: Fable.Core.TS.Dom.HTMLElement) : Field<ScrollContext> = Field.create<ScrollContext> "container" (box (U6.Case3 value))
    static member ``container`` (value: Fable.Core.JS.JS.Record<string, obj>) : Field<ScrollContext> = Field.create<ScrollContext> "container" (box (U6.Case4 value))
    static member ``container`` (value: Fable.Core.TS.Dom.NodeList) : Field<ScrollContext> = Field.create<ScrollContext> "container" (box (U6.Case5 value))
    static member ``container`` (value: Fable.Core.TS.Dom.SVGElement) : Field<ScrollContext> = Field.create<ScrollContext> "container" (box (U6.Case6 value))
    static member ``target`` (value: U6<string, (U5<string, Fable.Core.TS.Dom.HTMLElement, Fable.Core.JS.JS.Record<string, obj>, Fable.Core.TS.Dom.NodeList, Fable.Core.TS.Dom.SVGElement>)[], Fable.Core.TS.Dom.HTMLElement, Fable.Core.JS.JS.Record<string, obj>, Fable.Core.TS.Dom.NodeList, Fable.Core.TS.Dom.SVGElement>) : Field<ScrollContext> = Field.create<ScrollContext> "target" (box (value))
    static member ``target`` (value: string) : Field<ScrollContext> = Field.create<ScrollContext> "target" (box (U6.Case1 value))
    static member ``target`` (value: (U5<string, Fable.Core.TS.Dom.HTMLElement, Fable.Core.JS.JS.Record<string, obj>, Fable.Core.TS.Dom.NodeList, Fable.Core.TS.Dom.SVGElement>)[]) : Field<ScrollContext> = Field.create<ScrollContext> "target" (box (U6.Case2 value))
    static member ``target`` (value: Fable.Core.TS.Dom.HTMLElement) : Field<ScrollContext> = Field.create<ScrollContext> "target" (box (U6.Case3 value))
    static member ``target`` (value: Fable.Core.JS.JS.Record<string, obj>) : Field<ScrollContext> = Field.create<ScrollContext> "target" (box (U6.Case4 value))
    static member ``target`` (value: Fable.Core.TS.Dom.NodeList) : Field<ScrollContext> = Field.create<ScrollContext> "target" (box (U6.Case5 value))
    static member ``target`` (value: Fable.Core.TS.Dom.SVGElement) : Field<ScrollContext> = Field.create<ScrollContext> "target" (box (U6.Case6 value))
    static member ``axis`` (value: U3<string, (Raw.ScrollObserver -> Raw.ScrollObserverAxisCallback.Result), (Raw.ScrollObserver -> U2<string, (Raw.ScrollObserver -> Raw.ScrollObserverAxisCallback.Result)>)>) : Field<ScrollContext> = Field.create<ScrollContext> "axis" (box (value))
    static member ``axis`` (value: string) : Field<ScrollContext> = Field.create<ScrollContext> "axis" (box (U3.Case1 value))
    static member ``axis`` (value: (Raw.ScrollObserver -> Raw.ScrollObserverAxisCallback.Result)) : Field<ScrollContext> = Field.create<ScrollContext> "axis" (box (U3.Case2 value))
    static member ``axis`` (value: (Raw.ScrollObserver -> U2<string, (Raw.ScrollObserver -> Raw.ScrollObserverAxisCallback.Result)>)) : Field<ScrollContext> = Field.create<ScrollContext> "axis" (box (U3.Case3 value))
    static member ``enter`` (value: U5<string, float, (Raw.ScrollObserver -> U3<string, float, Raw.ScrollThresholdParam>), Raw.ScrollThresholdParam, (Raw.ScrollObserver -> U4<string, float, (Raw.ScrollObserver -> U3<string, float, Raw.ScrollThresholdParam>), Raw.ScrollThresholdParam>)>) : Field<ScrollContext> = Field.create<ScrollContext> "enter" (box (value))
    static member ``enter`` (value: string) : Field<ScrollContext> = Field.create<ScrollContext> "enter" (box (U5.Case1 value))
    static member ``enter`` (value: float) : Field<ScrollContext> = Field.create<ScrollContext> "enter" (box (U5.Case2 value))
    static member ``enter`` (value: (Raw.ScrollObserver -> U3<string, float, Raw.ScrollThresholdParam>)) : Field<ScrollContext> = Field.create<ScrollContext> "enter" (box (U5.Case3 value))
    static member ``enter`` (value: Raw.ScrollThresholdParam) : Field<ScrollContext> = Field.create<ScrollContext> "enter" (box (U5.Case4 value))
    static member ``enter`` (value: (Raw.ScrollObserver -> U4<string, float, (Raw.ScrollObserver -> U3<string, float, Raw.ScrollThresholdParam>), Raw.ScrollThresholdParam>)) : Field<ScrollContext> = Field.create<ScrollContext> "enter" (box (U5.Case5 value))
    static member ``leave`` (value: U5<string, float, (Raw.ScrollObserver -> U3<string, float, Raw.ScrollThresholdParam>), Raw.ScrollThresholdParam, (Raw.ScrollObserver -> U4<string, float, (Raw.ScrollObserver -> U3<string, float, Raw.ScrollThresholdParam>), Raw.ScrollThresholdParam>)>) : Field<ScrollContext> = Field.create<ScrollContext> "leave" (box (value))
    static member ``leave`` (value: string) : Field<ScrollContext> = Field.create<ScrollContext> "leave" (box (U5.Case1 value))
    static member ``leave`` (value: float) : Field<ScrollContext> = Field.create<ScrollContext> "leave" (box (U5.Case2 value))
    static member ``leave`` (value: (Raw.ScrollObserver -> U3<string, float, Raw.ScrollThresholdParam>)) : Field<ScrollContext> = Field.create<ScrollContext> "leave" (box (U5.Case3 value))
    static member ``leave`` (value: Raw.ScrollThresholdParam) : Field<ScrollContext> = Field.create<ScrollContext> "leave" (box (U5.Case4 value))
    static member ``leave`` (value: (Raw.ScrollObserver -> U4<string, float, (Raw.ScrollObserver -> U3<string, float, Raw.ScrollThresholdParam>), Raw.ScrollThresholdParam>)) : Field<ScrollContext> = Field.create<ScrollContext> "leave" (box (U5.Case5 value))
    static member ``repeat`` (value: U2<bool, (Raw.ScrollObserver -> bool)>) : Field<ScrollContext> = Field.create<ScrollContext> "repeat" (box (value))
    static member ``repeat`` (value: bool) : Field<ScrollContext> = Field.create<ScrollContext> "repeat" (box (U2.Case1 value))
    static member ``repeat`` (value: (Raw.ScrollObserver -> bool)) : Field<ScrollContext> = Field.create<ScrollContext> "repeat" (box (U2.Case2 value))
    static member ``debug`` (value: bool) : Field<ScrollContext> = Field.create<ScrollContext> "debug" (box (value))
    static member ``onEnterValue`` (value: (Raw.ScrollObserver -> obj)) : Field<ScrollContext> = Field.create<ScrollContext> "onEnter" (box (value))
    static member ``onEnter`` (value: (Raw.ScrollObserver -> unit)) : Field<ScrollContext> =
        let callback (handle: Raw.ScrollObserver) : obj = value handle; box ()
        Field.create<ScrollContext> "onEnter" (box (callback))
    static member ``onLeaveValue`` (value: (Raw.ScrollObserver -> obj)) : Field<ScrollContext> = Field.create<ScrollContext> "onLeave" (box (value))
    static member ``onLeave`` (value: (Raw.ScrollObserver -> unit)) : Field<ScrollContext> =
        let callback (handle: Raw.ScrollObserver) : obj = value handle; box ()
        Field.create<ScrollContext> "onLeave" (box (callback))
    static member ``onEnterForwardValue`` (value: (Raw.ScrollObserver -> obj)) : Field<ScrollContext> = Field.create<ScrollContext> "onEnterForward" (box (value))
    static member ``onEnterForward`` (value: (Raw.ScrollObserver -> unit)) : Field<ScrollContext> =
        let callback (handle: Raw.ScrollObserver) : obj = value handle; box ()
        Field.create<ScrollContext> "onEnterForward" (box (callback))
    static member ``onLeaveForwardValue`` (value: (Raw.ScrollObserver -> obj)) : Field<ScrollContext> = Field.create<ScrollContext> "onLeaveForward" (box (value))
    static member ``onLeaveForward`` (value: (Raw.ScrollObserver -> unit)) : Field<ScrollContext> =
        let callback (handle: Raw.ScrollObserver) : obj = value handle; box ()
        Field.create<ScrollContext> "onLeaveForward" (box (callback))
    static member ``onEnterBackwardValue`` (value: (Raw.ScrollObserver -> obj)) : Field<ScrollContext> = Field.create<ScrollContext> "onEnterBackward" (box (value))
    static member ``onEnterBackward`` (value: (Raw.ScrollObserver -> unit)) : Field<ScrollContext> =
        let callback (handle: Raw.ScrollObserver) : obj = value handle; box ()
        Field.create<ScrollContext> "onEnterBackward" (box (callback))
    static member ``onLeaveBackwardValue`` (value: (Raw.ScrollObserver -> obj)) : Field<ScrollContext> = Field.create<ScrollContext> "onLeaveBackward" (box (value))
    static member ``onLeaveBackward`` (value: (Raw.ScrollObserver -> unit)) : Field<ScrollContext> =
        let callback (handle: Raw.ScrollObserver) : obj = value handle; box ()
        Field.create<ScrollContext> "onLeaveBackward" (box (callback))
    static member ``onUpdateValue`` (value: (Raw.ScrollObserver -> obj)) : Field<ScrollContext> = Field.create<ScrollContext> "onUpdate" (box (value))
    static member ``onUpdate`` (value: (Raw.ScrollObserver -> unit)) : Field<ScrollContext> =
        let callback (handle: Raw.ScrollObserver) : obj = value handle; box ()
        Field.create<ScrollContext> "onUpdate" (box (callback))
    static member ``onResizeValue`` (value: (Raw.ScrollObserver -> obj)) : Field<ScrollContext> = Field.create<ScrollContext> "onResize" (box (value))
    static member ``onResize`` (value: (Raw.ScrollObserver -> unit)) : Field<ScrollContext> =
        let callback (handle: Raw.ScrollObserver) : obj = value handle; box ()
        Field.create<ScrollContext> "onResize" (box (callback))
    static member ``onSyncCompleteValue`` (value: (Raw.ScrollObserver -> obj)) : Field<ScrollContext> = Field.create<ScrollContext> "onSyncComplete" (box (value))
    static member ``onSyncComplete`` (value: (Raw.ScrollObserver -> unit)) : Field<ScrollContext> =
        let callback (handle: Raw.ScrollObserver) : obj = value handle; box ()
        Field.create<ScrollContext> "onSyncComplete" (box (callback))
    static member parameters (options: Options<ScrollContext>) : Raw.ScrollObserverParams = Options.toObject options |> unbox

[<AbstractClass; Sealed>]
type ``Scope`` private () =
    static member ``root`` (value: U6<string, Raw.AngularRef, Fable.Core.TS.Dom.HTMLElement, Fable.Core.TS.Dom.NodeList, Raw.ReactRef, Fable.Core.TS.Dom.SVGElement>) : Field<ScopeContext> = Field.create<ScopeContext> "root" (box (value))
    static member ``root`` (value: string) : Field<ScopeContext> = Field.create<ScopeContext> "root" (box (U6.Case1 value))
    static member ``root`` (value: Raw.AngularRef) : Field<ScopeContext> = Field.create<ScopeContext> "root" (box (U6.Case2 value))
    static member ``root`` (value: Fable.Core.TS.Dom.HTMLElement) : Field<ScopeContext> = Field.create<ScopeContext> "root" (box (U6.Case3 value))
    static member ``root`` (value: Fable.Core.TS.Dom.NodeList) : Field<ScopeContext> = Field.create<ScopeContext> "root" (box (U6.Case4 value))
    static member ``root`` (value: Raw.ReactRef) : Field<ScopeContext> = Field.create<ScopeContext> "root" (box (U6.Case5 value))
    static member ``root`` (value: Fable.Core.TS.Dom.SVGElement) : Field<ScopeContext> = Field.create<ScopeContext> "root" (box (U6.Case6 value))
    static member ``defaults`` (value: Raw.DefaultsParams) : Field<ScopeContext> = Field.create<ScopeContext> "defaults" (box (value))
    static member ``mediaQueries`` (value: Fable.Core.JS.JS.Record<string, string>) : Field<ScopeContext> = Field.create<ScopeContext> "mediaQueries" (box (value))
    static member parameters (options: Options<ScopeContext>) : Raw.ScopeParams = Options.toObject options |> unbox

[<AbstractClass; Sealed>]
type ``TextSplitter`` private () =
    static member ``lines`` (value: U4<string, bool, ((U2<Fable.Core.TS.Dom.HTMLElement, Fable.Core.TS.Dom.Node>) option -> obj), Raw.SplitTemplateParams>) : Field<TextSplitterContext> = Field.create<TextSplitterContext> "lines" (box (value))
    static member ``lines`` (value: string) : Field<TextSplitterContext> = Field.create<TextSplitterContext> "lines" (box (U4.Case1 value))
    static member ``lines`` (value: bool) : Field<TextSplitterContext> = Field.create<TextSplitterContext> "lines" (box (U4.Case2 value))
    static member ``lines`` (value: ((U2<Fable.Core.TS.Dom.HTMLElement, Fable.Core.TS.Dom.Node>) option -> obj)) : Field<TextSplitterContext> = Field.create<TextSplitterContext> "lines" (box (U4.Case3 value))
    static member ``lines`` (value: Raw.SplitTemplateParams) : Field<TextSplitterContext> = Field.create<TextSplitterContext> "lines" (box (U4.Case4 value))
    static member ``words`` (value: U4<string, bool, ((U2<Fable.Core.TS.Dom.HTMLElement, Fable.Core.TS.Dom.Node>) option -> obj), Raw.SplitTemplateParams>) : Field<TextSplitterContext> = Field.create<TextSplitterContext> "words" (box (value))
    static member ``words`` (value: string) : Field<TextSplitterContext> = Field.create<TextSplitterContext> "words" (box (U4.Case1 value))
    static member ``words`` (value: bool) : Field<TextSplitterContext> = Field.create<TextSplitterContext> "words" (box (U4.Case2 value))
    static member ``words`` (value: ((U2<Fable.Core.TS.Dom.HTMLElement, Fable.Core.TS.Dom.Node>) option -> obj)) : Field<TextSplitterContext> = Field.create<TextSplitterContext> "words" (box (U4.Case3 value))
    static member ``words`` (value: Raw.SplitTemplateParams) : Field<TextSplitterContext> = Field.create<TextSplitterContext> "words" (box (U4.Case4 value))
    static member ``chars`` (value: U4<string, bool, ((U2<Fable.Core.TS.Dom.HTMLElement, Fable.Core.TS.Dom.Node>) option -> obj), Raw.SplitTemplateParams>) : Field<TextSplitterContext> = Field.create<TextSplitterContext> "chars" (box (value))
    static member ``chars`` (value: string) : Field<TextSplitterContext> = Field.create<TextSplitterContext> "chars" (box (U4.Case1 value))
    static member ``chars`` (value: bool) : Field<TextSplitterContext> = Field.create<TextSplitterContext> "chars" (box (U4.Case2 value))
    static member ``chars`` (value: ((U2<Fable.Core.TS.Dom.HTMLElement, Fable.Core.TS.Dom.Node>) option -> obj)) : Field<TextSplitterContext> = Field.create<TextSplitterContext> "chars" (box (U4.Case3 value))
    static member ``chars`` (value: Raw.SplitTemplateParams) : Field<TextSplitterContext> = Field.create<TextSplitterContext> "chars" (box (U4.Case4 value))
    static member ``accessible`` (value: bool) : Field<TextSplitterContext> = Field.create<TextSplitterContext> "accessible" (box (value))
    static member ``includeSpaces`` (value: bool) : Field<TextSplitterContext> = Field.create<TextSplitterContext> "includeSpaces" (box (value))
    static member ``debug`` (value: bool) : Field<TextSplitterContext> = Field.create<TextSplitterContext> "debug" (box (value))
    static member parameters (options: Options<TextSplitterContext>) : Raw.TextSplitterParams = Options.toObject options |> unbox

[<AbstractClass; Sealed>]
type ``Defaults`` private () =
    static member ``id`` (value: U2<string, float>) : Field<DefaultsContext> = Field.create<DefaultsContext> "id" (box (value))
    static member ``id`` (value: string) : Field<DefaultsContext> = Field.create<DefaultsContext> "id" (box (U2.Case1 value))
    static member ``id`` (value: float) : Field<DefaultsContext> = Field.create<DefaultsContext> "id" (box (U2.Case2 value))
    static member ``keyframes`` (value: U2<(Raw.DurationKeyframes.Item)[], Fable.Core.JS.JS.Record<string, Raw.PercentageKeyframes.Item>>) : Field<DefaultsContext> = Field.create<DefaultsContext> "keyframes" (box (value))
    static member ``keyframes`` (value: (Raw.DurationKeyframes.Item)[]) : Field<DefaultsContext> = Field.create<DefaultsContext> "keyframes" (box (U2.Case1 value))
    static member ``keyframes`` (value: Fable.Core.JS.JS.Record<string, Raw.PercentageKeyframes.Item>) : Field<DefaultsContext> = Field.create<DefaultsContext> "keyframes" (box (U2.Case2 value))
    static member ``playbackEase`` (value: U4<string, (float -> float), Raw.Spring, Raw.TweakRegister>) : Field<DefaultsContext> = Field.create<DefaultsContext> "playbackEase" (box (value))
    static member ``playbackEase`` (value: string) : Field<DefaultsContext> = Field.create<DefaultsContext> "playbackEase" (box (U4.Case1 value))
    static member ``playbackEase`` (value: (float -> float)) : Field<DefaultsContext> = Field.create<DefaultsContext> "playbackEase" (box (U4.Case2 value))
    static member ``playbackEase`` (value: Raw.Spring) : Field<DefaultsContext> = Field.create<DefaultsContext> "playbackEase" (box (U4.Case3 value))
    static member ``playbackEase`` (value: Raw.TweakRegister) : Field<DefaultsContext> = Field.create<DefaultsContext> "playbackEase" (box (U4.Case4 value))
    static member ``playbackRate`` (value: float) : Field<DefaultsContext> = Field.create<DefaultsContext> "playbackRate" (box (value))
    static member ``frameRate`` (value: float) : Field<DefaultsContext> = Field.create<DefaultsContext> "frameRate" (box (value))
    static member ``loop`` (value: U2<float, bool>) : Field<DefaultsContext> = Field.create<DefaultsContext> "loop" (box (value))
    static member ``loop`` (value: float) : Field<DefaultsContext> = Field.create<DefaultsContext> "loop" (box (U2.Case1 value))
    static member ``loop`` (value: bool) : Field<DefaultsContext> = Field.create<DefaultsContext> "loop" (box (U2.Case2 value))
    static member ``reversed`` (value: bool) : Field<DefaultsContext> = Field.create<DefaultsContext> "reversed" (box (value))
    static member ``alternate`` (value: bool) : Field<DefaultsContext> = Field.create<DefaultsContext> "alternate" (box (value))
    static member ``persist`` (value: bool) : Field<DefaultsContext> = Field.create<DefaultsContext> "persist" (box (value))
    static member ``autoplay`` (value: U2<bool, Raw.ScrollObserver>) : Field<DefaultsContext> = Field.create<DefaultsContext> "autoplay" (box (value))
    static member ``autoplay`` (value: bool) : Field<DefaultsContext> = Field.create<DefaultsContext> "autoplay" (box (U2.Case1 value))
    static member ``autoplay`` (value: Raw.ScrollObserver) : Field<DefaultsContext> = Field.create<DefaultsContext> "autoplay" (box (U2.Case2 value))
    static member ``duration`` (value: U2<float, Raw.DurationKeyframes.Item.Duration>) : Field<DefaultsContext> = Field.create<DefaultsContext> "duration" (box (value))
    static member ``duration`` (value: float) : Field<DefaultsContext> = Field.create<DefaultsContext> "duration" (box (U2.Case1 value))
    static member ``duration`` (value: Raw.DurationKeyframes.Item.Duration) : Field<DefaultsContext> = Field.create<DefaultsContext> "duration" (box (U2.Case2 value))
    static member ``delay`` (value: U2<float, Raw.DurationKeyframes.Item.Duration>) : Field<DefaultsContext> = Field.create<DefaultsContext> "delay" (box (value))
    static member ``delay`` (value: float) : Field<DefaultsContext> = Field.create<DefaultsContext> "delay" (box (U2.Case1 value))
    static member ``delay`` (value: Raw.DurationKeyframes.Item.Duration) : Field<DefaultsContext> = Field.create<DefaultsContext> "delay" (box (U2.Case2 value))
    static member ``loopDelay`` (value: float) : Field<DefaultsContext> = Field.create<DefaultsContext> "loopDelay" (box (value))
    static member ``ease`` (value: U5<string, (float -> float), Raw.DurationKeyframes.Item.Duration, Raw.Spring, Raw.TweakRegister>) : Field<DefaultsContext> = Field.create<DefaultsContext> "ease" (box (value))
    static member ``ease`` (value: string) : Field<DefaultsContext> = Field.create<DefaultsContext> "ease" (box (U5.Case1 value))
    static member ``ease`` (value: (float -> float)) : Field<DefaultsContext> = Field.create<DefaultsContext> "ease" (box (U5.Case2 value))
    static member ``ease`` (value: Raw.DurationKeyframes.Item.Duration) : Field<DefaultsContext> = Field.create<DefaultsContext> "ease" (box (U5.Case3 value))
    static member ``ease`` (value: Raw.Spring) : Field<DefaultsContext> = Field.create<DefaultsContext> "ease" (box (U5.Case4 value))
    static member ``ease`` (value: Raw.TweakRegister) : Field<DefaultsContext> = Field.create<DefaultsContext> "ease" (box (U5.Case5 value))
    static member ``composition`` (value: U2<float, string>) : Field<DefaultsContext> = Field.create<DefaultsContext> "composition" (box (value))
    static member ``composition`` (value: float) : Field<DefaultsContext> = Field.create<DefaultsContext> "composition" (box (U2.Case1 value))
    static member ``composition`` (value: string) : Field<DefaultsContext> = Field.create<DefaultsContext> "composition" (box (U2.Case2 value))
    static member ``modifier`` (value: (obj -> obj)) : Field<DefaultsContext> = Field.create<DefaultsContext> "modifier" (box (value))
    static member ``onBeginValue`` (value: (U3<Raw.JSAnimation, Raw.Timeline, Raw.Timer> -> obj)) : Field<DefaultsContext> = Field.create<DefaultsContext> "onBegin" (box (value))
    static member ``onBegin`` (value: (U3<Raw.JSAnimation, Raw.Timeline, Raw.Timer> -> unit)) : Field<DefaultsContext> =
        let callback (handle: U3<Raw.JSAnimation, Raw.Timeline, Raw.Timer>) : obj = value handle; box ()
        Field.create<DefaultsContext> "onBegin" (box (callback))
    static member ``onBeforeUpdateValue`` (value: (U3<Raw.JSAnimation, Raw.Timeline, Raw.Timer> -> obj)) : Field<DefaultsContext> = Field.create<DefaultsContext> "onBeforeUpdate" (box (value))
    static member ``onBeforeUpdate`` (value: (U3<Raw.JSAnimation, Raw.Timeline, Raw.Timer> -> unit)) : Field<DefaultsContext> =
        let callback (handle: U3<Raw.JSAnimation, Raw.Timeline, Raw.Timer>) : obj = value handle; box ()
        Field.create<DefaultsContext> "onBeforeUpdate" (box (callback))
    static member ``onUpdateValue`` (value: (U3<Raw.JSAnimation, Raw.Timeline, Raw.Timer> -> obj)) : Field<DefaultsContext> = Field.create<DefaultsContext> "onUpdate" (box (value))
    static member ``onUpdate`` (value: (U3<Raw.JSAnimation, Raw.Timeline, Raw.Timer> -> unit)) : Field<DefaultsContext> =
        let callback (handle: U3<Raw.JSAnimation, Raw.Timeline, Raw.Timer>) : obj = value handle; box ()
        Field.create<DefaultsContext> "onUpdate" (box (callback))
    static member ``onLoopValue`` (value: (U3<Raw.JSAnimation, Raw.Timeline, Raw.Timer> -> obj)) : Field<DefaultsContext> = Field.create<DefaultsContext> "onLoop" (box (value))
    static member ``onLoop`` (value: (U3<Raw.JSAnimation, Raw.Timeline, Raw.Timer> -> unit)) : Field<DefaultsContext> =
        let callback (handle: U3<Raw.JSAnimation, Raw.Timeline, Raw.Timer>) : obj = value handle; box ()
        Field.create<DefaultsContext> "onLoop" (box (callback))
    static member ``onPauseValue`` (value: (U3<Raw.JSAnimation, Raw.Timeline, Raw.Timer> -> obj)) : Field<DefaultsContext> = Field.create<DefaultsContext> "onPause" (box (value))
    static member ``onPause`` (value: (U3<Raw.JSAnimation, Raw.Timeline, Raw.Timer> -> unit)) : Field<DefaultsContext> =
        let callback (handle: U3<Raw.JSAnimation, Raw.Timeline, Raw.Timer>) : obj = value handle; box ()
        Field.create<DefaultsContext> "onPause" (box (callback))
    static member ``onCompleteValue`` (value: (U3<Raw.JSAnimation, Raw.Timeline, Raw.Timer> -> obj)) : Field<DefaultsContext> = Field.create<DefaultsContext> "onComplete" (box (value))
    static member ``onComplete`` (value: (U3<Raw.JSAnimation, Raw.Timeline, Raw.Timer> -> unit)) : Field<DefaultsContext> =
        let callback (handle: U3<Raw.JSAnimation, Raw.Timeline, Raw.Timer>) : obj = value handle; box ()
        Field.create<DefaultsContext> "onComplete" (box (callback))
    static member ``onRenderValue`` (value: (U2<Raw.JSAnimation, Raw.Timeline> -> obj)) : Field<DefaultsContext> = Field.create<DefaultsContext> "onRender" (box (value))
    static member ``onRender`` (value: (U2<Raw.JSAnimation, Raw.Timeline> -> unit)) : Field<DefaultsContext> =
        let callback (handle: U2<Raw.JSAnimation, Raw.Timeline>) : obj = value handle; box ()
        Field.create<DefaultsContext> "onRender" (box (callback))
    static member parameters (options: Options<DefaultsContext>) : Raw.DefaultsParams = Options.toObject options |> unbox

[<AbstractClass; Sealed>]
type ``DurationKeyframe`` private () =
    static member ``duration`` (value: U6<string, float, (float -> float), Raw.DurationKeyframes.Item.Duration, Raw.Spring, Raw.TweakRegister>) : Field<DurationKeyframeContext> = Field.create<DurationKeyframeContext> "duration" (box (value))
    static member ``duration`` (value: string) : Field<DurationKeyframeContext> = Field.create<DurationKeyframeContext> "duration" (box (U6.Case1 value))
    static member ``duration`` (value: float) : Field<DurationKeyframeContext> = Field.create<DurationKeyframeContext> "duration" (box (U6.Case2 value))
    static member ``duration`` (value: (float -> float)) : Field<DurationKeyframeContext> = Field.create<DurationKeyframeContext> "duration" (box (U6.Case3 value))
    static member ``duration`` (value: Raw.DurationKeyframes.Item.Duration) : Field<DurationKeyframeContext> = Field.create<DurationKeyframeContext> "duration" (box (U6.Case4 value))
    static member ``duration`` (value: Raw.Spring) : Field<DurationKeyframeContext> = Field.create<DurationKeyframeContext> "duration" (box (U6.Case5 value))
    static member ``duration`` (value: Raw.TweakRegister) : Field<DurationKeyframeContext> = Field.create<DurationKeyframeContext> "duration" (box (U6.Case6 value))
    static member ``delay`` (value: U6<string, float, (float -> float), Raw.DurationKeyframes.Item.Duration, Raw.Spring, Raw.TweakRegister>) : Field<DurationKeyframeContext> = Field.create<DurationKeyframeContext> "delay" (box (value))
    static member ``delay`` (value: string) : Field<DurationKeyframeContext> = Field.create<DurationKeyframeContext> "delay" (box (U6.Case1 value))
    static member ``delay`` (value: float) : Field<DurationKeyframeContext> = Field.create<DurationKeyframeContext> "delay" (box (U6.Case2 value))
    static member ``delay`` (value: (float -> float)) : Field<DurationKeyframeContext> = Field.create<DurationKeyframeContext> "delay" (box (U6.Case3 value))
    static member ``delay`` (value: Raw.DurationKeyframes.Item.Duration) : Field<DurationKeyframeContext> = Field.create<DurationKeyframeContext> "delay" (box (U6.Case4 value))
    static member ``delay`` (value: Raw.Spring) : Field<DurationKeyframeContext> = Field.create<DurationKeyframeContext> "delay" (box (U6.Case5 value))
    static member ``delay`` (value: Raw.TweakRegister) : Field<DurationKeyframeContext> = Field.create<DurationKeyframeContext> "delay" (box (U6.Case6 value))
    static member ``ease`` (value: U5<string, (float -> float), Raw.DurationKeyframes.Item.Duration, Raw.Spring, Raw.TweakRegister>) : Field<DurationKeyframeContext> = Field.create<DurationKeyframeContext> "ease" (box (value))
    static member ``ease`` (value: string) : Field<DurationKeyframeContext> = Field.create<DurationKeyframeContext> "ease" (box (U5.Case1 value))
    static member ``ease`` (value: (float -> float)) : Field<DurationKeyframeContext> = Field.create<DurationKeyframeContext> "ease" (box (U5.Case2 value))
    static member ``ease`` (value: Raw.DurationKeyframes.Item.Duration) : Field<DurationKeyframeContext> = Field.create<DurationKeyframeContext> "ease" (box (U5.Case3 value))
    static member ``ease`` (value: Raw.Spring) : Field<DurationKeyframeContext> = Field.create<DurationKeyframeContext> "ease" (box (U5.Case4 value))
    static member ``ease`` (value: Raw.TweakRegister) : Field<DurationKeyframeContext> = Field.create<DurationKeyframeContext> "ease" (box (U5.Case5 value))
    static member ``modifier`` (value: (float -> U2<string, float>)) : Field<DurationKeyframeContext> = Field.create<DurationKeyframeContext> "modifier" (box (value))
    static member ``composition`` (value: U2<float, string>) : Field<DurationKeyframeContext> = Field.create<DurationKeyframeContext> "composition" (box (value))
    static member ``composition`` (value: float) : Field<DurationKeyframeContext> = Field.create<DurationKeyframeContext> "composition" (box (U2.Case1 value))
    static member ``composition`` (value: string) : Field<DurationKeyframeContext> = Field.create<DurationKeyframeContext> "composition" (box (U2.Case2 value))
    static member parameters (options: Options<DurationKeyframeContext>) : Raw.DurationKeyframes.Item = Options.toObject options |> unbox
    static member property (key: string, value: Raw.U10<string, float, bool, (U8<string, float, (float -> float), Raw.DurationKeyframes.Item.Duration, Raw.Spring, Raw.TweakRegister, (U6<string, float, (float -> float), Raw.DurationKeyframes.Item.Duration, Raw.Spring, Raw.TweakRegister> * U6<string, float, (float -> float), Raw.DurationKeyframes.Item.Duration, Raw.Spring, Raw.TweakRegister>), Raw.TweenKeyValue>)[], (float -> float), Raw.DurationKeyframes.Item.Duration, Raw.Spring, Raw.TweakRegister, (float -> U2<string, float>), Raw.TweenKeyValue>) : Field<DurationKeyframeContext> =
        if System.String.IsNullOrWhiteSpace key || List.contains key ["duration"; "delay"; "ease"; "modifier"; "composition"; "__proto__"; "constructor"; "prototype"] then invalidArg "key" "Custom keys must be nonempty and cannot name an option."
        Field.create<DurationKeyframeContext> key (box (value))
    static member property (key: string, value: string) : Field<DurationKeyframeContext> =
        if System.String.IsNullOrWhiteSpace key || List.contains key ["duration"; "delay"; "ease"; "modifier"; "composition"; "__proto__"; "constructor"; "prototype"] then invalidArg "key" "Custom keys must be nonempty and cannot name an option."
        Field.create<DurationKeyframeContext> key (box (Raw.U10.Case1 (value)))
    static member property (key: string, value: float) : Field<DurationKeyframeContext> =
        if System.String.IsNullOrWhiteSpace key || List.contains key ["duration"; "delay"; "ease"; "modifier"; "composition"; "__proto__"; "constructor"; "prototype"] then invalidArg "key" "Custom keys must be nonempty and cannot name an option."
        Field.create<DurationKeyframeContext> key (box (Raw.U10.Case2 (value)))
    static member property (key: string, value: bool) : Field<DurationKeyframeContext> =
        if System.String.IsNullOrWhiteSpace key || List.contains key ["duration"; "delay"; "ease"; "modifier"; "composition"; "__proto__"; "constructor"; "prototype"] then invalidArg "key" "Custom keys must be nonempty and cannot name an option."
        Field.create<DurationKeyframeContext> key (box (Raw.U10.Case3 (value)))
    static member property (key: string, value: (U8<string, float, (float -> float), Raw.DurationKeyframes.Item.Duration, Raw.Spring, Raw.TweakRegister, (U6<string, float, (float -> float), Raw.DurationKeyframes.Item.Duration, Raw.Spring, Raw.TweakRegister> * U6<string, float, (float -> float), Raw.DurationKeyframes.Item.Duration, Raw.Spring, Raw.TweakRegister>), Raw.TweenKeyValue>)[]) : Field<DurationKeyframeContext> =
        if System.String.IsNullOrWhiteSpace key || List.contains key ["duration"; "delay"; "ease"; "modifier"; "composition"; "__proto__"; "constructor"; "prototype"] then invalidArg "key" "Custom keys must be nonempty and cannot name an option."
        Field.create<DurationKeyframeContext> key (box (Raw.U10.Case4 (value)))
    static member property (key: string, value: (float -> float)) : Field<DurationKeyframeContext> =
        if System.String.IsNullOrWhiteSpace key || List.contains key ["duration"; "delay"; "ease"; "modifier"; "composition"; "__proto__"; "constructor"; "prototype"] then invalidArg "key" "Custom keys must be nonempty and cannot name an option."
        Field.create<DurationKeyframeContext> key (box (Raw.U10.Case5 (value)))
    static member property (key: string, value: Raw.DurationKeyframes.Item.Duration) : Field<DurationKeyframeContext> =
        if System.String.IsNullOrWhiteSpace key || List.contains key ["duration"; "delay"; "ease"; "modifier"; "composition"; "__proto__"; "constructor"; "prototype"] then invalidArg "key" "Custom keys must be nonempty and cannot name an option."
        Field.create<DurationKeyframeContext> key (box (Raw.U10.Case6 (value)))
    static member property (key: string, value: Raw.Spring) : Field<DurationKeyframeContext> =
        if System.String.IsNullOrWhiteSpace key || List.contains key ["duration"; "delay"; "ease"; "modifier"; "composition"; "__proto__"; "constructor"; "prototype"] then invalidArg "key" "Custom keys must be nonempty and cannot name an option."
        Field.create<DurationKeyframeContext> key (box (Raw.U10.Case7 (value)))
    static member property (key: string, value: Raw.TweakRegister) : Field<DurationKeyframeContext> =
        if System.String.IsNullOrWhiteSpace key || List.contains key ["duration"; "delay"; "ease"; "modifier"; "composition"; "__proto__"; "constructor"; "prototype"] then invalidArg "key" "Custom keys must be nonempty and cannot name an option."
        Field.create<DurationKeyframeContext> key (box (Raw.U10.Case8 (value)))
    static member property (key: string, value: (float -> U2<string, float>)) : Field<DurationKeyframeContext> =
        if System.String.IsNullOrWhiteSpace key || List.contains key ["duration"; "delay"; "ease"; "modifier"; "composition"; "__proto__"; "constructor"; "prototype"] then invalidArg "key" "Custom keys must be nonempty and cannot name an option."
        Field.create<DurationKeyframeContext> key (box (Raw.U10.Case9 (value)))
    static member property (key: string, value: Raw.TweenKeyValue) : Field<DurationKeyframeContext> =
        if System.String.IsNullOrWhiteSpace key || List.contains key ["duration"; "delay"; "ease"; "modifier"; "composition"; "__proto__"; "constructor"; "prototype"] then invalidArg "key" "Custom keys must be nonempty and cannot name an option."
        Field.create<DurationKeyframeContext> key (box (Raw.U10.Case10 (value)))

[<AbstractClass; Sealed>]
type ``PercentageKeyframe`` private () =
    static member ``ease`` (value: U4<string, (float -> float), Raw.Spring, Raw.TweakRegister>) : Field<PercentageKeyframeContext> = Field.create<PercentageKeyframeContext> "ease" (box (value))
    static member ``ease`` (value: string) : Field<PercentageKeyframeContext> = Field.create<PercentageKeyframeContext> "ease" (box (U4.Case1 value))
    static member ``ease`` (value: (float -> float)) : Field<PercentageKeyframeContext> = Field.create<PercentageKeyframeContext> "ease" (box (U4.Case2 value))
    static member ``ease`` (value: Raw.Spring) : Field<PercentageKeyframeContext> = Field.create<PercentageKeyframeContext> "ease" (box (U4.Case3 value))
    static member ``ease`` (value: Raw.TweakRegister) : Field<PercentageKeyframeContext> = Field.create<PercentageKeyframeContext> "ease" (box (U4.Case4 value))
    static member parameters (options: Options<PercentageKeyframeContext>) : Raw.PercentageKeyframes.Item = Options.toObject options |> unbox
    static member property (key: string, value: U6<string, float, (float -> float), Raw.DurationKeyframes.Item.Duration, Raw.Spring, Raw.TweakRegister>) : Field<PercentageKeyframeContext> =
        if System.String.IsNullOrWhiteSpace key || List.contains key ["ease"; "__proto__"; "constructor"; "prototype"] then invalidArg "key" "Custom keys must be nonempty and cannot name an option."
        Field.create<PercentageKeyframeContext> key (box (value))
    static member property (key: string, value: string) : Field<PercentageKeyframeContext> =
        if System.String.IsNullOrWhiteSpace key || List.contains key ["ease"; "__proto__"; "constructor"; "prototype"] then invalidArg "key" "Custom keys must be nonempty and cannot name an option."
        Field.create<PercentageKeyframeContext> key (box (U6.Case1 (value)))
    static member property (key: string, value: float) : Field<PercentageKeyframeContext> =
        if System.String.IsNullOrWhiteSpace key || List.contains key ["ease"; "__proto__"; "constructor"; "prototype"] then invalidArg "key" "Custom keys must be nonempty and cannot name an option."
        Field.create<PercentageKeyframeContext> key (box (U6.Case2 (value)))
    static member property (key: string, value: (float -> float)) : Field<PercentageKeyframeContext> =
        if System.String.IsNullOrWhiteSpace key || List.contains key ["ease"; "__proto__"; "constructor"; "prototype"] then invalidArg "key" "Custom keys must be nonempty and cannot name an option."
        Field.create<PercentageKeyframeContext> key (box (U6.Case3 (value)))
    static member property (key: string, value: Raw.DurationKeyframes.Item.Duration) : Field<PercentageKeyframeContext> =
        if System.String.IsNullOrWhiteSpace key || List.contains key ["ease"; "__proto__"; "constructor"; "prototype"] then invalidArg "key" "Custom keys must be nonempty and cannot name an option."
        Field.create<PercentageKeyframeContext> key (box (U6.Case4 (value)))
    static member property (key: string, value: Raw.Spring) : Field<PercentageKeyframeContext> =
        if System.String.IsNullOrWhiteSpace key || List.contains key ["ease"; "__proto__"; "constructor"; "prototype"] then invalidArg "key" "Custom keys must be nonempty and cannot name an option."
        Field.create<PercentageKeyframeContext> key (box (U6.Case5 (value)))
    static member property (key: string, value: Raw.TweakRegister) : Field<PercentageKeyframeContext> =
        if System.String.IsNullOrWhiteSpace key || List.contains key ["ease"; "__proto__"; "constructor"; "prototype"] then invalidArg "key" "Custom keys must be nonempty and cannot name an option."
        Field.create<PercentageKeyframeContext> key (box (U6.Case6 (value)))

[<AbstractClass; Sealed>]
type ``DragAxis`` private () =
    static member ``mapTo`` (value: string) : Field<DragAxisContext> = Field.create<DragAxisContext> "mapTo" (box (value))
    static member ``modifier`` (value: (float -> U2<string, float>)) : Field<DragAxisContext> = Field.create<DragAxisContext> "modifier" (box (value))
    static member ``composition`` (value: U2<float, string>) : Field<DragAxisContext> = Field.create<DragAxisContext> "composition" (box (value))
    static member ``composition`` (value: float) : Field<DragAxisContext> = Field.create<DragAxisContext> "composition" (box (U2.Case1 value))
    static member ``composition`` (value: string) : Field<DragAxisContext> = Field.create<DragAxisContext> "composition" (box (U2.Case2 value))
    static member ``snap`` (value: U3<float, (float)[], (Raw.Draggable -> U2<float, (float)[]>)>) : Field<DragAxisContext> = Field.create<DragAxisContext> "snap" (box (value))
    static member ``snap`` (value: float) : Field<DragAxisContext> = Field.create<DragAxisContext> "snap" (box (U3.Case1 value))
    static member ``snap`` (value: (float)[]) : Field<DragAxisContext> = Field.create<DragAxisContext> "snap" (box (U3.Case2 value))
    static member ``snap`` (value: (Raw.Draggable -> U2<float, (float)[]>)) : Field<DragAxisContext> = Field.create<DragAxisContext> "snap" (box (U3.Case3 value))
    static member parameters (options: Options<DragAxisContext>) : Raw.DraggableAxisParam = Options.toObject options |> unbox

[<AbstractClass; Sealed>]
type ``DragCursor`` private () =
    static member ``onHover`` (value: string) : Field<DragCursorContext> = Field.create<DragCursorContext> "onHover" (box (value))
    static member ``onGrab`` (value: string) : Field<DragCursorContext> = Field.create<DragCursorContext> "onGrab" (box (value))
    static member parameters (options: Options<DragCursorContext>) : Raw.DraggableCursorParams = Options.toObject options |> unbox

[<AbstractClass; Sealed>]
type ``DragThreshold`` private () =
    static member ``mouse`` (value: float) : Field<DragThresholdContext> = Field.create<DragThresholdContext> "mouse" (box (value))
    static member ``touch`` (value: float) : Field<DragThresholdContext> = Field.create<DragThresholdContext> "touch" (box (value))
    static member parameters (options: Options<DragThresholdContext>) : Raw.DraggableDragThresholdParams = Options.toObject options |> unbox

[<AbstractClass; Sealed>]
type ``AnimatableProperty`` private () =
    static member ``unit`` (value: string) : Field<AnimatablePropertyContext> = Field.create<AnimatablePropertyContext> "unit" (box (value))
    static member ``duration`` (value: U6<string, float, (float -> float), Raw.DurationKeyframes.Item.Duration, Raw.Spring, Raw.TweakRegister>) : Field<AnimatablePropertyContext> = Field.create<AnimatablePropertyContext> "duration" (box (value))
    static member ``duration`` (value: string) : Field<AnimatablePropertyContext> = Field.create<AnimatablePropertyContext> "duration" (box (U6.Case1 value))
    static member ``duration`` (value: float) : Field<AnimatablePropertyContext> = Field.create<AnimatablePropertyContext> "duration" (box (U6.Case2 value))
    static member ``duration`` (value: (float -> float)) : Field<AnimatablePropertyContext> = Field.create<AnimatablePropertyContext> "duration" (box (U6.Case3 value))
    static member ``duration`` (value: Raw.DurationKeyframes.Item.Duration) : Field<AnimatablePropertyContext> = Field.create<AnimatablePropertyContext> "duration" (box (U6.Case4 value))
    static member ``duration`` (value: Raw.Spring) : Field<AnimatablePropertyContext> = Field.create<AnimatablePropertyContext> "duration" (box (U6.Case5 value))
    static member ``duration`` (value: Raw.TweakRegister) : Field<AnimatablePropertyContext> = Field.create<AnimatablePropertyContext> "duration" (box (U6.Case6 value))
    static member ``ease`` (value: U4<string, (float -> float), Raw.Spring, Raw.TweakRegister>) : Field<AnimatablePropertyContext> = Field.create<AnimatablePropertyContext> "ease" (box (value))
    static member ``ease`` (value: string) : Field<AnimatablePropertyContext> = Field.create<AnimatablePropertyContext> "ease" (box (U4.Case1 value))
    static member ``ease`` (value: (float -> float)) : Field<AnimatablePropertyContext> = Field.create<AnimatablePropertyContext> "ease" (box (U4.Case2 value))
    static member ``ease`` (value: Raw.Spring) : Field<AnimatablePropertyContext> = Field.create<AnimatablePropertyContext> "ease" (box (U4.Case3 value))
    static member ``ease`` (value: Raw.TweakRegister) : Field<AnimatablePropertyContext> = Field.create<AnimatablePropertyContext> "ease" (box (U4.Case4 value))
    static member ``modifier`` (value: (float -> U2<string, float>)) : Field<AnimatablePropertyContext> = Field.create<AnimatablePropertyContext> "modifier" (box (value))
    static member ``composition`` (value: U2<float, string>) : Field<AnimatablePropertyContext> = Field.create<AnimatablePropertyContext> "composition" (box (value))
    static member ``composition`` (value: float) : Field<AnimatablePropertyContext> = Field.create<AnimatablePropertyContext> "composition" (box (U2.Case1 value))
    static member ``composition`` (value: string) : Field<AnimatablePropertyContext> = Field.create<AnimatablePropertyContext> "composition" (box (U2.Case2 value))
    static member parameters (options: Options<AnimatablePropertyContext>) : Raw.AnimatablePropertyParamsOptions = Options.toObject options |> unbox

[<AbstractClass; Sealed>]
type ``ScrollThreshold`` private () =
    static member ``target`` (value: U2<string, float>) : Field<ScrollThresholdContext> = Field.create<ScrollThresholdContext> "target" (box (value))
    static member ``target`` (value: string) : Field<ScrollThresholdContext> = Field.create<ScrollThresholdContext> "target" (box (U2.Case1 value))
    static member ``target`` (value: float) : Field<ScrollThresholdContext> = Field.create<ScrollThresholdContext> "target" (box (U2.Case2 value))
    static member ``container`` (value: U2<string, float>) : Field<ScrollThresholdContext> = Field.create<ScrollThresholdContext> "container" (box (value))
    static member ``container`` (value: string) : Field<ScrollThresholdContext> = Field.create<ScrollThresholdContext> "container" (box (U2.Case1 value))
    static member ``container`` (value: float) : Field<ScrollThresholdContext> = Field.create<ScrollThresholdContext> "container" (box (U2.Case2 value))
    static member parameters (options: Options<ScrollThresholdContext>) : Raw.ScrollThresholdParam = Options.toObject options |> unbox

[<AbstractClass; Sealed>]
type ``SplitTemplate`` private () =
    static member ``class`` (value: U2<string, bool>) : Field<SplitTemplateContext> = Field.create<SplitTemplateContext> "class" (box (value))
    static member ``class`` (value: string) : Field<SplitTemplateContext> = Field.create<SplitTemplateContext> "class" (box (U2.Case1 value))
    static member ``class`` (value: bool) : Field<SplitTemplateContext> = Field.create<SplitTemplateContext> "class" (box (U2.Case2 value))
    static member ``wrap`` (value: Raw.SplitTemplateParams.Wrap) : Field<SplitTemplateContext> = Field.create<SplitTemplateContext> "wrap" (box (value))
    static member ``clone`` (value: Raw.SplitTemplateParams.Clone) : Field<SplitTemplateContext> = Field.create<SplitTemplateContext> "clone" (box (value))
    static member parameters (options: Options<SplitTemplateContext>) : Raw.SplitTemplateParams = Options.toObject options |> unbox

[<AbstractClass; Sealed>]
type ``Keyframes`` private () =
    static member duration (frames: Options<DurationKeyframeContext> list) : (Raw.DurationKeyframes.Item)[] = frames |> List.map DurationKeyframe.parameters |> List.toArray
    static member at (percent: float) (options: Options<PercentageKeyframeContext>) : PercentageStop =
        if System.Double.IsNaN percent || System.Double.IsInfinity percent || percent < 0. || percent > 100. then invalidArg "percent" "Percentage must be finite and between zero and 100."
        PercentageStop (percent.ToString("0.################", System.Globalization.CultureInfo.InvariantCulture) + "%", options)
    static member percentage (frames: PercentageStop list) : Fable.Core.JS.JS.Record<string, Raw.PercentageKeyframes.Item> =
        frames |> List.map (fun (PercentageStop (key, options)) -> key, box (``PercentageKeyframe``.parameters options)) |> createObj |> unbox

[<AbstractClass; Sealed>]
type ``Css`` private () =
    /// The accent-color CSS property sets the accent color for user-interface controls generated by some elements. MDN Reference
    static member ``accentColor`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "accentColor" (box (value))
    /// The CSS align-content property sets the distribution of space between and around content items along a flexbox&apos;s cross axis, or a grid or block-level element&apos;s block axis. MDN Reference
    static member ``alignContent`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "alignContent" (box (value))
    /// The CSS align-items property sets the align-self value on all direct children as a group. In flexbox, it controls the alignment of items on the cross axis. In grid layout, it controls the alignment of items on the block axis within their grid areas. MDN Reference
    static member ``alignItems`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "alignItems" (box (value))
    /// The align-self CSS property overrides a grid or flex item&apos;s align-items value. In grid, it aligns the item inside the grid area. In flexbox, it aligns the item on the cross axis. MDN Reference
    static member ``alignSelf`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "alignSelf" (box (value))
    /// The alignment-baseline CSS property specifies the specific baseline used to align the box&apos;s text and inline-level contents. Baseline alignment is the relationship among the baselines of multiple alignment subjects within an alignment context. When performing baseline alignment, the alignment-baseline property value specifies which baseline of the box is aligned to the corresponding baseline of its alignment context. MDN Reference
    static member ``alignmentBaseline`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "alignmentBaseline" (box (value))
    /// The all shorthand CSS property resets all of an element&apos;s properties except unicode-bidi, direction, and CSS Custom Properties. It can set properties to their initial or inherited values, or to the values specified in another cascade layer or stylesheet origin. MDN Reference
    static member ``all`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "all" (box (value))
    /// The anchor-name CSS property enables defining an element as an anchor element by giving it one or more identifying anchor names. Each name can then be set as the value of a positioned element&apos;s position-anchor property to associate it with the anchor. MDN Reference
    static member ``anchorName`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "anchorName" (box (value))
    /// The anchor-scope CSS property can be used to limit the scope in which a positioned element can be associated with anchor elements to a particular subtree. MDN Reference
    static member ``anchorScope`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "anchorScope" (box (value))
    /// The animation shorthand CSS property applies an animation between styles. It is a shorthand for animation-name, animation-duration, animation-timing-function, animation-delay, animation-iteration-count, animation-direction, animation-fill-mode, animation-play-state, and animation-timeline. MDN Reference
    static member ``animation`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "animation" (box (value))
    /// The animation-composition CSS property specifies the composite operation to use when multiple animations affect the same property simultaneously. MDN Reference
    static member ``animationComposition`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "animationComposition" (box (value))
    /// The animation-delay CSS property specifies the amount of time to wait from applying the animation to an element before beginning to perform the animation. The animation can start later, immediately from its beginning, or immediately and partway through the animation. MDN Reference
    static member ``animationDelay`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "animationDelay" (box (value))
    /// The animation-direction CSS property sets whether an animation should play forward, backward, or alternate back and forth between playing the sequence forward and backward. MDN Reference
    static member ``animationDirection`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "animationDirection" (box (value))
    /// The animation-duration CSS property sets the length of time that an animation takes to complete one cycle. MDN Reference
    static member ``animationDuration`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "animationDuration" (box (value))
    /// The animation-fill-mode CSS property sets how a CSS animation applies styles to its target before and after its execution. MDN Reference
    static member ``animationFillMode`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "animationFillMode" (box (value))
    /// The animation-iteration-count CSS property sets the number of times an animation sequence should be played before stopping. MDN Reference
    static member ``animationIterationCount`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "animationIterationCount" (box (value))
    /// The animation-name CSS property specifies the names of one or more @keyframes at-rules that describe the animation to apply to an element. Multiple@keyframes at-rules are specified as a comma-separated list of names. If the specified name does not match any @keyframes at-rule, no properties are animated. MDN Reference
    static member ``animationName`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "animationName" (box (value))
    /// The animation-play-state CSS property sets whether an animation is running or paused. MDN Reference
    static member ``animationPlayState`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "animationPlayState" (box (value))
    /// The animation-range CSS shorthand property is used to set the start and end of an animation&apos;s attachment range along its timeline, i.e., where along the timeline an animation will start and end. MDN Reference
    static member ``animationRange`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "animationRange" (box (value))
    /// The animation-range-end CSS property sets the point on the timeline where an animation should end. MDN Reference
    static member ``animationRangeEnd`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "animationRangeEnd" (box (value))
    /// The animation-range-start CSS property sets the point on the timeline where an animation should start. MDN Reference
    static member ``animationRangeStart`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "animationRangeStart" (box (value))
    /// The animation-timeline CSS property specifies the timeline used to control the progress of a CSS animation. MDN Reference
    static member ``animationTimeline`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "animationTimeline" (box (value))
    /// The animation-timing-function CSS property sets how an animation progresses through the duration of each cycle. MDN Reference
    static member ``animationTimingFunction`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "animationTimingFunction" (box (value))
    /// The appearance CSS property specifies the rendered appearance of replaced UI widget elements such as form controls. Most commonly, such elements are given native, platform-specific styling based on the operating system&apos;s theme, or a primitive appearance with styles that can be overridden using CSS. MDN Reference
    static member ``appearance`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "appearance" (box (value))
    /// The aspect-ratio CSS property allows you to define the desired width-to-height ratio of an element&apos;s box. This means that even if the parent container or viewport size changes, the browser will adjust the element&apos;s dimensions to maintain the specified width-to-height ratio. The specified aspect ratio is used in the calculation of auto sizes and some other layout functions. MDN Reference
    static member ``aspectRatio`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "aspectRatio" (box (value))
    /// The backdrop-filter CSS property lets you apply graphical effects such as blurring or color shifting to the area behind an element. Because it applies to everything behind the element, to see the effect the element or its background needs to be transparent or partially transparent. MDN Reference
    static member ``backdropFilter`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "backdropFilter" (box (value))
    /// The backface-visibility CSS property sets whether the back face of an element is visible when turned towards the user. MDN Reference
    static member ``backfaceVisibility`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "backfaceVisibility" (box (value))
    /// The background shorthand CSS property sets all background style properties at once, such as color, image, origin, size, and repeat method. MDN Reference
    static member ``background`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "background" (box (value))
    /// The background-attachment CSS property sets whether a background image&apos;s position is fixed within the viewport, or scrolls with its containing block. MDN Reference
    static member ``backgroundAttachment`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "backgroundAttachment" (box (value))
    /// The background-blend-mode CSS property sets how an element&apos;s background images should blend with each other and with the element&apos;s background color. MDN Reference
    static member ``backgroundBlendMode`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "backgroundBlendMode" (box (value))
    /// The background-clip CSS property sets whether an element&apos;s background extends underneath its border box, padding box, or content box. MDN Reference
    static member ``backgroundClip`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "backgroundClip" (box (value))
    /// The background-color CSS property sets the background color of an element. MDN Reference
    static member ``backgroundColor`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "backgroundColor" (box (value))
    /// The background-image CSS property sets one or more background images on an element. MDN Reference
    static member ``backgroundImage`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "backgroundImage" (box (value))
    /// The background-origin CSS property sets the background&apos;s origin: from the border start, inside the border, or inside the padding. MDN Reference
    static member ``backgroundOrigin`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "backgroundOrigin" (box (value))
    /// The background-position CSS property sets the initial position for each background image. The position is relative to the position layer set by background-origin. MDN Reference
    static member ``backgroundPosition`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "backgroundPosition" (box (value))
    /// The background-position-x CSS property sets the initial horizontal position for each background image. The position is relative to the position layer set by background-origin. MDN Reference
    static member ``backgroundPositionX`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "backgroundPositionX" (box (value))
    /// The background-position-y CSS property sets the initial vertical position for each background image. The position is relative to the position layer set by background-origin. MDN Reference
    static member ``backgroundPositionY`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "backgroundPositionY" (box (value))
    /// The background-repeat CSS property sets how background images are repeated. A background image can be repeated along the horizontal and vertical axes, or not repeated at all. MDN Reference
    static member ``backgroundRepeat`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "backgroundRepeat" (box (value))
    /// The background-size CSS property sets the size of the element&apos;s background image. The image can be left to its natural size, stretched, or constrained to fit the available space. MDN Reference
    static member ``backgroundSize`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "backgroundSize" (box (value))
    /// The baseline-shift CSS property repositions the dominant-baseline of a text element relative to the dominant-baseline of its parent text content element. The shifted element might be a sub- or superscript. If the property is present, the value overrides the element&apos;s baseline-shift attribute. 
    static member ``baselineShift`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "baselineShift" (box (value))
    /// The baseline-source CSS property defines which baseline to use when inline-level boxes have multiple possible baselines, such as multi-line inline blocks or inline flex containers. The values allow for choosing between aligning to the box&apos;s first baseline, last baseline, or letting the browser decide automatically based on the box type. MDN Reference
    static member ``baselineSource`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "baselineSource" (box (value))
    /// The block-size CSS property defines the size of an element&apos;s block along the block axis. If the writing-mode is horizontal, it corresponds to the height; if the writing mode is vertical, it corresponds to the width. A related property is inline-size, which defines the other dimension of the element. MDN Reference
    static member ``blockSize`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "blockSize" (box (value))
    /// The border shorthand CSS property sets an element&apos;s border. It sets the values of border-width, border-style, and border-color. MDN Reference
    static member ``border`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "border" (box (value))
    /// The border-block CSS property is a shorthand property for setting the individual logical block border property values in a single place in the style sheet. MDN Reference
    static member ``borderBlock`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderBlock" (box (value))
    /// The border-block-color CSS property defines the color of the logical block borders of an element, which maps to a physical border color depending on the element&apos;s writing mode, directionality, and text orientation. It corresponds to the border-top-color and border-bottom-color, or border-right-color and border-left-color property depending on the values defined for writing-mode, direction, and text-orientation. MDN Reference
    static member ``borderBlockColor`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderBlockColor" (box (value))
    /// The border-block-end CSS property is a shorthand property for setting the individual logical block-end border property values in a single place in the style sheet. MDN Reference
    static member ``borderBlockEnd`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderBlockEnd" (box (value))
    /// The border-block-end-color CSS property defines the color of the logical block-end border of an element, which maps to a physical border color depending on the element&apos;s writing mode, directionality, and text orientation. It corresponds to the border-top-color, border-right-color, border-bottom-color, or border-left-color property depending on the values defined for writing-mode, direction, and text-orientation. MDN Reference
    static member ``borderBlockEndColor`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderBlockEndColor" (box (value))
    /// The border-block-end-style CSS property defines the style of the logical block-end border of an element, which maps to a physical border style depending on the element&apos;s writing mode, directionality, and text orientation. It corresponds to the border-top-style, border-right-style, border-bottom-style, or border-left-style property depending on the values defined for writing-mode, direction, and text-orientation. MDN Reference
    static member ``borderBlockEndStyle`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderBlockEndStyle" (box (value))
    /// The border-block-end-width CSS property defines the width of the logical block-end border of an element, which maps to a physical border width depending on the element&apos;s writing mode, directionality, and text orientation. It corresponds to the border-top-width, border-right-width, border-bottom-width, or border-left-width property depending on the values defined for writing-mode, direction, and text-orientation. MDN Reference
    static member ``borderBlockEndWidth`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderBlockEndWidth" (box (value))
    /// The border-block-start CSS property is a shorthand property for setting the individual logical block-start border property values in a single place in the style sheet. MDN Reference
    static member ``borderBlockStart`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderBlockStart" (box (value))
    /// The border-block-start-color CSS property defines the color of the logical block-start border of an element, which maps to a physical border color depending on the element&apos;s writing mode, directionality, and text orientation. It corresponds to the border-top-color, border-right-color, border-bottom-color, or border-left-color property depending on the values defined for writing-mode, direction, and text-orientation. MDN Reference
    static member ``borderBlockStartColor`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderBlockStartColor" (box (value))
    /// The border-block-start-style CSS property defines the style of the logical block start border of an element, which maps to a physical border style depending on the element&apos;s writing mode, directionality, and text orientation. It corresponds to the border-top-style, border-right-style, border-bottom-style, or border-left-style property depending on the values defined for writing-mode, direction, and text-orientation. MDN Reference
    static member ``borderBlockStartStyle`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderBlockStartStyle" (box (value))
    /// The border-block-start-width CSS property defines the width of the logical block-start border of an element, which maps to a physical border width depending on the element&apos;s writing mode, directionality, and text orientation. It corresponds to the border-top-width, border-right-width, border-bottom-width, or border-left-width property depending on the values defined for writing-mode, direction, and text-orientation. MDN Reference
    static member ``borderBlockStartWidth`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderBlockStartWidth" (box (value))
    /// The border-block-style CSS property defines the style of the logical block borders of an element, which maps to a physical border style depending on the element&apos;s writing mode, directionality, and text orientation. It corresponds to the border-top-style and border-bottom-style, or border-left-style and border-right-style properties depending on the values defined for writing-mode, direction, and text-orientation. MDN Reference
    static member ``borderBlockStyle`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderBlockStyle" (box (value))
    /// The border-block-width CSS property defines the width of the logical block borders of an element, which maps to a physical border width depending on the element&apos;s writing mode, directionality, and text orientation. It corresponds to the border-top-width and border-bottom-width, or border-left-width, and border-right-width property depending on the values defined for writing-mode, direction, and text-orientation. MDN Reference
    static member ``borderBlockWidth`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderBlockWidth" (box (value))
    /// The border-bottom shorthand CSS property sets an element&apos;s bottom border. It sets the values of border-bottom-width, border-bottom-style and border-bottom-color. MDN Reference
    static member ``borderBottom`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderBottom" (box (value))
    /// The border-bottom-color CSS property sets the color of an element&apos;s bottom border. It can also be set with the shorthand CSS properties border-color or border-bottom. MDN Reference
    static member ``borderBottomColor`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderBottomColor" (box (value))
    /// The border-bottom-left-radius CSS property rounds the bottom-left corner of an element by specifying the radius (or the radius of the semi-major and semi-minor axes) of the ellipse defining the curvature of the corner. MDN Reference
    static member ``borderBottomLeftRadius`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderBottomLeftRadius" (box (value))
    /// The border-bottom-right-radius CSS property rounds the bottom-right corner of an element by specifying the radius (or the radius of the semi-major and semi-minor axes) of the ellipse defining the curvature of the corner. MDN Reference
    static member ``borderBottomRightRadius`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderBottomRightRadius" (box (value))
    /// The border-bottom-style CSS property sets the line style of an element&apos;s bottom border. MDN Reference
    static member ``borderBottomStyle`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderBottomStyle" (box (value))
    /// The border-bottom-width CSS property sets the width of the bottom border of an element. MDN Reference
    static member ``borderBottomWidth`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderBottomWidth" (box (value))
    /// The border-collapse CSS property sets whether cells inside a &lt;table&gt; have shared or separate borders. MDN Reference
    static member ``borderCollapse`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderCollapse" (box (value))
    /// The border-color shorthand CSS property sets the color of an element&apos;s border. MDN Reference
    static member ``borderColor`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderColor" (box (value))
    /// The border-end-end-radius CSS property defines a logical border radius on an element, which maps to a physical border radius that depends on the element&apos;s writing-mode, direction, and text-orientation. This is useful when building styles to work regardless of the text orientation and writing mode. MDN Reference
    static member ``borderEndEndRadius`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderEndEndRadius" (box (value))
    /// The border-end-start-radius CSS property defines a logical border radius on an element, which maps to a physical border radius depending on the element&apos;s writing-mode, direction, and text-orientation. This is useful when building styles to work regardless of the text orientation and writing mode. MDN Reference
    static member ``borderEndStartRadius`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderEndStartRadius" (box (value))
    /// The border-image CSS property draws an image around a given element. It replaces the element&apos;s regular border. MDN Reference
    static member ``borderImage`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderImage" (box (value))
    /// The border-image-outset CSS property sets the distance by which an element&apos;s border image is set out from its border box. MDN Reference
    static member ``borderImageOutset`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderImageOutset" (box (value))
    /// The border-image-repeat CSS property defines how the images for the sides and the middle part of the border image are scaled and tiled. The middle region can be displayed by using the keyword &quot;fill&quot; in the border-image-slice property. MDN Reference
    static member ``borderImageRepeat`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderImageRepeat" (box (value))
    /// The border-image-slice CSS property divides the image specified by border-image-source into regions. These regions form the components of an element&apos;s border image. MDN Reference
    static member ``borderImageSlice`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderImageSlice" (box (value))
    /// The border-image-source CSS property sets the source image used to create an element&apos;s border image. MDN Reference
    static member ``borderImageSource`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderImageSource" (box (value))
    /// The border-image-width CSS property sets the width of an element&apos;s border image. MDN Reference
    static member ``borderImageWidth`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderImageWidth" (box (value))
    /// The border-inline CSS property is a shorthand property for setting the individual logical inline border property values in a single place in the style sheet. MDN Reference
    static member ``borderInline`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderInline" (box (value))
    /// The border-inline-color CSS property defines the color of the logical inline borders of an element, which maps to a physical border color depending on the element&apos;s writing mode, directionality, and text orientation. It corresponds to the border-top-color and border-bottom-color, or border-right-color and border-left-color property depending on the values defined for writing-mode, direction, and text-orientation. MDN Reference
    static member ``borderInlineColor`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderInlineColor" (box (value))
    /// The border-inline-end CSS property is a shorthand property for setting the individual logical inline-end border property values in a single place in the style sheet. MDN Reference
    static member ``borderInlineEnd`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderInlineEnd" (box (value))
    /// The border-inline-end-color CSS property defines the color of the logical inline-end border of an element, which maps to a physical border color depending on the element&apos;s writing mode, directionality, and text orientation. It corresponds to the border-top-color, border-right-color, border-bottom-color, or border-left-color property depending on the values defined for writing-mode, direction, and text-orientation. MDN Reference
    static member ``borderInlineEndColor`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderInlineEndColor" (box (value))
    /// The border-inline-end-style CSS property defines the style of the logical inline end border of an element, which maps to a physical border style depending on the element&apos;s writing mode, directionality, and text orientation. It corresponds to the border-top-style, border-right-style, border-bottom-style, or border-left-style property depending on the values defined for writing-mode, direction, and text-orientation. MDN Reference
    static member ``borderInlineEndStyle`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderInlineEndStyle" (box (value))
    /// The border-inline-end-width CSS property defines the width of the logical inline-end border of an element, which maps to a physical border width depending on the element&apos;s writing mode, directionality, and text orientation. It corresponds to the border-top-width, border-right-width, border-bottom-width, or border-left-width property depending on the values defined for writing-mode, direction, and text-orientation. MDN Reference
    static member ``borderInlineEndWidth`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderInlineEndWidth" (box (value))
    /// The border-inline-start CSS property is a shorthand property for setting the individual logical inline-start border property values in a single place in the style sheet. MDN Reference
    static member ``borderInlineStart`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderInlineStart" (box (value))
    /// The border-inline-start-color CSS property defines the color of the logical inline start border of an element, which maps to a physical border color depending on the element&apos;s writing mode, directionality, and text orientation. It corresponds to the border-top-color, border-right-color, border-bottom-color, or border-left-color property depending on the values defined for writing-mode, direction, and text-orientation. MDN Reference
    static member ``borderInlineStartColor`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderInlineStartColor" (box (value))
    /// The border-inline-start-style CSS property defines the style of the logical inline start border of an element, which maps to a physical border style depending on the element&apos;s writing mode, directionality, and text orientation. It corresponds to the border-top-style, border-right-style, border-bottom-style, or border-left-style property depending on the values defined for writing-mode, direction, and text-orientation. MDN Reference
    static member ``borderInlineStartStyle`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderInlineStartStyle" (box (value))
    /// The border-inline-start-width CSS property defines the width of the logical inline-start border of an element, which maps to a physical border width depending on the element&apos;s writing mode, directionality, and text orientation. It corresponds to the border-top-width, border-right-width, border-bottom-width, or border-left-width property depending on the values defined for writing-mode, direction, and text-orientation. MDN Reference
    static member ``borderInlineStartWidth`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderInlineStartWidth" (box (value))
    /// The border-inline-style CSS property defines the style of the logical inline borders of an element, which maps to a physical border style depending on the element&apos;s writing mode, directionality, and text orientation. It corresponds to the border-top-style and border-bottom-style, or border-left-style and border-right-style properties depending on the values defined for writing-mode, direction, and text-orientation. MDN Reference
    static member ``borderInlineStyle`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderInlineStyle" (box (value))
    /// The border-inline-width CSS property defines the width of the logical inline borders of an element, which maps to a physical border width depending on the element&apos;s writing mode, directionality, and text orientation. It corresponds to the border-top-width and border-bottom-width, or border-left-width, and border-right-width property depending on the values defined for writing-mode, direction, and text-orientation. MDN Reference
    static member ``borderInlineWidth`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderInlineWidth" (box (value))
    /// The border-left shorthand CSS property sets all the properties of an element&apos;s left border. MDN Reference
    static member ``borderLeft`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderLeft" (box (value))
    /// The border-left-color CSS property sets the color of an element&apos;s left border. It can also be set with the shorthand CSS properties border-color or border-left. MDN Reference
    static member ``borderLeftColor`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderLeftColor" (box (value))
    /// The border-left-style CSS property sets the line style of an element&apos;s left border. MDN Reference
    static member ``borderLeftStyle`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderLeftStyle" (box (value))
    /// The border-left-width CSS property sets the width of the left border of an element. MDN Reference
    static member ``borderLeftWidth`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderLeftWidth" (box (value))
    /// The border-radius CSS property rounds the corners of an element&apos;s outer border edge. You can set a single radius to make circular corners, or two radii to make elliptical corners. MDN Reference
    static member ``borderRadius`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderRadius" (box (value))
    /// The border-right shorthand CSS property sets all the properties of an element&apos;s right border. MDN Reference
    static member ``borderRight`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderRight" (box (value))
    /// The border-right-color CSS property sets the color of an element&apos;s right border. It can also be set with the shorthand CSS properties border-color or border-right. MDN Reference
    static member ``borderRightColor`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderRightColor" (box (value))
    /// The border-right-style CSS property sets the line style of an element&apos;s right border. MDN Reference
    static member ``borderRightStyle`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderRightStyle" (box (value))
    /// The border-right-width CSS property sets the width of the right border of an element. MDN Reference
    static member ``borderRightWidth`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderRightWidth" (box (value))
    /// The border-spacing CSS property sets the distance between the borders of adjacent cells in a &lt;table&gt;. This property applies only when border-collapse is separate. MDN Reference
    static member ``borderSpacing`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderSpacing" (box (value))
    /// The border-start-end-radius CSS property defines a logical border radius on an element, which maps to a physical border radius depending on the element&apos;s writing-mode, direction, and text-orientation. This is useful when building styles to work regardless of the text orientation and writing mode. MDN Reference
    static member ``borderStartEndRadius`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderStartEndRadius" (box (value))
    /// The border-start-start-radius CSS property defines a logical border radius on an element, which maps to a physical border radius that depends on the element&apos;s writing-mode, direction, and text-orientation. This is useful when building styles to work regardless of the text orientation and writing mode. MDN Reference
    static member ``borderStartStartRadius`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderStartStartRadius" (box (value))
    /// The border-style shorthand CSS property sets the line style for all four sides of an element&apos;s border. MDN Reference
    static member ``borderStyle`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderStyle" (box (value))
    /// The border-top shorthand CSS property sets all the properties of an element&apos;s top border. MDN Reference
    static member ``borderTop`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderTop" (box (value))
    /// The border-top-color CSS property sets the color of an element&apos;s top border. It can also be set with the shorthand CSS properties border-color or border-top. MDN Reference
    static member ``borderTopColor`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderTopColor" (box (value))
    /// The border-top-left-radius CSS property rounds the top-left corner of an element by specifying the radius (or the radius of the semi-major and semi-minor axes) of the ellipse defining the curvature of the corner. MDN Reference
    static member ``borderTopLeftRadius`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderTopLeftRadius" (box (value))
    /// The border-top-right-radius CSS property rounds the top-right corner of an element by specifying the radius (or the radius of the semi-major and semi-minor axes) of the ellipse defining the curvature of the corner. MDN Reference
    static member ``borderTopRightRadius`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderTopRightRadius" (box (value))
    /// The border-top-style CSS property sets the line style of an element&apos;s top border. MDN Reference
    static member ``borderTopStyle`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderTopStyle" (box (value))
    /// The border-top-width CSS property sets the width of the top border of an element. MDN Reference
    static member ``borderTopWidth`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderTopWidth" (box (value))
    /// The border-width shorthand CSS property sets the width of an element&apos;s border. MDN Reference
    static member ``borderWidth`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "borderWidth" (box (value))
    /// The bottom CSS property participates in setting the vertical position of a positioned element. This inset property has no effect on non-positioned elements. MDN Reference
    static member ``bottom`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "bottom" (box (value))
    /// The box-decoration-break CSS property specifies how an element&apos;s fragments should be rendered when broken across multiple lines, columns, or pages. MDN Reference
    static member ``boxDecorationBreak`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "boxDecorationBreak" (box (value))
    /// The box-shadow CSS property adds shadow effects around an element&apos;s frame. You can set multiple effects separated by commas. A box shadow is described by X and Y offsets relative to the element, blur and spread radius, and color. MDN Reference
    static member ``boxShadow`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "boxShadow" (box (value))
    /// The box-sizing CSS property sets how the total width and height of an element is calculated. MDN Reference
    static member ``boxSizing`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "boxSizing" (box (value))
    /// The break-after CSS property sets how page, column, or region breaks should behave after a generated box. If there is no generated box, the property is ignored. MDN Reference
    static member ``breakAfter`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "breakAfter" (box (value))
    /// The break-before CSS property sets how page, column, or region breaks should behave before a generated box. If there is no generated box, the property is ignored. MDN Reference
    static member ``breakBefore`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "breakBefore" (box (value))
    /// The break-inside CSS property sets how page, column, or region breaks should behave inside a generated box. If there is no generated box, the property is ignored. MDN Reference
    static member ``breakInside`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "breakInside" (box (value))
    /// The caption-side CSS property puts the content of a table&apos;s &lt;caption&gt; on the specified side. The values are relative to the writing-mode of the table. MDN Reference
    static member ``captionSide`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "captionSide" (box (value))
    /// The caret-color CSS property sets the color of the insertion caret, sometimes referred to as the text input cursor. This is the visible marker appearing at the insertion point where the next character typed will be added or where the next character deleted will be removed. MDN Reference
    static member ``caretColor`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "caretColor" (box (value))
    /// The clear CSS property sets whether an element must be moved below (cleared) floating elements that precede it. The clear property applies to floating and non-floating elements. MDN Reference
    static member ``clear`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "clear" (box (value))
    /// The clip CSS property defines a visible portion of an element. The clip property applies only to absolutely positioned elements — that is, elements with position:absolute or position:fixed. @deprecated MDN Reference
    static member ``clip`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "clip" (box (value))
    /// The clip-path CSS property creates a clipping region that sets what part of an element should be shown. Parts that are inside the region are shown, while those outside are hidden. MDN Reference
    static member ``clipPath`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "clipPath" (box (value))
    /// The clip-rule CSS property determines, when parts of the path overlap other parts, which pixels in a mask&apos;s box are inside the clipping shape defined by a clip path and which are outside. MDN Reference
    static member ``clipRule`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "clipRule" (box (value))
    /// The color CSS property sets the foreground color value of an element&apos;s text and text decorations, and sets the currentColor value. currentColor may be used as an indirect value on other properties and is the default for other color properties, such as border-color. MDN Reference
    static member ``color`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "color" (box (value))
    /// The color-interpolation CSS property is used in SVG to specify which color space to use for &lt;linearGradient&gt; and &lt;radialGradient&gt; SVG elements. MDN Reference
    static member ``colorInterpolation`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "colorInterpolation" (box (value))
    /// The color-interpolation-filters CSS property specifies the color space for imaging operations performed via SVG filter effects. If explicitly declared, the value of the CSS property overrides any value given in the element&apos;s color-interpolation-filters attribute. MDN Reference
    static member ``colorInterpolationFilters`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "colorInterpolationFilters" (box (value))
    /// The color-scheme CSS property allows an element to indicate which color schemes it can comfortably be rendered in. User agents change the following aspects of the UI chrome to match the used color scheme: MDN Reference
    static member ``colorScheme`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "colorScheme" (box (value))
    /// The column-count CSS property breaks an element&apos;s content into the specified number of columns. MDN Reference
    static member ``columnCount`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "columnCount" (box (value))
    /// The column-fill CSS property controls how an element&apos;s contents are balanced when broken into columns. MDN Reference
    static member ``columnFill`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "columnFill" (box (value))
    /// The column-gap CSS property sets the size of the gap (gutter) between an element&apos;s columns. MDN Reference
    static member ``columnGap`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "columnGap" (box (value))
    /// The column-rule shorthand CSS property sets the width, style, and color of the line drawn between columns in a multi-column layout. MDN Reference
    static member ``columnRule`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "columnRule" (box (value))
    /// The column-rule-color CSS property sets the color of the line drawn between columns in a multi-column layout. MDN Reference
    static member ``columnRuleColor`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "columnRuleColor" (box (value))
    /// The column-rule-style CSS property sets the style of the line drawn between columns in a multi-column layout. MDN Reference
    static member ``columnRuleStyle`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "columnRuleStyle" (box (value))
    /// The column-rule-width CSS property sets the width of the line drawn between columns in a multi-column layout. MDN Reference
    static member ``columnRuleWidth`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "columnRuleWidth" (box (value))
    /// The column-span CSS property makes it possible for an element to span across all columns when its value is set to all. MDN Reference
    static member ``columnSpan`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "columnSpan" (box (value))
    /// The column-width CSS property sets the ideal column width in a multi-column layout. The container will have as many columns as can fit without any of them having a width less than the column-width value. If the width of the container is narrower than the specified value, the single column&apos;s width will be smaller than the declared column width. MDN Reference
    static member ``columnWidth`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "columnWidth" (box (value))
    /// The columns CSS shorthand property sets the number of columns to use when drawing an element&apos;s contents, as well as those columns&apos; widths. MDN Reference
    static member ``columns`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "columns" (box (value))
    /// The contain CSS property indicates that an element and its contents are, as much as possible, independent from the rest of the document tree. Containment enables isolating a subsection of the DOM, providing performance benefits by limiting calculations of layout, style, paint, size, or any combination to a DOM subtree rather than the entire page. Containment can also be used to scope CSS counters and quotes. MDN Reference
    static member ``contain`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "contain" (box (value))
    /// The contain-intrinsic-block-size CSS logical property defines the block size of an element that a browser can use for layout when the element is subject to size containment. MDN Reference
    static member ``containIntrinsicBlockSize`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "containIntrinsicBlockSize" (box (value))
    /// The contain-intrinsic-height CSS property sets the height of an element that a browser can use for layout when the element is subject to size containment. MDN Reference
    static member ``containIntrinsicHeight`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "containIntrinsicHeight" (box (value))
    /// The contain-intrinsic-inline-size CSS logical property defines the inline-size of an element that a browser can use for layout when the element is subject to size containment. MDN Reference
    static member ``containIntrinsicInlineSize`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "containIntrinsicInlineSize" (box (value))
    /// The contain-intrinsic-size CSS shorthand property sets the size of an element that a browser will use for layout when the element is subject to size containment. MDN Reference
    static member ``containIntrinsicSize`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "containIntrinsicSize" (box (value))
    /// The contain-intrinsic-width CSS property sets the width of an element that a browser will use for layout when the element is subject to size containment. MDN Reference
    static member ``containIntrinsicWidth`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "containIntrinsicWidth" (box (value))
    /// The container shorthand CSS property establishes the element as a query container and specifies the name and type of the containment context used in a container query. MDN Reference
    static member ``container`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "container" (box (value))
    /// The container-name CSS property specifies a list of query container names used by the @container at-rule in a container query. A container query will apply styles to elements based on the size or scroll-state of the nearest ancestor with a containment context. When a containment context is given a name, it can be specifically targeted using the @container at-rule instead of the nearest ancestor with containment. MDN Reference
    static member ``containerName`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "containerName" (box (value))
    /// An element can be established as a query container using the container-type CSS property. container-type is used to define the type of container context used in a container query. The available container contexts are: MDN Reference
    static member ``containerType`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "containerType" (box (value))
    /// The content CSS property replaces content with a generated value. It can be used to define what is rendered inside an element or pseudo-element. For elements, the content property specifies whether the element renders normally (normal or none) or is replaced with an image (and associated &quot;alt&quot; text). For pseudo-elements and margin boxes, content defines the content as images, text, both, or none, which determines whether the element renders at all. MDN Reference
    static member ``content`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "content" (box (value))
    /// The content-visibility CSS property controls whether or not an element renders its contents at all, along with forcing a strong set of containments, allowing user agents to potentially omit large swathes of layout and rendering work until it becomes needed. It enables the user agent to skip an element&apos;s rendering work (including layout and painting) until it is needed — which makes the initial page load much faster. MDN Reference
    static member ``contentVisibility`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "contentVisibility" (box (value))
    /// The counter-increment CSS property can be used to increase or decrease the value of the named CSS counters by the specified values, or to prevent all counters or an individual counter&apos;s value from being changed. MDN Reference
    static member ``counterIncrement`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "counterIncrement" (box (value))
    /// The counter-reset CSS property creates named CSS counters and initializes them to a specific value. It supports creating counters that count up from one to the number of elements, as well as those that count down from the number of elements to one. MDN Reference
    static member ``counterReset`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "counterReset" (box (value))
    /// The counter-set CSS property sets CSS counters on the element to the given values. MDN Reference
    static member ``counterSet`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "counterSet" (box (value))
    /// The cssFloat property of the CSSStyleProperties interface returns the CSS float property. MDN Reference
    static member ``cssFloat`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "cssFloat" (box (value))
    /// The cursor CSS property sets the mouse cursor, if any, to show when the mouse pointer is over an element. MDN Reference
    static member ``cursor`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "cursor" (box (value))
    /// The cx CSS property defines the x-axis center point of an SVG &lt;circle&gt; or &lt;ellipse&gt; element. If present, it overrides the element&apos;s cx attribute. MDN Reference
    static member ``cx`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "cx" (box (value))
    /// The cy CSS property defines the y-axis center point of an SVG &lt;circle&gt; or &lt;ellipse&gt; elements. If present, it overrides the element&apos;s cy attribute. MDN Reference
    static member ``cy`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "cy" (box (value))
    /// The d CSS property defines a path to be drawn by the SVG &lt;path&gt; element. If present, it overrides the element&apos;s d attribute. MDN Reference
    static member ``d`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "d" (box (value))
    /// The direction CSS property sets the direction of text, table and grid columns, and horizontal overflow. Use rtl for languages written from right to left (like Hebrew or Arabic), and ltr for those written from left to right (like English and most other languages). MDN Reference
    static member ``direction`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "direction" (box (value))
    /// The display CSS property sets whether an element is treated as a block or inline box and the layout used for its children, such as flow layout, grid or flex. MDN Reference
    static member ``display`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "display" (box (value))
    /// The dominant-baseline CSS property specifies the specific baseline used to align the box&apos;s text and inline-level contents. It also indicates the default alignment baseline of any boxes participating in baseline alignment in the box&apos;s alignment context. If present, it overrides the shape&apos;s dominant-baseline attribute. MDN Reference
    static member ``dominantBaseline`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "dominantBaseline" (box (value))
    /// The dynamic-range-limit CSS property specifies the maximum luminance allowed for High Dynamic Range (HDR) content. MDN Reference
    static member ``dynamicRangeLimit`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "dynamicRangeLimit" (box (value))
    /// The empty-cells CSS property sets whether borders and backgrounds appear around &lt;table&gt; cells that have no visible content. MDN Reference
    static member ``emptyCells`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "emptyCells" (box (value))
    /// The field-sizing CSS property enables you to control the sizing behavior of elements that are given a default preferred size, such as form control elements. This property enables you to override the default sizing behavior, allowing form controls to adjust in size to fit their contents. MDN Reference
    static member ``fieldSizing`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "fieldSizing" (box (value))
    /// The fill CSS property defines how SVG text content and the interior canvas of SVG shapes are filled or painted. If present, it overrides the element&apos;s fill attribute. MDN Reference
    static member ``fill`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "fill" (box (value))
    /// The fill-opacity CSS property defines the opacity of the painting operation (color, gradient, pattern, etc.) applied to SVG shapes or text content elements to fill the element. The property defines the opacity of the element&apos;s fill only; it does not affect the stroke. If present, it overrides the element&apos;s fill-opacity attribute. MDN Reference
    static member ``fillOpacity`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "fillOpacity" (box (value))
    /// The fill-rule CSS property defines the rule used to determine which parts of the SVG shape&apos;s canvas are included inside a shape to be filled. If present, it overrides the element&apos;s fill-rule attribute. MDN Reference
    static member ``fillRule`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "fillRule" (box (value))
    /// The filter CSS property applies graphical effects like blur or color shift to an element. Filters are commonly used to adjust the rendering of images, backgrounds, and borders. MDN Reference
    static member ``filter`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "filter" (box (value))
    /// The flex CSS shorthand property sets how a flex item will grow or shrink to fit the space available in its flex container. MDN Reference
    static member ``flex`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "flex" (box (value))
    /// The flex-basis CSS property sets the initial main size of a flex item. It sets the size of the content box unless otherwise set with box-sizing. MDN Reference
    static member ``flexBasis`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "flexBasis" (box (value))
    /// The flex-direction CSS property sets how flex items are placed in the flex container defining the main axis and the direction (normal or reversed). MDN Reference
    static member ``flexDirection`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "flexDirection" (box (value))
    /// The flex-flow CSS shorthand property specifies the direction of a flex container, as well as its wrapping behavior. MDN Reference
    static member ``flexFlow`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "flexFlow" (box (value))
    /// The flex-grow CSS property sets the flex grow factor, which specifies how much of the flex container&apos;s positive free space, if any, should be assigned to the flex item&apos;s main size. MDN Reference
    static member ``flexGrow`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "flexGrow" (box (value))
    /// The flex-shrink CSS property sets the flex shrink factor of a flex item. If the size of all flex items is larger than the flex container, the flex items can shrink to fit according to their flex-shrink value. Each flex line&apos;s negative free space is distributed between the line&apos;s flex items that have a flex-shrink value greater than 0. MDN Reference
    static member ``flexShrink`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "flexShrink" (box (value))
    /// The flex-wrap CSS property sets whether flex items are forced onto one line or can wrap onto multiple lines. If wrapping is allowed, it sets the direction that lines are stacked. MDN Reference
    static member ``flexWrap`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "flexWrap" (box (value))
    /// MDN Reference
    static member ``float`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "float" (box (value))
    /// The flood-color CSS property defines the color of the current filter primitive subregion in &lt;feFlood&gt; and &lt;feDropShadow&gt; elements within a &lt;filter&gt;. If present, it overrides the element&apos;s flood-color attribute. MDN Reference
    static member ``floodColor`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "floodColor" (box (value))
    /// The flood-opacity CSS property defines the opacity of the current filter primitive subregion in &lt;feFlood&gt; and &lt;feDropShadow&gt; elements within a &lt;filter&gt;. If present, it overrides the element&apos;s flood-opacity attribute. MDN Reference
    static member ``floodOpacity`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "floodOpacity" (box (value))
    /// The font CSS shorthand property sets all the different properties of an element&apos;s font. Alternatively, it sets an element&apos;s font to a system font. MDN Reference
    static member ``font`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "font" (box (value))
    /// The font-family CSS property specifies a prioritized list of one or more font family names and/or generic family names for the selected element. MDN Reference
    static member ``fontFamily`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "fontFamily" (box (value))
    /// The font-feature-settings CSS property controls advanced typographic features in OpenType fonts. MDN Reference
    static member ``fontFeatureSettings`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "fontFeatureSettings" (box (value))
    /// The font-kerning CSS property sets the use of the kerning information stored in a font. MDN Reference
    static member ``fontKerning`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "fontKerning" (box (value))
    /// The font-language-override CSS property controls the use of language-specific glyphs in a typeface. MDN Reference
    static member ``fontLanguageOverride`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "fontLanguageOverride" (box (value))
    /// The font-optical-sizing CSS property sets whether text rendering is optimized for viewing at different sizes. MDN Reference
    static member ``fontOpticalSizing`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "fontOpticalSizing" (box (value))
    /// The font-palette CSS property allows specifying one of the many palettes contained in a color font that a user agent may use for the font. Users can also override the values in a palette or create a new palette by using the  @font-palette-values at-rule. MDN Reference
    static member ``fontPalette`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "fontPalette" (box (value))
    /// The font-size CSS property sets the size of the font. Changing the font size also updates the sizes of the font size-relative &lt;length&gt; units, such as em, ex, and so forth. MDN Reference
    static member ``fontSize`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "fontSize" (box (value))
    /// The font-size-adjust CSS property provides a way to modify the size of lowercase letters relative to the size of uppercase letters, which defines the overall font-size. This property is useful for situations where font fallback can occur. MDN Reference
    static member ``fontSizeAdjust`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "fontSizeAdjust" (box (value))
    /// The font-stretch CSS property selects a normal, condensed, or expanded face from a font. MDN Reference
    static member ``fontStretch`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "fontStretch" (box (value))
    /// The font-style CSS property sets whether a font should be styled with a normal, italic, or oblique face from its font-family. MDN Reference
    static member ``fontStyle`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "fontStyle" (box (value))
    /// The font-synthesis shorthand CSS property lets you specify whether or not the browser may synthesize the bold, italic, small-caps, and/or subscript and superscript typefaces when they are missing in the specified font-family. MDN Reference
    static member ``fontSynthesis`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "fontSynthesis" (box (value))
    /// The font-synthesis-small-caps CSS property lets you specify whether or not the browser may synthesize small-caps typeface when it is missing in a font family. Small-caps glyphs typically use the form of uppercase letters but are reduced to the size of lowercase letters. MDN Reference
    static member ``fontSynthesisSmallCaps`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "fontSynthesisSmallCaps" (box (value))
    /// The font-synthesis-style CSS property lets you specify whether or not the browser may synthesize the oblique typeface when it is missing in a font family. MDN Reference
    static member ``fontSynthesisStyle`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "fontSynthesisStyle" (box (value))
    /// The font-synthesis-weight CSS property lets you specify whether or not the browser may synthesize the bold typeface when it is missing in a font family. MDN Reference
    static member ``fontSynthesisWeight`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "fontSynthesisWeight" (box (value))
    /// The font-variant CSS shorthand property allows you to set all the font variants for a font. MDN Reference
    static member ``fontVariant`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "fontVariant" (box (value))
    /// The font-variant-alternates CSS property controls the usage of alternate glyphs. These alternate glyphs may be referenced by alternative names defined in  @font-feature-values . MDN Reference
    static member ``fontVariantAlternates`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "fontVariantAlternates" (box (value))
    /// The font-variant-caps CSS property controls the use of alternate glyphs used for small or petite capitals or for titling. MDN Reference
    static member ``fontVariantCaps`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "fontVariantCaps" (box (value))
    /// The font-variant-east-asian CSS property controls the use of alternate glyphs for East Asian scripts, like Japanese and Chinese. MDN Reference
    static member ``fontVariantEastAsian`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "fontVariantEastAsian" (box (value))
    /// The font-variant-emoji CSS property specifies the default presentation style for displaying emojis. MDN Reference
    static member ``fontVariantEmoji`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "fontVariantEmoji" (box (value))
    /// The font-variant-ligatures CSS property controls which ligatures and contextual forms are used in the textual content of the elements it applies to. This leads to more harmonized forms in the resulting text. MDN Reference
    static member ``fontVariantLigatures`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "fontVariantLigatures" (box (value))
    /// The font-variant-numeric CSS property controls the usage of alternate glyphs for numbers, fractions, and ordinal markers. MDN Reference
    static member ``fontVariantNumeric`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "fontVariantNumeric" (box (value))
    /// The font-variant-position CSS property controls the use of alternate, smaller glyphs that are positioned as superscript or subscript. MDN Reference
    static member ``fontVariantPosition`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "fontVariantPosition" (box (value))
    /// The font-variation-settings CSS property provides low-level control over variable font characteristics by letting you specify the four letter axis names of the characteristics you want to vary along with their values. MDN Reference
    static member ``fontVariationSettings`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "fontVariationSettings" (box (value))
    /// The font-weight CSS property sets the weight (or boldness) of the font. The weights available depend on the font-family that is currently set. MDN Reference
    static member ``fontWeight`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "fontWeight" (box (value))
    /// The forced-color-adjust CSS property allows authors to opt certain elements out of forced colors mode. This then restores the control of those values to CSS. MDN Reference
    static member ``forcedColorAdjust`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "forcedColorAdjust" (box (value))
    /// The gap CSS shorthand property sets the gaps (also called gutters) between rows and columns. This property applies to multi-column, flex, and grid containers. MDN Reference
    static member ``gap`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "gap" (box (value))
    /// The grid CSS property is a shorthand property that sets all of the explicit and implicit grid properties in a single declaration. MDN Reference
    static member ``grid`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "grid" (box (value))
    /// The grid-area CSS shorthand property specifies a grid item&apos;s size and location within a grid by contributing a line, a span, or nothing (automatic) to its grid placement, thereby specifying the edges of its grid area. MDN Reference
    static member ``gridArea`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "gridArea" (box (value))
    /// The grid-auto-columns CSS property specifies the size of an implicitly-created grid column track or pattern of tracks. MDN Reference
    static member ``gridAutoColumns`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "gridAutoColumns" (box (value))
    /// The grid-auto-flow CSS property controls how the auto-placement algorithm works, specifying exactly how auto-placed items get flowed into the grid. MDN Reference
    static member ``gridAutoFlow`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "gridAutoFlow" (box (value))
    /// The grid-auto-rows CSS property specifies the size of an implicitly-created grid row track or pattern of tracks. MDN Reference
    static member ``gridAutoRows`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "gridAutoRows" (box (value))
    /// The grid-column CSS shorthand property specifies a grid item&apos;s size and location within a grid column by contributing a line, a span, or nothing (automatic) to its grid placement, thereby specifying the inline-start and inline-end edge of its grid area. MDN Reference
    static member ``gridColumn`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "gridColumn" (box (value))
    /// The grid-column-end CSS property specifies a grid item&apos;s end position within the grid column by contributing a line, a span, or nothing (automatic) to its grid placement, thereby specifying the block-end edge of its grid area. MDN Reference
    static member ``gridColumnEnd`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "gridColumnEnd" (box (value))
    /// @deprecated This is a legacy alias of columnGap.
    static member ``gridColumnGap`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "gridColumnGap" (box (value))
    /// The grid-column-start CSS property specifies a grid item&apos;s start position within the grid column by contributing a line, a span, or nothing (automatic) to its grid placement. This start position defines the block-start edge of the grid area. MDN Reference
    static member ``gridColumnStart`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "gridColumnStart" (box (value))
    /// @deprecated This is a legacy alias of gap.
    static member ``gridGap`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "gridGap" (box (value))
    /// The grid-row CSS shorthand property specifies a grid item&apos;s size and location within a grid row by contributing a line, a span, or nothing (automatic) to its grid placement, thereby specifying the inline-start and inline-end edge of its grid area. MDN Reference
    static member ``gridRow`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "gridRow" (box (value))
    /// The grid-row-end CSS property specifies a grid item&apos;s end position within the grid row by contributing a line, a span, or nothing (automatic) to its grid placement, thereby specifying the inline-end edge of its grid area. MDN Reference
    static member ``gridRowEnd`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "gridRowEnd" (box (value))
    /// @deprecated This is a legacy alias of rowGap.
    static member ``gridRowGap`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "gridRowGap" (box (value))
    /// The grid-row-start CSS property specifies a grid item&apos;s start position within the grid row by contributing a line, a span, or nothing (automatic) to its grid placement, thereby specifying the inline-start edge of its grid area. MDN Reference
    static member ``gridRowStart`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "gridRowStart" (box (value))
    /// The grid-template CSS property is a shorthand property for defining grid columns, grid rows, and grid areas. MDN Reference
    static member ``gridTemplate`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "gridTemplate" (box (value))
    /// The grid-template-areas CSS property specifies named grid areas, establishing the cells in the grid and assigning them names. MDN Reference
    static member ``gridTemplateAreas`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "gridTemplateAreas" (box (value))
    /// The grid-template-columns CSS property defines the line names and track sizing functions of the grid columns. MDN Reference
    static member ``gridTemplateColumns`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "gridTemplateColumns" (box (value))
    /// The grid-template-rows CSS property defines the line names and track sizing functions of the grid rows. MDN Reference
    static member ``gridTemplateRows`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "gridTemplateRows" (box (value))
    /// The height CSS property specifies the height of an element. By default, the property defines the height of the content area. If box-sizing is set to border-box, however, it instead determines the height of the border area. MDN Reference
    static member ``height`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "height" (box (value))
    /// The hyphenate-character CSS property sets the character (or string) used at the end of a line before a hyphenation break. MDN Reference
    static member ``hyphenateCharacter`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "hyphenateCharacter" (box (value))
    /// The hyphenate-limit-chars CSS property specifies the minimum word length to allow hyphenation of words as well as the minimum number of characters before and after the hyphen. MDN Reference
    static member ``hyphenateLimitChars`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "hyphenateLimitChars" (box (value))
    /// The hyphens CSS property specifies how words should be hyphenated when text wraps across multiple lines. It can prevent hyphenation entirely, hyphenate at manually-specified points within the text, or let the browser automatically insert hyphens where appropriate. MDN Reference
    static member ``hyphens`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "hyphens" (box (value))
    /// The image-orientation CSS property specifies a layout-independent correction to the orientation of an image. MDN Reference
    static member ``imageOrientation`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "imageOrientation" (box (value))
    /// The image-rendering CSS property sets an image scaling algorithm. The property applies to an element itself, to any images set in its other properties, and to its descendants. MDN Reference
    static member ``imageRendering`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "imageRendering" (box (value))
    /// The inline-size CSS property defines the size of an element&apos;s block along the inline axis. If the writing-mode is horizontal, it corresponds to the width; if the writing mode is vertical, it corresponds to the height. A related property is block-size, which defines the other dimension of the element. MDN Reference
    static member ``inlineSize`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "inlineSize" (box (value))
    /// The inset CSS property is a shorthand that corresponds to the top, right, bottom, and/or left properties. It has the same multi-value syntax of the margin shorthand. MDN Reference
    static member ``inset`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "inset" (box (value))
    /// The inset-block CSS property defines the logical block start and end offsets of an element, which maps to physical offsets depending on the element&apos;s writing mode, directionality, and text orientation. It corresponds to the top and bottom, or right and left properties depending on the values defined for writing-mode, direction, and text-orientation. MDN Reference
    static member ``insetBlock`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "insetBlock" (box (value))
    /// The inset-block-end CSS property defines the logical block end offset of an element, which maps to a physical inset depending on the element&apos;s writing mode, directionality, and text orientation. It corresponds to the top, right, bottom, or left property depending on the values defined for writing-mode, direction, and text-orientation. MDN Reference
    static member ``insetBlockEnd`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "insetBlockEnd" (box (value))
    /// The inset-block-start CSS property defines the logical block start offset of an element, which maps to a physical inset depending on the element&apos;s writing mode, directionality, and text orientation. It corresponds to the top, right, bottom, or left property depending on the values defined for writing-mode, direction, and text-orientation. MDN Reference
    static member ``insetBlockStart`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "insetBlockStart" (box (value))
    /// The inset-inline CSS property defines the logical start and end offsets of an element in the inline direction, which maps to physical offsets depending on the element&apos;s writing mode, directionality, and text orientation. It corresponds to the top and bottom, or right and left properties depending on the values defined for writing-mode, direction, and text-orientation. MDN Reference
    static member ``insetInline`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "insetInline" (box (value))
    /// The inset-inline-end CSS property defines the logical inline end inset of an element, which maps to a physical offset depending on the element&apos;s writing mode, directionality, and text orientation. It corresponds to the top, right, bottom, or left property depending on the values defined for writing-mode, direction, and text-orientation. MDN Reference
    static member ``insetInlineEnd`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "insetInlineEnd" (box (value))
    /// The inset-inline-start CSS property defines the logical inline start inset of an element, which maps to a physical offset depending on the element&apos;s writing mode, directionality, and text orientation. It corresponds to the top, right, bottom, or left property depending on the values defined for writing-mode, direction, and text-orientation. MDN Reference
    static member ``insetInlineStart`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "insetInlineStart" (box (value))
    /// The isolation CSS property determines whether an element must create a new stacking context. MDN Reference
    static member ``isolation`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "isolation" (box (value))
    /// The CSS justify-content property defines how the browser distributes space between and around content items along the main axis of a flex container and the inline axis of grid and multicol containers. MDN Reference
    static member ``justifyContent`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "justifyContent" (box (value))
    /// The CSS justify-items property defines the default justify-self for all items of the box, giving them all a default way of justifying each box along the appropriate axis. MDN Reference
    static member ``justifyItems`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "justifyItems" (box (value))
    /// The CSS justify-self property sets the way a box is justified inside its alignment container along the appropriate axis. MDN Reference
    static member ``justifySelf`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "justifySelf" (box (value))
    /// The left CSS property participates in specifying the horizontal position of a positioned element. This inset property has no effect on non-positioned elements. MDN Reference
    static member ``left`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "left" (box (value))
    /// The letter-spacing CSS property sets the horizontal spacing behavior between text characters. This value is added to the natural spacing between characters while rendering the text. Positive values of letter-spacing causes characters to spread farther apart, while negative values of letter-spacing bring characters closer together. MDN Reference
    static member ``letterSpacing`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "letterSpacing" (box (value))
    /// The lighting-color CSS property defines the color of the light source for the &lt;feDiffuseLighting&gt; and &lt;feSpecularLighting&gt; SVG lighting filter primitives within an SVG &lt;filter&gt;. If present, it overrides the element&apos;s lighting-color attribute. MDN Reference
    static member ``lightingColor`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "lightingColor" (box (value))
    /// The line-break CSS property sets how to break lines of Chinese, Japanese, or Korean (CJK) text when working with punctuation and symbols. MDN Reference
    static member ``lineBreak`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "lineBreak" (box (value))
    /// The line-height CSS property sets the height of a line box in horizontal writing modes. In vertical writing modes, it sets the width of a line box. It&apos;s commonly used to set the distance between lines of text. On block-level elements in horizontal writing modes, it specifies the preferred height of line boxes within the element, and on non-replaced inline elements, it specifies the height that is used to calculate line box height. MDN Reference
    static member ``lineHeight`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "lineHeight" (box (value))
    /// The list-style CSS shorthand property allows you to set all the list style properties at once. MDN Reference
    static member ``listStyle`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "listStyle" (box (value))
    /// The list-style-image CSS property sets an image to be used as the list item marker. MDN Reference
    static member ``listStyleImage`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "listStyleImage" (box (value))
    /// The list-style-position CSS property sets the position of the ::marker relative to a list item. MDN Reference
    static member ``listStylePosition`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "listStylePosition" (box (value))
    /// The list-style-type CSS property sets the marker (such as a disc, character, or custom counter style) of a list item element. MDN Reference
    static member ``listStyleType`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "listStyleType" (box (value))
    /// The margin CSS shorthand property sets the margin area on all four sides of an element. MDN Reference
    static member ``margin`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "margin" (box (value))
    /// The margin-block CSS shorthand property defines the logical block start and end margins of an element, which maps to physical margins depending on the element&apos;s writing mode, directionality, and text orientation. MDN Reference
    static member ``marginBlock`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "marginBlock" (box (value))
    /// The margin-block-end CSS property defines the logical block end margin of an element, which maps to a physical margin depending on the element&apos;s writing mode, directionality, and text orientation. MDN Reference
    static member ``marginBlockEnd`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "marginBlockEnd" (box (value))
    /// The margin-block-start CSS property defines the logical block start margin of an element, which maps to a physical margin depending on the element&apos;s writing mode, directionality, and text orientation. MDN Reference
    static member ``marginBlockStart`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "marginBlockStart" (box (value))
    /// The margin-bottom CSS property sets the margin area on the bottom of an element. A positive value places it farther from its neighbors, while a negative value places it closer. MDN Reference
    static member ``marginBottom`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "marginBottom" (box (value))
    /// The margin-inline CSS shorthand property is a shorthand property that defines both the logical inline start and end margins of an element, which maps to physical margins depending on the element&apos;s writing mode, directionality, and text orientation. MDN Reference
    static member ``marginInline`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "marginInline" (box (value))
    /// The margin-inline-end CSS property defines the logical inline end margin of an element, which maps to a physical margin depending on the element&apos;s writing mode, directionality, and text orientation. In other words, it corresponds to the margin-top, margin-right, margin-bottom or margin-left property depending on the values defined for writing-mode, direction, and text-orientation. MDN Reference
    static member ``marginInlineEnd`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "marginInlineEnd" (box (value))
    /// The margin-inline-start CSS property defines the logical inline start margin of an element, which maps to a physical margin depending on the element&apos;s writing mode, directionality, and text orientation. It corresponds to the margin-top, margin-right, margin-bottom, or margin-left property depending on the values defined for writing-mode, direction, and text-orientation. MDN Reference
    static member ``marginInlineStart`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "marginInlineStart" (box (value))
    /// The margin-left CSS property sets the margin area on the left side of an element. A positive value places it farther from its neighbors, while a negative value places it closer. MDN Reference
    static member ``marginLeft`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "marginLeft" (box (value))
    /// The margin-right CSS property sets the margin area on the right side of an element. A positive value places it farther from its neighbors, while a negative value places it closer. MDN Reference
    static member ``marginRight`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "marginRight" (box (value))
    /// The margin-top CSS property sets the margin area on the top of an element. A positive value places it farther from its neighbors, while a negative value places it closer. MDN Reference
    static member ``marginTop`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "marginTop" (box (value))
    /// The marker CSS property points to a marker that will be drawn on the first, middle, and last vertices of the element&apos;s path; that is, at all of its vertices. The marker must have been defined using an SVG &lt;marker&gt; element, and can only be referenced with a &lt;url&gt; value. The value of the CSS property overrides any values of the marker-start, marker, and marker-end attributes in the SVG. MDN Reference
    static member ``marker`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "marker" (box (value))
    /// The marker-end CSS property points to a marker that will be drawn on the last vertex of the element&apos;s path; that is, at its ending vertex. The marker must have been defined using an SVG &lt;marker&gt; element, and can only be referenced with a &lt;url&gt; value. The value of the CSS property overrides any values of the marker-end attribute in the SVG. MDN Reference
    static member ``markerEnd`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "markerEnd" (box (value))
    /// The marker-mid CSS property points to a marker that will be drawn on the middle vertices of the element&apos;s path; that is, at each of its vertices between the start and end vertices. The marker must have been defined using an SVG &lt;marker&gt; element, and can only be referenced with a &lt;url&gt; value. The value of the CSS property overrides any values of the marker-mid attribute in the SVG. MDN Reference
    static member ``markerMid`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "markerMid" (box (value))
    /// The marker-start CSS property points to a marker that will be drawn on the first vertex of the element&apos;s path; that is, at its starting vertex. The marker must have been defined using an SVG &lt;marker&gt; element, and can only be referenced with a &lt;url&gt; value. The value of the CSS property overrides any values of the marker-start attribute in the SVG. MDN Reference
    static member ``markerStart`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "markerStart" (box (value))
    /// The mask CSS shorthand property hides an element (partially or fully) by masking or clipping a specified area of the image. It is a shorthand for all the mask-* properties. The property accepts one or more comma-separated values, where each value corresponds to a &lt;mask-layer&gt;. MDN Reference
    static member ``mask`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "mask" (box (value))
    /// The mask-clip CSS property determines the area which is affected by a mask. The painted content of an element must be restricted to this area. MDN Reference
    static member ``maskClip`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "maskClip" (box (value))
    /// The mask-composite CSS property represents a compositing operation used on the current mask layer with the mask layers below it. MDN Reference
    static member ``maskComposite`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "maskComposite" (box (value))
    /// The mask-image CSS property sets the image that is used as the mask layer for an element, hiding sections of the element on which the masking image is set based on the alpha channel of the mask image and, depending on the mask-mode property value, the luminance of the mask image&apos;s colors. MDN Reference
    static member ``maskImage`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "maskImage" (box (value))
    /// The mask-mode CSS property is set on the element being masked. It sets whether the mask reference defined by the mask-image is treated as a luminance or alpha mask. MDN Reference
    static member ``maskMode`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "maskMode" (box (value))
    /// The mask-origin CSS property sets the origin of a mask. This property determines the mask positioning area: the area within which a mask image is positioned. HTML elements can have masks contained within their content border box, padding box, or content box, whereas SVG elements (which don&apos;t have the associated CSS layout boxes) can have masks contained inside their fill, stroke, or view box. For elements rendered as multiple boxes, such as a &lt;span&gt; of text that spans more than one line, the mask-origin property specifies which boxes the box-decoration-break property operates on to determine the mask positioning area. MDN Reference
    static member ``maskOrigin`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "maskOrigin" (box (value))
    /// The mask-position CSS property sets the initial position, relative to the mask position layer set by mask-origin, for each defined mask image. MDN Reference
    static member ``maskPosition`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "maskPosition" (box (value))
    /// The mask-repeat CSS property sets how mask images are repeated. A mask image can be repeated along the horizontal axis, the vertical axis, both axes, or not repeated at all. MDN Reference
    static member ``maskRepeat`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "maskRepeat" (box (value))
    /// The mask-size CSS property specifies the sizes of specified mask images. Mask image sizes can be fully or partially constrained to preserve their intrinsic aspect ratios. MDN Reference
    static member ``maskSize`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "maskSize" (box (value))
    /// The mask-type CSS property applies to the SVG &lt;mask&gt; element. It defines whether to use the luminance (brightness) or alpha (transparency) content of the mask. This property may be overridden by the mask-mode property. The mask-type property has no effect on image or gradient masks. MDN Reference
    static member ``maskType`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "maskType" (box (value))
    /// The math-depth property describes a notion of depth for each element of a mathematical formula, with respect to the top-level container of that formula. This is used to scale the computed value of the font-size of elements when font-size: math is applied. MDN Reference
    static member ``mathDepth`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "mathDepth" (box (value))
    /// The math-shift property indicates whether superscripts inside MathML formulas should be raised by a normal or compact shift. MDN Reference
    static member ``mathShift`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "mathShift" (box (value))
    /// The math-style property indicates whether MathML equations should render with normal or compact height. MDN Reference
    static member ``mathStyle`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "mathStyle" (box (value))
    /// The max-block-size CSS property specifies the maximum size of an element in the direction opposite that of the writing direction as specified by writing-mode. That is, if the writing direction is horizontal, then max-block-size is equivalent to max-height; if the writing direction is vertical, max-block-size is the same as max-width. MDN Reference
    static member ``maxBlockSize`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "maxBlockSize" (box (value))
    /// The max-height CSS property sets the maximum height of an element. It prevents the used value of the height property from becoming larger than the value specified for max-height. MDN Reference
    static member ``maxHeight`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "maxHeight" (box (value))
    /// The max-inline-size CSS property defines the horizontal or vertical maximum size of an element&apos;s block, depending on its writing mode. It corresponds to either the max-width or the max-height property, depending on the value of writing-mode. MDN Reference
    static member ``maxInlineSize`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "maxInlineSize" (box (value))
    /// The max-width CSS property sets the maximum width of an element. It prevents the used value of the width property from becoming larger than the value specified by max-width. MDN Reference
    static member ``maxWidth`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "maxWidth" (box (value))
    /// The min-block-size CSS property defines the minimum horizontal or vertical size of an element&apos;s block, depending on its writing mode. It corresponds to either the min-width or the min-height property, depending on the value of writing-mode. MDN Reference
    static member ``minBlockSize`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "minBlockSize" (box (value))
    /// The min-height CSS property sets the minimum height of an element. It prevents the used value of the height property from becoming smaller than the value specified for min-height. MDN Reference
    static member ``minHeight`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "minHeight" (box (value))
    /// The min-inline-size CSS property defines the horizontal or vertical minimal size of an element&apos;s block, depending on its writing mode. It corresponds to either the min-width or the min-height property, depending on the value of writing-mode. MDN Reference
    static member ``minInlineSize`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "minInlineSize" (box (value))
    /// The min-width CSS property sets the minimum width of an element. It prevents the used value of the width property from becoming smaller than the value specified for min-width. MDN Reference
    static member ``minWidth`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "minWidth" (box (value))
    /// The mix-blend-mode CSS property sets how an element&apos;s content should blend with the content of the element&apos;s parent and the element&apos;s background. MDN Reference
    static member ``mixBlendMode`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "mixBlendMode" (box (value))
    /// The object-fit CSS property sets how the content of a replaced element, such as an &lt;img&gt; or &lt;video&gt;, should be resized to fit its container. MDN Reference
    static member ``objectFit`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "objectFit" (box (value))
    /// The object-position CSS property specifies the alignment of the selected replaced element&apos;s contents within the element&apos;s box. Areas of the box which aren&apos;t covered by the replaced element&apos;s object will show the element&apos;s background. MDN Reference
    static member ``objectPosition`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "objectPosition" (box (value))
    /// The offset CSS shorthand property sets all the properties required for animating an element along a defined path. The offset properties together help to define an offset transform, a transform that aligns a point in an element (offset-anchor) to an offset position (offset-position) on a path (offset-path) at various points along the path (offset-distance) and optionally rotates the element (offset-rotate) to follow the direction of the path. MDN Reference
    static member ``offset`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "offset" (box (value))
    /// The offset-anchor CSS property specifies the point inside the box of an element traveling along an offset-path that is actually moving along the path. MDN Reference
    static member ``offsetAnchor`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "offsetAnchor" (box (value))
    /// The offset-distance CSS property specifies a position along an offset-path for an element to be placed. MDN Reference
    static member ``offsetDistance`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "offsetDistance" (box (value))
    /// The offset-path CSS property specifies a path for an element to follow and determines the element&apos;s positioning within the path&apos;s parent container or the SVG coordinate system. The path is a line, a curve, or a geometrical shape along which the element gets positioned or moves. MDN Reference
    static member ``offsetPath`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "offsetPath" (box (value))
    /// The offset-position CSS property defines the initial position of an element along a path. This property is typically used in combination with the offset-path property to create a motion effect. The value of offset-position determines where the element gets placed initially for moving along an offset path if an offset-path function such as path() does not specify its own starting position. MDN Reference
    static member ``offsetPosition`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "offsetPosition" (box (value))
    /// The offset-rotate CSS property defines the orientation/direction of the element as it is positioned along the offset-path. MDN Reference
    static member ``offsetRotate`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "offsetRotate" (box (value))
    /// The opacity CSS property sets the opacity of an element. Opacity is the degree to which content behind an element is hidden, and is the opposite of transparency. MDN Reference
    static member ``opacity`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "opacity" (box (value))
    /// The order CSS property sets the order to lay out an item in a flex or grid container. Items in a container are sorted by ascending order value and then by their source code order. Items not given an explicit order value are assigned the default value of 0. MDN Reference
    static member ``order`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "order" (box (value))
    /// The orphans CSS property sets the minimum number of lines in a block container that must be shown at the bottom of a page, region, or column. MDN Reference
    static member ``orphans`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "orphans" (box (value))
    /// The outline CSS shorthand property sets most of the outline properties in a single declaration. MDN Reference
    static member ``outline`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "outline" (box (value))
    /// The outline-color CSS property sets the color of an element&apos;s outline. MDN Reference
    static member ``outlineColor`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "outlineColor" (box (value))
    /// The outline-offset CSS property sets the amount of space between an outline and the edge or border of an element. MDN Reference
    static member ``outlineOffset`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "outlineOffset" (box (value))
    /// The outline-style CSS property sets the style of an element&apos;s outline. An outline is a line that is drawn around an element, outside the border. MDN Reference
    static member ``outlineStyle`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "outlineStyle" (box (value))
    /// The CSS outline-width property sets the thickness of an element&apos;s outline. An outline is a line that is drawn around an element, outside the border. MDN Reference
    static member ``outlineWidth`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "outlineWidth" (box (value))
    /// The overflow CSS shorthand property sets the desired behavior when content does not fit in the element&apos;s padding box (overflows) in the horizontal and/or vertical direction. MDN Reference
    static member ``overflow`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "overflow" (box (value))
    /// The overflow-anchor CSS property provides a way to opt out of the browser&apos;s scroll anchoring behavior, which adjusts scroll position to minimize content shifts. MDN Reference
    static member ``overflowAnchor`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "overflowAnchor" (box (value))
    /// The overflow-block CSS property sets what shows when content overflows the block start and block end edges of a box. This may be nothing, a scroll bar, or the overflow content. MDN Reference
    static member ``overflowBlock`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "overflowBlock" (box (value))
    /// The overflow-clip-margin CSS property determines how far outside its bounds an element with overflow: clip may be painted before being clipped. The bound defined by this property is called the overflow clip edge of the box. MDN Reference
    static member ``overflowClipMargin`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "overflowClipMargin" (box (value))
    /// The overflow-inline CSS property sets what shows when content overflows the inline start and end edges of a box. This may be nothing, a scroll bar, or the overflow content. MDN Reference
    static member ``overflowInline`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "overflowInline" (box (value))
    /// The overflow-wrap CSS property applies to text, setting whether the browser should insert line breaks within an otherwise unbreakable string to prevent text from overflowing its line box. MDN Reference
    static member ``overflowWrap`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "overflowWrap" (box (value))
    /// The overflow-x CSS property sets what shows when content overflows a block-level element&apos;s left and right edges. This may be nothing, a scroll bar, or the overflow content. This property may also be set by using the overflow shorthand property. MDN Reference
    static member ``overflowX`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "overflowX" (box (value))
    /// The overflow-y CSS property sets what shows when content overflows a block-level element&apos;s top and bottom edges. This may be nothing, a scroll bar, or the overflow content. This property may also be set by using the overflow shorthand property. MDN Reference
    static member ``overflowY`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "overflowY" (box (value))
    /// The overscroll-behavior CSS property sets what a browser does when reaching the boundary of a scrolling area. MDN Reference
    static member ``overscrollBehavior`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "overscrollBehavior" (box (value))
    /// The overscroll-behavior-block CSS property sets the browser&apos;s behavior when the block direction boundary of a scrolling area is reached. MDN Reference
    static member ``overscrollBehaviorBlock`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "overscrollBehaviorBlock" (box (value))
    /// The overscroll-behavior-inline CSS property sets the browser&apos;s behavior when the inline direction boundary of a scrolling area is reached. MDN Reference
    static member ``overscrollBehaviorInline`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "overscrollBehaviorInline" (box (value))
    /// The overscroll-behavior-x CSS property sets the browser&apos;s behavior when the horizontal boundary of a scrolling area is reached. MDN Reference
    static member ``overscrollBehaviorX`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "overscrollBehaviorX" (box (value))
    /// The overscroll-behavior-y CSS property sets the browser&apos;s behavior when the vertical boundary of a scrolling area is reached. MDN Reference
    static member ``overscrollBehaviorY`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "overscrollBehaviorY" (box (value))
    /// The padding CSS shorthand property sets the padding area on all four sides of an element at once. MDN Reference
    static member ``padding`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "padding" (box (value))
    /// The padding-block CSS shorthand property defines the logical block start and end padding of an element, which maps to physical padding properties depending on the element&apos;s writing mode, directionality, and text orientation. MDN Reference
    static member ``paddingBlock`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "paddingBlock" (box (value))
    /// The padding-block-end CSS property defines the logical block end padding of an element, which maps to a physical padding depending on the element&apos;s writing mode, directionality, and text orientation. MDN Reference
    static member ``paddingBlockEnd`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "paddingBlockEnd" (box (value))
    /// The padding-block-start CSS property defines the logical block start padding of an element, which maps to a physical padding depending on the element&apos;s writing mode, directionality, and text orientation. MDN Reference
    static member ``paddingBlockStart`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "paddingBlockStart" (box (value))
    /// The padding-bottom CSS property sets the height of the padding area on the bottom of an element. MDN Reference
    static member ``paddingBottom`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "paddingBottom" (box (value))
    /// The padding-inline CSS shorthand property defines the logical inline start and end padding of an element, which maps to physical padding properties depending on the element&apos;s writing mode, directionality, and text orientation. MDN Reference
    static member ``paddingInline`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "paddingInline" (box (value))
    /// The padding-inline-end CSS property defines the logical inline end padding of an element, which maps to a physical padding depending on the element&apos;s writing mode, directionality, and text orientation. MDN Reference
    static member ``paddingInlineEnd`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "paddingInlineEnd" (box (value))
    /// The padding-inline-start CSS property defines the logical inline start padding of an element, which maps to a physical padding depending on the element&apos;s writing mode, directionality, and text orientation. MDN Reference
    static member ``paddingInlineStart`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "paddingInlineStart" (box (value))
    /// The padding-left CSS property sets the width of the padding area to the left of an element. MDN Reference
    static member ``paddingLeft`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "paddingLeft" (box (value))
    /// The padding-right CSS property sets the width of the padding area on the right of an element. MDN Reference
    static member ``paddingRight`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "paddingRight" (box (value))
    /// The padding-top CSS property sets the height of the padding area on the top of an element. MDN Reference
    static member ``paddingTop`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "paddingTop" (box (value))
    /// The page CSS property is used to specify the named page, a specific type of page defined by the  @page at-rule. MDN Reference
    static member ``page`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "page" (box (value))
    /// The page-break-after CSS property adjusts page breaks after the current element. @deprecated MDN Reference
    static member ``pageBreakAfter`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "pageBreakAfter" (box (value))
    /// The page-break-before CSS property adjusts page breaks before the current element. @deprecated MDN Reference
    static member ``pageBreakBefore`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "pageBreakBefore" (box (value))
    /// The page-break-inside CSS property adjusts page breaks inside the current element. @deprecated MDN Reference
    static member ``pageBreakInside`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "pageBreakInside" (box (value))
    /// The paint-order CSS property lets you control the order in which the fill and stroke (and painting markers) of text content and shapes are drawn. MDN Reference
    static member ``paintOrder`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "paintOrder" (box (value))
    /// The perspective CSS property determines the distance between the z=0 plane and the user in order to give a 3D-positioned element some perspective. MDN Reference
    static member ``perspective`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "perspective" (box (value))
    /// The perspective-origin CSS property determines the position at which the viewer is looking. It is used as the vanishing point by the perspective property. MDN Reference
    static member ``perspectiveOrigin`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "perspectiveOrigin" (box (value))
    /// The place-content CSS shorthand property allows you to align content along both the block and inline directions at once (i.e., the align-content and justify-content properties) in a relevant layout system such as Grid or Flexbox. MDN Reference
    static member ``placeContent`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "placeContent" (box (value))
    /// The CSS place-items shorthand property aligns items along both the block and inline directions at once. It sets the values of the align-items and justify-items properties. If the second value is not set, the first value is also used for it. MDN Reference
    static member ``placeItems`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "placeItems" (box (value))
    /// The place-self CSS shorthand property allows you to align an individual item in both the block and inline directions at once (i.e., the align-self and justify-self properties). This property applies to block-level boxes, absolutely-positioned boxes, and grid items. If the second value is not present, the first value is also used for it. MDN Reference
    static member ``placeSelf`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "placeSelf" (box (value))
    /// The pointer-events CSS property sets under what circumstances (if any) a particular graphic element can become the target of pointer events. MDN Reference
    static member ``pointerEvents`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "pointerEvents" (box (value))
    /// The position CSS property sets how an element is positioned in a document. The top, right, bottom, and left physical properties and the inset-block-start, inset-block-end, inset-inline-start, and inset-inline-end flow-relative logical properties can be used to determine the final location of positioned elements. MDN Reference
    static member ``position`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "position" (box (value))
    /// The position-anchor CSS property specifies the anchor name of the anchor element (i.e., an element that has an anchor name set on it via the anchor-name property) a positioned element is associated with. MDN Reference
    static member ``positionAnchor`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "positionAnchor" (box (value))
    /// The position-area CSS property enables an anchor-positioned element to be positioned relative to the edges of its associated anchor element by placing the positioned element on one or more tiles of an implicit 3x3 grid, where the anchoring element is the center cell. MDN Reference
    static member ``positionArea`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "positionArea" (box (value))
    /// The position-try CSS property is a shorthand that corresponds to the position-try-order and position-try-fallbacks properties. MDN Reference
    static member ``positionTry`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "positionTry" (box (value))
    /// The position-try-fallbacks CSS property enables you to specify a list of one or more alternative position try fallback options for anchor-positioned elements to be placed relative to their associated anchor elements. When the element would otherwise overflow its inset-modified containing block, the browser will try placing the positioned element in these different fallback positions, in the order provided, until it finds a value that stops it from overflowing its container or the viewport. MDN Reference
    static member ``positionTryFallbacks`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "positionTryFallbacks" (box (value))
    /// The position-try-order CSS property allows you to specify various fallback options that result in an available position-try fallback being used to set an anchor-positioned element&apos;s position, instead of its initial position settings. MDN Reference
    static member ``positionTryOrder`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "positionTryOrder" (box (value))
    /// The position-visibility CSS property enables conditionally hiding an anchor-positioned element depending on, for example, whether it is overflowing its containing element or the viewport. MDN Reference
    static member ``positionVisibility`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "positionVisibility" (box (value))
    /// The print-color-adjust CSS property sets what, if anything, the user agent may do to optimize the appearance of the element on the output device. By default, the browser is allowed to make any adjustments to the element&apos;s appearance it determines to be necessary and prudent given the type and capabilities of the output device. MDN Reference
    static member ``printColorAdjust`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "printColorAdjust" (box (value))
    /// The CSS quotes property sets how the browser should render quotation marks that are automatically added to the HTML &lt;q&gt; element or added using the open-quotes or close-quotes (or omitted using the no-open-quote and no-close-quote) values of the of the CSS content property. MDN Reference
    static member ``quotes`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "quotes" (box (value))
    /// The r CSS property defines the radius of a circle. It can only be used with the SVG &lt;circle&gt; element. If present, it overrides the circle&apos;s r attribute. MDN Reference
    static member ``r`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "r" (box (value))
    /// The resize CSS property sets whether an element is resizable, and if so, in which directions. MDN Reference
    static member ``resize`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "resize" (box (value))
    /// The right CSS property participates in specifying the horizontal position of a positioned element. This inset property has no effect on non-positioned elements. MDN Reference
    static member ``right`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "right" (box (value))
    /// The rotate CSS property allows you to specify rotation transforms individually and independently of the transform property. This maps better to typical user interface usage, and saves having to remember the exact order of transform functions to specify in the transform property. MDN Reference
    static member ``rotate`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "rotate" (box (value))
    /// The row-gap CSS property sets the size of the gap (gutter) between an element&apos;s rows. MDN Reference
    static member ``rowGap`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "rowGap" (box (value))
    /// The ruby-align CSS property defines the distribution of the different ruby elements over the base. MDN Reference
    static member ``rubyAlign`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "rubyAlign" (box (value))
    /// The ruby-position CSS property defines the position of a ruby element relative to its base element. It can be positioned over the element (over), under it (under), or between the characters on their right side (inter-character). MDN Reference
    static member ``rubyPosition`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "rubyPosition" (box (value))
    /// The rx CSS property defines the x-axis, or horizontal, radius of an SVG &lt;ellipse&gt; and the horizontal curve of the corners of an SVG &lt;rect&gt; rectangle. If present, it overrides the shape&apos;s rx attribute. MDN Reference
    static member ``rx`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "rx" (box (value))
    /// The ry CSS property defines the y-axis, or vertical, radius of an SVG &lt;ellipse&gt; and the vertical curve of the corners of an SVG &lt;rect&gt; rectangle. If present, it overrides the shape&apos;s ry attribute. MDN Reference
    static member ``ry`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "ry" (box (value))
    /// The scale CSS property allows you to specify scale transforms individually and independently of the transform property. This maps better to typical user interface usage, and saves having to remember the exact order of transform functions to specify in the transform value. MDN Reference
    static member ``scale`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scale" (box (value))
    /// The scroll-behavior CSS property sets the behavior for a scrolling box when scrolling is triggered by the navigation or CSSOM scrolling APIs. MDN Reference
    static member ``scrollBehavior`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollBehavior" (box (value))
    /// The scroll-margin shorthand property sets all of the scroll margins of an element at once, assigning values much like the margin property does for margins of an element. MDN Reference
    static member ``scrollMargin`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollMargin" (box (value))
    /// The scroll-margin-block shorthand property sets the scroll margins of an element in the block dimension. MDN Reference
    static member ``scrollMarginBlock`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollMarginBlock" (box (value))
    /// The scroll-margin-block-end property defines the margin of the scroll snap area at the end of the block dimension that is used for snapping this box to the snapport. The scroll snap area is determined by taking the transformed border box, finding its rectangular bounding box (axis-aligned in the scroll container&apos;s coordinate space), then adding the specified outsets. MDN Reference
    static member ``scrollMarginBlockEnd`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollMarginBlockEnd" (box (value))
    /// The scroll-margin-block-start property defines the margin of the scroll snap area at the start of the block dimension that is used for snapping this box to the snapport. The scroll snap area is determined by taking the transformed border box, finding its rectangular bounding box (axis-aligned in the scroll container&apos;s coordinate space), then adding the specified outsets. MDN Reference
    static member ``scrollMarginBlockStart`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollMarginBlockStart" (box (value))
    /// The scroll-margin-bottom property defines the bottom margin of the scroll snap area that is used for snapping this box to the snapport. The scroll snap area is determined by taking the transformed border box, finding its rectangular bounding box (axis-aligned in the scroll container&apos;s coordinate space), then adding the specified outsets. MDN Reference
    static member ``scrollMarginBottom`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollMarginBottom" (box (value))
    /// The scroll-margin-inline shorthand property sets the scroll margins of an element in the inline dimension. MDN Reference
    static member ``scrollMarginInline`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollMarginInline" (box (value))
    /// The scroll-margin-inline-end property defines the margin of the scroll snap area at the end of the inline dimension that is used for snapping this box to the snapport. The scroll snap area is determined by taking the transformed border box, finding its rectangular bounding box (axis-aligned in the scroll container&apos;s coordinate space), then adding the specified outsets. MDN Reference
    static member ``scrollMarginInlineEnd`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollMarginInlineEnd" (box (value))
    /// The scroll-margin-inline-start property defines the margin of the scroll snap area at the start of the inline dimension that is used for snapping this box to the snapport. The scroll snap area is determined by taking the transformed border box, finding its rectangular bounding box (axis-aligned in the scroll container&apos;s coordinate space), then adding the specified outsets. MDN Reference
    static member ``scrollMarginInlineStart`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollMarginInlineStart" (box (value))
    /// The scroll-margin-left property defines the left margin of the scroll snap area that is used for snapping this box to the snapport. The scroll snap area is determined by taking the transformed border box, finding its rectangular bounding box (axis-aligned in the scroll container&apos;s coordinate space), then adding the specified outsets. MDN Reference
    static member ``scrollMarginLeft`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollMarginLeft" (box (value))
    /// The scroll-margin-right property defines the right margin of the scroll snap area that is used for snapping this box to the snapport. The scroll snap area is determined by taking the transformed border box, finding its rectangular bounding box (axis-aligned in the scroll container&apos;s coordinate space), then adding the specified outsets. MDN Reference
    static member ``scrollMarginRight`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollMarginRight" (box (value))
    /// The scroll-margin-top property defines the top margin of the scroll snap area that is used for snapping this box to the snapport. The scroll snap area is determined by taking the transformed border box, finding its rectangular bounding box (axis-aligned in the scroll container&apos;s coordinate space), then adding the specified outsets. MDN Reference
    static member ``scrollMarginTop`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollMarginTop" (box (value))
    /// The scroll-padding shorthand property sets scroll padding on all sides of an element at once. It specifies offsets that define the optimal viewing region of a scrollport within a scroll container. MDN Reference
    static member ``scrollPadding`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollPadding" (box (value))
    /// The scroll-padding-block shorthand property sets the scroll padding of an element in the block dimension. MDN Reference
    static member ``scrollPaddingBlock`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollPaddingBlock" (box (value))
    /// The scroll-padding-block-end property defines offsets for the end edge in the block dimension of the optimal viewing region of the scrollport: the region used as the target region for placing things in view of the user. This allows the author to exclude regions of the scrollport that are obscured by other content (such as fixed-positioned toolbars or sidebars) or to put more breathing room between a targeted element and the edges of the scrollport. MDN Reference
    static member ``scrollPaddingBlockEnd`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollPaddingBlockEnd" (box (value))
    /// The scroll-padding-block-start property defines offsets for the start edge in the block dimension of the optimal viewing region of the scrollport: the region used as the target region for placing things in view of the user. This allows the author to exclude regions of the scrollport that are obscured by other content (such as fixed-positioned toolbars or sidebars) or to put more breathing room between a targeted element and the edges of the scrollport. MDN Reference
    static member ``scrollPaddingBlockStart`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollPaddingBlockStart" (box (value))
    /// The scroll-padding-bottom property defines offsets for the bottom of the optimal viewing region of the scrollport: the region used as the target region for placing things in view of the user. This allows the author to exclude regions of the scrollport that are obscured by other content (such as fixed-positioned toolbars or sidebars) or to put more breathing room between a targeted element and the edges of the scrollport. MDN Reference
    static member ``scrollPaddingBottom`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollPaddingBottom" (box (value))
    /// The scroll-padding-inline shorthand property sets the scroll padding of an element in the inline dimension. MDN Reference
    static member ``scrollPaddingInline`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollPaddingInline" (box (value))
    /// The scroll-padding-inline-end property defines offsets for the end edge in the inline dimension of the optimal viewing region of the scrollport: the region used as the target region for placing things in view of the user. This allows the author to exclude regions of the scrollport that are obscured by other content (such as fixed-positioned toolbars or sidebars) or to put more breathing room between a targeted element and the edges of the scrollport. MDN Reference
    static member ``scrollPaddingInlineEnd`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollPaddingInlineEnd" (box (value))
    /// The scroll-padding-inline-start property defines offsets for the start edge in the inline dimension of the optimal viewing region of the scrollport: the region used as the target region for placing things in view of the user. This allows the author to exclude regions of the scrollport that are obscured by other content (such as fixed-positioned toolbars or sidebars) or to put more breathing room between a targeted element and the edges of the scrollport. MDN Reference
    static member ``scrollPaddingInlineStart`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollPaddingInlineStart" (box (value))
    /// The scroll-padding-left property defines offsets for the left of the optimal viewing region of the scrollport: the region used as the target region for placing things in view of the user. This allows the author to exclude regions of the scrollport that are obscured by other content (such as fixed-positioned toolbars or sidebars) or to put more breathing room between a targeted element and the edges of the scrollport. MDN Reference
    static member ``scrollPaddingLeft`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollPaddingLeft" (box (value))
    /// The scroll-padding-right property defines offsets for the right of the optimal viewing region of the scrollport: the region used as the target region for placing things in view of the user. This allows the author to exclude regions of the scrollport that are obscured by other content (such as fixed-positioned toolbars or sidebars) or to put more breathing room between a targeted element and the edges of the scrollport. MDN Reference
    static member ``scrollPaddingRight`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollPaddingRight" (box (value))
    /// The scroll-padding-top property defines offsets for the top of the optimal viewing region of the scrollport: the region used as the target region for placing things in view of the user. This allows the author to exclude regions of the scrollport that are obscured by other content (such as fixed-positioned toolbars or sidebars) or to put more breathing room between a targeted element and the edges of the scrollport. MDN Reference
    static member ``scrollPaddingTop`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollPaddingTop" (box (value))
    /// The scroll-snap-align property specifies the box&apos;s snap position as an alignment of its snap area (as the alignment subject) within its snap container&apos;s snap port (as the alignment container). MDN Reference
    static member ``scrollSnapAlign`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollSnapAlign" (box (value))
    /// The scroll-snap-stop CSS property defines whether or not the scroll container is allowed to &quot;pass over&quot; possible snap positions. MDN Reference
    static member ``scrollSnapStop`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollSnapStop" (box (value))
    /// The scroll-snap-type CSS property is set on a scroll container, opting it into scroll snapping by setting the direction and strictness of snap point enforcement within the snap port. MDN Reference
    static member ``scrollSnapType`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollSnapType" (box (value))
    /// The scroll-timeline CSS shorthand property is used to define a named scroll progress timeline, which is progressed through by scrolling a scrollable element (scroller) between top and bottom (or left and right). MDN Reference
    static member ``scrollTimeline`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollTimeline" (box (value))
    /// The scroll-timeline-axis CSS property is used to specify the scrollbar direction that will be used to provide the timeline for a scroll driven animation, which is progressed through by scrolling a scrollable element (scroller). MDN Reference
    static member ``scrollTimelineAxis`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollTimelineAxis" (box (value))
    /// The scroll-timeline-name CSS property is used to define the name of a named scroll progress timeline, which is progressed through by scrolling a scrollable element (scroller) between top and bottom (or left and right). scroll-timeline-name is set on the scroller that will provide the timeline. MDN Reference
    static member ``scrollTimelineName`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollTimelineName" (box (value))
    /// The scrollbar-color CSS property sets the color of the scrollbar track and thumb. MDN Reference
    static member ``scrollbarColor`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollbarColor" (box (value))
    /// The scrollbar-gutter CSS property allows authors to reserve space for the scrollbar, preventing unwanted layout changes as the content grows while also avoiding unnecessary visuals when scrolling isn&apos;t needed. MDN Reference
    static member ``scrollbarGutter`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollbarGutter" (box (value))
    /// The scrollbar-width property allows the author to set the desired thickness of an element&apos;s scrollbars when they are shown. MDN Reference
    static member ``scrollbarWidth`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scrollbarWidth" (box (value))
    /// The shape-image-threshold CSS property sets the alpha channel threshold used to extract the shape using an image as the value for shape-outside. MDN Reference
    static member ``shapeImageThreshold`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "shapeImageThreshold" (box (value))
    /// The shape-margin CSS property sets a margin for a CSS shape created using shape-outside. MDN Reference
    static member ``shapeMargin`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "shapeMargin" (box (value))
    /// The shape-outside CSS property defines a shape—which may be non-rectangular—around which adjacent inline content should wrap. By default, inline content wraps around its margin box; shape-outside provides a way to customize this wrapping, making it possible to wrap text around complex objects rather than rectangular boxes. MDN Reference
    static member ``shapeOutside`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "shapeOutside" (box (value))
    /// The shape-rendering CSS property provides hints to the renderer about what tradeoffs to make when rendering shapes like paths, circles, or rectangles. It only has an effect on the &lt;circle&gt;, &lt;ellipse&gt;, &lt;line&gt;, &lt;path&gt;, &lt;polygon&gt;, &lt;polyline&gt;, and &lt;rect&gt; elements. If explicitly declared, the value of the CSS property overrides the any values of the element&apos;s shape-rendering attribute. MDN Reference
    static member ``shapeRendering`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "shapeRendering" (box (value))
    /// The stop-color CSS property defines the color to use for an SVG &lt;stop&gt; element within a gradient. If present, it overrides the element&apos;s stop-color attribute. MDN Reference
    static member ``stopColor`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "stopColor" (box (value))
    /// The stop-opacity CSS property defines the opacity of a given color gradient stop in the SVG &lt;stop&gt; element within an SVG gradient. If present, it overrides the element&apos;s stop-opacity attribute. MDN Reference
    static member ``stopOpacity`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "stopOpacity" (box (value))
    /// The stroke CSS property defines the color or SVG paint server used to draw an element&apos;s stroke. As such, stroke only has an effect on elements that can be given a stroke (for example, &lt;rect&gt; or &lt;ellipse&gt;); see the page on the SVG stroke attribute for a complete list. When declared, the CSS value overrides any value of the element&apos;s stroke SVG attribute. MDN Reference
    static member ``stroke`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "stroke" (box (value))
    /// The stroke-dasharray CSS property defines a pattern of dashes and gaps used in the painting of the SVG shape&apos;s stroke. If present, it overrides the element&apos;s stroke-dasharray attribute. MDN Reference
    static member ``strokeDasharray`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "strokeDasharray" (box (value))
    /// The stroke-dashoffset CSS property defines an offset for the starting point of the rendering of an SVG element&apos;s associated dash array. If present, it overrides the element&apos;s stroke-dashoffset attribute. MDN Reference
    static member ``strokeDashoffset`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "strokeDashoffset" (box (value))
    /// The stroke-linecap CSS property defines the shape to be used at the end of open subpaths of SVG elements&apos; unclosed strokes. If present, it overrides the element&apos;s stroke-linecap attribute. MDN Reference
    static member ``strokeLinecap`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "strokeLinecap" (box (value))
    /// The stroke-linejoin CSS property defines the shape to be used at the corners of an SVG element&apos;s stroked paths. If present, it overrides the element&apos;s stroke-linejoin attribute. MDN Reference
    static member ``strokeLinejoin`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "strokeLinejoin" (box (value))
    /// The stroke-miterlimit CSS property defines a limit on the ratio of the miter length to the stroke-width when the shape to be used at the corners of an SVG element&apos;s stroked path is a mitered join. If the limit defined by this property is exceeded, the join is converted from miter to bevel, thus making the corner appear truncated. MDN Reference
    static member ``strokeMiterlimit`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "strokeMiterlimit" (box (value))
    /// The stroke-opacity CSS property defines the opacity of an SVG shape&apos;s stroke. The effect is identical to that of opacity, except it is applied only to the stroke, not to the entire element. If present, it overrides the element&apos;s stroke-opacity attribute. MDN Reference
    static member ``strokeOpacity`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "strokeOpacity" (box (value))
    /// The stroke-width CSS property defines the width of a stroke applied to the SVG shape. If present, it overrides the element&apos;s stroke-width attribute. MDN Reference
    static member ``strokeWidth`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "strokeWidth" (box (value))
    /// The tab-size CSS property is used to customize the width of tab characters (U+0009). MDN Reference
    static member ``tabSize`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "tabSize" (box (value))
    /// The table-layout CSS property sets the algorithm used to lay out &lt;table&gt; cells, rows, and columns. MDN Reference
    static member ``tableLayout`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "tableLayout" (box (value))
    /// The text-align CSS property sets the horizontal alignment of the inline-level content inside a block element or table-cell box. This means it works like vertical-align but in the horizontal direction. MDN Reference
    static member ``textAlign`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textAlign" (box (value))
    /// The text-align-last CSS property sets how the last line of a block or a line, right before a forced line break, is aligned. MDN Reference
    static member ``textAlignLast`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textAlignLast" (box (value))
    /// The text-anchor CSS property aligns a box containing a string of text where the wrapping area is determined from the inline-size property, and the text is then placed relative to the anchor point of the element, which is defined using the x and y (or dx and dy) attributes. If present, the value of the CSS property overrides any value of the element&apos;s text-anchor attribute. MDN Reference
    static member ``textAnchor`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textAnchor" (box (value))
    /// The text-autospace CSS property allows you to specify the space applied between Chinese/Japanese/Korean (CJK) and non-CJK characters. MDN Reference
    static member ``textAutospace`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textAutospace" (box (value))
    /// The text-box CSS property is a shorthand that corresponds to the text-box-trim and text-box-edge properties, which together specify the amount of space to trim from the block-start edge and block-end edge of a text element&apos;s block container. MDN Reference
    static member ``textBox`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textBox" (box (value))
    /// The text-box-edge CSS property specifies an amount of space to trim from a text element&apos;s block container. MDN Reference
    static member ``textBoxEdge`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textBoxEdge" (box (value))
    /// The text-box-trim CSS property specifies which of the over and under edges of text content to trim from a text element&apos;s block container. MDN Reference
    static member ``textBoxTrim`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textBoxTrim" (box (value))
    /// The text-combine-upright CSS property sets the combination of characters into the space of a single character. If the combined text is wider than 1em, the user agent must fit the contents within 1em. The resulting composition is treated as a single upright glyph for layout and decoration. This property only has an effect in vertical writing modes. MDN Reference
    static member ``textCombineUpright`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textCombineUpright" (box (value))
    /// The text-decoration shorthand CSS property sets the appearance of decorative lines on text. It is a shorthand for text-decoration-line, text-decoration-color, text-decoration-style, and the newer text-decoration-thickness property. MDN Reference
    static member ``textDecoration`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textDecoration" (box (value))
    /// The text-decoration-color CSS property sets the color of decorations added to text by text-decoration-line. MDN Reference
    static member ``textDecorationColor`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textDecorationColor" (box (value))
    /// The text-decoration-line CSS property sets the kind of decoration that is used on text in an element, such as an underline or overline. MDN Reference
    static member ``textDecorationLine`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textDecorationLine" (box (value))
    /// The text-decoration-skip-ink CSS property specifies how overlines and underlines are drawn when they pass over glyph ascenders and descenders. MDN Reference
    static member ``textDecorationSkipInk`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textDecorationSkipInk" (box (value))
    /// The text-decoration-style CSS property sets the style of the lines specified by text-decoration-line. The style applies to all lines that are set with text-decoration-line. MDN Reference
    static member ``textDecorationStyle`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textDecorationStyle" (box (value))
    /// The text-decoration-thickness CSS property sets the stroke thickness of the decoration line that is used on text in an element, such as a line-through, underline, or overline. MDN Reference
    static member ``textDecorationThickness`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textDecorationThickness" (box (value))
    /// The text-emphasis CSS property applies emphasis marks to text (except spaces and control characters). It is a shorthand for text-emphasis-style and text-emphasis-color. MDN Reference
    static member ``textEmphasis`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textEmphasis" (box (value))
    /// The text-emphasis-color CSS property sets the color of emphasis marks. This value can also be set using the text-emphasis shorthand. MDN Reference
    static member ``textEmphasisColor`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textEmphasisColor" (box (value))
    /// The text-emphasis-position CSS property sets where emphasis marks are drawn. Similar to the text rendered by the &lt;ruby&gt; HTML element, if there isn&apos;t enough room for emphasis marks, the line height is increased. MDN Reference
    static member ``textEmphasisPosition`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textEmphasisPosition" (box (value))
    /// The text-emphasis-style CSS property sets the appearance of emphasis marks. It can also be set, and reset, using the text-emphasis shorthand. MDN Reference
    static member ``textEmphasisStyle`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textEmphasisStyle" (box (value))
    /// The text-indent CSS property sets the length of empty space (indentation) that is put before lines of text in a block. MDN Reference
    static member ``textIndent`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textIndent" (box (value))
    /// The text-justify CSS property sets what type of justification should be applied to text when text-align: justify; is set on an element. MDN Reference
    static member ``textJustify`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textJustify" (box (value))
    /// The text-orientation CSS property sets the orientation of the text characters in a line. It only affects text in vertical mode (when writing-mode is not horizontal-tb). It is useful for controlling the display of languages that use vertical script, and also for making vertical table headers. MDN Reference
    static member ``textOrientation`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textOrientation" (box (value))
    /// The text-overflow CSS property sets how hidden overflow content is signaled to users. It can be clipped, display an ellipsis (…), or display a custom string. MDN Reference
    static member ``textOverflow`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textOverflow" (box (value))
    /// The text-rendering CSS property provides information to the rendering engine about what to optimize for when rendering text. MDN Reference
    static member ``textRendering`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textRendering" (box (value))
    /// The text-shadow CSS property adds shadows to text. It accepts a comma-separated list of shadows to be applied to the text and any of its text-decoration. Each shadow is described by some combination of X and Y offsets from the element, blur radius, and color. MDN Reference
    static member ``textShadow`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textShadow" (box (value))
    /// The text-transform CSS property specifies how to capitalize an element&apos;s text. It can be used to make text appear in all-uppercase or all-lowercase, or with each word capitalized. It also can help improve legibility for ruby. MDN Reference
    static member ``textTransform`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textTransform" (box (value))
    /// The text-underline-offset CSS property sets the offset distance of an underline text decoration line (applied using text-decoration) from its original position. MDN Reference
    static member ``textUnderlineOffset`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textUnderlineOffset" (box (value))
    /// The text-underline-position CSS property specifies the position of the underline which is set using the text-decoration property&apos;s underline value. MDN Reference
    static member ``textUnderlinePosition`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textUnderlinePosition" (box (value))
    /// The text-wrap CSS shorthand property controls how text inside an element is wrapped. The different values provide: MDN Reference
    static member ``textWrap`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textWrap" (box (value))
    /// The text-wrap-mode CSS property controls whether the text inside an element is wrapped. The different values provide alternate ways of wrapping the content of a block element. It can also be set, and reset, using the text-wrap shorthand or the white-space shorthand. MDN Reference
    static member ``textWrapMode`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textWrapMode" (box (value))
    /// The text-wrap-style CSS property controls how text inside an element is wrapped. The different values provide alternate ways of wrapping the content of a block element. It can also be set, and reset, using the text-wrap shorthand. MDN Reference
    static member ``textWrapStyle`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "textWrapStyle" (box (value))
    /// The timeline-scope CSS property modifies the scope of a named animation timeline. MDN Reference
    static member ``timelineScope`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "timelineScope" (box (value))
    /// The top CSS property sets the vertical position of a positioned element. This inset property has no effect on non-positioned elements. MDN Reference
    static member ``top`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "top" (box (value))
    /// The touch-action CSS property sets how an element&apos;s region can be manipulated by a touchscreen user (for example, by zooming features built into the browser). MDN Reference
    static member ``touchAction`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "touchAction" (box (value))
    /// The transform CSS property lets you rotate, scale, skew, or translate an element. It modifies the coordinate space of the CSS visual formatting model. MDN Reference
    static member ``transform`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "transform" (box (value))
    /// The transform-box CSS property defines the layout box to which the transform, individual transform properties translate, scale, and rotate, and transform-origin properties relate. MDN Reference
    static member ``transformBox`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "transformBox" (box (value))
    /// The transform-origin CSS property sets the origin for an element&apos;s transformations. MDN Reference
    static member ``transformOrigin`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "transformOrigin" (box (value))
    /// The transform-style CSS property sets whether children of an element are positioned in the 3D space or are flattened in the plane of the element. MDN Reference
    static member ``transformStyle`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "transformStyle" (box (value))
    /// The transition CSS property is a shorthand property for transition-property, transition-duration, transition-timing-function, transition-delay, and transition-behavior. MDN Reference
    static member ``transition`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "transition" (box (value))
    /// The transition-behavior CSS property specifies whether transitions will be started for properties whose animation behavior is discrete. MDN Reference
    static member ``transitionBehavior`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "transitionBehavior" (box (value))
    /// The transition-delay CSS property specifies the duration to wait before starting a property&apos;s transition effect when its value changes. MDN Reference
    static member ``transitionDelay`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "transitionDelay" (box (value))
    /// The transition-duration CSS property sets the length of time a transition animation should take to complete. By default, the value is 0s, meaning that no animation will occur. MDN Reference
    static member ``transitionDuration`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "transitionDuration" (box (value))
    /// The transition-property CSS property sets the CSS properties to which a transition effect should be applied. MDN Reference
    static member ``transitionProperty`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "transitionProperty" (box (value))
    /// The transition-timing-function CSS property sets how intermediate values are calculated for CSS properties being affected by a transition effect. MDN Reference
    static member ``transitionTimingFunction`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "transitionTimingFunction" (box (value))
    /// The translate CSS property allows you to specify translation transforms individually and independently of the transform property. This maps better to typical user interface usage, and saves having to remember the exact order of transform functions to specify in the transform value. MDN Reference
    static member ``translate`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "translate" (box (value))
    /// The unicode-bidi CSS property, together with the direction property, determines how bidirectional text in a document is handled. For example, if a block of content contains both left-to-right and right-to-left text, the user-agent uses a complex Unicode algorithm to decide how to display the text. The unicode-bidi property overrides this algorithm and allows the developer to control the text embedding. MDN Reference
    static member ``unicodeBidi`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "unicodeBidi" (box (value))
    /// The user-select CSS property controls whether the user can select text. This doesn&apos;t have any effect on content loaded as part of a browser&apos;s user interface (its chrome), except in textboxes. MDN Reference
    static member ``userSelect`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "userSelect" (box (value))
    /// The vector-effect CSS property suppresses specific transformation effects in SVG, thus permitting effects like a road on a map staying the same width no matter how the map is zoomed, or allowing a diagram key to retain its position and size regardless of other transforms. It can only be used with SVG elements that accept the vector-effect attribute. When used, the CSS value overrides any values of the element&apos;s vector-effect attribute. MDN Reference
    static member ``vectorEffect`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "vectorEffect" (box (value))
    /// The vertical-align CSS property sets vertical alignment of an inline, inline-block or table-cell box. MDN Reference
    static member ``verticalAlign`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "verticalAlign" (box (value))
    /// The view-timeline CSS shorthand property defines a named view progress timeline&apos;s name, direction, and inset values. MDN Reference
    static member ``viewTimeline`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "viewTimeline" (box (value))
    /// The view-timeline-axis CSS property specifies the scroll direction to be used for a named view progress timeline. MDN Reference
    static member ``viewTimelineAxis`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "viewTimelineAxis" (box (value))
    /// The view-timeline-inset CSS property is used to specify one or two values representing an adjustment to the position of the scrollport (see Scroll container for more details) in which the subject element of a named view progress timeline animation is deemed to be visible. Put another way, this allows you to specify start and/or end inset (or outset) values that offset the position of the timeline. MDN Reference
    static member ``viewTimelineInset`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "viewTimelineInset" (box (value))
    /// The view-timeline-name CSS property specifies the names of one or more named view progress timelines associated with the element. MDN Reference
    static member ``viewTimelineName`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "viewTimelineName" (box (value))
    /// The view-transition-class CSS property provides the selected elements with an identifying class (a &lt;custom-ident&gt;), providing an additional method of styling the view transitions for those elements. MDN Reference
    static member ``viewTransitionClass`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "viewTransitionClass" (box (value))
    /// The view-transition-name CSS property specifies the view transition snapshot that selected elements will participate in. This enables you to animate those elements separately from the rest of the page, which uses the default cross-fade animation during a view transition. You can then define custom animation styles for these elements. MDN Reference
    static member ``viewTransitionName`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "viewTransitionName" (box (value))
    /// The visibility CSS property shows or hides an element without changing the layout of a document. The property can also hide rows or columns in a &lt;table&gt;. MDN Reference
    static member ``visibility`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "visibility" (box (value))
    /// @deprecated This is a legacy alias of alignContent. MDN Reference
    static member ``webkitAlignContent`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitAlignContent" (box (value))
    /// @deprecated This is a legacy alias of alignItems. MDN Reference
    static member ``webkitAlignItems`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitAlignItems" (box (value))
    /// @deprecated This is a legacy alias of alignSelf. MDN Reference
    static member ``webkitAlignSelf`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitAlignSelf" (box (value))
    /// @deprecated This is a legacy alias of animation. MDN Reference
    static member ``webkitAnimation`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitAnimation" (box (value))
    /// @deprecated This is a legacy alias of animationDelay. MDN Reference
    static member ``webkitAnimationDelay`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitAnimationDelay" (box (value))
    /// @deprecated This is a legacy alias of animationDirection. MDN Reference
    static member ``webkitAnimationDirection`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitAnimationDirection" (box (value))
    /// @deprecated This is a legacy alias of animationDuration. MDN Reference
    static member ``webkitAnimationDuration`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitAnimationDuration" (box (value))
    /// @deprecated This is a legacy alias of animationFillMode. MDN Reference
    static member ``webkitAnimationFillMode`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitAnimationFillMode" (box (value))
    /// @deprecated This is a legacy alias of animationIterationCount. MDN Reference
    static member ``webkitAnimationIterationCount`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitAnimationIterationCount" (box (value))
    /// @deprecated This is a legacy alias of animationName. MDN Reference
    static member ``webkitAnimationName`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitAnimationName" (box (value))
    /// @deprecated This is a legacy alias of animationPlayState. MDN Reference
    static member ``webkitAnimationPlayState`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitAnimationPlayState" (box (value))
    /// @deprecated This is a legacy alias of animationTimingFunction. MDN Reference
    static member ``webkitAnimationTimingFunction`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitAnimationTimingFunction" (box (value))
    /// @deprecated This is a legacy alias of appearance. MDN Reference
    static member ``webkitAppearance`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitAppearance" (box (value))
    /// @deprecated This is a legacy alias of backfaceVisibility. MDN Reference
    static member ``webkitBackfaceVisibility`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitBackfaceVisibility" (box (value))
    /// @deprecated This is a legacy alias of backgroundClip. MDN Reference
    static member ``webkitBackgroundClip`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitBackgroundClip" (box (value))
    /// @deprecated This is a legacy alias of backgroundOrigin. MDN Reference
    static member ``webkitBackgroundOrigin`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitBackgroundOrigin" (box (value))
    /// @deprecated This is a legacy alias of backgroundSize. MDN Reference
    static member ``webkitBackgroundSize`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitBackgroundSize" (box (value))
    /// @deprecated This is a legacy alias of borderBottomLeftRadius. MDN Reference
    static member ``webkitBorderBottomLeftRadius`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitBorderBottomLeftRadius" (box (value))
    /// @deprecated This is a legacy alias of borderBottomRightRadius. MDN Reference
    static member ``webkitBorderBottomRightRadius`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitBorderBottomRightRadius" (box (value))
    /// @deprecated This is a legacy alias of borderRadius. MDN Reference
    static member ``webkitBorderRadius`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitBorderRadius" (box (value))
    /// @deprecated This is a legacy alias of borderTopLeftRadius. MDN Reference
    static member ``webkitBorderTopLeftRadius`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitBorderTopLeftRadius" (box (value))
    /// @deprecated This is a legacy alias of borderTopRightRadius. MDN Reference
    static member ``webkitBorderTopRightRadius`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitBorderTopRightRadius" (box (value))
    /// @deprecated This is a legacy alias of boxAlign. MDN Reference
    static member ``webkitBoxAlign`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitBoxAlign" (box (value))
    /// @deprecated This is a legacy alias of boxFlex. MDN Reference
    static member ``webkitBoxFlex`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitBoxFlex" (box (value))
    /// @deprecated This is a legacy alias of boxOrdinalGroup. MDN Reference
    static member ``webkitBoxOrdinalGroup`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitBoxOrdinalGroup" (box (value))
    /// @deprecated This is a legacy alias of boxOrient. MDN Reference
    static member ``webkitBoxOrient`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitBoxOrient" (box (value))
    /// @deprecated This is a legacy alias of boxPack. MDN Reference
    static member ``webkitBoxPack`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitBoxPack" (box (value))
    /// @deprecated This is a legacy alias of boxShadow. MDN Reference
    static member ``webkitBoxShadow`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitBoxShadow" (box (value))
    /// @deprecated This is a legacy alias of boxSizing. MDN Reference
    static member ``webkitBoxSizing`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitBoxSizing" (box (value))
    /// @deprecated This is a legacy alias of filter. MDN Reference
    static member ``webkitFilter`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitFilter" (box (value))
    /// @deprecated This is a legacy alias of flex. MDN Reference
    static member ``webkitFlex`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitFlex" (box (value))
    /// @deprecated This is a legacy alias of flexBasis. MDN Reference
    static member ``webkitFlexBasis`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitFlexBasis" (box (value))
    /// @deprecated This is a legacy alias of flexDirection. MDN Reference
    static member ``webkitFlexDirection`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitFlexDirection" (box (value))
    /// @deprecated This is a legacy alias of flexFlow. MDN Reference
    static member ``webkitFlexFlow`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitFlexFlow" (box (value))
    /// @deprecated This is a legacy alias of flexGrow. MDN Reference
    static member ``webkitFlexGrow`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitFlexGrow" (box (value))
    /// @deprecated This is a legacy alias of flexShrink. MDN Reference
    static member ``webkitFlexShrink`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitFlexShrink" (box (value))
    /// @deprecated This is a legacy alias of flexWrap. MDN Reference
    static member ``webkitFlexWrap`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitFlexWrap" (box (value))
    /// @deprecated This is a legacy alias of justifyContent. MDN Reference
    static member ``webkitJustifyContent`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitJustifyContent" (box (value))
    /// MDN Reference
    static member ``webkitLineClamp`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitLineClamp" (box (value))
    /// @deprecated This is a legacy alias of mask. MDN Reference
    static member ``webkitMask`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitMask" (box (value))
    /// The non-standard prefixed -webkit-mask-box-image shorthand property sets the mask image for an element&apos;s border box.  @deprecated This is a legacy alias of maskBorder. MDN Reference
    static member ``webkitMaskBoxImage`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitMaskBoxImage" (box (value))
    /// @deprecated This is a legacy alias of maskBorderOutset. MDN Reference
    static member ``webkitMaskBoxImageOutset`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitMaskBoxImageOutset" (box (value))
    /// @deprecated This is a legacy alias of maskBorderRepeat. MDN Reference
    static member ``webkitMaskBoxImageRepeat`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitMaskBoxImageRepeat" (box (value))
    /// @deprecated This is a legacy alias of maskBorderSlice. MDN Reference
    static member ``webkitMaskBoxImageSlice`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitMaskBoxImageSlice" (box (value))
    /// @deprecated This is a legacy alias of maskBorderSource. MDN Reference
    static member ``webkitMaskBoxImageSource`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitMaskBoxImageSource" (box (value))
    /// @deprecated This is a legacy alias of maskBorderWidth. MDN Reference
    static member ``webkitMaskBoxImageWidth`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitMaskBoxImageWidth" (box (value))
    /// @deprecated This is a legacy alias of maskClip. MDN Reference
    static member ``webkitMaskClip`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitMaskClip" (box (value))
    /// The -webkit-mask-composite property specifies the manner in which multiple mask images applied to the same element are composited with one another. Mask images are composited in the opposite order that they are declared with the -webkit-mask-image property.  @deprecated This is a legacy alias of maskComposite. MDN Reference
    static member ``webkitMaskComposite`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitMaskComposite" (box (value))
    /// @deprecated This is a legacy alias of maskImage. MDN Reference
    static member ``webkitMaskImage`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitMaskImage" (box (value))
    /// @deprecated This is a legacy alias of maskOrigin. MDN Reference
    static member ``webkitMaskOrigin`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitMaskOrigin" (box (value))
    /// @deprecated This is a legacy alias of maskPosition. MDN Reference
    static member ``webkitMaskPosition`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitMaskPosition" (box (value))
    /// @deprecated This is a legacy alias of maskRepeat. MDN Reference
    static member ``webkitMaskRepeat`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitMaskRepeat" (box (value))
    /// @deprecated This is a legacy alias of maskSize. MDN Reference
    static member ``webkitMaskSize`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitMaskSize" (box (value))
    /// @deprecated This is a legacy alias of order. MDN Reference
    static member ``webkitOrder`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitOrder" (box (value))
    /// @deprecated This is a legacy alias of perspective. MDN Reference
    static member ``webkitPerspective`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitPerspective" (box (value))
    /// @deprecated This is a legacy alias of perspectiveOrigin. MDN Reference
    static member ``webkitPerspectiveOrigin`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitPerspectiveOrigin" (box (value))
    /// The -webkit-text-fill-color CSS property specifies the fill color of characters of text. If this property is not set, the value of the color property is used. MDN Reference
    static member ``webkitTextFillColor`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitTextFillColor" (box (value))
    /// @deprecated This is a legacy alias of textSizeAdjust. MDN Reference
    static member ``webkitTextSizeAdjust`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitTextSizeAdjust" (box (value))
    /// The -webkit-text-stroke CSS property specifies the width and color of strokes for text characters. This is a shorthand property for the longhand properties -webkit-text-stroke-width and -webkit-text-stroke-color. MDN Reference
    static member ``webkitTextStroke`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitTextStroke" (box (value))
    /// The -webkit-text-stroke-color CSS property specifies the stroke color of characters of text. If this property is not set, the value of the color property is used. MDN Reference
    static member ``webkitTextStrokeColor`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitTextStrokeColor" (box (value))
    /// The -webkit-text-stroke-width CSS property specifies the width of the stroke for text. MDN Reference
    static member ``webkitTextStrokeWidth`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitTextStrokeWidth" (box (value))
    /// @deprecated This is a legacy alias of transform. MDN Reference
    static member ``webkitTransform`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitTransform" (box (value))
    /// @deprecated This is a legacy alias of transformOrigin. MDN Reference
    static member ``webkitTransformOrigin`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitTransformOrigin" (box (value))
    /// @deprecated This is a legacy alias of transformStyle. MDN Reference
    static member ``webkitTransformStyle`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitTransformStyle" (box (value))
    /// @deprecated This is a legacy alias of transition. MDN Reference
    static member ``webkitTransition`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitTransition" (box (value))
    /// @deprecated This is a legacy alias of transitionDelay. MDN Reference
    static member ``webkitTransitionDelay`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitTransitionDelay" (box (value))
    /// @deprecated This is a legacy alias of transitionDuration. MDN Reference
    static member ``webkitTransitionDuration`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitTransitionDuration" (box (value))
    /// @deprecated This is a legacy alias of transitionProperty. MDN Reference
    static member ``webkitTransitionProperty`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitTransitionProperty" (box (value))
    /// @deprecated This is a legacy alias of transitionTimingFunction. MDN Reference
    static member ``webkitTransitionTimingFunction`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitTransitionTimingFunction" (box (value))
    /// @deprecated This is a legacy alias of userSelect. MDN Reference
    static member ``webkitUserSelect`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "webkitUserSelect" (box (value))
    /// The white-space CSS property sets how white space inside an element is handled. MDN Reference
    static member ``whiteSpace`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "whiteSpace" (box (value))
    /// The white-space-collapse CSS property controls how white space inside an element is collapsed. MDN Reference
    static member ``whiteSpaceCollapse`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "whiteSpaceCollapse" (box (value))
    /// The widows CSS property sets the minimum number of lines in a block container that must be shown at the top of a page, region, or column. MDN Reference
    static member ``widows`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "widows" (box (value))
    /// The width CSS property sets an element&apos;s width. By default, it sets the width of the content area, but if box-sizing is set to border-box, it sets the width of the border area. MDN Reference
    static member ``width`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "width" (box (value))
    /// The will-change CSS property hints to browsers how an element is expected to change. Browsers may set up optimizations before an element is actually changed. These kinds of optimizations can increase the responsiveness of a page by doing potentially expensive work before they are actually required. MDN Reference
    static member ``willChange`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "willChange" (box (value))
    /// The word-break CSS property sets whether line breaks appear wherever the text would otherwise overflow its content box. MDN Reference
    static member ``wordBreak`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "wordBreak" (box (value))
    /// The word-spacing CSS property sets the length of space between words and between tags. MDN Reference
    static member ``wordSpacing`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "wordSpacing" (box (value))
    /// @deprecated word-wrap is a legacy alias of overflow-wrap. MDN Reference
    static member ``wordWrap`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "wordWrap" (box (value))
    /// The writing-mode CSS property sets whether lines of text are laid out horizontally or vertically, as well as the direction in which blocks progress. When set for an entire document, it should be set on the root element (html element for HTML documents). MDN Reference
    static member ``writingMode`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "writingMode" (box (value))
    /// The x CSS property defines the x-axis coordinate of the top left corner of the SVG &lt;rect&gt; shape, &lt;image&gt; image, &lt;foreignObject&gt; viewport or nested &lt;svg&gt; viewport relative to the nearest &lt;svg&gt; ancestor&apos;s user coordinate system. If present, it overrides the element&apos;s x attribute. MDN Reference
    static member ``x`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "x" (box (value))
    /// The y CSS property defines the y-axis coordinate of the top left corner of the SVG &lt;rect&gt; shape, &lt;image&gt; image, &lt;foreignObject&gt; viewport and nested &lt;svg&gt; viewport relative to the nearest &lt;svg&gt; ancestor&apos;s user coordinate system. If present, it overrides the element&apos;s y attribute. MDN Reference
    static member ``y`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "y" (box (value))
    /// The z-index CSS property sets the z-order of a positioned element and its descendants or flex and grid items. Overlapping elements with a larger z-index cover those with a smaller one. MDN Reference
    static member ``zIndex`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "zIndex" (box (value))
    /// The zoom CSS property can be used to control the magnification level of an element. transform: scale() can be used as an alternative to this property. MDN Reference
    static member ``zoom`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "zoom" (box (value))

[<AbstractClass; Sealed>]
type ``Transform`` private () =
    static member ``x`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "x" (box (value))
    static member ``y`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "y" (box (value))
    static member ``z`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "z" (box (value))
    static member ``rotate`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "rotate" (box (value))
    static member ``rotateX`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "rotateX" (box (value))
    static member ``rotateY`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "rotateY" (box (value))
    static member ``rotateZ`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "rotateZ" (box (value))
    static member ``scale`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scale" (box (value))
    static member ``scaleX`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scaleX" (box (value))
    static member ``scaleY`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scaleY" (box (value))
    static member ``scaleZ`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "scaleZ" (box (value))
    static member ``skewX`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "skewX" (box (value))
    static member ``skewY`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "skewY" (box (value))
    static member ``translateX`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "translateX" (box (value))
    static member ``translateY`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "translateY" (box (value))
    static member ``translateZ`` (value: Raw.TweenOptions) : Field<AnimationContext> = Field.create<AnimationContext> "translateZ" (box (value))

[<AbstractClass; Sealed>]
type ``Tween`` private () =
    static member number (value: float) : Raw.TweenOptions = U8.Case2 (value)
    static member text (value: string) : Raw.TweenOptions = U8.Case1 (value)
    static member fromTo (first: float) (last: float) : Raw.TweenOptions =
        U8.Case8 (Raw.TweenKeyValue.Create(from = U6.Case2 (first), ``to`` = U7.Case2 (last), ?``duration`` = None))
    static member values (values: float list) : Raw.TweenOptions =
        let items : (U8<string, float, (float -> float), Raw.DurationKeyframes.Item.Duration, Raw.Spring, Raw.TweakRegister, (U6<string, float, (float -> float), Raw.DurationKeyframes.Item.Duration, Raw.Spring, Raw.TweakRegister> * U6<string, float, (float -> float), Raw.DurationKeyframes.Item.Duration, Raw.Spring, Raw.TweakRegister>), Raw.TweenKeyValue>)[] = values |> List.map (fun value -> U8.Case2 (value)) |> List.toArray
        U8.Case3 (items)

[<AbstractClass; Sealed>]
type ``Target`` private () =
    static member selector (value: string) : Raw.TargetsParam = U6.Case1 (value)
    static member ``object`` (value: Fable.Core.JS.JS.Record<string, obj>) : Raw.TargetsParam = U6.Case4 (value)
    static member element (value: Fable.Core.TS.Dom.HTMLElement) : Raw.TargetsParam = U6.Case3 (value)
    static member svg (value: Fable.Core.TS.Dom.SVGElement) : Raw.TargetsParam = U6.Case6 (value)

module Anime =
    let ``animate`` (``targets``: U6<string, (U5<string, Fable.Core.TS.Dom.HTMLElement, Fable.Core.JS.JS.Record<string, obj>, Fable.Core.TS.Dom.NodeList, Fable.Core.TS.Dom.SVGElement>)[], Fable.Core.TS.Dom.HTMLElement, Fable.Core.JS.JS.Record<string, obj>, Fable.Core.TS.Dom.NodeList, Fable.Core.TS.Dom.SVGElement>) (options: Options<AnimationContext>) : Raw.JSAnimation =
        let parameters = Options.toObject options |> unbox<Raw.AnimationParams>
        Raw.Exports.``animate``(``targets`` = ``targets``, ``parameters`` = parameters)
    let ``createTimer`` (options: Options<TimerContext>) : Raw.Timer =
        let parameters = Options.toObject options |> unbox<Raw.TimerParams>
        Raw.Exports.``createTimer``(``parameters`` = parameters)
    let ``createTimeline`` (options: Options<TimelineContext>) : Raw.Timeline =
        let parameters = Options.toObject options |> unbox<Raw.TimelineParams>
        Raw.Exports.``createTimeline``(``parameters`` = parameters)
    let ``stagger`` (``val``: float) (options: Options<StaggerContext>) : Raw.Utils.Stagger.Result =
        let parameters = Options.toObject options |> unbox<Raw.StaggerParams>
        Raw.Exports.``stagger``(``val`` = ``val``, ``params`` = parameters)
    let ``createSpring`` (options: Options<SpringContext>) : Raw.Spring =
        let parameters = Options.toObject options |> unbox<Raw.SpringParams>
        Raw.Exports.``createSpring``(``parameters`` = parameters)
    let ``createAnimatable`` (``targets``: U6<string, (U5<string, Fable.Core.TS.Dom.HTMLElement, Fable.Core.JS.JS.Record<string, obj>, Fable.Core.TS.Dom.NodeList, Fable.Core.TS.Dom.SVGElement>)[], Fable.Core.TS.Dom.HTMLElement, Fable.Core.JS.JS.Record<string, obj>, Fable.Core.TS.Dom.NodeList, Fable.Core.TS.Dom.SVGElement>) (options: Options<AnimatableContext>) : Raw.AnimatableObject =
        let parameters = Options.toObject options |> unbox<Raw.AnimatableParams>
        Raw.Exports.``createAnimatable``(``targets`` = ``targets``, ``parameters`` = parameters)
    let ``createDraggable`` (``target``: U6<string, (U5<string, Fable.Core.TS.Dom.HTMLElement, Fable.Core.JS.JS.Record<string, obj>, Fable.Core.TS.Dom.NodeList, Fable.Core.TS.Dom.SVGElement>)[], Fable.Core.TS.Dom.HTMLElement, Fable.Core.JS.JS.Record<string, obj>, Fable.Core.TS.Dom.NodeList, Fable.Core.TS.Dom.SVGElement>) (options: Options<DraggableContext>) : Raw.Draggable =
        let parameters = Options.toObject options |> unbox<Raw.DraggableParams>
        Raw.Exports.``createDraggable``(``target`` = ``target``, ``parameters`` = parameters)
    let ``onScroll`` (options: Options<ScrollContext>) : Raw.ScrollObserver =
        let parameters = Options.toObject options |> unbox<Raw.ScrollObserverParams>
        Raw.Exports.``onScroll``(``parameters`` = parameters)
    let ``createScope`` (options: Options<ScopeContext>) : Raw.Scope =
        let parameters = Options.toObject options |> unbox<Raw.ScopeParams>
        Raw.Exports.``createScope``(``params`` = parameters)
    let ``splitText`` (``target``: U4<string, (Fable.Core.TS.Dom.Element)[], Fable.Core.TS.Dom.Element, Fable.Core.TS.Dom.NodeList>) (options: Options<TextSplitterContext>) : Raw.TextSplitter =
        let parameters = Options.toObject options |> unbox<Raw.TextSplitterParams>
        Raw.Exports.``splitText``(``target`` = ``target``, ``parameters`` = parameters)
