using System.Collections.Generic;
using SmartPPT.Presentation.Contracts.DTOs;

namespace SmartPPT.Presentation.Contracts.Responses;

public sealed class PresentationResponse
{
    public DateTime CreatedAt { get; set; }

    public string PresentationId { get; set; } = string.Empty;

    public List<SlideDto> Slides { get; set; } = [];

    public string Status { get; set; } = string.Empty;

    public string TemplateId { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;
}
