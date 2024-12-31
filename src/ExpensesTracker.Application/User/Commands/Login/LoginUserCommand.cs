using ExpensesTracker.Application.Base.Commands;
using ExpensesTracker.Domain.Errors.Implementations;
using ExpensesTracker.Domain.Infrastructure.Hasher;
using ExpensesTracker.Domain.Infrastructure.Repositories.User;
using ExpensesTracker.Domain.Infrastructure.Tokens;
using ExpensesTracker.Domain.Requests.User;
using ExpensesTracker.Domain.Responses.Token;
using ExpensesTracker.Domain.Results;

namespace ExpensesTracker.Application.User.Commands.Login;

public sealed record LoginUserCommand(LoginUserRequest Request) : ICommand<TokenResponse>;

public sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, TokenResponse>
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IHasherService _hasherService;

    public LoginUserCommandHandler(IUserReadRepository userReadRepository, IJwtGenerator jwtGenerator, IHasherService hasherService)
    {
        _userReadRepository = userReadRepository;
        _jwtGenerator = jwtGenerator;
        _hasherService = hasherService;
    }

    public async Task<Result<TokenResponse>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _userReadRepository.GetUserByEmailAsync(command.Request.Email);

        if (user is null || _hasherService.IsInvalid(command.Request.Password, user.Password))
        {
            return Result.Failure<TokenResponse>(UserErrors.InvalidCredentials);
        }

        var token = await _jwtGenerator.GenerateAsync(user);

        return new TokenResponse(token);
    }
}