using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("GroupParticipants")]
public class GroupMessagesEntity
{
    [Column("id")]
    public int Id { get; set; }

    [Column("groupid")]
    public int GroupId { get; set; }

    [Column("participantName")]
    public string ParticipantName { get; set; }

    [Column("message")]
    public string Message { get; set; }

    [Column("messageDate")]
    public DateTimeOffset MessageDate { get; set; }
}

