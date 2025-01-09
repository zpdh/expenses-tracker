using ExpensesTracker.Domain.Dtos;
using ExpensesTracker.Domain.Entities;

namespace ExpensesTracker.Domain.Infrastructure.Repositories.Expenses;

public interface IExpenseReadRepository
{
    Task<List<Expense>> GetExpensesByUserIdAsync(GetExpensesDto request);
    bool ExpenseExists(int userId, int expenseId);
}