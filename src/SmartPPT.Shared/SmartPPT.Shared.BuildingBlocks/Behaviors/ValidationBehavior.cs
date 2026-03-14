using MediatR;
using SmartPPT.Shared.BuildingBlocks.Exceptions;

namespace SmartPPT.Shared.BuildingBlocks.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var errors = new List<string>();

        foreach (var validator in _validators)
        {
            var validationErrors = await validator.ValidateAsync(request, cancellationToken);

            if (validationErrors.Count > 0)
            {
                errors.AddRange(validationErrors.Where(error => !string.IsNullOrWhiteSpace(error)));
            }
        }

        if (errors.Count > 0)
        {
            throw new ValidationException(errors);
        }

        return await next();
    }
}
