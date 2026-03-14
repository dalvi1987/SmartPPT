using System.Collections.Generic;
using SmartPPT.Agent.Domain.Prompts;

namespace SmartPPT.Agent.Domain.Context;

public sealed class AgentContext
{
    public List<string> DataSources { get; set; } = [];

    public Prompt Prompt { get; set; } = new();

    public string TemplateId { get; set; } = string.Empty;
}
