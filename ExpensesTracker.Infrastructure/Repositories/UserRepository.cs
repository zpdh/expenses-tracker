using System.Data;
using Dapper;
using ExpensesTracker.Domain.Entities;
using ExpensesTracker.Domain.Repositories.User;
using ExpensesTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Infrastructure.Repositories;

public class UserRepository : IUserReadRepository, IUserWriteRepository
{
    private readonly DataContext _context;
    private readonly IDbConnection _connection;

    public UserRepository(DataContext context)
    {
        _context = context;
        _connection = context.Database.GetDbConnection();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        const string query = "SELECT * From Users WHERE Id = @id";
        var parameters = new { Id = id };

        var user = await _connection.QuerySingleOrDefaultAsync<User>(query, parameters);

        return user;
    }
}