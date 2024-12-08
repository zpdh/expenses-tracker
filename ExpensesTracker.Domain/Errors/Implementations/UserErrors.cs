using ExpensesTracker.Domain.Errors.Base;

namespace ExpensesTracker.Domain.Errors.Implementations;

public sealed record UserError
{
    public static Error UsernameLength => new("The provided username must have, at most, 36 characters.");
    public static Error EmptyUsername => new("The provided username must not be empty.");
    public static Error InvalidEmail => new("The provided email is not valid.");
    public static Error EmptyEmail => new("The provided email must not be empty.");
    public static Error PasswordLength => new("The provided password must be between 6 to 32 characters.");
    public static Error EmailAlreadyRegistered => new("A user with the provided email is already registered.");
    public static Error InvalidCredentials => new("The provided email or password are incorrect. Try again.");
    public static Error UserNotFound(int id) => new($"User with the id = {id} was not found.");
}