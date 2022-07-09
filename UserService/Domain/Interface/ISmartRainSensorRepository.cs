using Domain.Entities;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface ISmartRainSensorRepository
    {
        void InsertMeasurements(MeasurementEntity model);
        IEnumerable<T> GetIrrigationConfig<T>(int plantId);
        IEnumerable<T> GetLastMeasurement<T>(string sensorId);
        IEnumerable<T> GetPlants<T>();
        IEnumerable<T> GetAvailableSensors<T>();
        Task<int> InsertPlant(PlantEntity plantEntity);
        void InsertSensor(PlantEntity plantEntity);
        Task InsertPlantSensorRelationship(PlantEntity plantEntity);
        Task InsertIrrigationConfig(PlantEntity plantEntity);
        IEnumerable<T> GetPlant<T>(string plantName);
        bool UpdatePlant(PlantEntity plantEntity);
        void DeletePlant(int plantId);
        void DeletePlantSensorRelationshipByPlantId(int plantId);
        void DeleteIrrigationConfigByPlantId(int plantId);
    }
}
