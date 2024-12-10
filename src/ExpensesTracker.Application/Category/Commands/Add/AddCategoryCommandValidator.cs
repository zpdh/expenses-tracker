using ExpensesTracker.Application.Extensions;
using ExpensesTracker.Domain.Errors.Implementations;
using ExpensesTracker.Domain.Repositories.Category;
using FluentValidation;

namespace ExpensesTracker.Application.Category.Commands;

public class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
{
    public AddCategoryCommandValidator(ICategoryReadRepository repository)
    {
        RuleFor(cmd => cmd.Request.CategoryName)
            .NotEmpty()
            .WithError(CategoryErrors.EmptyName)
            .DependentRules(() => {
                RuleFor(cmd => cmd.Request.CategoryName)
                    .Must(repository.CategoryWithNameDoesNotExist)
                    .WithError(CategoryErrors.CategoryExists);
            });
    }
}