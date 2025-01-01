using ExpensesTracker.Api.Controllers.Base;
using ExpensesTracker.Application.Expenses.Commands.Add;
using ExpensesTracker.Domain.Enums;
using ExpensesTracker.Domain.Requests.Expense;
using ExpensesTracker.Infrastructure.Authentication.Permissions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.Api.Controllers.Implementations;

[HasPermission(Permission.Registered)]
public class ExpensesController : ApiController
{
    public ExpensesController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    [HasPermission(Permission.Registered)]
    public async Task<IActionResult> AddExpense([FromBody] AddExpenseRequest request, CancellationToken cancellationToken)
    {
        var command = new AddExpenseCommand(request);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Created() : HandleFailure(result);
    }
}