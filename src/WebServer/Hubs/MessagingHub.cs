using Common;
using Microsoft.AspNetCore.SignalR;
using WebServer.Hubs.Abstractions;

namespace WebServer.Hubs
{
    public class MessagingHub : Hub<IMessagingHub>
    {
        private readonly ILogger<MessagingHub> _logger;
        private Dictionary<string, string> _clientMessages = new();
        private readonly object _object = new object();

        public MessagingHub(ILogger<MessagingHub> logger)
        {
            _logger = logger;
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

            await Clients.All.ReceiveClientMessages(_clientMessages);
        }
    }
}
