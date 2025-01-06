using ExpensesTracker.Domain.Infrastructure.Repositories.User;
using Moq;

namespace TestUtils.Repositories.User;

public sealed class UserReadRepositoryMock
{
    public static Mock<IUserReadRepository> Create => new();

    public static void SetupIsEmailUnique(Mock<IUserReadRepository> mock)
    {
        mock.Setup(moq => moq.IsEmailUnique(It.IsAny<string>())).Returns(true);
        mock.Setup(moq => moq.IsEmailUniqueAsync(It.IsAny<string>())).ReturnsAsync(true);
    }

    public static void SetupGetUserByEmail(Mock<IUserReadRepository> mock, ExpensesTracker.Domain.Entities.User user)
    {
        mock.Setup(moq => moq.GetUserByEmail(user.Email)).Returns(user);
        mock.Setup(moq => moq.GetUserByEmailAsync(user.Email)).ReturnsAsync(user);
    }

    public static void SetupGetUserById(Mock<IUserReadRepository> mock, int id, ExpensesTracker.Domain.Entities.User user)
    {
        mock.Setup(moq => moq.GetByIdAsync(id)).ReturnsAsync(user);
    }

    public static void SetupUserExists(Mock<IUserReadRepository> mock, int id)
    {
        mock.Setup(moq => moq.UserExists(id)).Returns(true);
    }
}