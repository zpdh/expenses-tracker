using ExpensesTracker.Domain.Entities;

namespace ExpensesTracker.Domain.Repositories.User;

public interface IUserWriteRepository
{
    Task AddAsync(Entities.User user);
    Task UpdateAsync(Entities.User user);
    Task UpdatePasswordAsync(Entities.User user);
    Task DeleteAsync(int id);
}