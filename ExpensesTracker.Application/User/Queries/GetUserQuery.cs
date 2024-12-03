using MediatR;

namespace ExpensesTracker.Application.User.Queries;

public sealed record GetUserQuery(int Id) : IRequest;