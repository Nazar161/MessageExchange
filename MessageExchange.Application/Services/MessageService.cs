using MessageExchange.Application.Hubs;
using MessageExchange.Core.Abstractions;
using MessageExchange.Core.Models;
using Microsoft.AspNetCore.SignalR;

namespace MessageExchange.Application.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly IHubContext<MessageHub> _hubContext;

    public MessageService(IMessageRepository messageRepository, IHubContext<MessageHub> hubContext)
    {
        _messageRepository = messageRepository;
        _hubContext = hubContext;
    }

    public async Task<Message> SendMessage(string content, int orderNumber)
    {
        Message message = await _messageRepository.AddMessage(content, orderNumber);
        await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);

        return message;
    }

    public async Task<List<Message>> GetMessagesForPeriod(DateTime start, DateTime end)
    {
        return await _messageRepository.GetMessagesForPeriod(start, end);
    }
}