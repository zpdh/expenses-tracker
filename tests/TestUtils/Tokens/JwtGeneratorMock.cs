using ExpensesTracker.Domain.Entities;
using ExpensesTracker.Domain.Infrastructure.Tokens;
using Moq;

namespace TestUtils.Tokens;

public class JwtGeneratorMock
{
    public static Mock<IJwtGenerator> Create()
    {
        var mock = new Mock<IJwtGenerator>();

        mock.Setup(generator => generator.GenerateAsync(It.IsAny<User>())).ReturnsAsync(Guid.NewGuid().ToString);

        return mock;
    }
}