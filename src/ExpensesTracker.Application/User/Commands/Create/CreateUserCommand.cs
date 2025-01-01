using ExpensesTracker.Application.Base.Commands;
using ExpensesTracker.Domain.Entities;
using ExpensesTracker.Domain.Infrastructure.Hasher;
using ExpensesTracker.Domain.Infrastructure.Repositories;
using ExpensesTracker.Domain.Infrastructure.Repositories.User;
using ExpensesTracker.Domain.Requests.User;
using ExpensesTracker.Domain.Results;

namespace ExpensesTracker.Application.User.Commands.Create;

public sealed record CreateUserCommand(CreateUserRequest Request) : ICommand;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IHasherService _hasherService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserWriteRepository _writeRepository;

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
        await _writeRepository.AddRoleAsync(user, Role.Registered);
        await _unitOfWork.SaveChangesAsync();
    }
}