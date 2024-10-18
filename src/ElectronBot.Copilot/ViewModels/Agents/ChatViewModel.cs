using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSharp.Abstraction.Agents;
using BotSharp.Abstraction.Agents.Enums;
using BotSharp.Abstraction.Agents.Models;
using BotSharp.Abstraction.Conversations;
using BotSharp.Abstraction.Conversations.Models;
using BotSharp.Abstraction.Models;
using BotSharp.Abstraction.Repositories.Filters;
using BotSharp.Abstraction.Routing;
using BotSharp.Abstraction.Users;
using BotSharp.Abstraction.Users.Enums;
using BotSharp.Abstraction.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using ElectronBot.Braincase.Contracts.ViewModels;
using Microsoft.Extensions.DependencyInjection;


namespace ElectronBot.Copilot.ViewModels;

public partial class ChatViewModel : ObservableRecipient, INavigationAware
{
    private readonly IConversationService _conversationService;
    private readonly IUserIdentity _userIdentity;
    private readonly IUserService _userService;
    private readonly IServiceProvider _services;
    public ChatViewModel(IConversationService conversationService, IUserIdentity userIdentity, IUserService userService, IServiceProvider services)
    {
        _conversationService = conversationService;
        _userIdentity = userIdentity;
        _userService = userService;
        _services = services;
    }

    [ObservableProperty]
    List<Agent> _agents = new();

    [ObservableProperty]
    List<Conversation> _conversationList = new();

    [ObservableProperty]
    List<RoleDialogModel> _chatMessageList = new();

    [ObservableProperty]
    Conversation? _selectedConversation;

    [ObservableProperty]
    string? _sendText;

    [RelayCommand]
    public Task ConvSelectAsync(Conversation? conv)
    {
        if (conv == null) return Task.CompletedTask;
        _conversationService.SetConversationId(conv.Id, new List<MessageState>());
        var history = _conversationService.GetDialogHistory(fromBreakpoint: false);
        ChatMessageList = history;
        return Task.CompletedTask;
    }

    [RelayCommand]
    public async Task SendChatAsync()
    {
        if (SelectedConversation == null) return;

        if (string.IsNullOrEmpty(SendText)) return;

        var inputMsg = new RoleDialogModel(AgentRole.User, SendText)
        {
            MessageId = Guid.NewGuid().ToString(),
            CreatedAt = DateTime.UtcNow
        };

        var routing = _services.GetRequiredService<IRoutingService>();
        routing.Context.SetMessageId(SelectedConversation.Id, inputMsg.MessageId);

        _conversationService.SetConversationId(SelectedConversation.Id, new());

        await _conversationService.SendMessage(SelectedConversation.AgentId, inputMsg,
            replyMessage: null,
            async msg =>
            {
                ChatMessageList.Add(msg);
            });
    }

    [RelayCommand]
    public Task StartChatAsync(string? sendText)
    {
        if (string.IsNullOrEmpty(sendText)) return Task.CompletedTask;
        return Task.CompletedTask;
    }


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
