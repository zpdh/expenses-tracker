using ExpensesTracker.Api.Accessors;
using ExpensesTracker.Api.Controllers.Base;
using ExpensesTracker.Application.Expenses.Commands.Add;
using ExpensesTracker.Application.Expenses.Commands.Delete;
using ExpensesTracker.Application.Expenses.Queries;
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

    [HttpGet]
    public async Task<IActionResult> GetExpenses([FromBody] GetExpensesRequest request, CancellationToken cancellationToken)
    {
        var userId = _userAccessor.GetRequestingUserId();
        var dto = new GetExpensesDto(userId, request.Filter);
        var query = new GetExpensesQuery(dto);

        var result = await Sender.Send(query, cancellationToken);

        return Ok(result.Value.Expenses);
    }

    [HttpPost]
    public async Task<IActionResult> AddExpense([FromBody] AddExpenseRequest request, CancellationToken cancellationToken)
    {
        var userId = _userAccessor.GetRequestingUserId();
        var dto = new AddExpenseDto(request.CategoryId, userId, request.Name, request.Price);
        var command = new AddExpenseCommand(dto);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Created() : HandleFailure(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteExpense([FromBody] DeleteExpenseRequest request, CancellationToken cancellationToken)
    {
        var userId = _userAccessor.GetRequestingUserId();
        var dto = new DeleteExpenseDto(userId, request.ExpenseId);
        var command = new DeleteExpenseCommand(dto);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }
}