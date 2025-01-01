using ExpensesTracker.Application.Extensions;
using ExpensesTracker.Domain.Errors.Implementations;
using ExpensesTracker.Domain.Infrastructure.Repositories.Category;
using ExpensesTracker.Domain.Infrastructure.Repositories.User;
using FluentValidation;

namespace ExpensesTracker.Application.Expenses.Commands.Add;

public class AddExpenseCommandValidator : AbstractValidator<AddExpenseCommand>
{
    public AddExpenseCommandValidator(IUserReadRepository userReadRepository, ICategoryReadRepository categoryReadRepository)
    {
        RuleFor(cmd => cmd.Request.UserId)
            .Must(userReadRepository.UserExists)
            .WithError(ExpenseErrors.InvalidUserId);

        RuleFor(cmd => cmd.Request.CategoryId)
            .Must(categoryReadRepository.CategoryExists)
            .WithError(ExpenseErrors.InvalidCategoryId);

        RuleFor(cmd => cmd.Request.Name)
            .NotEmpty()
            .WithError(ExpenseErrors.EmptyName);

        RuleFor(cmd => cmd.Request.Price)
            .GreaterThan(0)
            .WithError(ExpenseErrors.InvalidPrice);
    }
}