using Domain.Entities;
using Domain.Interface;
using Infrastructure.Clients;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class ChatRepository : DapperClient, IChatRepository
{
    private const string SQL_GET_MESSAGES = @"select participantName, [message] 
                                                from GroupMessages 
                                                where groupid = @ChatId;";

    private const string SQL_GET_CHAT_NAME = @"select top 1 [name] from ChatGroups where id = @ChatId;";

    private const string SQL_GET_CHAT_PARTICIPANTS = @"select participantName
                                                        from GroupParticipants 
                                                        where groupid = @ChatId;";

    private const string SQL_GET_ALL_CHATS = "select id as ChatId, [name] as ChatName, category as CategoryName from ChatGroups";

    public ChatRepository(IConfiguration configuration) : base(configuration, "SmartRainSensorDatabase")
    {
    }

    public async Task<int> CreateChat(ChatGroupsEntity chatEntity)
    {
        return await Insert(new { chatEntity.Name, chatEntity.Category }, tableName: "ChatGroups");
    }

    public IEnumerable<T> GetMessages<T>(int chatId)
    {
        return Get<T>(SQL_GET_MESSAGES, new { ChatId = chatId });
    }

    public IEnumerable<T> GetChatName<T>(int chatId)
    {
        return Get<T>(SQL_GET_CHAT_NAME, new { ChatId = chatId });
    }

    public IEnumerable<T> GetChatParticipants<T>(int chatId)
    {
        return Get<T>(SQL_GET_CHAT_PARTICIPANTS, new { ChatId = chatId });
    }

    public async Task<int> InsertParticipant(GroupParticipantsEntity participantEntity)
    {
        return await Insert(new { participantEntity.ParticipantName, participantEntity.GroupId }, tableName: "GroupParticipants");
    }

    public async Task<int> SendMessage(GroupMessagesEntity messageEntity)
    {
        return await Insert(new { messageEntity.Message, messageEntity.MessageDate, messageEntity.GroupId, messageEntity.ParticipantName }, tableName: "GroupMessages");
    }

    public IEnumerable<T> GetAllChats<T>()
    {
        return Get<T>(SQL_GET_ALL_CHATS);
    }
}

