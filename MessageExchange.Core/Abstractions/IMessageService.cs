using MessageExchange.Core.Models;

namespace MessageExchange.Core.Abstractions;

public interface IMessageService
{
    Task<Message> SendMessage(string content, int orderNumber);
    Task<List<Message>> GetMessagesForPeriod(DateTime start, DateTime end);
}