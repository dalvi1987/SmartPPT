using MediatR;

namespace SmartPPT.Shared.BuildingBlocks.Pipeline;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
