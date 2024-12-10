using ExpensesTracker.Application.Extensions;
using ExpensesTracker.Domain.Errors.Implementations;
using ExpensesTracker.Domain.Repositories.User;
using FluentValidation;

namespace ExpensesTracker.Application.User.Commands.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator(IUserReadRepository readRepository)
    {
        RuleFor(cmd => cmd.Request.Name)
            .NotEmpty()
            .WithError(UserErrors.EmptyUsername)
            .DependentRules(() => {
                RuleFor(cmd => cmd.Request.Name)
                    .MaximumLength(36)
                    .WithError(UserErrors.UsernameLength);
            });

        RuleFor(cmd => cmd.Request.Email)
            .NotEmpty()
            .WithError(UserErrors.EmptyEmail)
            .DependentRules(() => {
                RuleFor(cmd => cmd.Request.Email)
                    .EmailAddress()
                    .WithError(UserErrors.InvalidEmail);

                RuleFor(cmd => cmd.Request.Email)
                    .Must(readRepository.IsEmailUnique).WithError(UserErrors.EmailAlreadyRegistered);
            });

        RuleFor(cmd => cmd.Request.Password)
            .Length(6, 32)
            .WithError(UserErrors.PasswordLength);
    }
}