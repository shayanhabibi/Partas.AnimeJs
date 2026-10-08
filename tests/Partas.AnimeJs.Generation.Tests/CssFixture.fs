namespace Fable.Core.TS.Dom

type SyntheticStyle =
    abstract backgroundColor: string with get, set
    abstract cssText: string with get, set
    abstract length: int
    abstract Item: int -> string with get, set

type CSSStyleProperties =
    inherit SyntheticStyle
    /// Opacity from the fixture documentation.
    abstract opacity: string with get, set
    [<Fable.Core.Emit("$0.opacity")>]
    abstract aliasOpacity: string with get, set
    /// Border color with an escaped identifier.
    abstract ``border-color``: string with get, set
    abstract setProperty: string * string -> unit
