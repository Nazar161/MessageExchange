using MessageExchange.Core.Abstractions;
using MessageExchange.Core.Models;
using Npgsql;

namespace MessageExchange.DataAccess.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly string _connectionString;

    public MessageRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<Message> AddMessage(string content, int orderNumber)
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        connection.Open();
        var cmd = new NpgsqlCommand(
            "INSERT INTO messages (content, ordernumber, timestamp) VALUES (@content, @ordernumber, @timestamp) RETURNING id, content, ordernumber, timestamp;",
            connection);
        cmd.Parameters.AddWithValue("content", content);
        cmd.Parameters.AddWithValue("ordernumber", orderNumber);
        cmd.Parameters.AddWithValue("timestamp", DateTime.UtcNow);

        Message message = null;

        var reader = await cmd.ExecuteReaderAsync();
        while (reader.Read())
        {
            message = new Message(reader.GetInt32(0), reader.GetString(1),
                reader.GetInt32(2), reader.GetDateTime(3));
        }

        if (message is null)
        {
            throw new NpgsqlException("Something went wrong when trying to insert a message to the database.");
        }

        return message;
    }

    public async Task<List<Message>> GetMessagesForPeriod(DateTime start, DateTime end)
    {
        List<Message> messages = [];
        await using var connection = new NpgsqlConnection(_connectionString);

        connection.Open();
        var command = new NpgsqlCommand(
            "SELECT id, content, ordernumber, timestamp FROM messages WHERE timestamp BETWEEN @start AND @end",
            connection);
        command.Parameters.AddWithValue("start", start.ToUniversalTime());
        command.Parameters.AddWithValue("end", end.ToUniversalTime());

        await using (var reader = await command.ExecuteReaderAsync())
        {
            while (reader.Read())
            {
                messages.Add(new Message(reader.GetInt32(0), reader.GetString(1),
                    reader.GetInt32(2), reader.GetDateTime(3)));
            }
        }

        return messages;
    }

    public async Task CreateTableIfNotExist()
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        connection.Open();
        string createTableQuery = """
                                  CREATE TABLE IF NOT EXISTS messages (  
                                      id SERIAL PRIMARY KEY,
                                      content VARCHAR(128) NOT NULL,
                                      ordernumber INT NOT NULL,
                                      timestamp TIMESTAMPTZ NOT NULL
                                  );
                                  """;
        await using (var command = new NpgsqlCommand(createTableQuery, connection))
        {
            await command.ExecuteNonQueryAsync();
        }
    }
}