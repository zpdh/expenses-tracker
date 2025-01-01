using ExpensesTracker.Application.Base.Commands;
using ExpensesTracker.Domain.Dtos;
using ExpensesTracker.Domain.Entities;
using ExpensesTracker.Domain.Infrastructure.Repositories;
using ExpensesTracker.Domain.Infrastructure.Repositories.Expenses;
using ExpensesTracker.Domain.Requests.Expense;
using ExpensesTracker.Domain.Results;

namespace ExpensesTracker.Application.Expenses.Commands.Add;

public sealed record AddExpenseCommand(AddExpenseDto Request) : ICommand;

public sealed class AddExpenseCommandHandler : ICommandHandler<AddExpenseCommand>
{
    private readonly IExpenseWriteRepository _writeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddExpenseCommandHandler(IExpenseWriteRepository writeRepository, IUnitOfWork unitOfWork)
    {
        _writeRepository = writeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AddExpenseCommand command, CancellationToken cancellationToken)
    {
        var expense = Expense.Create(command.Request);

        await AddToDatabaseAsync(expense);

        return Result.Success();
    }

    private async Task AddToDatabaseAsync(Expense expense)
    {
        await _writeRepository.AddExpenseAsync(expense);
        await _unitOfWork.SaveChangesAsync();
    }
}