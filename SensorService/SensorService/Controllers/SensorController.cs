using AutoMapper;
using Domain.Interface;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using SensorService.Models;

namespace SensorService.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SensorController : ControllerBase
    {
        private readonly ISmartRainSensorService _sensorService;
        private readonly IMapper _mapper;

        public SensorController(ISmartRainSensorService sensorService, IMapper mapper)
        {
            _sensorService = sensorService;
            _mapper = mapper;
        }

        [HttpPost]
        public  string SaveSensorId(string sensorId)
        {
            _sensorService.InsertSensor(sensorId);

            return sensorId;
        }

        [HttpPost]
        public Measurement SaveMeasurements(Measurement measurement)
        {
            measurement.ReadingDateTime = DateTime.Now;
            _sensorService.InsertMeasurements(_mapper.Map<MeasurementEntity>(measurement));
            return measurement;
        }

        [HttpGet]
        public bool GetIrrigationConfirmation(string sensorId)
        {
            var confirmation = _sensorService.GetIrrigationConfirmation(sensorId);

            return confirmation;
        }
    }
}