using ExpensesTracker.Domain.Results;

namespace ExpensesTracker.Application.Validators;

public interface IValidator<in TRequest>
{
    Result Validate(TRequest request);
}

public interface IAsyncValidator<in TRequest>
{
    Task<Result> ValidateAsync(TRequest request);
}