using ExpensesTracker.Application.Base.Commands;
using ExpensesTracker.Domain.Dtos;
using ExpensesTracker.Domain.Infrastructure.Repositories;
using ExpensesTracker.Domain.Infrastructure.Repositories.Expenses;
using ExpensesTracker.Domain.Results;

namespace ExpensesTracker.Application.Expenses.Commands.Delete;

public sealed record DeleteExpenseCommand(DeleteExpenseDto Request) : ICommand;

public sealed record DeleteExpenseCommandHandler : ICommandHandler<DeleteExpenseCommand>
{
    private readonly IExpenseWriteRepository _writeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteExpenseCommandHandler(IExpenseWriteRepository writeRepository, IUnitOfWork unitOfWork)
    {
        _writeRepository = writeRepository;
        _unitOfWork = unitOfWork;

    }

    public async Task<Result> Handle(DeleteExpenseCommand command, CancellationToken cancellationToken)
    {
        await DeleteFromDatabaseAsync(command.Request);

        return Result.Success();
    }

    private async Task DeleteFromDatabaseAsync(DeleteExpenseDto dto)
    {
        await _writeRepository.DeleteExpenseAsync(dto.UserId, dto.ExpenseId);
        await _unitOfWork.SaveChangesAsync();
    }
}