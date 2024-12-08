using ExpensesTracker.Application.Extensions;
using ExpensesTracker.Domain.Errors.Implementations;
using ExpensesTracker.Domain.Repositories.User;
using FluentValidation;

namespace ExpensesTracker.Application.User.Commands;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator(IUserReadRepository readRepository)
    {
        RuleFor(cmd => cmd.Request.Name)
            .NotEmpty()
            .WithError(UserError.EmptyUsername)
            .DependentRules(() => {
                RuleFor(cmd => cmd.Request.Name)
                    .MaximumLength(36)
                    .WithError(UserError.UsernameLength);
            });

        RuleFor(cmd => cmd.Request.Email)
            .NotEmpty()
            .WithError(UserError.EmptyEmail)
            .DependentRules(() => {
                RuleFor(cmd => cmd.Request.Email)
                    .EmailAddress()
                    .WithError(UserError.InvalidEmail);

                RuleFor(cmd => cmd.Request.Email)
                    .Must(email => readRepository.IsEmailUnique(email)).WithError(UserError.EmailAlreadyRegistered);
            });

        RuleFor(cmd => cmd.Request.Password)
            .Length(6, 32)
            .WithError(UserError.PasswordLength);
    }
}