using ExpensesTracker.Application.Base.Queries;
using ExpensesTracker.Domain.Dtos;
using ExpensesTracker.Domain.Infrastructure.Repositories.Expenses;
using ExpensesTracker.Domain.Requests.Expense;
using ExpensesTracker.Domain.Responses.Expenses;
using ExpensesTracker.Domain.Results;

namespace ExpensesTracker.Application.Expenses.Queries;

public sealed record GetExpensesQuery(GetExpensesDto Dto) : IQuery<GetExpensesResponse>;

public sealed class GetExpensesQueryHandler : IQueryHandler<GetExpensesQuery, GetExpensesResponse>
{
    private readonly IExpenseReadRepository _readRepository;

    public GetExpensesQueryHandler(IExpenseReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<Result<GetExpensesResponse>> Handle(GetExpensesQuery query, CancellationToken cancellationToken)
    {
        var expenses = await _readRepository.GetExpensesByUserIdAsync(query.Dto);

        var response = new GetExpensesResponse(expenses);

        return response;
    }
}