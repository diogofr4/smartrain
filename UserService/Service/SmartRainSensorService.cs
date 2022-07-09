using Domain.Entities;
using Domain.Interface;
using Infrastructure.Entities;

namespace Service
{
    public class SmartRainSensorService : ISmartRainSensorService
    {
        private readonly ISmartRainSensorRepository _smartRainSensorRepository;

        public SmartRainSensorService
        (
            ISmartRainSensorRepository smartRainSensorRepository
        )
        {
            _smartRainSensorRepository = smartRainSensorRepository;
        }

        public IrrigationConfigEntity GetIrrigationConfig(int plantId)
        {
            var config = _smartRainSensorRepository.GetIrrigationConfig<IrrigationConfigEntity>(plantId);

            return config?.FirstOrDefault();
        }

        public MeasurementEntity GetLastMeasurement(string sensorId)
        {
            var measurement = _smartRainSensorRepository.GetLastMeasurement<MeasurementEntity>(sensorId);
            
            return measurement?.FirstOrDefault();
        }


        public void InsertMeasurements(MeasurementEntity measurement)
        {
            _smartRainSensorRepository.InsertMeasurements(measurement);
        }

        public IEnumerable<PlantEntity> GetPlants()
        {
            var plants = _smartRainSensorRepository.GetPlants<PlantEntity>();
            foreach (var plant in plants)
            {
                plant.IrrigationConfig = GetIrrigationConfig(plant.PlantId.Value);
                plant.Measurements = new List<MeasurementEntity>() { GetLastMeasurement(plant.SensorId) };
            }

            return plants;
        }

        public IEnumerable<string> GetAvailableSensors() => _smartRainSensorRepository.GetAvailableSensors<string>();

        public async void CreatePlant(PlantEntity createPlantModel)
        {
            createPlantModel.PlantId = await _smartRainSensorRepository.InsertPlant(createPlantModel);
            await _smartRainSensorRepository.InsertPlantSensorRelationship(createPlantModel);
            await _smartRainSensorRepository.InsertIrrigationConfig(createPlantModel);

        }

        public void UpdatePlant(PlantEntity plantEntity)
        {
            _smartRainSensorRepository.UpdatePlant(plantEntity);
        }

        public void DeletePlant(int plantId)
        {
            _smartRainSensorRepository.DeletePlantSensorRelationshipByPlantId(plantId);
            _smartRainSensorRepository.DeleteIrrigationConfigByPlantId(plantId);
            _smartRainSensorRepository.DeletePlant(plantId);
        }
    }
}