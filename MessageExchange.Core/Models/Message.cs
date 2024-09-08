namespace MessageExchange.Core.Models;

public class Message(int id, string content, int orderNumber, DateTime timestamp)
{
    public int Id { get; } = id;
    public string Content { get; } = content;
    public int OrderNumber { get; } = orderNumber;
    public DateTime Timestamp { get; } = timestamp;
}