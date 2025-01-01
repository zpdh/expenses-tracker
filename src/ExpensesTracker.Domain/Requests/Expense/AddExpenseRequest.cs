namespace ExpensesTracker.Domain.Requests.Expense;

public sealed record AddExpenseRequest(int CategoryId, int UserId, string Name, double Price);