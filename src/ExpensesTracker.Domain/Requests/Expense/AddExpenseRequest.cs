namespace ExpensesTracker.Domain.Requests.Expense;

public sealed record AddExpenseRequest(int CategoryId, string Name, double Price);