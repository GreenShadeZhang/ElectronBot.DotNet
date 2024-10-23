using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotSharp.Abstraction.Agents.Models;
using BotSharp.Abstraction.Conversations.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ElectronBot.Copilot.ViewModels;
public partial class ChatViewModel
{

    [ObservableProperty]
    List<Agent> _agents = new();

    [ObservableProperty]
    ObservableCollection<Conversation> _conversationList = new();

    [ObservableProperty]
    ObservableCollection<RoleDialogModel> _chatMessageList = new();

    [ObservableProperty]
    Conversation? _selectedConversation;

    [ObservableProperty]
    string? _sendText;


    [ObservableProperty]
    private string _content;

    [ObservableProperty]
    private bool _isUser;

    [ObservableProperty]
    private bool _isAssistant;

    [ObservableProperty]
    private string _time;

    [ObservableProperty]
    private bool _isEditing;

    [ObservableProperty]
    private string _author;

    [ObservableProperty]
    private string _agentId;

    /// <summary>
    /// 请求滚动到底部.
    /// </summary>
    public event EventHandler RequestScrollToBottom;
}
