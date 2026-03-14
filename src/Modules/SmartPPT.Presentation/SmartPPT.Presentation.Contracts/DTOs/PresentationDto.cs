namespace SmartPPT.Presentation.Contracts.DTOs;

public sealed class PresentationDto
{
    public DateTime CreatedAt { get; set; }

    public string Id { get; set; } = string.Empty;

    public int SlideCount { get; set; }

    public string Status { get; set; } = string.Empty;

    public string TemplateId { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;
}
