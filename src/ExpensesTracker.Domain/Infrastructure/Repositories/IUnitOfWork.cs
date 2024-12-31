namespace ExpensesTracker.Domain.Infrastructure.Repositories;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}