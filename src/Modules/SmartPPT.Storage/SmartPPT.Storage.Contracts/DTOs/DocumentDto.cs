namespace SmartPPT.Storage.Contracts.DTOs;

public sealed class DocumentDto
{
    public DateTime CreatedAt { get; set; }

    public string FilePath { get; set; } = string.Empty;

    public string Format { get; set; } = string.Empty;

    public string Id { get; set; } = string.Empty;

    public string PresentationId { get; set; } = string.Empty;
}
