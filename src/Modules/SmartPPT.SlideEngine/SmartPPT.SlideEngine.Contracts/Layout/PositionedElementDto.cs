using SmartPPT.SlideEngine.Contracts.Enums;

namespace SmartPPT.SlideEngine.Contracts.Layout;

public sealed class PositionedElementDto
{
    public string Content { get; set; } = string.Empty;

    public ElementType ElementType { get; set; }

    public double Height { get; set; }

    public double Width { get; set; }

    public double X { get; set; }

    public double Y { get; set; }
}
