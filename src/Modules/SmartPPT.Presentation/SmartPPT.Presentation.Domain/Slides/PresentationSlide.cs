using System.Collections.Generic;

namespace SmartPPT.Presentation.Domain.Slides;

public sealed class PresentationSlide
{
    public List<string> Elements { get; set; } = [];

    public string PatternId { get; set; } = string.Empty;

    public string SlideId { get; set; } = string.Empty;
}
