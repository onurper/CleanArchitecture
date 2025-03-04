using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanArchitecture.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var errorDictionary = validators.Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .GroupBy(x => x.PropertyName, s => s.ErrorMessage, (propertyName, errorMessage) => new
            {
                Key = propertyName,
                Values = errorMessage.Distinct().ToList()
            }).ToDictionary(s => s.Key, s => s.Values[0]);

        if (!errorDictionary.Any()) return await next();

        var errors = errorDictionary
            .Select(x => new ValidationFailure
            {
                PropertyName = x.Value,
                ErrorCode = x.Key
            }).ToList();

        throw new ValidationException(errors);
    }
}