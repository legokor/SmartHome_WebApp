using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmartHome.Model
{
    public class MasterUnit
    {
        public Guid Id { get; set; }

        public string CustomName { get; set; }

        public List<Design> Designs { get; set; }

        public bool IsOn { get; set; }

        [Required]
        public User Owner { get; set; }

        public List<DataSample> DataSamples { get; set; }
    }
}
