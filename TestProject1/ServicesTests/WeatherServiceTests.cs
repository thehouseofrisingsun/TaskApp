using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.Services.Interfaces;
using WeatherApp.Services;
using Moq;
using Moq.Protected;
using WeatherApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using WeatherApp.Data.Repositories;
using WeatherApp.Data;
using WeatherApp.Data.Repositories.Interfaces;

namespace WeatherApp.Tests.ServicesTests
{
    [TestFixture]
    public class WeatherServiceTests
    {
        private Mock<HttpMessageHandler> _httpMessageHandler;
        private HttpClient _httpClient;
        private Mock<IAttemptLogService> _logger;
        private Mock<IWeatherRepository> _weatherRepository;
        private WeatherService _weatherService;
        private Mock<IConfiguration> _config;

        [SetUp]
        public void SetUp()
        {
            _logger = new Mock<IAttemptLogService>();
            _weatherRepository = new Mock<IWeatherRepository>();
            _config = new Mock<IConfiguration>();
            _config.Setup(c => c["OpenWeatherBaseUrl"]).Returns("https://api.openweathermap.org/data/2.5/weather");
            _config.Setup(c => c["OpenWeatherAPIKey"]).Returns("74dc0d4c0a0820e8083081024e484b6b");

            _httpMessageHandler = new Mock<HttpMessageHandler>();
            _httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"temperature\": 15}") 
                });

            _httpClient = new HttpClient(_httpMessageHandler.Object);

            _logger = new Mock<IAttemptLogService>();
            _weatherService = new WeatherService(_config.Object,_weatherRepository.Object, _logger.Object);
        }

        [Test]
        public async Task GetCurrentWeather_ShouldLogPayload()
        {
            // Arrange
            var location = new LocationModel { lat = 44.0, lon = 21.0 };

            // Act
            var result = await _weatherService.GetCurrentWeather(location);

            _logger.Verify(service => service.AddLog(It.Is<AttemptLogModel>(log =>
                log.StatusCode == 200 &&
                log.RequestUri.Contains("lat=44") &&
                log.RequestUri.Contains("lon=21") &&
                log.ErrorMessage == string.Empty
            )), Times.Once);
        }

        [TearDown]
        public void TearDown()
        {
            _httpClient.Dispose();
        }
    }
}
