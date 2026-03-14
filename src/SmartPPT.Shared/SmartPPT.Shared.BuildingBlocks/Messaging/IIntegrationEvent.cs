namespace SmartPPT.Shared.BuildingBlocks.Messaging;

public interface IIntegrationEvent
{
    Guid EventId { get; }

    DateTime OccurredOnUtc { get; }
}
