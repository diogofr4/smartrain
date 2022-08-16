using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using Service;
using UserService.Models;

namespace UserService.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ChatController : ControllerBase
{

    private readonly IMapper _mapper;

    private readonly IChatService _chatService;

    public ChatController(IChatService chatService, IMapper mapper)
    {
        _chatService = chatService;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult CreateChat(ChatGroups chat)
    {
        try
        {
            _chatService.CreateChat(_mapper.Map<ChatGroupsEntity>(chat));
            return Ok(chat);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPost]
    public IActionResult InsertParticipant(GroupParticipants participant)
    {
        try
        {
            _chatService.InsertParticipant(_mapper.Map<GroupParticipantsEntity>(participant));
            return Ok(participant);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPost]
    public IActionResult SendMessage(GroupMessages message)
    {
        try
        {
            _chatService.SendMessage(_mapper.Map<GroupMessagesEntity>(message));
            return Ok(message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet]
    public ChatDTO GetMessages(int chatId)
    {
        return _chatService.GetMessages(chatId);
    }

    [HttpGet]
    public IEnumerable<AllChats> GetAllChats()
    {
        return _chatService.GetAllChats();
    }
}

