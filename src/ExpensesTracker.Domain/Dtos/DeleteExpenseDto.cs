namespace ExpensesTracker.Domain.Dtos;

public sealed record DeleteExpenseDto(int UserId, int ExpenseId);