namespace AnimeJs.Build

open System.IO

module Paths =
    let private search start =
        let rec walk (directory: DirectoryInfo) =
            if isNull directory then None
            elif File.Exists(Path.Combine(directory.FullName, "global.json")) &&
                 File.Exists(Path.Combine(directory.FullName, "Partas.AnimeJs", "Partas.AnimeJs.fsproj")) then
                Some directory.FullName
            else walk directory.Parent
        walk (DirectoryInfo(Path.GetFullPath start))

    let findRoot start =
        match search start with
        | Some root -> root
        | None ->
            match search System.AppContext.BaseDirectory with
            | Some root -> root
            | None -> invalidOp "Cannot locate repository from the supplied path or build executable."
