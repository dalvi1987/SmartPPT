using System.Collections.Generic;

namespace SmartPPT.SlideEngine.Domain.Layout;

public sealed class LayoutResult
{
    public List<PositionedElement> PositionedElements { get; set; } = [];
}
