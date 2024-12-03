using ExpensesTracker.Api.Controllers.Base;
using ExpensesTracker.Application.User.Commands;
using ExpensesTracker.Domain.Requests.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.Api.Controllers.Implementations;

public class UserController : BaseController
{
    private readonly ISender _sender;

    public UserController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand(request);

        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
}