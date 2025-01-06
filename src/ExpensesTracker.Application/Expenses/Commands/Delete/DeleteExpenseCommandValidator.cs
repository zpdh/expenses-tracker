using ExpensesTracker.Application.Extensions;
using ExpensesTracker.Domain.Errors.Implementations;
using ExpensesTracker.Domain.Infrastructure.Repositories.Expenses;
using FluentValidation;

namespace ExpensesTracker.Application.Expenses.Commands.Delete;

public class DeleteExpenseCommandValidator : AbstractValidator<DeleteExpenseCommand>
{
    public DeleteExpenseCommandValidator(IExpenseReadRepository readRepository)
    {
        RuleFor(cmd => cmd.Request).Must(req => readRepository.ExpenseExists(req.UserId, req.ExpenseId)).WithError(ExpenseErrors.NotFound);
    }
}