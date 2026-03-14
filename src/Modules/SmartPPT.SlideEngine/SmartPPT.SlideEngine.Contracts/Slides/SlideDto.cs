using System.Collections.Generic;
using SmartPPT.SlideEngine.Contracts.Elements;

namespace SmartPPT.SlideEngine.Contracts.Slides;

public sealed class SlideDto
{
    public List<SlideElementDto> Elements { get; set; } = [];

    public string PatternId { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;
}
