using Microsoft.AspNetCore.SignalR.Client;

namespace Common
{
    public static class HubConnectionFactory
    {
        public static HubConnection CreateHubConnection()
        {
            return new HubConnectionBuilder()
                .WithUrl($"{WebServerConstants.WebServerUrl}/messaging-hub")
                .WithAutomaticReconnect()
                .Build();
        }
    }
}
