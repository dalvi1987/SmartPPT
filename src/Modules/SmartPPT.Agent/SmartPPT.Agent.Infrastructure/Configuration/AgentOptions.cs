namespace SmartPPT.Agent.Infrastructure.Configuration;

public sealed class AgentOptions
{
    public string ApiKey { get; set; } = string.Empty;

    public int MaxTokens { get; set; }

    public string Model { get; set; } = "gpt-4.1-mini";

    public double Temperature { get; set; } = 0.2d;
}
