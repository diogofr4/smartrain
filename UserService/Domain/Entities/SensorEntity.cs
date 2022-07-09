using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    [Table("Sensor")]
    public class SensorEntity
    {
        [Column("sensorId")]
        public string SensorId { get; set; }
    }
}
