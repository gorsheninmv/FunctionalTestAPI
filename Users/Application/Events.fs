namespace Users.Application

open Users.Domain

type DomainEvents =
  | UserCreated of UserId
  | UserDeleted of UserId
