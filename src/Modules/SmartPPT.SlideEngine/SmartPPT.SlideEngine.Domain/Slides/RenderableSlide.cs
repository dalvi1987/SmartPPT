using System.Collections.Generic;
using SmartPPT.SlideEngine.Domain.Layout;

namespace SmartPPT.SlideEngine.Domain.Slides;

public sealed class RenderableSlide
{
    public List<PositionedElement> PositionedElements { get; set; } = [];

    public string Title { get; set; } = string.Empty;
}
