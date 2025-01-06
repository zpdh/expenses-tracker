using ExpensesTracker.Domain.Entities;

namespace ExpensesTracker.Domain.Infrastructure.Repositories.Expenses;

public interface IExpenseWriteRepository
{
    Task AddExpenseAsync(Expense expense);
    Task DeleteExpenseAsync(int userId, int expenseId);
}