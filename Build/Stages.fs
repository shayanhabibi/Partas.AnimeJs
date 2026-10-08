namespace AnimeJs.Build

open Partas.Build

module Stages =
    let restore = stage "restore local tools" {
        run (cmd $"dotnet" |> Cmd.args [ "tool"; "restore" ])
    }
