namespace LocalApplication.ViewModels;

internal class MainViewModel
{
    public MessagesDashboardViewModel MessagesDashboardViewModel { get; }

    public MainViewModel(MessagesDashboardViewModel messagesDashboardViewModel)
    {
        MessagesDashboardViewModel = messagesDashboardViewModel;
    }
}
