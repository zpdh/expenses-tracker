namespace ExpensesTracker.Domain.Requests.User;

public sealed record LoginUserRequest(string Email, string Password);