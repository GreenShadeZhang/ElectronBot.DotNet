using BotSharp.Abstraction.Conversations;
using BotSharp.Abstraction.Conversations.Models;

namespace Verdure.Plugin.Copilot.Functions;

public class LearnWordsFn : IFunctionCallback
{
    public string Name => "learn_words";

    private readonly IServiceProvider _service;
    public LearnWordsFn(IServiceProvider service)
    {
        _service = service;
    }

    public async Task<bool> Execute(RoleDialogModel message)
    {
        message.Content = "The order number is P123-01";
        var state = _service.GetRequiredService<IConversationStateService>();
        state.SetState("order_number", "P123-01");

        return true;
    }
}
