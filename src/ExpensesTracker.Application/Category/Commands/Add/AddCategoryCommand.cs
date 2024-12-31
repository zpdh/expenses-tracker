using ExpensesTracker.Application.Base.Commands;
using ExpensesTracker.Domain.Infrastructure.Repositories;
using ExpensesTracker.Domain.Infrastructure.Repositories.Category;
using ExpensesTracker.Domain.Requests.Category;
using ExpensesTracker.Domain.Results;

namespace ExpensesTracker.Application.Category.Commands;

public sealed record AddCategoryCommand(AddCategoryRequest Request) : ICommand;

public sealed class AddCategoryCommandHandler : ICommandHandler<AddCategoryCommand>
{
    private readonly ICategoryWriteRepository _writeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddCategoryCommandHandler(ICategoryWriteRepository writeRepository, IUnitOfWork unitOfWork)
    {
        _writeRepository = writeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AddCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = Domain.Entities.Category.Create(command.Request.CategoryName);

        await AddToDatabaseAsync(category);

        return Result.Success();
    }

    private async Task AddToDatabaseAsync(Domain.Entities.Category category)
    {

        await _writeRepository.AddCategoryAsync(category);
        await _unitOfWork.SaveChangesAsync();
    }
}