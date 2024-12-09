namespace ExpensesTracker.Domain.Responses.User;

public sealed record GetUserByIdResponse(int Id, string Name, string Email);