#I __SOURCE_DIRECTORY__
#load "../src/Client/pages/sorting/BubbleSort.fs"

open Sorting.BubbleSort

let a = [1;9;2;5;3;5;6;3]

let x = 
  a 
  |> List.toArray
  |> runSortState
  |> Seq.iter(printfn "%A")