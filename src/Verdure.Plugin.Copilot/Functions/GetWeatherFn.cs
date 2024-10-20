using BotSharp.Abstraction.Conversations.Models;

namespace Verdure.Plugin.Copilot.Functions;

public class GetWeatherFn : IFunctionCallback
{
    public string Name => "get_weather";

    public async Task<bool> Execute(RoleDialogModel message)
    {
        message.Content = "ready to deliver, will arrived in about 15 minutes.";
        message.Data = new
        {
            Status = "Ready to deliver",
            EstimatedTime = "15 minuts"
        };
        return true;
    }
}
