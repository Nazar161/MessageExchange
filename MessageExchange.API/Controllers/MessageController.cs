using MessageExchange.API.Contracts;
using MessageExchange.Core.Abstractions;
using MessageExchange.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace MessageExchange.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpPost("send")]
    public async Task<ActionResult<int>> SendMessage([FromBody] SendMessageRequest request)
    {
        var messageId = await _messageService.SendMessage(request.Content, request.OrderNumber);

        return Ok(messageId);
    }

    [HttpGet("history")]
    public async Task<ActionResult<List<MessageResponse>>> GetMessagesForPeriod(DateTime start, DateTime end)
    {
        List<Message> messages = await _messageService.GetMessagesForPeriod(start, end);

        if (messages.Count > 0)
        {
            return Ok(messages);
        }

        return NotFound("There are no new messages");
    }
}