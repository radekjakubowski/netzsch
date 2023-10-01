using Common;
using LocalApplication.Commands;
using LocalApplication.Services;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LocalApplication.ViewModels;

public class MessagesDashboardViewModel : ViewModelBase
{
    private string _clientId = "No connection made";
    private string _connectionId = "No connection made";
    private string _clientMessage = string.Empty;
    private string _serverMessage = string.Empty;

    public string ClientMessage
    {
        get { return _clientMessage; }
        set 
        {
            SetProperty(ref _clientMessage, value);
            Task.Run(DispatchClientMessage);
        }
    }

    private void DispatchClientMessage()
    {
        SendMessageCommand?.Execute(this);
    }

    public string ServerMessage
    {
        get { return _serverMessage; }
        set { SetProperty(ref _serverMessage, value); }
    }

    public string ClientId
    {
        get { return _clientId; }
        set { SetProperty(ref _clientId, value); }
    }
    public string ConnectionId
    {
        get { return _connectionId; }
        set { SetProperty(ref _connectionId, value); }
    }

    public ICommand? SendMessageCommand { get; }

    public MessagesDashboardViewModel(SignalRMessagingService messagingService, string clientId)
    {
        ConnectionId = messagingService.ConnectionId;
        ClientId = clientId;
        SendMessageCommand = new SendMessageCommand(this, messagingService);
        RegisterEventHandlers(messagingService);
    }

    private void RegisterEventHandlers(SignalRMessagingService messagingService)
    {
        messagingService.MessageReceived += MessagingService_MessageReceived;
    }

    private void MessagingService_MessageReceived(Message message)
    {
        ServerMessage = message.Text;
    }
}
