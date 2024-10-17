using System.Collections.Generic;
using System.Linq;
using BotSharp.Abstraction.Agents;
using BotSharp.Abstraction.Agents.Models;
using BotSharp.Abstraction.Conversations;
using BotSharp.Abstraction.Conversations.Models;
using BotSharp.Abstraction.Repositories.Filters;
using BotSharp.Abstraction.Users;
using BotSharp.Abstraction.Users.Enums;
using BotSharp.Abstraction.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using ElectronBot.Braincase.Contracts.ViewModels;

namespace ElectronBot.Copilot.ViewModels;

public partial class ChatViewModel : ObservableRecipient, INavigationAware
{
    private readonly IConversationService _conversationService;
    private readonly IUserIdentity _userIdentity;
    private readonly IUserService _userService;
    public ChatViewModel(IConversationService conversationService, IUserIdentity userIdentity, IUserService userService)
    {
        _conversationService = conversationService;
        _userIdentity = userIdentity;
        _userService = userService;
    }

    [ObservableProperty]
    List<Agent> _agents = new();

    [ObservableProperty]
    List<Conversation> _conversationList = new();

    public void OnNavigatedFrom()
    {

    }
    public async void OnNavigatedTo(object parameter)
    {
        var user = await _userService.GetUser(_userIdentity.Id);

        if (user == null)
        {
            await _userService.CreateUser(new BotSharp.Abstraction.Users.Models.User
            {
                Id = _userIdentity.Id,
                Email = _userIdentity.Email,
                UserName = _userIdentity.UserName,
                FirstName = _userIdentity.FirstName,
                LastName = _userIdentity.LastName,
                Role = UserRole.Admin,
                Type = UserType.Client,
            });
        }

        var agentService = Ioc.Default.GetRequiredService<IAgentService>();

        Agents = (await agentService.GetAgents(new AgentFilter
        {
            Pager = new Pagination
            {
                Page = 1,
                Size = 10
            }
        })).Items.ToList();

        ConversationList = (await _conversationService.GetConversations(new ConversationFilter
        {
            Pager = new Pagination
            {
                Page = 1,
                Size = 10
            }
        })).Items.ToList();
    }
}
