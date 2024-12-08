namespace ExpensesTracker.Domain.Repositories.User;

public interface IUserReadRepository
{
    Task<Entities.User?> GetByIdAsync(int id);
    bool IsEmailUnique(string email);
    Task<bool> IsEmailUniqueAsync(string email);
}