using ExpensesTracker.Application.Base.Queries;
using ExpensesTracker.Domain.Infrastructure.Repositories.Category;
using ExpensesTracker.Domain.Responses.Category;
using ExpensesTracker.Domain.Results;

namespace ExpensesTracker.Application.Category.Queries;

public sealed record GetAllCategoriesQuery : IQuery<GetAllCategoriesResponse>;

public sealed class GetAllCategoriesQueryHandler : IQueryHandler<GetAllCategoriesQuery, GetAllCategoriesResponse>
{
    private readonly ICategoryReadRepository _readRepository;

    public GetAllCategoriesQueryHandler(ICategoryReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<Result<GetAllCategoriesResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _readRepository.GetAllCategoriesAsync();

        var response = new GetAllCategoriesResponse(categories);

        return response;
    }
}