module InvalidReadonly
open Partas.AnimeJs
let invalid (handle: Raw.DrawableSVGGeometry) =
    handle.pathLength <- Unchecked.defaultof<_>
