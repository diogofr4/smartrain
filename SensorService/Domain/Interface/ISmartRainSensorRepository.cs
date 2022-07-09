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
        IEnumerable<T> GetIrrigationConfig<T>(string sensorId);
        IEnumerable<T> GetLastMeasurement<T>(string sensorId);
        IEnumerable<T> GetSensor<T>(string sensorId);
        void InsertSensor(string sensorId);
    }
}
