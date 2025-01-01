using ExpensesTracker.Application.User.Commands.Login;
using ExpensesTracker.Domain.Errors.Base;
using ExpensesTracker.Domain.Errors.Implementations;
using ExpensesTracker.Domain.Requests.User;
using FluentAssertions;
using TestUtils.Hasher;
using TestUtils.Repositories;
using TestUtils.Repositories.User;
using TestUtils.Tokens;

namespace Application.Tests.User.Commands.Login;

public class LoginUserCommandTests
{
    [Fact]
    public async Task Handler_Should_ReturnSuccess()
    {
        var request = new LoginUserRequest("email@gmail.com", "Password");
        var command = new LoginUserCommand(request);
        var handler = CreateHandler(email: request.Email);

        var result = await handler.Handle(command, default);

        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(Error.None);
    }

    [Fact]
    public async Task Handler_Should_ReturnFailure_When_Password_Is_Invalid()
    {
        var request = new LoginUserRequest("email@gmail.com", "Password");
        var command = new LoginUserCommand(request);
        var handler = CreateHandler(password: request.Password, email: request.Email);

        var result = await handler.Handle(command, default);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(UserErrors.InvalidCredentials);
    }

    [Fact]
    public async Task Handler_Should_ReturnFailure_When_User_Not_Found()
    {
        var request = new LoginUserRequest("email@gmail.com", "Password");
        var command = new LoginUserCommand(request);
        var handler = CreateHandler();

        var result = await handler.Handle(command, default);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(UserErrors.InvalidCredentials);
    }

    private static LoginUserCommandHandler CreateHandler(string? password = null, string? email = null)
    {
        var readRepository = UserReadRepositoryMock.Create;
        var jwtGenerator = JwtGeneratorMock.Create().Object;
        var hasher = HasherServiceMock.Create();

        if (email is not null)
        {
            var user = ExpensesTracker.Domain.Entities.User.Create("Name", email, "Password");

            UserReadRepositoryMock.SetupGetUserByEmail(readRepository, user);
        }

        if (password is null)
        {
            HasherServiceMock.SetupValidationMethods(hasher);
        }

        return new LoginUserCommandHandler(readRepository.Object, jwtGenerator, hasher.Object);
    }
}