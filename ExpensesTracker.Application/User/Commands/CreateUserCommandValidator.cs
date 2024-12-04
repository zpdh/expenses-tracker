using FluentValidation;

namespace ExpensesTracker.Application.User.Commands;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(cmd => cmd.Request.Name).NotEmpty()
            .DependentRules(() => {
                RuleFor(cmd => cmd.Request.Name).MaximumLength(36);
            });

        RuleFor(cmd => cmd.Request.Email).NotEmpty()
            .DependentRules(() => {
                RuleFor(cmd => cmd.Request.Email).EmailAddress();
            });

        RuleFor(cmd => cmd.Request.Password).NotEmpty()
            .DependentRules(() => {
                RuleFor(cmd => cmd.Request.Password).MinimumLength(6);
                RuleFor(cmd => cmd.Request.Password).MaximumLength(32);
            });
    }
}