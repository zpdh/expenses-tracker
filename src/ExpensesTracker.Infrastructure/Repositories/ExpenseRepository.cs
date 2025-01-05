using System.Data;
using Dapper;
using ExpensesTracker.Domain.Entities;
using ExpensesTracker.Domain.Infrastructure.Repositories.Expenses;
using ExpensesTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Infrastructure.Repositories;

public class ExpenseRepository : IExpenseReadRepository, IExpenseWriteRepository
{
    private readonly IDbConnection _connection;
    private readonly DataContext _context;


    public async Task<List<Expense>> GetExpensesByUserId(int userId)
    {
        const string query = "SELECT * FROM Expenses WHERE UserId = @userId";
        var parameters = new { UserId = userId };

        var expenses = await _connection.QueryAsync<Expense>(query, parameters);

        return expenses.ToList();
    }

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