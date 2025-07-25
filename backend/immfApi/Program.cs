using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using immfApi.Models;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;
using immfApi.Endpoints;
using immfApi.DataAccessLayer;
using immfApi.Endpoints.LovedOnes;
using immfApi.Endpoints.Hangouts;

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
builder.Services.AddSqlite<IMissMyFriendsDbContext>(connectionString);
builder.Services.AddScoped<ILovedOneRepository, LovedOneRepository>();
builder.Services.AddScoped<ILovedOneService, LovedOneService>();
builder.Services.AddScoped<IHangoutRepository, HangoutRepository>();
builder.Services.AddScoped<IHangoutService, HangoutService>();

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

//Register Endpoints
app.AddLovedOneEndpoints();
app.AddHangoutEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "I Miss My Friends API V1");
    });
}

app.Run();
