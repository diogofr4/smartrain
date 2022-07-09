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

        public bool GetIrrigationConfirmation(string sensorId)
        {
            var config = GetIrrigationConfig(sensorId);
            var measurement = GetLastMeasurement(sensorId);
            var humidityFlag = config?.HumidityMin != null ? int.Parse(measurement.Humidity) <= config.HumidityMin : true;
            var temperatureFlag = config?.TemperatureMax != null ? int.Parse(measurement.Temperature) >= config.TemperatureMax : true;
            
            return humidityFlag && temperatureFlag;
        }

        public IrrigationConfigEntity GetIrrigationConfig(string sensorId)
        {
            var configs = _smartRainSensorRepository.GetIrrigationConfig<IrrigationConfigEntity>(sensorId);

            return configs.FirstOrDefault();
        }

        public MeasurementEntity GetLastMeasurement(string sensorId)
        {
            var measurement = _smartRainSensorRepository.GetLastMeasurement<MeasurementEntity>(sensorId);
            
            return measurement.FirstOrDefault();
        }


        public void InsertMeasurements(MeasurementEntity measurement)
        {
            _smartRainSensorRepository.InsertMeasurements(measurement);
        }

        public void InsertSensor(string sensorId)
        {
            var sensor = _smartRainSensorRepository.GetSensor<SensorEntity>(sensorId);
            if(sensor.Count() == 0 || sensor == null)
                _smartRainSensorRepository.InsertSensor(sensorId);
        }
    }
}