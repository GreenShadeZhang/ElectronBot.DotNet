using BotSharp.Abstraction.Agents.Enums;
using BotSharp.Abstraction.Conversations;
using CommunityToolkit.Mvvm.ComponentModel;
using ElectronBot.Braincase.Contracts.ViewModels;

namespace ElectronBot.Copilot.ViewModels;

public class AgentViewModel : ObservableRecipient, INavigationAware
{
    private readonly IConversationService _conversationService;
    public AgentViewModel(IConversationService conversationService)
    {
        _conversationService = conversationService;
    }

    public void OnNavigatedFrom() => throw new System.NotImplementedException();
    public async void OnNavigatedTo(object parameter)
    {
        var result = await _conversationService.NewConversation(new BotSharp.Abstraction.Conversations.Models.Conversation
        {
            AgentId = BuiltInAgentId.Chatbot
        });
    }
}
