namespace ExpensesTracker.Domain.Errors;

public record Error(string ErrorMessage)
{
    public static readonly Error None = new(string.Empty);
    public static readonly Error Null = new("The specified value is null.");
    public static readonly Error ConditionNotMet = new("The specified condition was not met.");
}