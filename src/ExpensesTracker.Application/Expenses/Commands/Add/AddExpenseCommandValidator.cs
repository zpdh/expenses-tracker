using ExpensesTracker.Domain.Infrastructure.Repositories.Expenses;
using ExpensesTracker.Domain.Infrastructure.Repositories.User;
using FluentValidation;

namespace ExpensesTracker.Application.Expenses.Commands.Add;

public class AddExpenseCommandValidator : AbstractValidator<AddExpenseCommand>
{
    public AddExpenseCommandValidator(IUserReadRepository userReadRepository, IExpenseReadRepository expenseReadRepository)
    {

    }
}