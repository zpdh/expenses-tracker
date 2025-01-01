using ExpensesTracker.Application.User.Commands.Create;
using ExpensesTracker.Domain.Errors.Base;
using ExpensesTracker.Domain.Requests.User;
using FluentAssertions;
using TestUtils.Hasher;
using TestUtils.Repositories;
using TestUtils.Repositories.User;

namespace Application.Tests.User.Commands.Create;

public class CreateUserCommandTests
{
    [Fact]
    public async Task Handler_Should_ReturnSuccess()
    {
        var request = new CreateUserRequest("Name", "email@test.com", "Password");
        var command = new CreateUserCommand(request);
        var handler = CreateHandler();

        var result = await handler.Handle(command, default);

        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(Error.None);
    }

    private static CreateUserCommandHandler CreateHandler()
    {
        var writeRepository = UserWriteRepositoryMock.Create.Object;
        var unitOfWork = UnitOfWorkMock.Create.Object;
        var hasher = HasherServiceMock.Create().Object;

        return new CreateUserCommandHandler(writeRepository, unitOfWork, hasher);
    }
}