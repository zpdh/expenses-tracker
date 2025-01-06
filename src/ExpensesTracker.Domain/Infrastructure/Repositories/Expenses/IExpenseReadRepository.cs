using ExpensesTracker.Domain.Entities;

namespace ExpensesTracker.Domain.Infrastructure.Repositories.Expenses;

public interface IExpenseReadRepository
{
    Task<List<Expense>> GetExpensesByUserIdAsync(int userId);
    bool ExpenseExists(int userId, int expenseId);
}