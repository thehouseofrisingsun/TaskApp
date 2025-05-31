using Google.Protobuf.WellKnownTypes;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using WeatherApp.Data;
using WeatherApp.Data.Repositories;
using WeatherApp.Data.Repositories.Interfaces;
using WeatherApp.Services;
using WeatherApp.Services.Interfaces;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

var connStr = Environment.GetEnvironmentVariable("SqlConnectionString");

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration(config =>
    {
        config.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
    })
    .ConfigureServices(services =>
    {

        services.AddDbContext<WeatherContext>(options =>
            options.UseSqlServer(connStr));
        
        services.AddScoped<IWeatherService, WeatherService>();
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<IWeatherRepository, WeatherRepository>();
        services.AddScoped<IAttemptLogRepository, AttemptLogRepository>();
        services.AddScoped<IAttemptLogService, AttemptLogService>();
    })
.Build();

using (var scope = host.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<WeatherContext>();
    db.Database.Migrate();
}

host.Run();

//builder.Build().Run();

