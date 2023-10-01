using Common;
using LocalApplication.Services;
using LocalApplication.Services.Abstractions;
using LocalApplication.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Windows;

namespace LocalApplication;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IServiceProvider _sp;
    private readonly ILogger<App> _logger;

    public App(ILogger<App> logger, IServiceProvider sp)
    {
        _logger = logger;
        _sp = sp;
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        var messagesDashboardViewModel = _sp.GetRequiredService<MessagesDashboardViewModel>();

        MainWindow window = new MainWindow
        {
            DataContext = new MainViewModel(messagesDashboardViewModel)
        };

        _logger.LogInformation("Showing client app window");
        window.Show();
    }
}
