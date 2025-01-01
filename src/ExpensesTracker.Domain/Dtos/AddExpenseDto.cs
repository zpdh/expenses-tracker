namespace ExpensesTracker.Domain.Dtos;

public sealed record AddExpenseDto(int CategoryId, int UserId, string Name, double Price);