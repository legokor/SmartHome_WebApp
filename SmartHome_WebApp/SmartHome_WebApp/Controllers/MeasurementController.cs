using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SmartHome_WebApp.Models;
using SmartHome_WebApp.Data;

namespace SmartHome_WebApp.Controllers
{
    [Authorize]
    public class MeasurementController : Controller
    {
        private RepositoryService _repository;

        public MeasurementController(RepositoryService repository)
        {
            _repository = repository;
            _repository.DataSamples.Add(
                new DataSample {
                    CoLevel = 1.2,
                    Humidity = 39,
                    Temperature = 25,
                    SmokeLevel = 6.0,
                    Movement = 1,
                    LpgLevel = 0.002,
                    SenderId = 1123123123,
                    TimeStamp = DateTime.Now,
                    SamplingId = new Guid()
                });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SaveData([FromBody] DataSample measurement)
        {
            //Check if the model is valid
            if(!ModelState.IsValid)
            {
                return new BadRequestResult();
            }

            if (!await _repository.DataSamples.Add(measurement))
            {
                //If the saving fails returns internal server error
                return StatusCode(500);
            }

            return new OkResult();
        }

        [HttpGet]
        public async Task<IActionResult> ReadMeasurements(string Id)
        {
            //If no Id was provided
            if(Id == null)
            {
                return new BadRequestResult();
            }

            var result = await _repository.DataSamples.Find(wer => wer.SamplingId == Guid.Parse(Id));

            return new JsonResult(result);
        }
    }
}