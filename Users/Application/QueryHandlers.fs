namespace Users.Application

open FSharp.UMX
open MongoDB.Bson
open MongoDB.Bson.Serialization
open MongoDB.Driver
open Users.Domain

type QueryHandler(users: IMongoCollection<BsonDocument>) =

  let create (dto: UserDto) : User =
    if isNull dto.Email then
      nullArg (nameof dto.Email)

    if isNull dto.Login then
      nullArg (nameof dto.Login)

    if isNull dto.Id then
      nullArg (nameof dto.Id)

    SignedUp
      { id = % dto.Id
        email = % dto.Email
        login = dto.Login }

  let handle query =
    match query with
    | AllUsers ->
      task {
        let! dtos = users.AsQueryable().ToListAsync()

        return
          dtos
          |> Seq.map (fun bson -> BsonSerializer.Deserialize<UserDto>(bson))
          |> Seq.map (fun dto -> create dto)
          |> Seq.toArray
      }

  member _.Handle(query) = handle query
