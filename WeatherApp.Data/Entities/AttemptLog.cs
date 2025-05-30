using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Data.Entities
{
    public class AttemptLog
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string RequestUri { get; set; }

        public int StatusCode { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
