using ExpensesTracker.Domain.Errors.Base;

namespace ExpensesTracker.Domain.Results;
public class ValidationResult : Result, IValidationResult
{
    public IEnumerable<Error> Errors { get; }

    private ValidationResult(IEnumerable<Error> errors) : base(IValidationResult.ValidationError)
    {
        Errors = errors;
    }

    public static ValidationResult WithErrors(IEnumerable<Error> errors) => new(errors);
}

public class ValidationResult<TValue> : Result<TValue>, IValidationResult
{
    public IEnumerable<Error> Errors { get; }

    private ValidationResult(IEnumerable<Error> errors) : base(default, IValidationResult.ValidationError)
    {
        Errors = errors;
    }

    public static ValidationResult<TValue> WithErrors(IEnumerable<Error> errors) => new(errors);
}