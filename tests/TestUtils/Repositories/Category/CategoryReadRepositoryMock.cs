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
}