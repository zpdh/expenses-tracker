using ExpensesTracker.Domain.Infrastructure.Repositories.Category;
using Moq;

namespace TestUtils.Repositories.Category;

public class CategoryReadRepositoryMock
{
    public static Mock<ICategoryReadRepository> Create => new();

    public static void SetupCategoryWithNameDoesNotExist(Mock<ICategoryReadRepository> mock, string name)
    {
        mock.Setup(moq => moq.CategoryWithNameDoesNotExist(name)).Returns(true);
    }

    public static void SetupGetAllCategoriesAsync(Mock<ICategoryReadRepository> mock, List<ExpensesTracker.Domain.Entities.Category> categories)
    {
        mock.Setup(moq => moq.GetAllCategoriesAsync()).ReturnsAsync(categories);
    }

    public static void SetupCategoryExists(Mock<ICategoryReadRepository> mock, int id)
    {
        mock.Setup(moq => moq.CategoryExists(id)).Returns(true);
    }
}