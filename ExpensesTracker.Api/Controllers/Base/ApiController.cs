using ExpensesTracker.Domain.Errors.Base;
using ExpensesTracker.Domain.Responses;
using ExpensesTracker.Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.Api.Controllers.Base;

[Route("api/[controller]")]
public abstract class ApiController : ControllerBase
{
    protected readonly ISender Sender;

    protected ApiController(ISender sender)
    {
        Sender = sender;
    }

    protected IActionResult HandleFailure(Result result)
    {
        return result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException("You cannot handle a result success' failure."),
            IValidationResult validationResult => HandleValidationResult(result, validationResult),
            _ => HandleBadRequestResult(result)
        };
    }

    private BadRequestObjectResult HandleBadRequestResult(Result result)
    {

        return BadRequest(
            CreateProblemDetails(
                "Bad Request",
                StatusCodes.Status400BadRequest,
                result.Error));
    }

    private BadRequestObjectResult HandleValidationResult(Result result, IValidationResult validationResult)
    {

        return BadRequest(
            CreateProblemDetails(
                "Validation Error",
                StatusCodes.Status400BadRequest,
                result.Error,
                validationResult.Errors));
    }

    private static CustomProblemDetails CreateProblemDetails(string title, int statusCode, Error error, Error[]? errors = null)
    {
        return new CustomProblemDetails
        {
            Title = title,
            Detail = error.ErrorMessage,
            StatusCode = statusCode,
            Errors = errors
        };
    }
}