namespace ExpensesTracker.Domain.Dtos;

public sealed record GetExpensesDto(int UserId, string Filter, DateTime Since);