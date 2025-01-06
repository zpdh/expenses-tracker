using ExpensesTracker.Application.Expenses.Queries;
using ExpensesTracker.Domain.Entities;
using ExpensesTracker.Domain.Requests.Expense;
using FluentAssertions;
using TestUtils.Repositories.Expenses;

namespace Application.Tests.Expenses.Queries.Get;

public class GetCategoriesQueryTests
{
    private readonly List<Expense> _expenses =
    [
        Expense.Create(1, 1, "Expense1", 14.5),
        Expense.Create(2, 1, "Expense2", 27.8),
        Expense.Create(3, 1, "Expense3", 71.1)
    ];

    private const int UserId = 92;

    [Fact]
    public async Task Handler_Should_ReturnSuccess()
    {
        var request = new GetExpensesRequest(UserId);
        var query = new GetExpensesQuery(request);
        var handler = CreateHandler(UserId);

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Expenses.Should().Contain(_expenses);
    }

    private GetExpensesQueryHandler CreateHandler(int? userId = null)
    {
        var readRepository = ExpenseReadRepositoryMock.Create;

        if (userId is not null)
        {
            ExpenseReadRepositoryMock.SetupGetExpensesByUseridAsync(readRepository, _expenses, (int)userId);
        }

        return new GetExpensesQueryHandler(readRepository.Object);
    }
}