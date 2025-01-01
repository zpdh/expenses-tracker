using ExpensesTracker.Domain.Infrastructure.Repositories.Category;
using Moq;

namespace TestUtils.Repositories.Category;

public class CategoryWriteRepositoryMock
{
    public static Mock<ICategoryWriteRepository> Create => new();
}