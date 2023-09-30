using Common;
using LocalApplication.Services;
using LocalApplication.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Windows;

namespace LocalApplication;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override async void OnStartup(StartupEventArgs e)
    {
        StartupArgumentsService startupArgumentsService = new();
        var startupArguments = startupArgumentsService.ParseStartupArguments(e.Args);
        startupArguments.TryGetValue("ClientId", out string startupClientId);

        if (startupClientId == null)
        {
            throw new ArgumentNullException($"{nameof(startupClientId)} cannot be null");
        }

        HubConnection connection = HubConnectionFactory.CreateHubConnection();

        SignalRMessagingService messagingService = new SignalRMessagingService(connection);

        try
        {
            await messagingService.Connect();
        }
        catch (Exception)
        {
            throw new ApplicationException("Couldn't connect to signalR hub");
        }

        var messagesDashboardViewModel = new MessagesDashboardViewModel(messagingService, startupClientId);

        MainWindow window = new MainWindow
        {
            DataContext = new MainViewModel(messagesDashboardViewModel)
        };

        window.Show();
    }
}
