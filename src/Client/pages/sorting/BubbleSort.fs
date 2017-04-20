module Sorting.BubbleSort

open Client.Messages

let swap i j (l : _ []) = 
  let t = l.[i]
  l.[i] <- l.[j]
  l.[j] <- t
  l

type SortState<'a> = 
  { List : 'a []
    Swaps : int
    Position : int
    Pass : int option
  }

let rec bSort { List = l; Swaps = swaps; Position = pos; Pass = pass } = 
  seq {
    match pos, swaps with 
    | p, 0 when p = l.Length - 1 -> 
      yield Complete l
      ()
    | p, _ when p = l.Length - 1 -> 
      yield! bSort {List = l; Swaps = 0; Position = 0; Pass = pass |> Option.map((+) 1)}
    | p, s -> 
      yield Compare(l, p, p+1, s)
      match l.[p] > l.[p+1] with 
      | true -> 
        let lSwapped = swap p (p+1) l
        yield Swapped(lSwapped, p, p+1, s+1)
        yield! bSort { List = lSwapped; Swaps = s + 1; Position = p+1; Pass = pass}
      | false -> yield! bSort { List = l; Swaps = s; Position = p+1; Pass = pass}
  }

let rec bSort2 { List = l; Swaps = swaps; Position = pos; Pass = pass } = 
  match pos, swaps with 
  | p, 0 when p = l.Length - 1 -> 
    { List = l; Swaps = 0; Position = 0; Pass = None }, Complete l
  | p, _ when p = l.Length - 1 -> 
    { List = l; Swaps = 0; Position = 0; Pass = pass |> Option.map((+) 1) }, NewPass l
  | p, s when l.[p] <= l.[p+1] -> 
    { List = l; Swaps = s; Position = p+1; Pass = pass }, Compare(l, p, p+1, s)
  | p, s ->
    let lSwapped = swap p (p+1) l
    { List = lSwapped; Swaps = s+1; Position = p+1; Pass = pass }, Swapped(lSwapped, p, p+1, s+1)

let defaultState l = { List = l; Swaps = 0; Position = 0; Pass = Some 0}

let runSortState l = 
  Seq.unfold(fun s -> 
    let s', m = bSort2 s
    match m with
    | Complete _ -> None
    | _ -> Some (s',s')) (defaultState l)