using CommunityToolkit.Mvvm.ComponentModel;
using ElectronBot.Braincase.Contracts.ViewModels;

namespace ElectronBot.Copilot.ViewModels;

public class AgentViewModel : ObservableRecipient, INavigationAware
{
    public AgentViewModel()
    {
    }

    public void OnNavigatedFrom() => throw new System.NotImplementedException();
    public void OnNavigatedTo(object parameter) => throw new System.NotImplementedException();
}
