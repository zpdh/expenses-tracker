using ExpensesTracker.Domain.Errors.Base;

namespace ExpensesTracker.Domain.Errors.Implementations;

public sealed record CategoryErrors
{
    public static Error EmptyName => new("The category name must not be empty.");
    public static Error CategoryExists => new("There is an existing category with this name.");
}