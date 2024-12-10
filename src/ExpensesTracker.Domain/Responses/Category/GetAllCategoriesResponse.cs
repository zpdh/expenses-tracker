namespace ExpensesTracker.Domain.Responses.Category;

public sealed record GetAllCategoriesResponse(List<Entities.Category>? Categories);