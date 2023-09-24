namespace Users.Application

open System
open MongoDB.Bson
open MongoDB.Bson.Serialization.Attributes

[<BsonIgnoreExtraElements>]
[<Serializable>]
type UserDto() =

  [<BsonRepresentation(BsonType.ObjectId)>]
  member val Id       = Unchecked.defaultof<string> with get, set
  member val Login    = Unchecked.defaultof<string> with get, set
  member val Email    = Unchecked.defaultof<string> with get, set
