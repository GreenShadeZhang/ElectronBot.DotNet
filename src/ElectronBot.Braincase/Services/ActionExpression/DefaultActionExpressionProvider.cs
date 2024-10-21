using ElectronBot.Braincase.Contracts.Services;
using ElectronBot.Braincase.Models;
using Verdure.WinUI.Common.Models;

namespace ElectronBot.Braincase.Services;
public class DefaultActionExpressionProvider : IActionExpressionProvider
{
    public string Name => "Default";

    public async Task PlayActionExpressionAsync(string actionName)
    {

    }

    public async Task PlayActionExpressionAsync(string actionName, List<ElectronBotAction> actions)
    {

    }

    public async Task PlayActionExpressionAsync(EmoticonAction emoticonAction, List<ElectronBotAction> actions)
    {

    }

    public async Task PlayActionExpressionAsync(EmoticonAction emoticonAction)
    {

    }
}
