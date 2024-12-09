using ExpensesTracker.Domain.Errors.Base;

namespace ExpensesTracker.Domain.Results;
public class ValidationResult : Result, IValidationResult
{
    public Error[] Errors { get; }

    private ValidationResult(Error[] errors) : base(IValidationResult.ValidationError)
    {
        Errors = errors;
    }

    public static ValidationResult WithErrors(Error[] errors) => new(errors);
}

public class ValidationResult<TValue> : Result<TValue>, IValidationResult
{
    public Error[] Errors { get; }

    private ValidationResult(Error[] errors) : base(default, IValidationResult.ValidationError)
    {
        Errors = errors;
    }

    public static ValidationResult<TValue> WithErrors(Error[] errors) => new(errors);
}