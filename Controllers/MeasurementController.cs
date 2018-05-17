using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SmartHome.Persistence;
using SmartHome.Model;
using SmartHome.Dto;

namespace SmartHome_WebApp.Controllers
{
    [Authorize]
    public class MeasurementController : Controller
    {
        private RepositoryService _repository;

        public MeasurementController(RepositoryService repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SaveData([FromBody] DataSampleDto measurement)
        {
            //Check if the model is valid
            if(!ModelState.IsValid)
            {
                return new BadRequestResult();
            }

            DataSample data = DataSampleDto.ToDataSample(measurement);

            if (!await _repository.DataSamples.AddAsync(data))
            {
                //If the saving fails returns internal server error
                return StatusCode(500);
            }

            return new OkResult();
        }

        [HttpGet]
        public async Task<IActionResult> ReadMeasurement(Guid id)
        {
            //If no Id was provided
            if (id == null || id == Guid.Empty)
            {
                return new BadRequestResult();
            }
            var dataRecord = await _repository.DataSamples.FindAsync(wer => wer.SampleId == id);

            if (dataRecord == null)
            {
                return NotFound();
            }

            DataSampleDto result = DataSampleDto.FromDataSample(dataRecord);

            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> ReadAllMeasurementsFromSender(Guid senderId)
        {
            //If no Id was provided
            if (senderId == null || senderId == Guid.Empty)
            {
                return new BadRequestResult();
            }

            var dataRecord = await _repository.DataSamples.FindListAsync(wer => wer.MasterUnitId == senderId);

            if (dataRecord == null || dataRecord?.Count == 0)
            {
                return NotFound();
            }

            List<DataSampleDto> result = new List<DataSampleDto>();
            foreach (var item in dataRecord)
            {
                result.Add(DataSampleDto.FromDataSample(item));
            }

            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> ReadAllMeasurements()
        {
            //Reading all datas
            var dataRecord = await _repository.DataSamples.FindListAsync(wer => true);

            if (dataRecord == null || dataRecord?.Count == 0)
            {
                return NotFound();
            }

            //Convert DataSamples to DataSampleDto
            List<DataSampleDto> result = new List<DataSampleDto>();
            foreach (var item in dataRecord)
            {
                result.Add(DataSampleDto.FromDataSample(item));
            }

            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> ReadMeasurementBetween(DateTime from, DateTime until)
        {
            //If no Id was provided
            if (from >= until)
            {
                return new BadRequestResult();
            }

            var dataRecord = await _repository.DataSamples.FindListAsync(wer => (wer.TimeStamp >= from) && (wer.TimeStamp <= until));

            if (dataRecord == null || dataRecord?.Count == 0)
            {
                return NotFound();
            }

            //Convert DataSamples to DataSampleDto
            List<DataSampleDto> result = new List<DataSampleDto>();
            foreach (var item in dataRecord)
            {
                result.Add(DataSampleDto.FromDataSample(item));
            }

            return new JsonResult(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMeasurment(Guid measurmentId)
        {
            if (measurmentId == null || measurmentId == Guid.Empty)
            {
                return new BadRequestResult();
            }

            if(!await _repository.DataSamples.RemoveAsync(measurmentId))
            {
                return NotFound();
            }

            return Ok();
        }

    }
}