using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using WeatherApp.Data.Entities;
using WeatherApp.Data.Repositories.Interfaces;
using WeatherApp.Models;
using WeatherApp.Services.Interfaces;

namespace WeatherApp.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _client;
        private readonly string _apiKey;
        private readonly string _openWeatherUrl;
        private readonly IWeatherRepository _weatherRepository;
        IAttemptLogService _attemptLogService;

        public WeatherService(IConfiguration configuration, 
            IWeatherRepository weatherRepository,
            IAttemptLogService attemptLogService)
        {
            _openWeatherUrl = configuration["OpenWeatherBaseUrl"];
            _client = new();
            _weatherRepository = weatherRepository;
            _apiKey = configuration["OpenWeatherAPIKey"];
            _attemptLogService = attemptLogService;
        }

        public async Task BulkUpdateCurrentWeatherInfo(List<CurrentWeatherModel> currentWeatherBatch)
        {
            var entityList = new List<CurrentWeather>();
            var list = currentWeatherBatch.Select(data =>
                new CurrentWeather
                {
                    Country = data.sys?.country,
                    City = data.name,
                    Latitude = data.coord?.lat,
                    Longitude = data.coord?.lon,
                    Temperature = data.main?.temp,
                    WindSpeed = data.wind?.speed,
                    LastUpdate = DateTime.Now
                });
            await _weatherRepository.BulkUpdateCurrentWeather(list);
        }

        public async Task<CurrentWeatherModel> GetCurrentWeather(LocationModel location)
        {
            CurrentWeatherModel weatherForecast = new();
            string requestUri = GetCurrentWeatherRequestUri(location);

            HttpResponseMessage response = await _client.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                weatherForecast = await response.Content.ReadFromJsonAsync<CurrentWeatherModel>();
            }

            var log = new AttemptLogModel
            {
                Date = DateTime.Now,
                StatusCode = (int)response.StatusCode,
                ErrorMessage = response.IsSuccessStatusCode ? string.Empty : await response.Content.ReadAsStringAsync(),
                RequestUri = requestUri
            };

            await _attemptLogService.AddLog(log);

            return weatherForecast;
        }

        public async Task<List<MinMaxTemperatureModel>> GetMinMaxTemperature(DateTime from, DateTime to)
        {
            var entity = await _weatherRepository.GetMinMaxTemperature(from, to);
            var minTemperatureModel = entity.Select(d =>
                new MinMaxTemperatureModel
                {
                    City = d.City ?? string.Empty,
                    Country = d.Country ?? string.Empty,
                    MinTemperatureLastUpdate = d.MinTemperatureLastUpdate,
                    MaxTemperatureLastUpdate = d.MaxTemperatureLastUpdate,
                    MinTemperature = d.MinTemperature ?? 0,
                    MaxTemperature = d.MaxTemperature ?? 0
                }).ToList();
            return minTemperatureModel;
        }

        private string GetCurrentWeatherRequestUri(LocationModel location) =>
                    $"{_openWeatherUrl}?lat={location.lat}&lon={location.lon}&units=metric&appid={_apiKey}";
    }
}
