using Common;
using LocalApplication.Services;
using LocalApplication.ViewModels;
using System;
using System.Windows.Input;

namespace LocalApplication.Commands;

public class SendMessageCommand : ICommand
{
    private readonly MessagesDashboardViewModel _viewModel;
    private SignalRMessagingService _messagingService;

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

        }
    }
}
