using SmartPPT.Agent.Domain.Enums;

namespace SmartPPT.Agent.Domain.Content;

public sealed class SlideContent
{
    public ContentType ContentType { get; set; }

    public string Value { get; set; } = string.Empty;
}
