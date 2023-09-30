using Common;
using Microsoft.AspNetCore.SignalR;
using WebServer.Hubs.Abstractions;
using WebServer.Persistence.Abstractions;

namespace WebServer.Hubs;

public class MessagingHub : Hub<IMessagingHub>
{
    private readonly ILogger<MessagingHub> _logger;
    private readonly IMessagesStore _messagesStore;

    public MessagingHub(ILogger<MessagingHub> logger, IMessagesStore messagesStore)
    {
        _logger = logger;
        _messagesStore = messagesStore;
    }

    public override async Task OnConnectedAsync()
    {
        _logger.LogInformation("New client connected");
        await base.OnConnectedAsync();
    }

    public async Task SendMessageToClients(Message message)
    {
        _logger.LogInformation($"Sending message {message} to clients");
        await Clients.All.ReceiveServerMessage(message);
    }

    public async Task SendMessageToServer(Message message)
    {
        _logger.LogInformation($"Obtained message {message.Text} from client {message.ClientId}");
        _messagesStore.StoreMessage(message);

        var messages = _messagesStore.GetMessages();
        await Clients.All.ReceiveClientMessages(messages);
    }
}
