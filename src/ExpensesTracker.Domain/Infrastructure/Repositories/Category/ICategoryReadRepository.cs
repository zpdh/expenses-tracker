namespace ExpensesTracker.Domain.Infrastructure.Repositories.Category;

public interface ICategoryReadRepository
{
    Task<List<Entities.Category>?> GetAllCategoriesAsync();
    bool CategoryWithNameDoesNotExist(string name);
    bool CategoryExists(int id);
}