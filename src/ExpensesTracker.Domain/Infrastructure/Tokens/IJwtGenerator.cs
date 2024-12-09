using ExpensesTracker.Domain.Entities;

namespace ExpensesTracker.Domain.Infrastructure.Tokens;

public interface IJwtGenerator
{
    string Generate(User user);
}