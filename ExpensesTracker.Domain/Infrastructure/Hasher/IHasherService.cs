namespace ExpensesTracker.Domain.Infrastructure.Hasher;

public interface IHasherService
{
    string Hash(string cleanString);
    bool IsValid(string cleanString, string hashedString);
    bool IsInvalid(string cleanString, string hashedString);
}