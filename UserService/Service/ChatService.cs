using Domain.Entities;
using Domain.Interface;

namespace Service;

public class ChatService : IChatService
{
    private readonly IChatRepository _chatRepository;

    public ChatService(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async void CreateChat(ChatGroupsEntity chatEntity)
    {
        chatEntity.Id = await _chatRepository.CreateChat(chatEntity);
    }

    public ChatDTO GetMessages(int chatId)
    {


        var messages = _chatRepository.GetMessages<AllMessages>(chatId);

        var chatDTO = new ChatDTO()
        {
            Name = string.Empty,
            Messages = new List<MessageDTO>(),
            Participants = new List<string>()
        };

        if (messages.Any())
        {
            chatDTO.Name = _chatRepository.GetChatName<string>(chatId).First();
            chatDTO.Participants = _chatRepository.GetChatParticipants<string>(chatId);
            foreach (var message in messages)
                chatDTO.Messages = chatDTO.Messages.Append(new MessageDTO() { Message = message.Message, Participant = message.ParticipantName });
        }

        return chatDTO;
    }

    public async void InsertParticipant(GroupParticipantsEntity participantEntity)
    {
        await _chatRepository.InsertParticipant(participantEntity);
    }

    public async void SendMessage(GroupMessagesEntity messageEntity)
    {
        messageEntity.MessageDate = DateTimeOffset.Now;
        await _chatRepository.SendMessage(messageEntity);
    }

    public IEnumerable<AllChats> GetAllChats()
    {
        return _chatRepository.GetAllChats<AllChats>();
    }
}

