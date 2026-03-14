namespace SmartPPT.Shared.BuildingBlocks.Exceptions;

public sealed class ValidationException : ApplicationExceptionBase
{
    public ValidationException(IReadOnlyCollection<string> errors)
        : base("One or more validation errors occurred.")
    {
        Errors = errors;
    }

    public IReadOnlyCollection<string> Errors { get; }
}
