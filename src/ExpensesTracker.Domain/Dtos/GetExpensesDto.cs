namespace ExpensesTracker.Domain.Requests.Expense;

public sealed record GetExpensesDto(int UserId, string Filter);