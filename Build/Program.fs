module AnimeJs.Build.Program

open System.IO
open Partas.Build

[<EntryPoint>]
let main argv =
    let root = Paths.findRoot (Directory.GetCurrentDirectory())
    (Command.invoke argv (Commands.create root)).ExitCode
