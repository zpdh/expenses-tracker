using ExpensesTracker.Application.Expenses.Commands.Delete;
using ExpensesTracker.Domain.Dtos;
using ExpensesTracker.Domain.Errors.Base;
using FluentAssertions;
using TestUtils.Repositories;
using TestUtils.Repositories.Expenses;

namespace Application.Tests.Expenses.Commands.Delete;

public class DeleteExpenseCommandTests
{
    private const int ExpenseId = 18;
    private const int UserId = 91;

    [Fact]
    public async Task Handler_Should_ReturnSuccess()
    {
        var dto = new DeleteExpenseDto(UserId, ExpenseId);
        var command = new DeleteExpenseCommand(dto);
        var handler = CreateHandler();

        var result = await handler.Handle(command, default);

        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(Error.None);
    }

    private static DeleteExpenseCommandHandler CreateHandler()
    {
        var writeRepository = ExpenseWriteRepositoryMock.Create;
        var unitOfWork = UnitOfWorkMock.Create;

        return new DeleteExpenseCommandHandler(writeRepository.Object, unitOfWork.Object);
    }
}