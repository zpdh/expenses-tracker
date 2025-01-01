using ExpensesTracker.Api.Accessors;
using ExpensesTracker.Api.Controllers.Base;
using ExpensesTracker.Application.Expenses.Commands.Add;
using ExpensesTracker.Domain.Dtos;
using ExpensesTracker.Domain.Enums;
using ExpensesTracker.Domain.Requests.Expense;
using ExpensesTracker.Infrastructure.Authentication.Permissions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.Api.Controllers.Implementations;

[HasPermission(Permission.Registered)]
public class ExpensesController : ApiController
{
    private readonly IUserAccessor _userAccessor;

    public ExpensesController(ISender sender, IUserAccessor userAccessor) : base(sender)
    {
        _userAccessor = userAccessor;
    }

    [HttpPost]
    [HasPermission(Permission.Registered)]
    public async Task<IActionResult> AddExpense([FromBody] AddExpenseRequest request, CancellationToken cancellationToken)
    {
        var userId = _userAccessor.GetRequestingUserId();

        var dto = new AddExpenseDto(request.CategoryId, userId, request.Name, request.Price);
        var command = new AddExpenseCommand(dto);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Created() : HandleFailure(result);
    }
}