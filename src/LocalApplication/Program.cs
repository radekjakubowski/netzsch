using Common;
using LocalApplication.Services;
using LocalApplication.Services.Abstractions;
using LocalApplication.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace LocalApplication;

public class Program
{
    [STAThread]
    public static void Main()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<App>();
                services.AddSingleton<MainWindow>();
                services.AddTransient(sp =>
                {
                    var hubConnection = HubConnectionFactory.CreateHubConnection();
                    hubConnection.StartAsync().Wait();
                    return hubConnection;
                });

                services.AddTransient<IStartupArgumentsService, StartupArgumentsService>();
                services.AddTransient<SignalRMessagingService>();

                services.AddTransient(sp => 
                {
                    var messagingService = sp.GetRequiredService<SignalRMessagingService>();
                    var startupArgsService = sp.GetRequiredService<IStartupArgumentsService>();
                    var startupArgs = startupArgsService.ParseStartupArguments(Environment.GetCommandLineArgs());
                    var clientId = startupArgs.TryGetValue("ClientId", out string startupClientId);

                    if (string.IsNullOrWhiteSpace(startupClientId))
                    {
                        throw new ApplicationException("Startup client id argument is required to start the application");
                    }

                    return new MessagesDashboardViewModel(messagingService, startupClientId);
                });
            })
            .Build();

        var app = host.Services.GetRequiredService<App>();
        app.Run();
    }
}
