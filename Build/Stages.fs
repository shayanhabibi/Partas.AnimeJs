namespace AnimeJs.Build

open Partas.Build

module Stages =
    let restore = stage "restore local tools" {
        run (cmd $"dotnet" |> Cmd.args [ "tool"; "restore" ])
        run (cmd $"npm" |> Cmd.args [ "ci"; "--prefix"; "generation"; "--ignore-scripts" ])
        run (cmd $"npm" |> Cmd.args [ "ci"; "--ignore-scripts" ])
    }
