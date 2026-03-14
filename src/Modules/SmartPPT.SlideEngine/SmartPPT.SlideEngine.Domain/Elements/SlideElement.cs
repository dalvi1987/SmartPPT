using SmartPPT.SlideEngine.Domain.Enums;

namespace SmartPPT.SlideEngine.Domain.Elements;

public sealed class SlideElement
{
    public string Content { get; set; } = string.Empty;

    public ElementType ElementType { get; set; }

    public string SlotName { get; set; } = string.Empty;
}
