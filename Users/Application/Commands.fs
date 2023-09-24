namespace Users.Application

[<Struct>]
type Command = CreateUser of UserDto

module CommandBuilder =

  [<CompiledName("CreateUserCommand")>]
  let buildCreateUserCommand args = CreateUser args
