using ExpensesTracker.Domain.Errors.Base;

namespace ExpensesTracker.Domain.Errors.Implementations;

public sealed record ExpenseErrors
{
    public static Error InvalidUserId => new("The userId field must correspond to a registered user.");
    public static Error InvalidCategoryId => new("The categoryId field must correspond to a valid category.");
    public static Error EmptyName => new("The expense name must not be empty.");
    public static Error InvalidPrice => new("The price field must be greater than 0.");
    public static Error NotFound => new("No expense with the provided id was found.");
}