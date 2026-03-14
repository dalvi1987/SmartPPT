using System.Collections.Generic;

namespace SmartPPT.Agent.Contracts.Prompts;

public sealed class PromptDto
{
    public List<string> DataSources { get; set; } = [];

    public string TemplateId { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;
}
