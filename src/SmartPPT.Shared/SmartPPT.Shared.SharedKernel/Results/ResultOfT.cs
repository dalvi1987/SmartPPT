namespace SmartPPT.Shared.SharedKernel.Results;

public class Result<T> : Result
{
    internal Result(T? value, bool isSuccess, string? error)
        : base(isSuccess, error)
    {
        Value = value;
    }

    public T? Value { get; }
}
