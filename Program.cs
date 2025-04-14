using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using DotNetEnv;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();  // Add this line to register the controllers

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

Env.Load();

var frontendUrl = Environment.GetEnvironmentVariable("PUBLIC_FRONTEND_URL");
Console.WriteLine($"Frontend URL: {frontendUrl}");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        if (!string.IsNullOrEmpty(frontendUrl))
        {
            policy.WithOrigins(frontendUrl)
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        }
        else
        {
            throw new InvalidOperationException("Environment variable 'PUBLIC_API_SERVER_URL' is not set or is empty.");
        }
    });
});

builder.Services.AddDbContext<StocksDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (connectionString == null)
    {
        throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    }
    options.UseMySQL(connectionString);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.UseCors("AllowAll");
app.MapControllers();

var runHost = Environment.GetEnvironmentVariable("RUN_HOST");
Console.WriteLine($"Run host: {runHost}");

app.Run(runHost);
