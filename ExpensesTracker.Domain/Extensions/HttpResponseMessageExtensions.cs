using System.Net.Http.Json;
using ExpensesTracker.Domain.Responses;

namespace ExpensesTracker.Domain.Extensions;

public static class HttpResponseMessageExtensions
{
    public static async Task<CustomProblemDetails> GetProblemDetailsAsync(this HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException("Cannot convert a successful response into problem details.");
        }

        var problemDetails = await response.Content.ReadFromJsonAsync<CustomProblemDetails>();

        if (problemDetails is null)
        {
            throw new NullReferenceException("Problem details is null.");
        }

        return problemDetails;
    }
}