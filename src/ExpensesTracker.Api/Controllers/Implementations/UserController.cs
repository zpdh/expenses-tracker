using ExpensesTracker.Api.Accessors;
using ExpensesTracker.Api.Controllers.Base;
using ExpensesTracker.Application.User.Commands.Create;
using ExpensesTracker.Application.User.Commands.Delete;
using ExpensesTracker.Application.User.Commands.Login;
using ExpensesTracker.Application.User.Queries;
using ExpensesTracker.Domain.Enums;
using ExpensesTracker.Domain.Requests.User;
using ExpensesTracker.Infrastructure.Authentication.Permissions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.Api.Controllers.Implementations;

public class UserController : ApiController
{
    private readonly IUserAccessor _userAccessor;

    public UserController(ISender sender, IUserAccessor userAccessor) : base(sender)
    {
        _userAccessor = userAccessor;
    }

    [HttpGet]
    [HasPermission(Permission.Registered)]
    public async Task<IActionResult> GetUser(CancellationToken cancellationToken)
    {
        var userId = _userAccessor.GetRequestingUserId();
        var query = new GetUserByIdQuery(userId);

        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand(request);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Created() : HandleFailure(result);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginUserCommand(request);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }

    [HttpDelete]
    [HasPermission(Permission.Registered)]
    public async Task<IActionResult> DeleteUser(CancellationToken cancellationToken)
    {
        var userId = _userAccessor.GetRequestingUserId();
        var request = new DeleteUserRequest(userId);
        var command = new DeleteUserCommand(request);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }
}