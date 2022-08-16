namespace UserService.Models;

public class CategoriesDTO
{
    public ILookup<string, ChatDTO> Chats { get; set; }
}

