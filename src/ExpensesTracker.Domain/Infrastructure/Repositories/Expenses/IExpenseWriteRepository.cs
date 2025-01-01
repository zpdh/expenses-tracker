using ExpensesTracker.Domain.Entities;

namespace ExpensesTracker.Domain.Infrastructure.Repositories.Expenses;

public interface IExpenseWriteRepository
{
    Task AddExpenseAsync(Expense expense);
}