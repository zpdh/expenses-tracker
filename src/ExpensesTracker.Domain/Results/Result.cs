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

    public static Result Success()
    {
        return new Result(Error.None);
    }

    public static Result<TValue> Success<TValue>(TValue value)
    {
        return new Result<TValue>(value, Error.None);
    }

    public static Result Failure(Error error)
    {
        return new Result(error);
    }

    public static Result<TValue> Failure<TValue>(Error error)
    {
        return new Result<TValue>(default, error);
    }

    public static Result Create(bool condition)
    {
        return condition ? Success() : Failure(Error.ConditionNotMet);
    }

    public static Result<TValue> Create<TValue>(TValue value)
    {
        return value is not null ? Success(value) : Failure<TValue>(Error.NullArgument);
    }
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;
    public TValue Value => IsSuccess ? _value! : throw new InvalidOperationException("The value of a failure result cannot be accessed");

    public Result(TValue? value, Error error) : base(error)
    {
        _value = value;
    }

    public static implicit operator Result<TValue>(TValue value)
    {
        return Create(value);
    }
}