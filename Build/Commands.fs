namespace AnimeJs.Build

open Partas.Build

module Commands =
    let create (root: string) = Command.root {
        description "Partas.AnimeJs repository automation"
        workingDir root
        command "restore" {
            description "Restore pinned local generation tools"
            Stages.restore
        }
    }
