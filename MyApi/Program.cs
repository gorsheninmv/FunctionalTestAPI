using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Users;
using Users.Application;
using Users.Infrastructure;

Context.Init();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<CommandHandler>(_ => new CommandHandler(Persistent.GetUserCollection()));
builder.Services.AddScoped<QueryHandler>(_ => new QueryHandler(Persistent.GetUserCollection()));
builder.Services.Configure<JsonOptions>(options =>
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/users", async (CommandHandler handler, UserDto user) =>
{
    var command = CommandBuilder.CreateUserCommand(user);
    var domainEvent = await handler.Handle(command);
    var httpResult = domainEvent.ToResult();
    return httpResult.status switch
    {
        Http.Status.Ok => Results.Ok(httpResult.payload),
        Http.Status.Error => Results.BadRequest(httpResult.payload)
    };
});

app.MapGet("/users", async (QueryHandler handler) =>
{
    var query = QueryBuilder.AllUsersQuery;
    var queryResult = await handler.Handle(query);
    var httpResult = queryResult.ToResult();
    return httpResult.status switch
    {
        Http.Status.Ok => Results.Ok(httpResult.payload),
        Http.Status.Error => Results.BadRequest(httpResult.payload)
    };
});

app.Run();
