using ExpensesTracker.Api.Controllers.Base;
using ExpensesTracker.Application.Category.Commands;
using ExpensesTracker.Application.Category.Queries;
using ExpensesTracker.Domain.Enums;
using ExpensesTracker.Domain.Requests.Category;
using ExpensesTracker.Infrastructure.Authentication.Permissions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.Api.Controllers.Implementations;

public class CategoryController : ApiController
{
    public CategoryController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [HasPermission(Permission.Registered)]
    public async Task<IActionResult> GetAllCategories(CancellationToken cancellationToken)
    {
        var query = new GetAllCategoriesQuery();

        var result = await Sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }

    [HttpPost]
    [HasPermission(Permission.Administrator)]
    public async Task<IActionResult> AddCategory([FromBody] AddCategoryRequest request, CancellationToken cancellationToken)
    {
        var command = new AddCategoryCommand(request);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Created() : HandleFailure(result);
    }
}