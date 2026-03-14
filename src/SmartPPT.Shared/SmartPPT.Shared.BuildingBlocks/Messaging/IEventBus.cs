namespace SmartPPT.Shared.BuildingBlocks.Messaging;

public interface IEventBus
{
    Task PublishAsync(IIntegrationEvent integrationEvent, CancellationToken cancellationToken = default);
}
