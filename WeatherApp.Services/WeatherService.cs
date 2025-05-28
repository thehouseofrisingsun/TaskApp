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
        private readonly IWeatherRepository _weatherRepository;

        public WeatherService(IConfiguration configuration, IWeatherRepository weatherRepository)
        {
            _client = new();
            _weatherRepository = weatherRepository;
            _apiKey = configuration["OpenWeatherAPIKey"];
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
                weatherForecast = await response.Content.ReadFromJsonAsync<CurrentWeatherModel>();

            return weatherForecast;
        }

        public async Task<IList<MaxWindSpeedModel>> GetMaxWindSpeed()
        {
            var entity = await _weatherRepository.GetMaxWindSpeed();
            var mxWindSpeedModel = entity.Select(d =>
                new MaxWindSpeedModel
                {
                    City = d.City ?? string.Empty,
                    Country = d.Country ?? string.Empty,
                    LastUpdate = d.LastUpdate,
                    MaxWindSpeed = d.WindSpeed ?? 0
                }).ToList();
            return mxWindSpeedModel;
        }

        public async Task<List<MinTemperatureModel>> GetMinTemperature()
        {
            var entity = await _weatherRepository.GetMinTemperature();
            var minTemperatureModel = entity.Select(d =>
                new MinTemperatureModel
                {
                    City = d.City ?? string.Empty,
                    Country = d.Country ?? string.Empty,
                    LastUpdate = d.LastUpdate,
                    MinTemperature = d.Temperature ?? 0
                }).ToList();
            return minTemperatureModel;
        }

        private string GetCurrentWeatherRequestUri(LocationModel location) =>
                    $"https://api.openweathermap.org/data/2.5/weather?lat={location.lat}&lon={location.lon}&units=metric&appid={_apiKey}";
    }
}
