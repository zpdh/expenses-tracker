using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTracker.Api.ExceptionHandler;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Type = exception.GetType().Name,
                    Title = "An unknown error occurred while processing your request.",
                    Detail = exception.Message
                },
                cancellationToken);

            return true;
    }
}