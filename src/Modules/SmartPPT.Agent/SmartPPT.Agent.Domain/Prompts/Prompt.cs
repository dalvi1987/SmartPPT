namespace SmartPPT.Agent.Domain.Prompts;

public sealed class Prompt
{
    public DateTime CreatedAt { get; set; }

    public string Text { get; set; } = string.Empty;
}
