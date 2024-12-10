using ExpensesTracker.Application.Base.Commands;
using ExpensesTracker.Domain.Results;

namespace ExpensesTracker.Application.Category.Commands;

public sealed record AddCategoryCommand : ICommand;

public sealed class AddCategoryCommandHandler : ICommandHandler<AddCategoryCommand>
{
    public async Task<Result> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {


        return Result.Success();
    }
}