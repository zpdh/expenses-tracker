namespace ExpensesTracker.Domain.Repositories;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}