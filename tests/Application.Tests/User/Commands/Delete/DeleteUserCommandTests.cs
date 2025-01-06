using ExpensesTracker.Application.User.Commands.Delete;
using ExpensesTracker.Domain.Requests.User;
using FluentAssertions;
using TestUtils.Repositories;
using TestUtils.Repositories.User;

namespace Application.Tests.User.Commands.Delete;

public class DeleteUserCommandTests
{
    private const int UserId = 13;

    [Fact]
    public async Task Handler_Should_ReturnSuccess()
    {
        var request = new DeleteUserRequest(UserId);
        var command = new DeleteUserCommand(request);
        var handler = CreateHandler();

        var result = await handler.Handle(command, default);

        result.IsSuccess.Should().BeTrue();
    }

    private static DeleteUserCommandHandler CreateHandler()
    {
        var writeRepository = UserWriteRepositoryMock.Create.Object;
        var unitOfWork = UnitOfWorkMock.Create.Object;

        return new DeleteUserCommandHandler(writeRepository, unitOfWork);
    }
}