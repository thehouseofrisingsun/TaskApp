﻿
namespace WeatherApp.Models
{
    public class LocationModel
    {
        public string name { get; set; }
        public LocalNames local_names { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string country { get; set; }
        public string state { get; set; }
    }
    public class LocalNames
    {
        public string sr { get; set; }
        public string lt { get; set; }
        public string ar { get; set; }
        public string uk { get; set; }
        public string ru { get; set; }
        public string ur { get; set; }
        public string fa { get; set; }
        public string mk { get; set; }
        public string hu { get; set; }
        public string ko { get; set; }
        public string en { get; set; }
        public string zh { get; set; }
    }
}
