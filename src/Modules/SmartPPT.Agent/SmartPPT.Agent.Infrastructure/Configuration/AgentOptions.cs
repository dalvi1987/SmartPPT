namespace SmartPPT.Agent.Infrastructure.Configuration;

public sealed class AgentOptions
{
    public OpenAiOptions OpenAi { get; set; } = new();

    public OllamaOptions Ollama { get; set; } = new();

    public OpenRouterOptions OpenRouter { get; set; } = new();

    public string Provider { get; set; } = "OpenAi";
}
