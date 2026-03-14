namespace SmartPPT.Agent.Infrastructure.Configuration;

public sealed class OllamaOptions
{
    public string BaseUrl { get; set; } = "http://localhost:11434";

    public int MaxTokens { get; set; } = 2000;

    public string Model { get; set; } = "llama3";

    public double Temperature { get; set; } = 0.7d;
}
