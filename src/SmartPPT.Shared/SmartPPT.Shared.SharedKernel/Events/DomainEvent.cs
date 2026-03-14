using SmartPPT.Shared.SharedKernel.Abstractions;

namespace SmartPPT.Shared.SharedKernel.Events;

public abstract class DomainEvent : IDomainEvent
{
    protected DomainEvent()
    {
        EventId = Guid.NewGuid();
        OccurredOnUtc = DateTime.UtcNow;
    }

    public Guid EventId { get; }

    public DateTime OccurredOnUtc { get; }
}
