using SmartPPT.SlideEngine.Contracts.Enums;

namespace SmartPPT.SlideEngine.Contracts.Elements;

public sealed class SlideElementDto
{
    public string Content { get; set; } = string.Empty;

    public ElementType ElementType { get; set; }

    public string SlotName { get; set; } = string.Empty;
}
