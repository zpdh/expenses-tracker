using ExpensesTracker.Domain.Entities.Base;

namespace ExpensesTracker.Domain.Entities;

public class Permission
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}