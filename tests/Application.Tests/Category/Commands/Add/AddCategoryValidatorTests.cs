using ExpensesTracker.Application.Category.Commands;
using ExpensesTracker.Application.Category.Commands.Add;
using ExpensesTracker.Domain.Errors.Implementations;
using ExpensesTracker.Domain.Requests.Category;
using FluentAssertions;
using TestUtils.Repositories.Category;

namespace Application.Tests.Category.Commands.Add;

public class AddCategoryValidatorTests
{
    [Fact]
    public async Task Result_Should_ReturnSuccess()
    {
        var request = new AddCategoryRequest("CategoryName");
        var command = new AddCategoryCommand(request);
        var validator = CreateValidator(request.CategoryName);

        var result = await validator.ValidateAsync(command);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Result_Should_ReturnFailure_When_Name_Is_Empty()
    {
        var request = new AddCategoryRequest(string.Empty);
        var command = new AddCategoryCommand(request);
        var validator = CreateValidator(request.CategoryName);

        var result = await validator.ValidateAsync(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(err => err.ErrorMessage == CategoryErrors.EmptyName.ErrorMessage);
    }

    [Fact]
    public async Task Result_Should_ReturnFailure_When_Name_Is_Not_Unique()
    {
        var request = new AddCategoryRequest("CategoryName");
        var command = new AddCategoryCommand(request);
        var validator = CreateValidator();

        var result = await validator.ValidateAsync(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(err => err.ErrorMessage == CategoryErrors.CategoryExists.ErrorMessage);
    }

    private static AddCategoryCommandValidator CreateValidator(string? name = null)
    {
        var readRepository = CategoryReadRepositoryMock.Create;

        if (name is not null)
        {
            CategoryReadRepositoryMock.SetupCategoryWithNameDoesNotExist(readRepository, name);
        }

        return new AddCategoryCommandValidator(readRepository.Object);
    }
}