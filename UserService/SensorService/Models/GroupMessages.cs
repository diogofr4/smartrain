namespace UserService.Models;

public class GroupMessages
{
    public int GroupId { get; set; }

    public string ParticipantName { get; set; }

    public string Message { get; set; }

    public DateTimeOffset MessageDate { get; set; }
}

