namespace Service;

public class ChatDTO
{
    public string Name { get; set; }

    public IEnumerable<MessageDTO> Messages { get; set; }

    public IEnumerable<string> Participants { get; set; }
}

