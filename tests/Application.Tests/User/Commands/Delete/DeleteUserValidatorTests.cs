using ExpensesTracker.Application.User.Commands.Delete;
using ExpensesTracker.Domain.Errors.Implementations;
using ExpensesTracker.Domain.Requests.User;
using FluentAssertions;
using TestUtils.Repositories.User;

namespace Application.Tests.User.Commands.Delete;

public class DeleteUserValidatorTests
{
    private const int UserId = 58;

    [Fact]
    public async Task Validator_Should_ReturnSuccess_When_User_Exists()
    {
        var request = new DeleteUserRequest(UserId);
        var command = new DeleteUserCommand(request);
        var validator = CreateValidator(UserId);

        var result = await validator.ValidateAsync(command);

        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public async Task Validator_Should_ReturnFailure_When_User_Doesnt_Exist()
    {
        var request = new DeleteUserRequest(UserId);
        var command = new DeleteUserCommand(request);
        var validator = CreateValidator();

        var result = await validator.ValidateAsync(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(err => err.ErrorMessage == UserErrors.UserNotFound(0).ErrorMessage);
    }

    private static DeleteUserCommandValidator CreateValidator(int? id = null)
    {
        var readRepository = UserReadRepositoryMock.Create;

        if (id is not null)
        {
            UserReadRepositoryMock.SetupUserExists(readRepository, (int)id);
        }

        return new DeleteUserCommandValidator(readRepository.Object);
    }
}