using ExpensesTracker.Application.Base.Commands;
using ExpensesTracker.Domain.Infrastructure.Repositories;
using ExpensesTracker.Domain.Infrastructure.Repositories.User;
using ExpensesTracker.Domain.Requests.User;
using ExpensesTracker.Domain.Results;

namespace ExpensesTracker.Application.User.Commands.Delete;

public sealed record DeleteUserCommand(DeleteUserRequest Request) : ICommand;

public sealed class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
{
    private readonly IUserWriteRepository _writeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUserWriteRepository writeRepository, IUnitOfWork unitOfWork)
    {
        _writeRepository = writeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        await DeleteFromDatabaseAsync(command.Request);

        return Result.Success();
    }

    private async Task DeleteFromDatabaseAsync(DeleteUserRequest request)
    {
        // IDEA: Add event queue for deletion. Prevents app from being potentially slowed down by deleting a user with a lot of data.
        await _writeRepository.DeleteAsync(request.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}