using ExpensesTracker.Domain.Errors.Base;

namespace ExpensesTracker.Domain.Results;

public class ValidationResult : Result, IValidationResult
{

    private ValidationResult(Error[] errors) : base(IValidationResult.ValidationError)
    {
        Errors = errors;
    }

    public Error[] Errors { get; }

    public static ValidationResult WithErrors(Error[] errors)
    {
        return new ValidationResult(errors);
    }
}

public class ValidationResult<TValue> : Result<TValue>, IValidationResult
{

    private ValidationResult(Error[] errors) : base(default, IValidationResult.ValidationError)
    {
        Errors = errors;
    }

    public Error[] Errors { get; }

    public static ValidationResult<TValue> WithErrors(Error[] errors)
    {
        return new ValidationResult<TValue>(errors);
    }
}