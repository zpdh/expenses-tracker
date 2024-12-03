using ExpensesTracker.Domain.Errors.Base;

namespace ExpensesTracker.Domain.Errors.Implementations;

public sealed record UserError
{
    public static Error UsernameLength => new("The provided username must be between 4 to 36 characters.");
    public static Error InvalidEmail => new("The provided email is not valid.");
    public static Error PasswordLength => new("The provided password must be between 6 to 32 characters.");
    public static Error UserNotFound(int id) => new($"User with the id = {id} was not found.");
}