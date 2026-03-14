using System.Collections.Generic;
using SmartPPT.Agent.Domain.Content;

namespace SmartPPT.Agent.Domain.Plans;

public sealed class SlidePlan
{
    public List<SlideContent> Contents { get; set; } = [];

    public string PatternId { get; set; } = string.Empty;
}
