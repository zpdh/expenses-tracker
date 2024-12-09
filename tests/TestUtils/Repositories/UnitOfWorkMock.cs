using ExpensesTracker.Domain.Repositories;
using Moq;

namespace TestUtils.Repositories;

public sealed class UnitOfWorkMock
{
    public static Mock<IUnitOfWork> Create => new();
}