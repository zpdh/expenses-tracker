using ExpensesTracker.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;

    public UnitOfWork(DbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}