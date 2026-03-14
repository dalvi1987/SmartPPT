namespace SmartPPT.Agent.Infrastructure.Configuration;

public sealed class OpenRouterOptions
{
    public string BaseUrl { get; set; } = "https://openrouter.ai/api/v1";

    public int MaxTokens { get; set; } = 2000;

    public string Model { get; set; } = "google/gemma-3n-e2b-it:free";

    public double Temperature { get; set; } = 0.7d;
}
