namespace MessageExchange.API.Contracts;

public record SendMessageRequest(string Content, int OrderNumber);

public record MessageResponse(int Id, string Content, int OrderNumber, DateTime Timestamp);