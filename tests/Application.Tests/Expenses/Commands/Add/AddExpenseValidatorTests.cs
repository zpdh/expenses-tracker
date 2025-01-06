using ExpensesTracker.Application.Expenses.Commands.Add;
using ExpensesTracker.Domain.Dtos;
using ExpensesTracker.Domain.Errors.Implementations;
using ExpensesTracker.Domain.Requests.Expense;
using FluentAssertions;
using TestUtils.Repositories.Category;
using TestUtils.Repositories.User;

namespace Application.Tests.Expenses.Commands.Add;

public class AddExpenseValidatorTests
{
    private const int CategoryId = 5;
    private const int UserId = 18;

    [Fact]
    public async Task Validator_Should_ReturnSuccess()
    {
        var dto = new AddExpenseDto(CategoryId, UserId, "Expense", 12.35);
        var command = new AddExpenseCommand(dto);
        var validator = CreateValidator(dto.UserId, dto.CategoryId);

        var result = await validator.ValidateAsync(command);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validator_Should_ReturnFailure_When_UserId_Is_Invalid()
    {
        var dto = new AddExpenseDto(CategoryId, UserId, "Expense", 12.35);
        var command = new AddExpenseCommand(dto);
        var validator = CreateValidator(categoryId: dto.CategoryId);

        var result = await validator.ValidateAsync(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(err => err.ErrorMessage == ExpenseErrors.InvalidUserId.ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnFailure_When_CategoryId_Is_Invalid()
    {
        var dto = new AddExpenseDto(CategoryId, UserId, "Expense", 12.35);
        var command = new AddExpenseCommand(dto);
        var validator = CreateValidator(dto.UserId);

        var result = await validator.ValidateAsync(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(err => err.ErrorMessage == ExpenseErrors.InvalidCategoryId.ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnFailure_When_Name_Is_Empty()
    {
        var dto = new AddExpenseDto(CategoryId, UserId, string.Empty, 12.35);
        var command = new AddExpenseCommand(dto);
        var validator = CreateValidator(dto.UserId, dto.CategoryId);

        var result = await validator.ValidateAsync(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(err => err.ErrorMessage == ExpenseErrors.EmptyName.ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnFailure_When_Price_Is_Zero()
    {
        var dto = new AddExpenseDto(CategoryId, UserId, "Expense", 0);
        var command = new AddExpenseCommand(dto);
        var validator = CreateValidator(dto.UserId, dto.CategoryId);

        var result = await validator.ValidateAsync(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(err => err.ErrorMessage == ExpenseErrors.InvalidPrice.ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnFailure_When_Price_Is_Negative()
    {
        var dto = new AddExpenseDto(CategoryId, UserId, "Expense", -27);
        var command = new AddExpenseCommand(dto);
        var validator = CreateValidator(dto.UserId, dto.CategoryId);

        var result = await validator.ValidateAsync(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(err => err.ErrorMessage == ExpenseErrors.InvalidPrice.ErrorMessage);
    }

    private static AddExpenseCommandValidator CreateValidator(int? userId = null, int? categoryId = null)
    {
        var userReadRepository = UserReadRepositoryMock.Create;
        var categoryReadRepository = CategoryReadRepositoryMock.Create;

        if (userId is not null)
        {
            UserReadRepositoryMock.SetupUserExists(userReadRepository, (int)userId);
        }

        if (categoryId is not null)
        {
            CategoryReadRepositoryMock.SetupCategoryExists(categoryReadRepository, (int)categoryId);
        }

        return new AddExpenseCommandValidator(userReadRepository.Object, categoryReadRepository.Object);
    }
}