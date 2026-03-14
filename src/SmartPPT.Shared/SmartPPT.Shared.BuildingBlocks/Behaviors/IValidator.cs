namespace SmartPPT.Shared.BuildingBlocks.Behaviors;

public interface IValidator<in TRequest>
{
    Task<IReadOnlyCollection<string>> ValidateAsync(TRequest request, CancellationToken cancellationToken = default);
}
