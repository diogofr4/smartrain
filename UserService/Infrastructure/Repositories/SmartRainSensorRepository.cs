using Infrastructure.Clients;
using Infrastructure.Entities;
using Microsoft.Extensions.Configuration;
using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class SmartRainSensorRepository : DapperClient, ISmartRainSensorRepository
    {
        private const string SQL_GET_LAST_MEASUREMENT = @"
            SELECT TOP 1
                     id
                    ,readingDateTime
                    ,humidity
                    ,rain
                    ,temperature
                    ,luminosity
            FROM 
	                Measurement
            WHERE
	                sensorId = @SensorId
            ORDER BY
                    id DESC
        ";

        private const string SQL_GET_PLANTS = @"
            SELECT 
		        p.name PlantName,
		        p.id PlantId,
		        s.sensorId SensorId		
            FROM Plant p
            INNER JOIN Plant_Sensor s ON p.id = s.plantId
        ";

        private const string SQL_GET_IRRIGATION_CONFIG = @"
            SELECT
                i.humidityMin HumidityMin,
                i.temperatureMax TemperatureMax
            FROM 
                IrrigationConfig i
            WHERE
                i.plantId = @PlantId
        ";

        private const string SQL_GET_AVAILABLE_SENSORS = @"
            SELECT 
	            sensorId
            FROM
	            Sensor
            WHERE
	            sensorId NOT IN(
		            SELECT 
			            sensorId
		            FROM
			            Plant_Sensor
	            )
        ";

        private const string SQL_GET_PLANT = @"
            SELECT 
		        p.name PlantName,
		        p.id PlantId		     
            FROM 
                Plant p
            WHERE
                p.name = @PlantName
        ";

        private const string SQL_DELETE_PLANT = @"
            DELETE FROM
                Plant
            WHERE
                id = @PlantId
        ";

        private const string SQL_DELETE_IRRIGATION_CONFIG_BY_PLANTID = @"
            DELETE FROM
                IrrigationConfig
            WHERE
                plantId = @PlantId
        ";

        private const string SQL_DELETE_PLANT_SENSOR_BY_PLANTID = @"
            DELETE FROM
                Plant_Sensor
            WHERE
                plantId = @PlantId
        ";

        public SmartRainSensorRepository(IConfiguration configuration) : base(configuration, "SmartRainSensorDatabase")
        {
        }

        public void InsertMeasurements(MeasurementEntity model) => Insert(model);
        public IEnumerable<T> GetLastMeasurement<T>(string sensorId) => Get<T>(SQL_GET_LAST_MEASUREMENT, new {SensorId = sensorId});
        public IEnumerable<T> GetPlants<T>() => Get<T>(SQL_GET_PLANTS);
        public IEnumerable<T> GetIrrigationConfig<T>(int plantId) => Get<T>(SQL_GET_IRRIGATION_CONFIG, new {PlantId = plantId});
        public IEnumerable<T> GetAvailableSensors<T>() => Get<T>(SQL_GET_AVAILABLE_SENSORS);
        public IEnumerable<T> GetPlant<T>(string plantName) => Get<T>(SQL_GET_PLANT, new { PlantName = plantName });

        public async Task<int> InsertPlant(PlantEntity plantEntity) => await Insert(
            new { 
                name = plantEntity.PlantName 
            }
            , tableName: "Plant");


        public async void InsertSensor(PlantEntity plantEntity) => await Insert(
            new
            {
                sensorId = plantEntity.SensorId 
            }
            , tableName: "Sensor");


        public async Task InsertPlantSensorRelationship(PlantEntity plantEntity) => await Insert(
            new { 
                sensorId = plantEntity.SensorId, 
                plantId = plantEntity.PlantId 
            }
            , tableName: "Plant_Sensor");


        public async Task InsertIrrigationConfig(PlantEntity plantEntity) => await Insert(
            new
            {
                humidityMin = plantEntity.IrrigationConfig.HumidityMin,
                temperatureMax = plantEntity.IrrigationConfig.TemperatureMax,
                plantId = plantEntity.PlantId
            }
            , tableName: "IrrigationConfig");

        public bool UpdatePlant(PlantEntity plantEntity) => UpdatePlant(new
        {
            id = plantEntity.PlantId,
            name = plantEntity.PlantName
        }, new List<string>
        {
            "id"
        }
        ,tableName: "Plant");

        public void DeletePlant(int plantId) => Delete(SQL_DELETE_PLANT, new { PlantId = plantId });
        public void DeleteIrrigationConfigByPlantId(int plantId) => Delete(SQL_DELETE_IRRIGATION_CONFIG_BY_PLANTID, new { PlantId = plantId });
        public void DeletePlantSensorRelationshipByPlantId(int plantId) => Delete(SQL_DELETE_PLANT_SENSOR_BY_PLANTID, new { PlantId = plantId });
        
    }
}
