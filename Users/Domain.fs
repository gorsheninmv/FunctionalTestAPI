module Users.Domain

open FSharp.UMX

[<AutoOpen>]
module UserId =

  [<Measure>] type userId
  type UserId = string<userId>

[<AutoOpen>]
module Email =

  [<Measure>] type email
  type Email = string<email>

type User =
  | SignedUp of SignedUp
  | Unknown

and SignedUp =
  { id: UserId
    email: Email
    login: string }
