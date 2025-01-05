using ExpensesTracker.Application.Extensions;
using ExpensesTracker.Domain.Errors.Implementations;
using ExpensesTracker.Domain.Infrastructure.Repositories.User;
using FluentValidation;

namespace ExpensesTracker.Application.User.Commands.Delete;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator(IUserReadRepository userReadRepository)
    {
        RuleFor(cmd => cmd.Request.Id).Must(userReadRepository.UserExists).WithError(UserErrors.UserNotFound());
    }
}