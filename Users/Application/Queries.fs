namespace Users.Application

open Users.Domain

type Query =
  | AllUsers
  | User of UserId

module QueryBuilder =

  [<CompiledName("AllUsersQuery")>]
  let allUsersQuery = AllUsers
