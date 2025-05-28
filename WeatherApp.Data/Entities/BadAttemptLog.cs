using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Data.Entities
{
    public class BadAttemptLog
    {
        public int ID { get; set; }

        public string ErrorMessage { get; set; }

        public DateTime Date { get; set; }
    }
}
