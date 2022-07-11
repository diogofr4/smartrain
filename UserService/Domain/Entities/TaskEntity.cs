using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Task")]
    public class TaskEntity
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("responsibleUser")]
        public string ResponsibleUser { get; set; }

        [Column("startDateTime")]
        public DateTimeOffset StartDateTime { get; set; }

        [Column("endDateTime")]
        public DateTimeOffset EndDateTime { get; set; }
    }
}
