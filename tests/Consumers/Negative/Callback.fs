module InvalidCallback
open Partas.AnimeJs
let callback (handle: Raw.Timeline) = handle.pause() |> ignore
let invalid = Animation.onComplete callback
