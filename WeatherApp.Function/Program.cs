using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
    .ConfigureServices(services =>
    {

        services.AddDbContext<WeatherContext>(options =>
            options.UseSqlServer(connStr));
        services.AddScoped<IWeatherService, WeatherService>();
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<IWeatherRepository, WeatherRepository>();
        services.AddScoped<IBadAttemptLogRepository, BadAttemptLogRepository>();
        services.AddScoped<IBadAttemptLogService, BadAttemptLogService>();
    })
.Build();

host.Run();

//builder.Build().Run();

