using Neo4j.Driver;
using nosql_neo4j.Controllers;
using nosql_neo4j.Repository;
using nosql_neo4j.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(s =>
{
    var uri = builder.Configuration.GetValue<string>("ApplicationSettings:Neo4jConnection");
    var user = builder.Configuration.GetValue<string>("ApplicationSettings:Neo4jUser");
    var password = builder.Configuration.GetValue<string>("ApplicationSettings:Neo4jPassword");

    return GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
});

builder.Services.AddSingleton<IDeliveryRepository, DeliveryRepository>();
builder.Services.AddSingleton<Neo4JService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

DeliveryController.MapDeliveryApi(app.MapGroup("/api/delivery"));

app.Run();
