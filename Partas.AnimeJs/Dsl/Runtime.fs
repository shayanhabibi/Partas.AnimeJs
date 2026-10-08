namespace Partas.AnimeJs

open Fable.Core.JsInterop

type Field<'Context> = private Field of string * obj
type Options<'Context> = private Options of Field<'Context> list

module internal Field =
    let create<'Context> key value : Field<'Context> = Field(key, value)

module internal Options =
    let toObject (Options fields: Options<'Context>) : obj =
        fields |> List.map (fun (Field(key, value)) -> key, value) |> createObj

type OptionsBuilder<'Context>() =
    member _.Yield(field: Field<'Context>) : Field<'Context> list = [field]
    member _.YieldFrom(fields: seq<Field<'Context>>) : Field<'Context> list = List.ofSeq fields
    member _.YieldFrom(Options fields: Options<'Context>) : Field<'Context> list = fields
    member _.Zero() : Field<'Context> list = []
    member _.Combine(first: Field<'Context> list, second: Field<'Context> list) = first @ second
    member _.Delay(generator: unit -> Field<'Context> list) = generator()
    member _.For(values: seq<'T>, generator: 'T -> Field<'Context> list) = values |> Seq.collect generator |> List.ofSeq
    member _.Run(fields: Field<'Context> list) = Options fields
