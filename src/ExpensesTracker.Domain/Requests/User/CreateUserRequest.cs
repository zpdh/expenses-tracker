namespace ExpensesTracker.Domain.Requests.User;

public sealed record CreateUserRequest(string Name, string Email, string Password);