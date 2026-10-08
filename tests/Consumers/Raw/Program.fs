module RawConsumer

open Partas.AnimeJs.Raw

let animateElement (element: Fable.Core.TS.Dom.HTMLElement) (parameters: AnimationParams) =
    Exports.animate(Fable.Core.U6.Case3 element, parameters)

let iterableIdentity (value: TypeScript.Lib.Es.Iterable<Segment, obj, obj>) : Fable.Core.TS.Es.Iterable<Segment, obj, obj> = value
