namespace SmartPPT.Agent.Infrastructure.Configuration;

public sealed class OpenAiOptions
{
    public string BaseUrl { get; set; } = "https://api.openai.com/v1";

    public int MaxTokens { get; set; } = 2000;

    public string Model { get; set; } = "gpt-4";

    public double Temperature { get; set; } = 0.7d;
}
