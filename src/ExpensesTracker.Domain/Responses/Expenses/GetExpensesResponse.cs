using ExpensesTracker.Domain.Entities;

namespace ExpensesTracker.Domain.Responses.Expenses;

public sealed record GetExpensesResponse(List<Expense>? Expenses);