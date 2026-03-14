using MediatR;
using Microsoft.Extensions.Logging;

namespace SmartPPT.Shared.BuildingBlocks.Behaviors;

public sealed class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        _logger.LogInformation("Handling request {RequestName} with payload {@Request}", requestName, request);

        try
        {
            var response = await next();

            _logger.LogInformation("Handled request {RequestName} with response {@Response}", requestName, response);

            return response;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unhandled exception while processing request {RequestName}", requestName);
            throw;
        }
    }
}
