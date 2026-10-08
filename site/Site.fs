module Partas.AnimeJs.Site

open System
open System.IO
open Nacara.Core
open Nacara.Plugins
open Nacara.Theme

let library = Path.GetFullPath(Path.Combine(__SOURCE_DIRECTORY__, "../Partas.AnimeJs/bin/Debug/net9.0"))
let apiOptions = {
    FSharpApi.defaults with
        Root = "reference"
        Title = "API reference"
        Sources = [FSharpApiSource.create (Path.Combine(library, "Partas.AnimeJs.dll")) |> FSharpApiSource.searchPaths [library; AppContext.BaseDirectory]]
}

let theme =
    Theme.defaults
    |> Theme.navbar [NavbarSection("Guide", "content", "/"); NavbarSection("Reference", "reference", "/reference/")]
    |> Theme.menu "content" [
        Menu.page "index"
        Menu.page "examples/GettingStarted"
        Menu.page "examples/Timer"
        Menu.page "examples/Timeline"
        Menu.page "examples/Keyframes"
        Menu.page "examples/Stagger"
        Menu.page "examples/Spring"
        Menu.page "examples/Draggable"
        Menu.page "examples/Scroll"
        Menu.page "examples/SvgText"
        Menu.page "migration"
        Menu.page "regeneration"
    ]

let reference =
    FSharpApi.collection "reference" DocFrontMatter.decoder apiOptions
    |> Collection.title _.Title
    |> Collection.layout (Theme.layout theme)

let examples =
    Theme.docs theme "examples"
    |> Collection.source "../examples" ["*.fs"]
    |> Collection.route (fun page ->
        let name = Path.GetFileNameWithoutExtension(RelativePath.value page.RelativePath)
        let slug = System.Text.RegularExpressions.Regex.Replace(name, "([a-z])([A-Z])", "$1-$2").ToLowerInvariant()
        Route.create page.Locale [slug])

let site =
    Site.create "Partas.AnimeJs"
    |> Site.origin "https://shayanhabibi.github.io"
    |> Site.baseUrl "/Partas.AnimeJs/"
    |> Site.output "output"
    |> Markdown.register
    |> TreeSitter.register
    // The Build docs stage compiles these exact .fs files against the shipped
    // library with .NET and Fable; standalone FSI has no project references.
    |> Literate.registerWith (fun options -> { options with TypeCheck = false })
    |> FSharpApi.register apiOptions
    |> LinkValidator.registerWith (fun options -> { options with CheckExternal = false })
    |> Theme.register theme
    |> Site.collection (Theme.docs theme "content")
    |> Site.collection examples
    |> Site.collection reference

[<EntryPoint>]
let main argv = Nacara.run site argv
