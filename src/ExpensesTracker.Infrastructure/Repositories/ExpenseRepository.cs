using System.Data;
using ExpensesTracker.Domain.Entities;
using ExpensesTracker.Domain.Infrastructure.Repositories.Expenses;
using ExpensesTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Infrastructure.Repositories;

public class ExpenseRepository : IExpenseReadRepository, IExpenseWriteRepository
{
    private readonly IDbConnection _connection;
    private readonly DataContext _context;

    public ExpenseRepository(DataContext context)
    {
        _context = context;
        _connection = context.Database.GetDbConnection();
    }

    public async Task AddExpenseAsync(Expense expense)
    {
        await _context.Expenses.AddAsync(expense);
    }
}