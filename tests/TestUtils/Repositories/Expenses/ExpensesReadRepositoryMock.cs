using ExpensesTracker.Domain.Entities;
using ExpensesTracker.Domain.Infrastructure.Repositories.Expenses;
using Moq;

namespace TestUtils.Repositories.Expenses;

public class ExpensesReadRepositoryMock
{
    public static Mock<IExpenseReadRepository> Create => new();

    public static void SetupGetExpensesByUseridAsync(Mock<IExpenseReadRepository> mock, List<Expense> expenses, int userId)
    {
        mock.Setup(moq => moq.GetExpensesByUserIdAsync(userId)).ReturnsAsync(expenses);
    }
}