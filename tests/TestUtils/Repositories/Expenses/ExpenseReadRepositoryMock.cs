using ExpensesTracker.Domain.Dtos;
using ExpensesTracker.Domain.Entities;
using ExpensesTracker.Domain.Infrastructure.Repositories.Expenses;
using Moq;

namespace TestUtils.Repositories.Expenses;

public static class ExpenseReadRepositoryMock
{
    public static Mock<IExpenseReadRepository> Create => new();

    public static void SetupGetExpensesByUserIdAsync(
        Mock<IExpenseReadRepository> mock,
        List<Expense> expenses,
        GetExpensesDto dto)
    {
        mock.Setup(moq => moq.GetExpensesByUserIdAsync(dto))
            .ReturnsAsync(expenses.Where(expense => expense.Name.Contains(dto.Filter, StringComparison.CurrentCultureIgnoreCase)).ToList);
    }

    public static void SetupExpenseExists(Mock<IExpenseReadRepository> mock, int userId, int categoryId)
    {
        mock.Setup(moq => moq.ExpenseExists(userId, categoryId)).Returns(true);
    }
}