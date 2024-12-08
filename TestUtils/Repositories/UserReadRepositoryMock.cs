using ExpensesTracker.Domain.Repositories.User;
using Moq;

namespace TestUtils.Repositories;

public sealed class UserReadRepositoryMock
{
    public static Mock<IUserReadRepository> Create => new();

    public static void SetupIsEmailUnique(Mock<IUserReadRepository> mock)
    {
        mock.Setup(moq => moq.IsEmailUnique(It.IsAny<string>())).Returns(true);
        mock.Setup(moq => moq.IsEmailUniqueAsync(It.IsAny<string>())).ReturnsAsync(true);
    }
}