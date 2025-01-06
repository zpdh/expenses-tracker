using ExpensesTracker.Domain.Infrastructure.Repositories.Expenses;
using Moq;

namespace TestUtils.Repositories.Expenses;

public static class ExpenseWriteRepositoryMock
{
    public static Mock<IExpenseWriteRepository> Create => new();
}