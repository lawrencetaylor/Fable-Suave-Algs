module Client.Sort


open System
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.Import.Browser
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Elmish

open Messages
open Sorting.BubbleSort

type Model =
  { Sort : SortState<int>
  }

let init() = 
  let r = System.Random()
  let initial = [1..10] |> List.map(fun _ -> r.Next(20)) |> Array.ofList
  let model = 
    { Sort = Sorting.BubbleSort.defaultState initial }
  model, Cmd.none

let update (msg:BubbleMsg) model : Model*Cmd<BubbleMsg> = 
  match msg with
  | NextMove -> 
    let (modelPrime, whatHappened) = bSort2 model.Sort
    { model with Sort = modelPrime }, Cmd.ofMsg (SideEffect whatHappened)
  | _ -> model, []

/// VIEW

let view model (dispatch: AppMsg -> unit) = 
  div [] 
    [ div [] [ button [ OnClick (fun ev -> dispatch (BubbleSortMsg NextMove)) ] [ str "NEXT "] ]
      h3 [] [ model.Pass |> sprintf "%A" |> str ]
      div [] [ model.List |> Array.map(fun a -> a.ToString()) |> String.concat "," |> str ]
    ]
  
  
