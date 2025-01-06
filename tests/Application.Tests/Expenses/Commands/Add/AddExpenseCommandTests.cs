using ExpensesTracker.Application.Expenses.Commands.Add;
using ExpensesTracker.Domain.Dtos;
using ExpensesTracker.Domain.Errors.Base;
using FluentAssertions;
using TestUtils.Repositories;
using TestUtils.Repositories.Expenses;

namespace Application.Tests.Expenses.Commands.Add;

public class AddExpenseCommandTests
{
    private const int CategoryId = 3;
    private const int UserId = 7;

    [Fact]
    public async Task Handler_Should_ReturnSuccess()
    {
        var dto = new AddExpenseDto(CategoryId, UserId, "Expense", 15.0);
        var command = new AddExpenseCommand(dto);
        var handler = CreateHandler();

        var result = await handler.Handle(command, default);

        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(Error.None);
    }

    private static AddExpenseCommandHandler CreateHandler()
    {
        var writeRepository = ExpenseWriteRepositoryMock.Create.Object;
        var unitOfWork = UnitOfWorkMock.Create.Object;

        return new AddExpenseCommandHandler(writeRepository, unitOfWork);
    }
}