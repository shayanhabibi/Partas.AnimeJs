namespace Partas.AnimeJs.Generation

module Findings =
    let validate (model: DslModel) (findings: Xantham.Generator.Finding list) (reviews: ReviewedFinding list) : Result<ReviewedFinding list, Diagnostic list> =
        let fields = model.Fields @ model.CssFields @ (model.ValueFields |> Map.toList |> List.collect snd) |> List.map _.Source
        let indexers = model.Values |> List.choose (fun value -> match value.Type, value.DynamicType with Named name, Some _ -> Some(name + ".[]()") | _ -> None)
        let sources = fields @ indexers @ (model.Factories |> List.choose (fun factory -> factory.DynamicType |> Option.map fst))
        let aliases = sources |> List.collect (fun source -> Map.tryFind source model.AliasDependencies |> Option.defaultValue []) |> List.distinct
        let selected = findings |> List.filter (fun finding ->
            (sources @ aliases) |> List.exists (fun source -> finding.Symbol = source || finding.Symbol.StartsWith(source + "(")))
        let failures = selected |> List.choose (fun finding ->
            let reviewed = reviews |> List.exists (fun review ->
                review.Source = finding.Symbol && review.Code = finding.Name
                && not (System.String.IsNullOrWhiteSpace review.Reason) && not (System.String.IsNullOrWhiteSpace review.Review))
            if finding.Tier = Xantham.Generator.Exact || finding.Tier = Xantham.Generator.Ergonomic || reviewed then None
            else Some { Code = "fidelity/unreviewed"; Source = Some finding.Symbol; Message = finding.Name + ": " + finding.Message })
        if failures.IsEmpty then Ok (reviews |> List.filter (fun review -> selected |> List.exists (fun finding -> finding.Symbol = review.Source && finding.Name = review.Code))) else Error failures
