using ExpensesTracker.Application.Expenses.Commands.Delete;
using ExpensesTracker.Domain.Dtos;
using ExpensesTracker.Domain.Errors.Implementations;
using FluentAssertions;
using TestUtils.Repositories.Expenses;

namespace Application.Tests.Expenses.Commands.Delete;

public class DeleteExpenseValidatorTests
{
    private const int ExpenseId = 12;
    private const int UserId = 77;

    [Fact]
    public async Task Validator_Should_ReturnSuccess()
    {
        var dto = new DeleteExpenseDto(UserId, ExpenseId);
        var command = new DeleteExpenseCommand(dto);
        var validator = CreateValidator(dto.UserId, dto.ExpenseId);

        var result = await validator.ValidateAsync(command);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validator_Should_ReturnFailure_When_UserId_Is_Invalid()
    {
        var dto = new DeleteExpenseDto(UserId, ExpenseId);
        var command = new DeleteExpenseCommand(dto);
        var validator = CreateValidator(expenseId: dto.ExpenseId);

        var result = await validator.ValidateAsync(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(err => err.ErrorMessage == ExpenseErrors.NotFound.ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnFailure_When_ExpenseId_Is_Invalid()
    {
        var dto = new DeleteExpenseDto(UserId, ExpenseId);
        var command = new DeleteExpenseCommand(dto);
        var validator = CreateValidator(dto.UserId);

        var result = await validator.ValidateAsync(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(err => err.ErrorMessage == ExpenseErrors.NotFound.ErrorMessage);
    }

    private DeleteExpenseCommandValidator CreateValidator(int? userId = null, int? expenseId = null)
    {
        var readRepository = ExpenseReadRepositoryMock.Create;

        if (userId is not null && expenseId is not null)
        {
            ExpenseReadRepositoryMock.SetupExpenseExists(readRepository, (int)userId, (int)expenseId);
        }

        return new DeleteExpenseCommandValidator(readRepository.Object);
    }
}