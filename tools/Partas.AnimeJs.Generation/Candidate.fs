namespace Partas.AnimeJs.Generation

open System
open System.IO

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Candidate =
    let private inventory = ".generated-files"
    let private diagnostic code message = { Code = code; Source = None; Message = message }

    let internal validatePaths (artifacts: Artifact list) =
        let paths = artifacts |> List.map _.RelativePath
        let invalid path =
            String.IsNullOrWhiteSpace path || Path.IsPathRooted path || path.Contains(':')
            || (path.Replace('\\', '/').Split('/') |> Array.exists (fun part -> part = ".." || part = "." || part = ""))
            || path = inventory
        if paths |> List.exists invalid then Error [diagnostic "candidate/path" "Artifact paths must remain inside the managed directory."]
        elif (paths |> List.map (fun path -> path.Replace('\\', '/').ToUpperInvariant()) |> List.distinct |> List.length) <> paths.Length then
            Error [diagnostic "candidate/path" "Artifact paths must be unique."]
        else Ok ()

    let private validate (candidate: Candidate) =
        match validatePaths candidate.Artifacts with
        | Error errors -> Error errors
        | Ok () when not candidate.Diagnostics.IsEmpty -> Error candidate.Diagnostics
        | Ok () -> Ok ()

    let private owned root =
        let path = Path.Combine(root, inventory)
        if File.Exists path then File.ReadAllLines path |> Array.toList else []

    let compare root candidate : Result<unit, Diagnostic list> =
        match validate candidate with
        | Error errors -> Error errors
        | Ok () ->
            let expected = candidate.Artifacts |> List.map _.RelativePath |> Set.ofList
            let stale = owned root |> List.filter (fun path -> not (Set.contains path expected))
            let drift = candidate.Artifacts |> List.filter (fun artifact ->
                let path = Path.Combine(root, artifact.RelativePath)
                not (File.Exists path) || File.ReadAllText path <> artifact.Content)
            if stale.IsEmpty && drift.IsEmpty then Ok ()
            else Error [diagnostic "candidate/drift" "Generated output differs from the validated candidate."]

    let promoteWith write outputRoot candidate : Result<unit, Diagnostic list> =
        match validate candidate with
        | Error errors -> Error errors
        | Ok () ->
            let root = Path.GetFullPath outputRoot
            let parent = Directory.GetParent root
            let suffix = Guid.NewGuid().ToString("N")
            let staging = Path.Combine(parent.FullName, ".candidate-" + suffix)
            let backup = root + ".generation-backup"
            let mutable swapped = false
            try
                Directory.CreateDirectory parent.FullName |> ignore
                use lease = new FileStream(root + ".generation-lock", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None, 1, FileOptions.DeleteOnClose)
                if Directory.Exists backup then
                    if Directory.Exists root then Directory.Delete(backup, true)
                    else Directory.Move(backup, root)
                if Directory.Exists root && (File.GetAttributes root &&& FileAttributes.ReparsePoint) <> enum 0 then
                    raise (IOException "Managed output root cannot be a symbolic link or junction.")
                Directory.CreateDirectory staging |> ignore
                if Directory.Exists root then
                    for path in Directory.EnumerateFileSystemEntries(root, "*", SearchOption.AllDirectories) do
                        if (File.GetAttributes path &&& FileAttributes.ReparsePoint) <> enum 0 then
                            raise (IOException "Managed output cannot contain symbolic links or junctions.")
                        let target = Path.Combine(staging, Path.GetRelativePath(root, path))
                        if Directory.Exists path then Directory.CreateDirectory target |> ignore
                        else
                            Directory.CreateDirectory(Path.GetDirectoryName target) |> ignore
                            File.Copy(path, target)
                for path in owned root do
                    let absolute = Path.GetFullPath(Path.Combine(staging, path))
                    if not (absolute.StartsWith(staging + string Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase)) then
                        raise (IOException "Generated inventory contains an unsafe path.")
                    if File.Exists absolute then File.Delete absolute
                for artifact in candidate.Artifacts do
                    let path = Path.Combine(staging, artifact.RelativePath)
                    Directory.CreateDirectory(Path.GetDirectoryName path) |> ignore
                    write path artifact.Content
                File.WriteAllLines(Path.Combine(staging, inventory), candidate.Artifacts |> List.map _.RelativePath |> List.sort)
                if Directory.Exists root then Directory.Move(root, backup)
                try Directory.Move(staging, root); swapped <- true
                with error ->
                    if Directory.Exists backup then Directory.Move(backup, root)
                    raise error
                if Directory.Exists backup then Directory.Delete(backup, true)
                Ok ()
            with error ->
                if Directory.Exists staging then Directory.Delete(staging, true)
                if swapped then Error [diagnostic "candidate/cleanup" error.Message]
                else Error [diagnostic "candidate/promotion" error.Message]
    let promote outputRoot candidate = promoteWith (fun path text -> System.IO.File.WriteAllText(path, text)) outputRoot candidate
