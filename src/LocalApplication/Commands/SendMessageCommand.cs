using Common;
using LocalApplication.Services;
using LocalApplication.Services.Abstractions;
using LocalApplication.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Windows.Input;

namespace LocalApplication.Commands;

public class SendMessageCommand : ICommand
{
    private readonly MessagesDashboardViewModel _viewModel;
    private readonly SignalRMessagingService _messagingService;
    private readonly ILogger<SendMessageCommand> _logger;
    

    public SendMessageCommand(MessagesDashboardViewModel viewModel, SignalRMessagingService messagingService)
    {
        _viewModel = viewModel;
        _messagingService = messagingService;
    }

    public event EventHandler? CanExecuteChanged = null;

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public async void Execute(object? parameter)
    {
        try
        {
            var message = new Message() { ClientId = _viewModel.ClientId, Text = _viewModel.ClientMessage };
            await _messagingService.SendMessageToServer(message);
        } 
        catch (Exception)
        {
            _logger.LogError("Error while sending message to server");
        }
    }
}
