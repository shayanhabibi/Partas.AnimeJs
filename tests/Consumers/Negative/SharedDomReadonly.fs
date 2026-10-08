module NegativeSharedDomReadonly
let invalid (element: Fable.Core.TS.Dom.Element) = element.tagName <- "div"
