using ExpensesTracker.Application.Category.Queries;
using FluentAssertions;
using TestUtils.Repositories.Category;

namespace Application.Tests.Category.Queries;

public class GetAllCategoriesQueryTests
{
    private readonly List<ExpensesTracker.Domain.Entities.Category> _categories =
    [
        ExpensesTracker.Domain.Entities.Category.Create("Category1"),
        ExpensesTracker.Domain.Entities.Category.Create("Category2"),
        ExpensesTracker.Domain.Entities.Category.Create("Category3")
    ];

    [Fact]
    public async Task Result_Should_ReturnSuccess()
    {
        var query = new GetAllCategoriesQuery();
        var handler = CreateHandler();

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Categories.Should().Contain(_categories);
    }


    private GetAllCategoriesQueryHandler CreateHandler()
    {
        var readRepository = CategoryReadRepositoryMock.Create;

        CategoryReadRepositoryMock.SetupGetAllCategoriesAsync(readRepository, _categories);

        return new GetAllCategoriesQueryHandler(readRepository.Object);
    }

}