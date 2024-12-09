using ExpensesTracker.Domain.Repositories.User;
using Moq;

namespace TestUtils.Repositories;

public sealed class UserWriteRepositoryMock
{
    public static Mock<IUserWriteRepository> Create => new();
}