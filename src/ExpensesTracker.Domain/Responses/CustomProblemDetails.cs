using ExpensesTracker.Domain.Errors.Base;

namespace ExpensesTracker.Domain.Responses;

public sealed class CustomProblemDetails
{
    public string Title { get; set; }
    public string Detail { get; set; }
    public int StatusCode { get; set; }
    public Error[]? Errors { get; set; } = [];
}