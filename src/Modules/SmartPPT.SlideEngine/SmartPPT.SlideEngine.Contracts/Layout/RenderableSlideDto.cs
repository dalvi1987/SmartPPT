using System.Collections.Generic;

namespace SmartPPT.SlideEngine.Contracts.Layout;

public sealed class RenderableSlideDto
{
    public List<PositionedElementDto> PositionedElements { get; set; } = [];

    public string Title { get; set; } = string.Empty;
}
