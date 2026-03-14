namespace SmartPPT.Shared.SharedKernel.Abstractions;

public interface IDomainEvent
{
    Guid EventId { get; }

    DateTime OccurredOnUtc { get; }
}
