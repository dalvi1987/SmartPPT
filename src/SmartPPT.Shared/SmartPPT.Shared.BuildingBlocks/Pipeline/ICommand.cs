using MediatR;

namespace SmartPPT.Shared.BuildingBlocks.Pipeline;

public interface ICommand : IRequest
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
