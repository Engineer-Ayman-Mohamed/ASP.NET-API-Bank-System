using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.Memory;
using WebApplication3.Data.Context;
using WebApplication3.Models.Dto;
using WebApplication3.Models.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BankManagementSystemContext>(optionsAction: options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Default"),
        sqlServerOptionsAction: sqlOptions => sqlOptions.EnableRetryOnFailure(3)
    ).EnableSensitiveDataLogging().LogTo(Console.WriteLine, LogLevel.Information);
});
builder.Configuration.Sources.Insert(0, new MemoryConfigurationSource()
{
    InitialData = new Dictionary<string, string>() { ["AppName"] = "From InMemory" }
});

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.MapGet("/", () =>
{
    return  $"Hello World! {DateTime.Now} {app.Environment.ApplicationName} {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}";
});


app.MapGet("/test", (IConfiguration config) =>
{
    var value = config["AppName"];
    return Results.Ok(new
    {
        ResolvedValue = value,
        EnvironmentValue = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
    });
});
app.Run();