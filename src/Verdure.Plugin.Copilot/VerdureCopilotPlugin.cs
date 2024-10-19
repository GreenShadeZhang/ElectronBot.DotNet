using BotSharp.Abstraction.Agents;
using Verdure.Plugin.Copilot.Hooks;

namespace Verdure.Plugin.Copilot;

public class VerdureCopilotPlugin : IBotSharpPlugin
{
    public string Id => "dfc60ce4-fce4-40a7-b0e2-f945c95e19dc";
    public string Name => "Verdure Copilot";
    public string Description => "An example of an enterprise-grade AI Chatbot.";
    public string IconUrl => "https://cdn-icons-png.flaticon.com/512/6978/6978255.png";
    public string[] AgentIds => new[]
    {
        "8970b1e5-d260-4e2c-90b1-f1415a257c18",
        "b284db86-e9c2-4c25-a59e-4649797dd130",
        "c2b57a74-ae4e-4c81-b3ad-9ac5bff982bd",
        "dfd9b46d-d00c-40af-8a75-3fbdc2b89869",
        "fe8c60aa-b114-4ef3-93cb-a8efeac80f75"
    };

    public void RegisterDI(IServiceCollection services, IConfiguration config)
    {
        // Register hooks
        services.AddScoped<IAgentHook, PizzaBotAgentHook>();
    }
}
