using System.Collections.Generic;

namespace SmartPPT.Agent.Contracts.Plans;

public sealed class PresentationPlanDto
{
    public List<SlidePlanDto> Slides { get; set; } = [];

    public string Title { get; set; } = string.Empty;
}
