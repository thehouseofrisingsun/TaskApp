using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Data.Entities
{
    public class WeatherStatInfo
    {
        public string City { get; set; }

        public string Country { get; set; }

        public double? MaxTemperature { get; set; }

        public double? MinTemperature { get; set; }

        public DateTime MaxTemperatureLastUpdate { get; set; }

        public DateTime MinTemperatureLastUpdate { get; set; }
    }
}
