namespace ExpensesTracker.Domain.Requests.Expense;

public sealed record GetExpensesRequest(string Filter, DateTime Since)
{
    public GetExpensesRequest() : this(string.Empty, DateTime.MinValue)
    {
    }
}