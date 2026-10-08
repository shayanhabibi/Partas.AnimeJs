namespace Partas.AnimeJs

module Ease =
    /// An AnimeJs easing name. AnimeJs resolves its meaning when a factory runs.
    let named (value: string) =
        if System.String.IsNullOrWhiteSpace value then invalidArg "value" "An easing name cannot be empty."
        value
