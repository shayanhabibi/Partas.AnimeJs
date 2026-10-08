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

// The theme calls a route's first segment its section, and keys menus and the navbar by it,
// so every guide page lives under /guide/. The landing page at / shares the same menu.
let guideMenu = [
    Menu.page "content:index.md"
    Menu.section "Examples" [
        Menu.page "examples:GettingStarted.fs"
        Menu.page "examples:Timer.fs"
        Menu.page "examples:Timeline.fs"
        Menu.page "examples:Keyframes.fs"
        Menu.page "examples:Stagger.fs"
        Menu.page "examples:Spring.fs"
        Menu.page "examples:Draggable.fs"
        Menu.page "examples:Scroll.fs"
        Menu.page "examples:SvgText.fs"
    ]
    Menu.page "content:migration.md"
    Menu.page "content:regeneration.md"
]

let theme =
    Theme.defaults
    |> Theme.navbar [NavbarSection("Guide", "guide", "/"); NavbarSection("Reference", "reference", "/reference/")]
    |> Theme.menu "" guideMenu
    |> Theme.menu "guide" guideMenu

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
        Route.create page.Locale ["guide"; slug])

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
    |> Site.collection (
        Theme.docs theme "content"
        |> Collection.route (fun page ->
            match RelativePath.value page.RelativePath with
            | "index.md" -> Route.home page.Locale
            | path -> Route.ofPath page.Locale ("guide/" + Path.ChangeExtension(path, null))))
    |> Site.collection examples
    |> Site.collection reference

[<EntryPoint>]
let main argv = Nacara.run site argv
