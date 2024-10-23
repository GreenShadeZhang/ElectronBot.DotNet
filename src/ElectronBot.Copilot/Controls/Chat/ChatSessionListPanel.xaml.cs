// Copyright (c) Rodel. All rights reserved.

namespace ElectronBot.Copilot.Controls.Chat;

/// <summary>
/// 聊天会话列表面板.
/// </summary>
public sealed partial class ChatSessionListPanel : ChatModuleControl
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ChatSessionListPanel"/> class.
    /// </summary>
    public ChatSessionListPanel() => InitializeComponent();

    private async void OnRenameItemClickAsync(object sender, RoutedEventArgs e)
    {
        //var context = (sender as FrameworkElement)?.DataContext as ChatSessionViewModel;
        //var dialog = new SessionRenameDialog(context);
        //await dialog.ShowAsync();
    }

    private void OnDeleteItemClick(object sender, RoutedEventArgs e)
    {
        //var context = (sender as FrameworkElement)?.DataContext as ChatSessionViewModel;
        //ViewModel.RemoveSessionCommand.Execute(context);
    }

    private void OnItemClick(object sender, BotSharp.Abstraction.Conversations.Models.Conversation e)
    {
        ViewModel.ConvSelectCommand.Execute(e);
    }
}
