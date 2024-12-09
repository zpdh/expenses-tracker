namespace ExpensesTracker.Domain.Responses.Token;

public sealed record TokenResponse(string Token, string? RefreshToken = null);