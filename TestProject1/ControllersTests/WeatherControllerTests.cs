using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Data.Repositories;
using WeatherApp.Data;
using WeatherApp.Services.Interfaces;
using WeatherApp.Services;
using WeatherApp.WebSite.Controllers;
using WeatherApp.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace WeatherApp.Tests.ControllersTests
{
    [TestFixture]
    public class WeatherControllerTests
    {
        private Mock<IWeatherService> _mockWeatherService;
        private Mock<ILocationService> _mockLocationService;


        [SetUp]
        public void Setup()
        {
             _mockWeatherService = new Mock<IWeatherService>();
            _mockLocationService = new Mock<ILocationService>();

        }

        [Test]
        public void GetWeather_ReturnsForecastFromService()
        {
            // Arrange
            var controller = new WeatherController(_mockWeatherService.Object, _mockLocationService.Object);
            var dateFrom = DateTime.Now;
            var dateTo = DateTime.Now.AddDays(-7);
            var minMaxTemperatures = new List<MinMaxTemperatureModel>
            {
                new MinMaxTemperatureModel() {City = "City1", Country = "Country1", MaxTemperature = 17, MinTemperature = 15, MaxTemperatureLastUpdate = DateTime.Now, MinTemperatureLastUpdate = DateTime.Now.AddDays(-2)},
                new MinMaxTemperatureModel() {City = "City2", Country = "Country2", MaxTemperature = 19, MinTemperature = 16, MaxTemperatureLastUpdate = DateTime.Now, MinTemperatureLastUpdate = DateTime.Now.AddDays(-1)},
                new MinMaxTemperatureModel() {City = "City3", Country = "Country3", MaxTemperature = 20, MinTemperature = 21, MaxTemperatureLastUpdate = DateTime.Now, MinTemperatureLastUpdate = DateTime.Now.AddDays(-3)},
            };
            _mockWeatherService.Setup(ws => ws.GetMinMaxTemperature(dateFrom, dateTo)).ReturnsAsync(minMaxTemperatures);

            // Act
            var result =  controller.MinMaxTemperature(dateFrom, dateTo).Result;

            // Assert
            Assert.IsInstanceOf<JsonResult>(result);
            CollectionAssert.AreEqual(JsonSerializer.Serialize(minMaxTemperatures), JsonSerializer.Serialize(result.Value));
        }
    }
}
