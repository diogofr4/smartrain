using Domain.Entities;

namespace Domain.Interface;

public interface IChatRepository
{
    Task<int> CreateChat(ChatGroupsEntity chatEntity);

    IEnumerable<T> GetMessages<T>(int groupId);

    Task<int> InsertParticipant(GroupParticipantsEntity participantEntity);

    Task<int> SendMessage(GroupMessagesEntity messageEntity);

    IEnumerable<T> GetChatName<T>(int chatId);

    IEnumerable<T> GetChatParticipants<T>(int chatId);

    IEnumerable<T> GetAllChats<T>();
}

