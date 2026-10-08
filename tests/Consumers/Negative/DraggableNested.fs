module NegativeDraggableNested
open Partas.AnimeJs
let invalid = Draggable.x (dragAxis { DragAxis.snap 10. })
