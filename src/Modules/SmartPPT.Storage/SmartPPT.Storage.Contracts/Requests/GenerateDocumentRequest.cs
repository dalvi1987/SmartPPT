using System.Collections.Generic;

namespace SmartPPT.Storage.Contracts.Requests;

public sealed class GenerateDocumentRequest
{
    public string Format { get; set; } = string.Empty;

    public string PresentationId { get; set; } = string.Empty;

    public List<string> Slides { get; set; } = [];
}
