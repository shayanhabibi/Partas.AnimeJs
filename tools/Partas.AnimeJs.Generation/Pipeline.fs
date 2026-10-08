namespace Partas.AnimeJs.Generation

open System
open System.IO
open System.Diagnostics
open System.Security.Cryptography
open System.Text
open System.Text.Json
open System.Xml.Linq

module Pipeline =
    let private hash bytes = SHA256.HashData(bytes: byte array) |> Convert.ToHexString |> _.ToLowerInvariant()
    let private textHash (text: string) = Encoding.UTF8.GetBytes text |> hash
    let private fileHash path = (File.ReadAllText path).Replace("\r\n", "\n") |> textHash
    let private json value = JsonSerializer.Serialize(value, JsonSerializerOptions(WriteIndented = true)) + "\n"
    let private reviews path =
        use document = JsonDocument.Parse(File.ReadAllText path)
        document.RootElement.EnumerateArray() |> Seq.map (fun entry ->
            let get name = entry.GetProperty(name: string).GetString()
            { Source = get "source"; Code = get "code"; Reason = get "reason"; Review = get "review" }) |> Seq.toList

    let private run root args = async {
        let info = ProcessStartInfo("dotnet", WorkingDirectory = root, RedirectStandardOutput = true, RedirectStandardError = true)
        info.StandardOutputEncoding <- Encoding.UTF8
        info.StandardErrorEncoding <- Encoding.UTF8
        for argument in args do info.ArgumentList.Add argument
        use child = Process.Start info
        let stdout = child.StandardOutput.ReadToEndAsync()
        let stderr = child.StandardError.ReadToEndAsync()
        do! child.WaitForExitAsync() |> Async.AwaitTask
        let! output = stdout |> Async.AwaitTask
        let! errors = stderr |> Async.AwaitTask
        return child.ExitCode, output + errors
    }

    let validateSources (request: GenerationRequest) artifacts = async {
        match Candidate.validatePaths artifacts with
        | Error diagnostics -> invalidArg "artifacts" diagnostics.Head.Message
        | Ok () -> ()
        let scratch = Path.Combine(request.RepositoryRoot, ".superpowers", "candidates", Guid.NewGuid().ToString("N"))
        Directory.CreateDirectory scratch |> ignore
        try
            for artifact in artifacts do
                let path = Path.Combine(scratch, artifact.RelativePath)
                Directory.CreateDirectory(Path.GetDirectoryName path) |> ignore
                File.WriteAllText(path, artifact.Content)
            let fs = artifacts |> List.filter (fun artifact -> artifact.RelativePath.EndsWith(".fs") && artifact.RelativePath <> "Dsl.fs")
            let items = fs |> List.map (fun artifact -> $"<Compile Include=\"{artifact.RelativePath}\" />") |> String.concat ""
            let escape (path: string) = System.Security.SecurityElement.Escape path
            let runtime = escape (Path.Combine(request.RepositoryRoot, "Partas.AnimeJs/Dsl/Runtime.fs"))
            let conveniences = escape (Path.Combine(request.RepositoryRoot, "Partas.AnimeJs/Dsl/Conveniences.fs"))
            File.WriteAllText(Path.Combine(scratch, "Candidate.fsproj"), $"<Project Sdk=\"Microsoft.NET.Sdk\"><PropertyGroup><TargetFramework>net9.0</TargetFramework></PropertyGroup><ItemGroup>{items}<Compile Include=\"{runtime}\" /><Compile Include=\"Dsl.fs\" /><Compile Include=\"{conveniences}\" /></ItemGroup><ItemGroup><PackageReference Include=\"Fable.Core\" /><PackageReference Include=\"Xantham.Fable.Core\" /><PackageReference Include=\"Xantham.Fable.Core.TS\" /></ItemGroup></Project>")
            let! buildCode, buildOutput = run request.RepositoryRoot ["build"; Path.Combine(scratch, "Candidate.fsproj"); "--nologo"]
            if buildCode <> 0 then return Error [{ Code = "candidate/dotnet"; Source = None; Message = buildOutput }]
            else
                let! fableCode, fableOutput = run request.RepositoryRoot ["fable"; Path.Combine(scratch, "Candidate.fsproj"); "--noCache"; "-o"; Path.Combine(scratch, "output"); "-e"; ".fs.js"]
                if fableCode <> 0 then return Error [{ Code = "candidate/fable"; Source = None; Message = fableOutput }]
                else return Ok ()
        finally
            // Scratch is a directly created child of the repository candidate directory.
            if Directory.Exists scratch then Directory.Delete(scratch, true)
    }

    let generate (request: GenerationRequest) : Async<Result<Candidate, Diagnostic list>> = async {
        try
            let! generated = XanthamAdapter.generate request
            match generated with
            | Error errors -> return Error errors
            | Ok raw ->
                let projected =
                    Policy.load request.PolicyPath
                    |> Result.bind (Projection.project raw)
                    |> Result.bind (fun model -> CssMetadata.read request.SupportAssembly request.SupportDocumentation |> Result.bind (CssMetadata.attach model))
                match projected with
                | Error errors -> return Error errors
                | Ok model ->
                    let baseline = reviews (Path.Combine(request.RepositoryRoot, "generation/fidelity-baseline.json"))
                    match Findings.validate model raw.Model.Findings baseline, Emit.sources model with
                    | Error errors, _ | _, Error errors -> return Error errors
                    | Ok findings, Ok dsl ->
                        let rawArtifacts = raw.Artifacts |> List.map (fun artifact -> { artifact with RelativePath = "Raw/" + artifact.RelativePath })
                        let sources = rawArtifacts @ dsl
                        let rawIncludes = rawArtifacts |> List.filter (fun artifact -> artifact.RelativePath.EndsWith(".fs")) |> List.map (fun artifact -> $"    <Compile Include=\"$(MSBuildThisFileDirectory){artifact.RelativePath}\" />")
                        let props = { RelativePath = "Generated.props"; Content = String.concat "\n" (["<Project>"; "  <ItemGroup>"] @ rawIncludes @ ["    <Compile Include=\"$(MSBuildThisFileDirectory)../Dsl/Runtime.fs\" />"; "    <Compile Include=\"$(MSBuildThisFileDirectory)Dsl.fs\" />"; "    <Compile Include=\"$(MSBuildThisFileDirectory)../Dsl/Conveniences.fs\" />"; "  </ItemGroup>"; "</Project>"; ""]) }
                        let artifacts = sources @ [props]
                        let! validation = validateSources request sources
                        match validation with
                        | Error errors -> return Error errors
                        | Ok () ->
                            let root = request.RepositoryRoot
                            let pins = XDocument.Load(Path.Combine(root, "Directory.Packages.props")).Descendants(XName.Get "PackageVersion") |> Seq.map (fun element -> element.Attribute(XName.Get "Include").Value, element.Attribute(XName.Get "Version").Value) |> Map.ofSeq
                            use lockfile = JsonDocument.Parse(File.ReadAllText(Path.Combine(root, "generation/package-lock.json")))
                            let anime = lockfile.RootElement.GetProperty("packages").GetProperty("node_modules/animejs")
                            let typescript = lockfile.RootElement.GetProperty("packages").GetProperty("node_modules/typescript")
                            let fingerprint = CssMetadata.fingerprint request.SupportAssembly request.SupportDocumentation
                            use tools = JsonDocument.Parse(File.ReadAllText(Path.Combine(root, "dotnet-tools.json")))
                            use sdk = JsonDocument.Parse(File.ReadAllText(Path.Combine(root, "global.json")))
                            let versions = pins |> Map.add "animejs" (anime.GetProperty("version").GetString()) |> Map.add "typescript" (typescript.GetProperty("version").GetString())
                                                |> Map.add "fable" (tools.RootElement.GetProperty("tools").GetProperty("fable").GetProperty("version").GetString())
                                                |> Map.add "xantham" (tools.RootElement.GetProperty("tools").GetProperty("xantham").GetProperty("version").GetString())
                                                |> Map.add "sdk" (sdk.RootElement.GetProperty("sdk").GetProperty("version").GetString())
                            let tracked = ["generation/package-lock.json"; "generation/xantham.json"; "generation/dsl-policy.json"; "generation/fidelity-baseline.json"; "Directory.Packages.props"; "dotnet-tools.json"; "global.json"; "Partas.AnimeJs/Dsl/Runtime.fs"; "Partas.AnimeJs/Dsl/Conveniences.fs"]
                            let implementation = Directory.EnumerateFiles(Path.Combine(root, "tools/Partas.AnimeJs.Generation"), "*.fs") |> Seq.map (fun path -> Path.GetRelativePath(root, path).Replace('\\', '/')) |> Seq.toList
                            let inputs = tracked @ implementation |> List.map (fun path -> path, fileHash (Path.Combine(root, path)))
                            let declarations = Directory.EnumerateFiles(request.InputDirectory, "*.d.ts", SearchOption.AllDirectories) |> Seq.map (fun path -> "animejs/" + Path.GetRelativePath(request.InputDirectory, path).Replace('\\', '/'), fileHash path) |> Seq.toList
                            let hashes = artifacts |> List.map (fun artifact -> artifact.RelativePath, textHash artifact.Content) |> Map.ofList
                            let digest = hashes |> Map.toList |> List.map (fun (path, value) -> path + ":" + value) |> String.concat "\n" |> textHash
                            let provenance = {
                                SchemaVersion = 1; Versions = versions; InputIntegrity = anime.GetProperty("integrity").GetString()
                                InputHashes = inputs @ declarations @ ["Xantham.Fable.Core.TS.dll", fingerprint.AssemblyHash; "Xantham.Fable.Core.TS.xml", fingerprint.DocumentationHash] |> Map.ofList
                                ArtifactHashes = hashes; Findings = findings; ExcludedFields = model.ExcludedFields; CompilationDigest = digest }
                            return Ok { Artifacts = artifacts @ [{ RelativePath = "provenance.json"; Content = json provenance }]; Diagnostics = []; Provenance = provenance }
        with error -> return Error [{ Code = "candidate/generation"; Source = None; Message = error.ToString() }]
    }
