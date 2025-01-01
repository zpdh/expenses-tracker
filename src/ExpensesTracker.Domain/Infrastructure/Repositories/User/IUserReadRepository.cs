namespace ExpensesTracker.Domain.Infrastructure.Repositories.User;

public interface IUserReadRepository
{
    Task<Entities.User?> GetByIdAsync(int id);
    Task<Entities.User?> GetUserByEmailAsync(string email);
    Entities.User? GetUserByEmail(string email);

    bool IsEmailUnique(string email);
    Task<bool> IsEmailUniqueAsync(string email);
    bool UserExists(int id);
}