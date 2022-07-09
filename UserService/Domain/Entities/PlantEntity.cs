using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PlantEntity
    {
        public int? PlantId { get; set; }

        public string PlantName { get; set; }

        public string SensorId { get; set; }

        public IrrigationConfigEntity IrrigationConfig { get; set; }

        public List<MeasurementEntity> Measurements {  get; set; }
    }
}
