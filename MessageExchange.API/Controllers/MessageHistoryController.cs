using MessageExchange.API.Contracts;
using MessageExchange.Core.Abstractions;
using MessageExchange.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace MessageExchange.API.Controllers;

public class MessageHistoryController : Controller
{
    private readonly IMessageService _messageService;

    public MessageHistoryController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    public async Task<IActionResult> Index()
    {
        DateTime end = DateTime.UtcNow;
        DateTime start = end.AddMinutes(-10);
        List<Message> messages = await _messageService.GetMessagesForPeriod(start, end);
        List<MessageResponse> responseMessages = new List<MessageResponse>();
        foreach (Message message in messages)
        {
            responseMessages.Add(new MessageResponse(message.Id, message.Content, message.OrderNumber, message.Timestamp));
        }
        
        return View(responseMessages);
    }
}