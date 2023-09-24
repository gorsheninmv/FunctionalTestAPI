namespace Users

open MongoDB.Bson.Serialization.Conventions

type Context() =

  static member Init() =
    let pack = ConventionPack()
    pack.Add(CamelCaseElementNameConvention())
    ConventionRegistry.Register("Camel case convention", pack, (fun t -> true))
