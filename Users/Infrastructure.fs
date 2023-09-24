namespace Users.Infrastructure

open System.Runtime.CompilerServices
open MongoDB.Bson
open MongoDB.Driver
open FSharp.Infrastructure
open FSharp.UMX
open Users.Domain
open Users.Application

module Persistent =

  [<CompiledName("GetUserCollection")>]
  let getUserCollection () =
    let mongoClient =
      MongoClient("mongodb://admin:11111@localhost:27017?authSource=admin")

    let db = mongoClient.GetDatabase("my_api")
    db.GetCollection<BsonDocument>("users")

[<AutoOpen>]
module Http =

  type Result = { status: Status; payload: obj }

  and Status =
    | Ok = 1
    | Error = 2

  let internal eventToDto event =
    match event with
    | UserCreated id -> { status = Status.Ok; payload = id }

  let internal usersToDto users =
    let usersDto =
      users
      |> Seq.map (fun user ->
        let mutable dto = UserDto()

        match user with
        | SignedUp u ->
          dto.Email <- % u.email
          dto.Id <- % u.id
        | Unknown -> dto.Id <- "unknown"

        dto)

    { status = Status.Ok
      payload = (usersDto |> Seq.toArray) }

[<Extension>]
[<CompilerMessage("Not designed for F#", 1001, IsHidden = true)>]
type ResultExtensions() =

  [<Extension>]
  static member ToResult(event: DomainEvents) =
    if isNull event then
      nullArg (nameof event)

    eventToDto event

  [<Extension>]
  static member ToResult(users: User seq) =
    if isNull users then
      nullArg (nameof users)

    usersToDto users
