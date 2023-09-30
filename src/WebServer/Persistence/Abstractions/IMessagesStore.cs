using Common;

namespace WebServer.Persistence.Abstractions;

public interface IMessagesStore
{
    void StoreMessage(Message message);
    Dictionary<string, string> GetMessages();
}
