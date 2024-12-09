using ExpensesTracker.Domain.Infrastructure.Hasher;
using Moq;

namespace TestUtils.Hasher;

public class HasherServiceMock
{
    public static Mock<IHasherService> Create()
    {
        var mock = new Mock<IHasherService>();

        mock.Setup(hasher => hasher.Hash(It.IsAny<string>())).Returns("hashed-password");
        mock.Setup(hasher => hasher.IsValid(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
        mock.Setup(hasher => hasher.IsInvalid(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

        return mock;
    }

    public static void SetupValidationMethods(Mock<IHasherService> mock)
    {
        mock.Setup(hasher => hasher.IsValid(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
        mock.Setup(hasher => hasher.IsInvalid(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
    }
}