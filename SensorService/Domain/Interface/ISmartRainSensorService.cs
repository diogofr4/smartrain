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
        bool GetIrrigationConfirmation(string sensorId);
        IrrigationConfigEntity GetIrrigationConfig(string sensorId);
        MeasurementEntity GetLastMeasurement(string sensorId);
        void InsertMeasurements(MeasurementEntity measurement);
        void InsertSensor(string sensorId);
    }
}
