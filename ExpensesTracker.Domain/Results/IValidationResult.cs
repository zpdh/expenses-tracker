using ExpensesTracker.Domain.Errors.Base;

namespace ExpensesTracker.Domain.Results;

public interface IValidationResult
{
    public static readonly Error ValidationError = new("A validation problem occurred.");
    ICollection<Error> Errors { get; }
}