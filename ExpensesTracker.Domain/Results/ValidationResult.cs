using ExpensesTracker.Domain.Errors.Base;

namespace ExpensesTracker.Domain.Results;
public class ValidationResult : Result, IValidationResult
{
    public ICollection<Error> Errors { get; }

    private ValidationResult(ICollection<Error> errors) : base(IValidationResult.ValidationError)
    {
        Errors = errors;
    }

    public static ValidationResult WithErrors(ICollection<Error> errors) => new(errors);
}

public class ValidationResult<TValue> : Result<TValue>, IValidationResult
{
    public ICollection<Error> Errors { get; }

    private ValidationResult(ICollection<Error> errors) : base(default, IValidationResult.ValidationError)
    {
        Errors = errors;
    }

    public static ValidationResult<TValue> WithErrors(ICollection<Error> errors) => new(errors);
}