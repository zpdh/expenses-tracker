using ExpensesTracker.Api.Controllers.Base;
using ExpensesTracker.Application.User.Commands;
using ExpensesTracker.Application.User.Commands.Create;
using ExpensesTracker.Application.User.Commands.Login;
using ExpensesTracker.Application.User.Queries;
using ExpensesTracker.Domain.Enums;
using ExpensesTracker.Domain.Requests.User;
using ExpensesTracker.Infrastructure.Authentication.Permissions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.Api.Controllers.Implementations;

public class UserController(ISender sender) : ApiController(sender)
{
    [HttpGet]
    [HasPermission(Permission.Registered)]
    public async Task<IActionResult> GetUserById(int id, CancellationToken cancellationToken)
    {
        var command = new GetUserByIdQuery(id);

        var result = await Sender.Send(command, cancellationToken);

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
}