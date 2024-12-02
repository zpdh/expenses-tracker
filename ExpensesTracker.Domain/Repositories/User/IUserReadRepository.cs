namespace ExpensesTracker.Domain.Repositories.User;

public interface IUserReadRepository
{
    Task<Entities.User?> GetByIdAsync(int id);
}