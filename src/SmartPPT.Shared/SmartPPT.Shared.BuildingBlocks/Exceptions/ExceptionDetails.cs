namespace SmartPPT.Shared.BuildingBlocks.Exceptions;

public sealed class ExceptionDetails
{
    public int StatusCode { get; init; }

    public string Title { get; init; } = string.Empty;

    public string Detail { get; init; } = string.Empty;

    public IReadOnlyCollection<string> Errors { get; init; } = Array.Empty<string>();
}
