using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome_WebApp.Models
{
    public class DataSample
    {
        [Key]
        public Guid SamplingId { get; set; }
        [Required]
        public int SenderId { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double CoLevel { get; set; }
        public double SmokeLevel { get; set; }
        public double LpgLevel { get; set; }

        public int Movement { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
