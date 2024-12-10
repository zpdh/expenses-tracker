namespace ExpensesTracker.Domain.Repositories.Category;

public interface ICategoryWriteRepository
{
    Task AddCategoryAsync(Entities.Category category);
}