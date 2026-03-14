using SmartPPT.SlideEngine.Domain.Enums;

namespace SmartPPT.SlideEngine.Domain.Layout;

public sealed class PositionedElement
{
    public ElementType ElementType { get; set; }

    public double Height { get; set; }

    public double Width { get; set; }

    public double X { get; set; }

    public double Y { get; set; }
}
