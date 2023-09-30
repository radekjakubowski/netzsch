using Common;

namespace WebServer.Hubs.Abstractions
{
    public interface IMessagingHub
    {
        Task ReceiveClientMessages(Dictionary<string, string> messages);
        Task ReceiveServerMessage(Message message);
    }
}
