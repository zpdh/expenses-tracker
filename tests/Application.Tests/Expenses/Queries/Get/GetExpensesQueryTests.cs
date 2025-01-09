using ExpensesTracker.Application.Expenses.Queries;
using ExpensesTracker.Domain.Dtos;
using ExpensesTracker.Domain.Entities;
using ExpensesTracker.Domain.Requests.Expense;
using FluentAssertions;
using TestUtils.Repositories.Expenses;

namespace Application.Tests.Expenses.Queries.Get;

public class GetCategoriesQueryTests
{
    private readonly List<Expense> _expenses =
    [
        Expense.Create(1, 1, "Apple", 14.5),
        Expense.Create(2, 1, "Banana", 27.8),
        Expense.Create(3, 1, "Television", 71.1)
    ];

    private const int UserId = 92;

    [Fact]
    public async Task Handler_Should_ReturnSuccess_With_All_Expenses()
    {
        var request = new GetExpensesDto(UserId, "", DateTime.UtcNow.AddDays(-7));
        var query = new GetExpensesQuery(request);
        var handler = CreateHandler(request);

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Expenses.Should().Contain(_expenses);
    }

    [Fact]
    public async Task Handler_Should_ReturnSuccess_With_Expenses_That_Contain_A()
    {
        var request = new GetExpensesDto(UserId, "a", DateTime.UtcNow.AddDays(-7));
        var query = new GetExpensesQuery(request);
        var handler = CreateHandler(request);

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Expenses.Should().Contain(_expenses.Where(exp => exp.Name.Contains(request.Filter, StringComparison.CurrentCultureIgnoreCase)));
    }

    [Fact]
    public async Task Handler_Should_ReturnSuccess_With_Expenses_That_Contain_Different_Cases()
    {
        var request = new GetExpensesDto(UserId, "TELEVISION", DateTime.UtcNow.AddDays(-7));
        var query = new GetExpensesQuery(request);
        var handler = CreateHandler(request);

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Expenses.Should().Contain(_expenses.Where(exp => exp.Name.Contains(request.Filter, StringComparison.CurrentCultureIgnoreCase)));
    }

    [Fact]
    public async Task Handler_Should_ReturnSuccess_With_Different_Insertion_Dates()
    {
        var request = new GetExpensesDto(UserId, "TELEVISION", DateTime.UtcNow);
        var query = new GetExpensesQuery(request);
        var handler = CreateHandler(request);

        var result = await handler.Handle(query, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Expenses.Should().BeEmpty();
    }

    private GetExpensesQueryHandler CreateHandler(GetExpensesDto? dto = null)
    {
        var readRepository = ExpenseReadRepositoryMock.Create;

        if (dto is not null)
        {
            ExpenseReadRepositoryMock.SetupGetExpensesByUserIdAsync(readRepository, _expenses, dto);
        }

        return new GetExpensesQueryHandler(readRepository.Object);
    }
}