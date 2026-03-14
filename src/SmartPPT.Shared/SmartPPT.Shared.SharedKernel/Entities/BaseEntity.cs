using SmartPPT.Shared.SharedKernel.Abstractions;

namespace SmartPPT.Shared.SharedKernel.Entities;

public abstract class BaseEntity : IEntity
{
    private readonly List<IDomainEvent> _domainEvents = [];

    protected BaseEntity(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; protected set; }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);

        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
