using ExpensesTracker.Domain.Entities;

namespace ExpensesTracker.Domain.Infrastructure.Repositories.Expenses;

public interface IExpenseReadRepository
{
    Task<List<Expense>> GetExpensesByUserId(int userId);
}