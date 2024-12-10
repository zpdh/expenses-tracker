using ExpensesTracker.Application.User.Commands;
using ExpensesTracker.Application.User.Commands.Create;
using ExpensesTracker.Domain.Errors.Implementations;
using ExpensesTracker.Domain.Repositories.User;
using ExpensesTracker.Domain.Requests.User;
using FluentAssertions;
using TestUtils.Repositories;

namespace Application.Tests.User.Commands.Create;

public class CreateUserValidatorTests
{
    [Fact]
    public async Task Result_Should_ReturnSuccess()
    {
        var request = new CreateUserRequest("Name", "email@gmail.com", "123456");
        var command = new CreateUserCommand(request);
        var validator = CreateValidator();

        var result = await validator.ValidateAsync(command);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Result_Should_ReturnFailure_When_Name_Is_Empty()
    {
        var request = new CreateUserRequest("", "email@gmail.com", "123456");
        var command = new CreateUserCommand(request);
        var validator = CreateValidator();

        var result = await validator.ValidateAsync(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == UserErrors.EmptyUsername.ErrorMessage);
    }

    [Fact]
    public async Task Result_Should_ReturnFailure_When_Name_Is_Too_Long()
    {
        var request = new CreateUserRequest("ThisStringIsOver36CharactersLongCanYouBelieveIt", "email@gmail.com", "123456");
        var command = new CreateUserCommand(request);
        var validator = CreateValidator();

        var result = await validator.ValidateAsync(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == UserErrors.UsernameLength.ErrorMessage);
    }

    [Fact]
    public async Task Result_Should_ReturnFailure_When_Email_Is_Empty()
    {
        var request = new CreateUserRequest("Name", "", "123456");
        var command = new CreateUserCommand(request);
        var validator = CreateValidator();

        var result = await validator.ValidateAsync(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == UserErrors.EmptyEmail.ErrorMessage);
    }

    [Fact]
    public async Task Result_Should_ReturnFailure_When_Email_Is_Invalid()
    {
        var request = new CreateUserRequest("Name", "NotAnEmail", "123456");
        var command = new CreateUserCommand(request);
        var validator = CreateValidator();

        var result = await validator.ValidateAsync(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == UserErrors.InvalidEmail.ErrorMessage);
    }

    [Fact]
    public async Task Result_Should_ReturnFailure_When_Email_Is_Not_Unique()
    {
        var request = new CreateUserRequest("Name", "email@gmail.com", "123456");
        var command = new CreateUserCommand(request);
        var validator = CreateValidator(request.Email);

        var result = await validator.ValidateAsync(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == UserErrors.EmailAlreadyRegistered.ErrorMessage);
    }

    [Fact]
    public async Task Result_Should_ReturnFailure_When_Password_Is_Invalid_Length()
    {
        var request = new CreateUserRequest("Name", "email@gmail.com", "123");
        var command = new CreateUserCommand(request);
        var validator = CreateValidator();

        var result = await validator.ValidateAsync(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == UserErrors.PasswordLength.ErrorMessage);
    }


    private static CreateUserCommandValidator CreateValidator(string? email = null)
    {
        var readRepository = UserReadRepositoryMock.Create;

        if (email is null)
        {
            UserReadRepositoryMock.SetupIsEmailUnique(readRepository);
        }

        return new CreateUserCommandValidator(readRepository.Object);
    }
}