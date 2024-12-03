using ExpensesTracker.Domain.Results;
using MediatR;

namespace ExpensesTracker.Application.Base.Queries;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> where TQuery : IQuery<TResponse>;