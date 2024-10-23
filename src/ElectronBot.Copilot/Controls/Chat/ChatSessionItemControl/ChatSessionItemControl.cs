// Copyright (c) Rodel. All rights reserved.

using BotSharp.Abstraction.Conversations.Models;
using ElectronBot.Copilot.Controls.Chat;
using ElectronBot.Copilot.ViewModels;

namespace ElectronBot.Copilot.Controls;

/// <summary>
/// 聊天会话项控件.
/// </summary>
public sealed class ChatSessionItemControl : UserControl
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ChatSessionItemControl"/> class.
    /// </summary>
    public ChatSessionItemControl() => DefaultStyleKey = typeof(ChatSessionItemControl);

    public Conversation ViewModel => (Conversation)DataContext;
    /// <summary>
    /// 条目被点击.
    /// </summary>
    public event EventHandler<Conversation?> Click;

    /// <inheritdoc/>
    protected override void OnApplyTemplate()
    {
        var rootCard = GetTemplateChild("RootCard") as CardPanel;
        if (rootCard != null)
        {
            var conv = DataContext as Conversation;
            rootCard.Click += (sender, e) => Click?.Invoke(this, conv);
        }
    }
}
