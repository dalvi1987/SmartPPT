using System.Collections.Generic;
using SmartPPT.SlideEngine.Domain.Elements;

namespace SmartPPT.SlideEngine.Domain.Slides;

public sealed class SlideModel
{
    public List<SlideElement> Elements { get; set; } = [];

    public string PatternId { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;
}
