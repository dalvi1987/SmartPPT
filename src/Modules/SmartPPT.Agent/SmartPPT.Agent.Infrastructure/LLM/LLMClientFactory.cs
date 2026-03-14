using Microsoft.Extensions.Options;
using SmartPPT.Agent.Infrastructure.Configuration;
using SmartPPT.Shared.BuildingBlocks.AI;

namespace SmartPPT.Agent.Infrastructure.LLM;

public sealed class LLMClientFactory
{
    private readonly IOptions<AgentOptions> _options;
    private readonly OpenAiClient _openAiClient;
    private readonly OllamaClient _ollamaClient;
    private readonly OpenRouterClient _openRouterClient;

    public LLMClientFactory(
        IOptions<AgentOptions> options,
        OpenAiClient openAiClient,
        OllamaClient ollamaClient,
        OpenRouterClient openRouterClient)
    {
        _options = options;
        _openAiClient = openAiClient;
        _ollamaClient = ollamaClient;
        _openRouterClient = openRouterClient;
    }

    public ILLMClient Create()
    {
        return _options.Value.Provider?.Trim().ToLowerInvariant() switch
        {
            "ollama" => _ollamaClient,
            "openrouter" => _openRouterClient,
            _ => _openAiClient
        };
    }
}
