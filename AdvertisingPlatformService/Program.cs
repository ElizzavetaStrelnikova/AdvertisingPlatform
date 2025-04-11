using AdvertisingPlatform.Service.Interfaces;
using AdvertisingPlatform.Service.Models;
using AdvertisingPlatform.Service.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddFile("logs/adplatform-{Date}.txt", minimumLevel: LogLevel.Information);

builder.Configuration.AddJsonFile("appsettings.json");

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IAdvertisingPlatformService<Platform>, AdvertisingPlatformService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
