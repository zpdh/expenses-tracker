using ExpensesTracker.Domain.Entities;
using ExpensesTracker.Domain.Infrastructure.Repositories.Expenses;
using Moq;

namespace TestUtils.Repositories.Expenses;

public static class ExpenseReadRepositoryMock
{
    public static Mock<IExpenseReadRepository> Create => new();

    public static void SetupGetExpensesByUseridAsync(Mock<IExpenseReadRepository> mock, List<Expense> expenses, int userId)
    {
        mock.Setup(moq => moq.GetExpensesByUserIdAsync(userId)).ReturnsAsync(expenses);
    }

    public static void SetupExpenseExists(Mock<IExpenseReadRepository> mock, int userId, int categoryId)
    {
        mock.Setup(moq => moq.ExpenseExists(userId, categoryId)).Returns(true);
    }
}