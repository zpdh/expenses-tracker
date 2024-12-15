using ExpensesTracker.Domain.Entities;

namespace ExpensesTracker.Domain.Infrastructure.Tokens;

public interface IJwtGenerator
{
    Task<string> GenerateAsync(User user);
}