using System.Collections.Generic;

namespace SmartPPT.Presentation.Contracts.Requests;

public sealed class GeneratePresentationRequest
{
    public List<string> DataSources { get; set; } = [];

    public string Prompt { get; set; } = string.Empty;

    public string TemplateId { get; set; } = string.Empty;
}
