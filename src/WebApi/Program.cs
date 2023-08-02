using System.Diagnostics;
using Application;
using Infrastructure;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Console.WriteLine($"Environment: {builder.Environment.EnvironmentName}");
//var env = builder.Environment.EnvironmentName.Equals("Production") ? $"appsettings.json" : $"appsettings.{builder.Environment.EnvironmentName}.json";
//Console.WriteLine($"Environment AppSettings: {env}");
//builder.Configuration.AddJsonFile($"{env}").AddEnvironmentVariables();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebUIServices();

var app = builder.Build();

if (Debugger.IsAttached)
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetService<BlogDataContext>();
    // await context!.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }