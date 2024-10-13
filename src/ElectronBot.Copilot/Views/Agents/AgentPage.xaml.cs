using CommunityToolkit.Mvvm.DependencyInjection;
using ElectronBot.Copilot.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace ElectronBot.Braincase.Views;

public sealed partial class AgentPage : Page
{
    public AgentPage()
    {
        InitializeComponent();
        DataContext = Ioc.Default.GetRequiredService<AgentViewModel>();
    }

    public AgentViewModel ViewModel => (AgentViewModel)DataContext;
}
