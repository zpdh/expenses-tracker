using ExpensesTracker.Application.Category.Commands;
using ExpensesTracker.Domain.Requests.Category;
using FluentAssertions;
using TestUtils.Repositories;
using TestUtils.Repositories.Category;

namespace Application.Tests.Category.Commands.Add;

public class AddCategoryCommandTests
{
    [Fact]
    public async Task Result_Should_ReturnSuccess()
    {
        var request = new AddCategoryRequest("CategoryName");
        var command = new AddCategoryCommand(request);
        var handler = CreateCommandHandler();

        var result = await handler.Handle(command, default);

        result.IsSuccess.Should().BeTrue();
    }

    private static AddCategoryCommandHandler CreateCommandHandler()
    {
        var writeRepository = CategoryWriteRepositoryMock.Create.Object;
        var unitOfWork = UnitOfWorkMock.Create.Object;

        return new AddCategoryCommandHandler(writeRepository, unitOfWork);
    }
}