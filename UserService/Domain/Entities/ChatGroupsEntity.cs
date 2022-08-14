using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("ChatGroups")]
public class ChatGroupsEntity
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("category")]
    public string Category { get; set; }
}

