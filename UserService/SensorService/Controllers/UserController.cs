using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using SensorService.Models;
using UserService.Models;

namespace SensorService.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly ISmartRainSensorService _sensorService;
        private readonly IMapper _mapper;

        public UserController(ISmartRainSensorService sensorService, IMapper mapper)
        {
            _sensorService = sensorService;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<Plant> GetPlants()
        {
            return _mapper.Map<IEnumerable<Plant>>(_sensorService.GetPlants());
        }

        [HttpGet]
        public IEnumerable<string> GetAvailableSensors()
        {
            return _sensorService.GetAvailableSensors();
        }

        [HttpPost]
        public IActionResult CreatePlant(CreatePlantModel createPlantModel)
        {
            try
            {
                _sensorService.CreatePlant(_mapper.Map<PlantEntity>(createPlantModel));
                return Ok(createPlantModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]   
        public IActionResult EditPlant(int plantId, CreatePlantModel createPlantModel)
        {
            try
            {
                createPlantModel.PlantId = plantId;
                _sensorService.UpdatePlant(_mapper.Map<PlantEntity>(createPlantModel));
                return Ok(createPlantModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        public IActionResult DeletePlant(int plantId)
        {
            try
            {
                _sensorService.DeletePlant(plantId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}