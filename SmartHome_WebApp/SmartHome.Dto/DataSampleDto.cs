using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SmartHome.Model;

namespace SmartHome.Dto
{
    public class DataSampleDto
    {
        [Required]
        public Guid MasterUnitId { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double CoLevel { get; set; }
        public double SmokeLevel { get; set; }
        public double LpgLevel { get; set; }
        public int Movement { get; set; }
        public Guid SampleId { get; set; }


        public static DataSampleDto FromDataSample(DataSample dataSample)
        {
            return new DataSampleDto
            {
                CoLevel = dataSample.CoLevel,
                MasterUnitId = dataSample.MasterUnitId,
                Temperature = dataSample.Temperature,
                Humidity = dataSample.Humidity,
                SmokeLevel = dataSample.SmokeLevel,
                LpgLevel = dataSample.LpgLevel,
                Movement = dataSample.Movement,
                SampleId = dataSample.SampleId
            };
        }

        public static DataSample ToDataSample(DataSampleDto measurement)
        {
            return new DataSample
            {
                CoLevel = measurement.CoLevel,
                MasterUnitId = measurement.MasterUnitId,
                Temperature = measurement.Temperature,
                Humidity = measurement.Humidity,
                SmokeLevel = measurement.SmokeLevel,
                LpgLevel = measurement.LpgLevel,
                Movement = measurement.Movement,
                SampleId = measurement.SampleId,
                TimeStamp = DateTime.Now
            };
        }
    }
}
