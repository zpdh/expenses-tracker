using ExpensesTracker.Application.Base.Commands;
using ExpensesTracker.Domain.Errors.Implementations;
using ExpensesTracker.Domain.Extensions;
using ExpensesTracker.Domain.Repositories;
using ExpensesTracker.Domain.Repositories.User;
using ExpensesTracker.Domain.Requests.User;
using ExpensesTracker.Domain.Results;

namespace ExpensesTracker.Application.User.Commands;

public sealed record CreateUserCommand(CreateUserRequest Request) : ICommand;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserWriteRepository _writeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUserWriteRepository writeRepository, IUnitOfWork unitOfWork)
    {
        _writeRepository = writeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = new Domain.Entities.User(
            command.Request.Name,
            command.Request.Email,
            command.Request.Password);

        var validationResult = Validate(command.Request);

        if (validationResult.IsFailure)
        {
            return validationResult;
        }

        await AddToDatabaseAsync(user);

        return Result.Success();
    }

    private async Task AddToDatabaseAsync(Domain.Entities.User user)
    {
        await _writeRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }

    private static Result Validate(CreateUserRequest request)
    {
        if (request.Name.Length is < 4 or > 36)
        {
            return UserError.UsernameLength;
        }

        if (request.Email.IsInvalidEmail())
        {
            return UserError.InvalidEmail;
        }

        if (request.Password.Length is < 6 or > 32)
        {
            return UserError.PasswordLength;
        }

        return Result.Success();
    }
}