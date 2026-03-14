using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartPPT.Agent.Application.Orchestrators;
using SmartPPT.Agent.Contracts.Services;
using SmartPPT.Agent.Infrastructure.Configuration;
using SmartPPT.Agent.Infrastructure.LLM;
using SmartPPT.Agent.Infrastructure.Prompts;
using SmartPPT.Shared.BuildingBlocks.AI;
using Microsoft.Extensions.Http;
namespace SmartPPT.Agent.Infrastructure.DependencyInjection;

public static class AgentModule
{
    public static IServiceCollection AddAgentModule(this IServiceCollection services, IConfiguration configuration)
    {
        var agentOptions = BuildAgentOptions(configuration);

        services.AddSingleton<IOptions<AgentOptions>>(Options.Create(agentOptions));
        services.AddSingleton(agentOptions);
        services.AddSingleton<PromptTemplates>();
        services.AddHttpClient<OpenAiClient>();
        services.AddHttpClient<OllamaClient>();
        services.AddHttpClient<OpenRouterClient>();
        services.AddScoped<LLMClientFactory>();
        services.AddScoped<IAgentOrchestrator, AgentOrchestrator>();
        services.AddScoped<ILLMClient>(serviceProvider =>
            serviceProvider.GetRequiredService<LLMClientFactory>().Create());

        return services;
    }

    private static AgentOptions BuildAgentOptions(IConfiguration configuration)
    {
        var section = configuration.GetSection("Agent");

        return new AgentOptions
        {
            Provider = section["Provider"] ?? "OpenAi",
            OpenAi = new OpenAiOptions
            {
                BaseUrl = section["OpenAi:BaseUrl"] ?? "https://api.openai.com/v1",
                Model = section["OpenAi:Model"] ?? "gpt-4",
                Temperature = ParseDouble(section["OpenAi:Temperature"], 0.7d),
                MaxTokens = ParseInt(section["OpenAi:MaxTokens"], 2000)
            },
            Ollama = new OllamaOptions
            {
                BaseUrl = section["Ollama:BaseUrl"] ?? "http://localhost:11434",
                Model = section["Ollama:Model"] ?? "llama3",
                Temperature = ParseDouble(section["Ollama:Temperature"], 0.7d),
                MaxTokens = ParseInt(section["Ollama:MaxTokens"], 2000)
            },
            OpenRouter = new OpenRouterOptions
            {
                BaseUrl = section["OpenRouter:BaseUrl"] ?? "https://openrouter.ai/api/v1",
                Model = section["OpenRouter:Model"] ?? "google/gemma-3n-e2b-it:free",
                Temperature = ParseDouble(section["OpenRouter:Temperature"], 0.7d),
                MaxTokens = ParseInt(section["OpenRouter:MaxTokens"], 2000)
            }
        };
    }

    private static double ParseDouble(string? value, double defaultValue)
    {
        return double.TryParse(value, out var result) ? result : defaultValue;
    }

    private static int ParseInt(string? value, int defaultValue)
    {
        return int.TryParse(value, out var result) ? result : defaultValue;
    }
}
