using ExpensesTracker.Application.Base.Commands;
using ExpensesTracker.Domain.Errors.Implementations;
using ExpensesTracker.Domain.Infrastructure.Tokens;
using ExpensesTracker.Domain.Repositories.User;
using ExpensesTracker.Domain.Requests.User;
using ExpensesTracker.Domain.Responses.Token;
using ExpensesTracker.Domain.Results;

namespace ExpensesTracker.Application.User.Commands.Login;

public sealed record LoginUserCommand(LoginUserRequest Request) : ICommand<TokenResponse>;

public sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, TokenResponse>
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly IJwtGenerator _jwtGenerator;

    public LoginUserCommandHandler(IUserReadRepository userReadRepository, IJwtGenerator jwtGenerator)
    {
        _userReadRepository = userReadRepository;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<Result<TokenResponse>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _userReadRepository.GetUserByEmailAsync(command.Request.Email);

        if (user is null)
        {
            return Result.Failure<TokenResponse>(UserError.InvalidCredentials);
        }

        var token = _jwtGenerator.Generate(user);

        return new TokenResponse(token);
    }
}