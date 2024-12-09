using ExpensesTracker.Domain.Infrastructure.Hasher;
using Moq;

namespace TestUtils.Hasher;

public class HasherServiceMock
{
    public static Mock<IHasherService> Create(string password)
    {
        var mock = new Mock<IHasherService>();

        mock.Setup(hasher => hasher.Hash(password)).Returns($"hashed-{password}");
        return mock;
    }

    public static Mock<IHasherService> SetupValidationMethods(Mock<IHasherService> mock, string password)
    {
        mock.Setup(hasher => hasher.IsValid(password, hasher.Hash(password))).Returns(true);
        mock.Setup(hasher => hasher.IsInvalid(password, hasher.Hash(password))).Returns(false);

        return mock;
    }
}