using ExpensesTracker.Domain.Errors;
using ExpensesTracker.Domain.Errors.Base;

namespace ExpensesTracker.Domain.Results;

public class Result
{
    public Error Error { get; }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    protected Result(Error error)
    {
        Error = error;

        if (Error == Error.None)
        {
            IsSuccess = true;
            return;
        }

        IsSuccess = false;
    }

    public static Result Success() => new(Error.None);
    public static Result<TValue> Success<TValue>(TValue value) => new(value, Error.None);
    public static Result Failure(Error error) => new(error);
    public static Result<TValue> Failure<TValue>(Error error) => new(default, error);
    public static Result Create(bool condition) => condition ? Success() : Failure(Error.ConditionNotMet);
    public static Result<TValue> Create<TValue>(TValue value) => value is not null ? Success(value) : Failure<TValue>(Error.Null);
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    public Result(TValue? value, Error error) : base(error)
    {
        _value = value;
    }

    public static implicit operator Result<TValue>(TValue value) => Create(value);
}