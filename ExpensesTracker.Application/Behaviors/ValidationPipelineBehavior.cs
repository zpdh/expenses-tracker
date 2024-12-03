using ExpensesTracker.Domain.Errors.Base;
using ExpensesTracker.Domain.Results;
using FluentValidation;
using MediatR;

namespace ExpensesTracker.Application.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var errors = GetErrors(request);

        if (errors.Length != 0)
        {
            return CreateValidationResult<TResponse>(errors);
        }

        return await next();
    }

    private Error[] GetErrors(TRequest request)
    {
        var errors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationResult => validationResult is not null)
            .Select(failure => new Error(failure.ErrorMessage))
            .Distinct()
            .ToArray();

        return errors;
    }

    private static TResult CreateValidationResult<TResult>(IEnumerable<Error> errors) where TResult : Result
    {
        return typeof(TResult) == typeof(Result)
            ? ResultWithoutGenericType<TResult>(errors)
            : ResultWithGenericType<TResult>(errors);
    }

    private static TResult ResultWithoutGenericType<TResult>(IEnumerable<Error> errors) where TResult : Result
    {
        return (ValidationResult.WithErrors(errors) as TResult)!;
    }

    private static TResult ResultWithGenericType<TResult>(IEnumerable<Error> errors) where TResult : Result
    {
        var validationResult = typeof(ValidationResult<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
            .GetMethod(nameof(ValidationResult.WithErrors))!
            .Invoke(null, [errors]);

        return (validationResult as TResult)!;
    }
}