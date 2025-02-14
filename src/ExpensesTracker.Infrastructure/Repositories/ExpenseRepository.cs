﻿using System.Data;
using Dapper;
using ExpensesTracker.Domain.Dtos;
using ExpensesTracker.Domain.Entities;
using ExpensesTracker.Domain.Extensions;
using ExpensesTracker.Domain.Infrastructure.Repositories.Expenses;
using ExpensesTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Infrastructure.Repositories;

public class ExpenseRepository : IExpenseReadRepository, IExpenseWriteRepository
{
    private readonly IDbConnection _connection;
    private readonly DataContext _context;


    public async Task<List<Expense>> GetExpensesByUserIdAsync(GetExpensesDto request)
    {
        var query = "SELECT * FROM Expenses WHERE UserId = @userId AND InsertionDate >= @since";

        var parameters = new DynamicParameters();
        parameters.Add("UserId", request.UserId);
        parameters.Add("Since", request.Since);

        if (!request.Filter.IsNullOrWhitespace())
        {
            query += " AND Name LIKE @filter";
            parameters.Add("Filter", $"%{request.Filter}%");
        }

        var expenses = await _connection.QueryAsync<Expense>(query, parameters);

        return expenses.ToList();
    }

    public bool ExpenseExists(int userId, int expenseId)
    {
        const string query = "SELECT COUNT(1) FROM Expenses WHERE UserId = @userId AND Id = @id";
        var parameters = new { UserId = userId, Id = expenseId };

        var count = _connection.ExecuteScalar<int>(query, parameters);

        return count > 0;
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

    public async Task DeleteExpenseAsync(int userId, int expenseId)
    {
        var expense = await _context.Expenses.FirstAsync(exp => exp.UserId == userId && exp.Id == expenseId);

        _context.Remove(expense);
    }
}