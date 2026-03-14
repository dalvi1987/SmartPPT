using System.Collections.Generic;
using SmartPPT.Agent.Contracts.Content;

namespace SmartPPT.Agent.Contracts.Plans;

public sealed class SlidePlanDto
{
    public List<SlideContentDto> Contents { get; set; } = [];

    public string PatternId { get; set; } = string.Empty;
}
