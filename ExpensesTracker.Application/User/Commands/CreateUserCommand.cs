using ExpensesTracker.Application.Base.Commands;
using ExpensesTracker.Application.Validators;
using ExpensesTracker.Domain.Repositories;
using ExpensesTracker.Domain.Repositories.User;
using ExpensesTracker.Domain.Requests.User;
using ExpensesTracker.Domain.Results;
using MediatR;

namespace ExpensesTracker.Application.User.Commands;

public sealed record CreateUserCommand(CreateUserRequest Params) : ICommand;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserWriteRepository _writeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUserWriteRepository writeRepository, IUnitOfWork unitOfWork)
    {
        _writeRepository = writeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new Domain.Entities.User(
            request.Params.Name,
            request.Params.Email,
            request.Params.Password);

        await AddToDatabaseAsync(user);

        return Result.Success();
    }

    private async Task AddToDatabaseAsync(Domain.Entities.User user)
    {
        await _writeRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }
}