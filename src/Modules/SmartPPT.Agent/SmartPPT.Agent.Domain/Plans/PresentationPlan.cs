using System.Collections.Generic;

namespace SmartPPT.Agent.Domain.Plans;

public sealed class PresentationPlan
{
    public List<SlidePlan> Slides { get; set; } = [];

    public string Title { get; set; } = string.Empty;
}
