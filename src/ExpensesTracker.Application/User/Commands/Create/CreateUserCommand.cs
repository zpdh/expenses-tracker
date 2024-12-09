using ExpensesTracker.Application.Base.Commands;
using ExpensesTracker.Domain.Infrastructure.Hasher;
using ExpensesTracker.Domain.Repositories;
using ExpensesTracker.Domain.Repositories.User;
using ExpensesTracker.Domain.Requests.User;
using ExpensesTracker.Domain.Results;

namespace ExpensesTracker.Application.User.Commands.Create;

public sealed record CreateUserCommand(CreateUserRequest Request) : ICommand;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserWriteRepository _writeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHasherService _hasherService;

    public CreateUserCommandHandler(IUserWriteRepository writeRepository, IUnitOfWork unitOfWork, IHasherService hasherService)
    {
        _writeRepository = writeRepository;
        _unitOfWork = unitOfWork;
        _hasherService = hasherService;
    }

    public async Task<Result> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var hashedPassword = _hasherService.Hash(command.Request.Password);

        var user = Domain.Entities.User.Create(
            command.Request.Name,
            command.Request.Email,
            hashedPassword);

        await AddToDatabaseAsync(user);

        return Result.Success();
    }

    private async Task AddToDatabaseAsync(Domain.Entities.User user)
    {
        await _writeRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }
}