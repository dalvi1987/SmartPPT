using Microsoft.Extensions.DependencyInjection;
using SmartPPT.Agent.Application.Orchestrators;
using SmartPPT.Agent.Contracts.Services;
using SmartPPT.Agent.Infrastructure.Configuration;
using SmartPPT.Agent.Infrastructure.LLM;
using SmartPPT.Agent.Infrastructure.Prompts;
using SmartPPT.Shared.BuildingBlocks.AI;

namespace SmartPPT.Agent.Infrastructure.DependencyInjection;

public static class AgentModule
{
    public static IServiceCollection AddAgentModule(this IServiceCollection services)
    {
        services.AddSingleton(new AgentOptions());
        services.AddSingleton<PromptTemplates>();
        services.AddScoped<IAgentOrchestrator, AgentOrchestrator>();
        services.AddScoped<ILLMClient, OpenAiClient>();

        return services;
    }
}
