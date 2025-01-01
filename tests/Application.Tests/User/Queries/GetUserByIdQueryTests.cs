using ExpensesTracker.Application.User.Queries;
using ExpensesTracker.Domain.Errors.Base;
using ExpensesTracker.Domain.Errors.Implementations;
using FluentAssertions;
using TestUtils.Repositories.User;

namespace Application.Tests.User.Queries;

public class GetUserByIdQueryTests
{
    private const int UserId = 21;

    [Fact]
    public async Task Handler_Should_ReturnSuccess()
    {
        var query = new GetUserByIdQuery(UserId);
        var handler = CreateHandler(UserId);

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(Error.None);
    }

    [Fact]
    public async Task Handler_Should_ReturnFailure_When_Invalid_Id()
    {
        var query = new GetUserByIdQuery(UserId);
        var handler = CreateHandler();

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(UserErrors.UserNotFound(UserId));
    }

    private static GetUserByIdQueryHandler CreateHandler(int? id = null)
    {
        var readRepository = UserReadRepositoryMock.Create;

        if (id is not null)
        {
            var user = ExpensesTracker.Domain.Entities.User.Create("Name", "Email", "Password");
            UserReadRepositoryMock.SetupGetUserById(readRepository, UserId, user);
        }

        return new GetUserByIdQueryHandler(readRepository.Object);
    }
}