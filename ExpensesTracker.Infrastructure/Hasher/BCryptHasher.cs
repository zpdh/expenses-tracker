using ExpensesTracker.Domain.Infrastructure.Hasher;

namespace ExpensesTracker.Infrastructure.Hasher;

public class BCryptHasher : IHasherService
{

    public string Hash(string cleanString)
    {
        return BCrypt.Net.BCrypt.HashPassword(cleanString);
    }

    public bool IsValid(string cleanString, string hashedString)
    {
        return BCrypt.Net.BCrypt.Verify(cleanString, hashedString);
    }

    public bool IsInvalid(string cleanString, string hashedString)
    {
        return !IsValid(cleanString, hashedString);
    }
}