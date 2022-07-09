using Infrastructure.Clients;
using Infrastructure.Entities;
using Microsoft.Extensions.Configuration;
using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Repositories
{
    public class SmartRainSensorRepository : DapperClient, ISmartRainSensorRepository
    {
        private const string SQL_GET_IRRIGATION_CONFIG = @"
            SELECT TOP 1
                    id,
                    plantId,
                    humidityMin,
                    temperatureMax
            FROM 
                    dbo.IrrigationConfig i
			INNER JOIN
					dbo.Plant_Sensor ps on ps.plantId = i.plantId
            WHERE
                    ps.sensorId = @SensorId
            ORDER BY
                    id DESC
        ";
        
        private const string SQL_GET_LAST_MEASUREMENT = @"
            SELECT TOP 1
                     id
                    ,sensorId
                    ,readingDateTime
                    ,humidity
                    ,rain
                    ,temperature
                    ,luminosity
            FROM 
	                dbo.Measurement
            WHERE
	                sensorId = @SensorId
            ORDER BY
                    id DESC
        ";

        private const string SQL_GET_SENSOR = @"
            SELECT
                sensorId
            FROM Sensor
            WHERE
                sensorId = @SensorId
        ";

        public SmartRainSensorRepository(IConfiguration configuration) : base(configuration, "SmartRainSensorDatabase")
        {
        }

        public void InsertMeasurements(MeasurementEntity model) => Insert(model);
        public IEnumerable<T> GetIrrigationConfig<T>(string sensorId) => Get<T>(SQL_GET_IRRIGATION_CONFIG, new {SensorId = sensorId});
        public IEnumerable<T> GetLastMeasurement<T>(string sensorId) => Get<T>(SQL_GET_LAST_MEASUREMENT, new { SensorId = sensorId });
        public IEnumerable<T> GetSensor<T>(string sensorId) => Get<T>(SQL_GET_SENSOR, new { SensorId = sensorId });
        public void InsertSensor(string sensorId) => Insert(new SensorEntity { SensorId = sensorId });
    }
}
