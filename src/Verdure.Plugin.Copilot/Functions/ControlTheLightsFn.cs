using BotSharp.Abstraction.Conversations.Models;
using System.Text.Json;

namespace Verdure.Plugin.Copilot.Functions;

public class ControlTheLightsFn : IFunctionCallback
{
    public string Name => "control_the_lights";

    public async Task<bool> Execute(RoleDialogModel message)
    {
        message.Data = new
        {
            pepperoni_unit_price = 3.2,
            cheese_unit_price = 3.5,
            margherita_unit_price = 3.8,
        };
        message.Content = JsonSerializer.Serialize(message.Data);
        return true;
    }
}
