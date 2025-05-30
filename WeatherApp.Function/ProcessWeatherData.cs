using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using WeatherApp.Models;
using WeatherApp.Services.Interfaces;

namespace WeatherApiFunctionApp;

public class ProcessWeatherData
{
    private readonly ILogger _logger;
    private readonly ILocationService _locationService;
    private readonly IWeatherService _weatherService;
    private readonly IAttemptLogService _attemptLogService;

    public ProcessWeatherData(ILoggerFactory loggerFactory,
        ILocationService locationService,
        IWeatherService weatherService,
        IAttemptLogService attemptLogService)
    {
        _logger = loggerFactory.CreateLogger<ProcessWeatherData>();
        _locationService = locationService;
        _weatherService = weatherService;
        _attemptLogService = attemptLogService;
    }

    [Function("ProcessWeatherData")]
    public async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer)
    {
        _logger.LogInformation("C# Timer trigger function executed at: {executionTime}", DateTime.Now);

        if (myTimer.ScheduleStatus is not null)
        {
            _logger.LogInformation("Next timer schedule at: {nextSchedule}", myTimer.ScheduleStatus.Next);
        }

        var locations = await _locationService.GetLocations();
        var currentWeatherDTOs = new List<CurrentWeatherModel>();

        var locationDtos = locations.Select(l =>
            new LocationModel
            {
                lat = l.Latitude,
                country = l.Country,
                lon = l.Longitude,
                state = l.Country
            });

        foreach (var loc in locationDtos)
        {
            var data = await _weatherService.GetCurrentWeather(loc);
            if (data != null)
            {
                currentWeatherDTOs.Add(data);
            }
        }

        await _weatherService.BulkUpdateCurrentWeatherInfo(currentWeatherDTOs);

    }
}