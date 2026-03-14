using System.Collections.Generic;

namespace SmartPPT.Presentation.Contracts.DTOs;

public sealed class SlideDto
{
    public List<string> Elements { get; set; } = [];

    public string PatternId { get; set; } = string.Empty;

    public string SlideId { get; set; } = string.Empty;
}
