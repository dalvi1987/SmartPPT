namespace SmartPPT.Storage.Contracts.Responses;

public sealed class GenerateDocumentResponse
{
    public string DocumentId { get; set; } = string.Empty;

    public string FilePath { get; set; } = string.Empty;
}
