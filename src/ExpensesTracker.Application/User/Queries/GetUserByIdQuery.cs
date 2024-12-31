using ExpensesTracker.Application.Base.Queries;
using ExpensesTracker.Domain.Errors.Base;
using ExpensesTracker.Domain.Errors.Implementations;
using ExpensesTracker.Domain.Infrastructure.Repositories.User;
using ExpensesTracker.Domain.Responses.User;
using ExpensesTracker.Domain.Results;

namespace ExpensesTracker.Application.User.Queries;

public sealed record GetUserByIdQuery(int Id) : IQuery<GetUserByIdResponse>;

public sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, GetUserByIdResponse>
{
    private readonly IUserReadRepository _readRepository;

    public GetUserByIdQueryHandler(IUserReadRepository readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<Result<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _readRepository.GetByIdAsync(request.Id);

        if (user is null)
        {
            return Result.Failure<GetUserByIdResponse>(UserErrors.UserNotFound(request.Id));
        }

        var response = new GetUserByIdResponse(user.Id, user.Name, user.Email);

        return response;
    }
}