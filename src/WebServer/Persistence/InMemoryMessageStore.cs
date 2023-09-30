using Common;
using WebServer.Persistence.Abstractions;

namespace WebServer.Persistence;

public class InMemoryMessageStore : IMessagesStore
{
    private Dictionary<string, string> _clientMessages = new();
    private readonly object _object = new object();

    public Dictionary<string, string> GetMessages()
    {
        return _clientMessages;
    }

    public void StoreMessage(Message message)
    {
        lock (_object)
        {
            if (!_clientMessages.ContainsKey(message.ClientId))
            {
                _clientMessages.Add(message.ClientId, message.Text);
            }

            if (_clientMessages.ContainsKey(message.ClientId))
            {
                _clientMessages[message.ClientId] = message.Text;
            }

            if (string.IsNullOrWhiteSpace(message.Text) && _clientMessages.ContainsKey(message.ClientId))
            {
                _clientMessages.Remove(message.ClientId);
            }
        }
    }
}
