using System.Collections.Generic;
using SmartPPT.Presentation.Domain.Enums;
using SmartPPT.Presentation.Domain.Slides;

namespace SmartPPT.Presentation.Domain.Presentations;

public sealed class Presentation
{
    public DateTime CreatedAt { get; set; }

    public Guid Id { get; set; }

    public List<PresentationSlide> Slides { get; set; } = [];

    public PresentationStatus Status { get; set; }

    public string TemplateId { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;
}
