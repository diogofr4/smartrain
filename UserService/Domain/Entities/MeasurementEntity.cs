using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    [Table("Measurement")]
    public class MeasurementEntity
    {
        [Column("readingDateTime")]
        public DateTime ReadingDateTime { get; set; }

        [Column("humidity")]
        public string Humidity { get; set; }

        [Column("rain")]
        public string Rain { get; set; }

        [Column("temperature")]
        public string Temperature { get; set; }

        [Column("luminosity")]
        public string Luminosity { get; set; }
    }
}
