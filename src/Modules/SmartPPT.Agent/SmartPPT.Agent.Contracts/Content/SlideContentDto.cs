using SmartPPT.Agent.Contracts.Enums;

namespace SmartPPT.Agent.Contracts.Content;

public sealed class SlideContentDto
{
    public ContentType ContentType { get; set; }

    public string Value { get; set; } = string.Empty;
}
