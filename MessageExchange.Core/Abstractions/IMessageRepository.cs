using MessageExchange.Core.Models;

namespace MessageExchange.Core.Abstractions;

public interface IMessageRepository
{
    Task<Message> AddMessage(string content, int orderNumber);
    Task<List<Message>> GetMessagesForPeriod(DateTime start, DateTime end);
    Task CreateTableIfNotExist();
}