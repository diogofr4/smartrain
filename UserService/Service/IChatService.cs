using Domain.Entities;

namespace Service;

public interface IChatService
{
    void CreateChat(ChatGroupsEntity chatEntity);

    void InsertParticipant(GroupParticipantsEntity participantEntity);

    void SendMessage(GroupMessagesEntity messageEntity);

    ChatDTO GetMessages(int groupId);

    IEnumerable<AllChats> GetAllChats();
}

