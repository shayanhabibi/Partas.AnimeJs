module Partas.AnimeJs.Tests.Common

open Fli
open System.IO
open System

let platformShell =
    match Environment.OSVersion.Platform with
    | PlatformID.Unix | PlatformID.MacOSX -> ZSH
    | _ -> CMD
