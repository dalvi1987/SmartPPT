using SmartPPT.Storage.Domain.Enums;

namespace SmartPPT.Storage.Domain.Documents;

public sealed class Document
{
    public DateTime CreatedAt { get; set; }

    public string FilePath { get; set; } = string.Empty;

    public ExportFormat Format { get; set; }

    public Guid Id { get; set; }

    public string PresentationId { get; set; } = string.Empty;
}
