using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("GroupParticipants")]
public class GroupParticipantsEntity
{
    [Column("id")]
    public int Id { get; set; }

    [Column("groupid")]
    public int GroupId { get; set; }

    [Column("participantName")]
    public string ParticipantName { get; set; }
}

