using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    [Table("IrrigationConfig")]
    public class IrrigationConfigEntity
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("plantId")]
        public string PlantId { get; set; }

        [Column("humidityMin")]
        public int HumidityMin { get; set; }

        [Column("temperatureMax")]
        public int TemperatureMax { get; set; }
    }
}
