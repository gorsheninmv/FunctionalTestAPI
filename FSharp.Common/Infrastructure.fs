namespace FSharp.Infrastructure

[<AutoOpen>]
module Common =
  let isNull (x: obj) = System.Object.ReferenceEquals(x, null)
