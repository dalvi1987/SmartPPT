namespace SmartPPT.Presentation.Domain.Metadata;

public sealed class PresentationMetadata
{
    public DateTime CreatedAt { get; set; }

    public string TemplateId { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;
}
