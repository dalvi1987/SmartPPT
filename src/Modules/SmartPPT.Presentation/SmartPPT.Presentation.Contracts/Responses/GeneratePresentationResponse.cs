namespace SmartPPT.Presentation.Contracts.Responses;

public sealed class GeneratePresentationResponse
{
    public string PresentationId { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;
}
