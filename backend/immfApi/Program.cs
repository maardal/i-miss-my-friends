using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Immf.Models;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;

const string Database = "Immf";

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString(Database) ?? throw new InvalidOperationException($"Connection string for {Database} not found.");

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        }
    );
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSqlite<IMissMyFriendsDb>(connectionString);
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "I Miss My Friends AP1",
        Description = "Serving loved ones you're missing",
        Version = "v1"
    });
});


var app = builder.Build();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "I Miss My Friends API V1");
    });
}

//lovedones endpoint

app.MapGet("/", () => "Hello World!");

app.MapGet("/lovedones", async (IMissMyFriendsDb db) => await db.LovedOnes.ToListAsync());

app.MapPost("/lovedone", async (IMissMyFriendsDb db, string name, string relationship, string date) =>
{

    if (!Enum.IsDefined(typeof(Relationship), relationship))
    {
        return Results.BadRequest($"Relationship must be one of: {EnumTools.EnumListPrettified()}");
    }

    var lovedOne = new LovedOne { Name = name, Relationship = EnumTools.RelationshipMapper(relationship), Date = date };

    await db.LovedOnes.AddAsync(lovedOne);
    await db.SaveChangesAsync();
    return Results.Created($"/lovedone/{lovedOne.Id}", lovedOne);
});

app.MapGet("/lovedone/{id}", async (IMissMyFriendsDb db, int id) => await db.LovedOnes.FindAsync(id));

app.MapPut("/lovedone/{id}", async (IMissMyFriendsDb db, LovedOne loved, int id) =>
{
    var oldLoved = await db.LovedOnes.FindAsync(id);
    if (oldLoved is null) return Results.NotFound();
    oldLoved.Name = loved.Name;
    oldLoved.Relationship = loved.Relationship;
    oldLoved.Date = loved.Date;
    await db.SaveChangesAsync();
    return Results.NoContent();

});

app.MapDelete("/lovedone/{id}", async (IMissMyFriendsDb db, int id) =>
{
    var loved = await db.LovedOnes.FindAsync(id);
    if (loved is null) return Results.NotFound();
    db.LovedOnes.Remove(loved);
    await db.SaveChangesAsync();
    return Results.Ok();
});

//Hangout endpoints
app.MapGet("/hangouts/", async (IMissMyFriendsDb db) => await db.Hangouts.Include(hangout => hangout.LovedOne).ToListAsync());

app.MapGet("hangout/{id}", async (IMissMyFriendsDb db, int id) => await db.Hangouts.FindAsync(id));

app.MapPost("/hangout", async (IMissMyFriendsDb db, int lovedOneId, string date) =>
{
    var lovedOne = await db.LovedOnes.FindAsync(lovedOneId);
    if (lovedOne == null) return Results.NotFound($"No LovedOne by id {lovedOneId}");

    //Temporary adding todays date.
    var todaysDate = DateTime.Today;

    var hangout = new Hangout { Date = todaysDate, LovedOne = lovedOne };
    await db.Hangouts.AddAsync(hangout);
    await db.SaveChangesAsync();
    return Results.Created($"/hangout/{hangout.Id}", hangout);
});

app.MapDelete("/hangout/{id}", async (IMissMyFriendsDb db, int id) =>
{
    var hangout = await db.Hangouts.FindAsync(id);
    if (hangout == null) return Results.NotFound();
    db.Hangouts.Remove(hangout);
    await db.SaveChangesAsync();
    return Results.Ok();

});

app.Run();
