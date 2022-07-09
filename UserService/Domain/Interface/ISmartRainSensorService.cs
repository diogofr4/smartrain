using Domain.Entities;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface ISmartRainSensorService
    {
        IrrigationConfigEntity GetIrrigationConfig(int plantId);
        MeasurementEntity GetLastMeasurement(string sensorId);
        void InsertMeasurements(MeasurementEntity measurement);
        IEnumerable<PlantEntity> GetPlants();
        IEnumerable<string> GetAvailableSensors();
        void CreatePlant(PlantEntity createPlantModel);
        void UpdatePlant(PlantEntity plantEntity);
        void DeletePlant(int plantId);
    }
}
