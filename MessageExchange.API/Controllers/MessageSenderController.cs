using MessageExchange.API.Contracts;
using MessageExchange.Core.Abstractions;
using MessageExchange.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace MessageExchange.API.Controllers;

public class MessageSenderController : Controller
{
    private readonly IMessageService _messageService;
    public MessageSenderController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(string content, int orderNumber)
    {
        Message message = await _messageService.SendMessage(content, orderNumber);

        return RedirectToAction("Index");
    }
}