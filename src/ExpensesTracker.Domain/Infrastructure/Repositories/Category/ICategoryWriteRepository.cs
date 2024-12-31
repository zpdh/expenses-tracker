namespace ExpensesTracker.Domain.Infrastructure.Repositories.Category;

public interface ICategoryWriteRepository
{
    Task AddCategoryAsync(Entities.Category category);
}