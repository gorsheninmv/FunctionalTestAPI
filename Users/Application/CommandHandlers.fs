namespace Users.Application

open MongoDB.Bson
open MongoDB.Driver
open FSharp.UMX

type CommandHandler(users: IMongoCollection<BsonDocument>) =

  let handle command =
    match command with
    | CreateUser dto ->
      task {
        let bson = dto.ToBsonDocument()
        do! users.InsertOneAsync(bson)
        let objectId = % bson.GetValue("_id").AsObjectId.ToString()
        return UserCreated(objectId)
      }

  member _.Handle(command) = handle command
