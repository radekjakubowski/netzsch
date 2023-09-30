using Common;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace LocalApplication.Services
{
    public class SignalRMessagingService
    {
        private readonly HubConnection _connection;
        public event Action<Message> MessageReceived;

        public string ConnectionId { get; set; }

        public SignalRMessagingService(HubConnection connection)
        {
            _connection = connection;
            RegisterEventHandlers();
        }

        private void RegisterEventHandlers()
        {
            _connection.On<Message>("ReceiveServerMessage", (message) => MessageReceived?.Invoke(message));
        }

        public async Task Connect()
        {
            await _connection.StartAsync();
            ConnectionId = _connection.ConnectionId;
        }

        public async Task SendMessageToServer(Message message)
        {
            await _connection.SendAsync("SendMessageToServer", message);
        }
    }
}
