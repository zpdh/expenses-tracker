using ExpensesTracker.Api.Controllers.Base;
using ExpensesTracker.Application.User.Commands;
using ExpensesTracker.Application.User.Queries;
using ExpensesTracker.Domain.Requests.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.Api.Controllers.Implementations;

public class UserController(ISender sender) : ApiController(sender)
{
    [HttpGet]
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
}