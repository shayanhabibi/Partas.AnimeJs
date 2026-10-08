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
    |> Theme.menu "content" [Menu.page "content/index"; Menu.page "examples/GettingStarted"; Menu.page "content/migration"; Menu.page "content/regeneration"]

let reference =
    FSharpApi.collection "reference" DocFrontMatter.decoder apiOptions
    |> Collection.title _.Title
    |> Collection.layout (Theme.layout theme)

let examples =
    Theme.docs theme "examples"
    |> Collection.source "../examples" ["GettingStarted.fs"]
    |> Collection.route (fun page -> Route.create page.Locale ["getting-started"])

let site =
    Site.create "Partas.AnimeJs"
    |> Site.origin "https://shayanhabibi.github.io"
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
