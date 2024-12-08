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

    public bool IsEmailUnique(string email)
    {
        const string query = "SELECT * From Users WHERE Email = @email";
        var parameters = new { Email = email };

        var user = _connection.QueryFirstOrDefault<User>(query, parameters);

        return user is null;
    }

    public async Task<bool> IsEmailUniqueAsync(string email)
    {
        const string query = "SELECT * From Users WHERE Email = @email";
        var parameters = new { Email = email };

        var user = await _connection.QueryFirstOrDefaultAsync<User>(query, parameters);

        return user is null;
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task UpdateAsync(User user)
    {
        var userInDb = await _context.Users.FirstAsync(u => u.Id == user.Id);

        userInDb.Name = user.Name;
        userInDb.Email = user.Email;
    }

    public async Task UpdatePasswordAsync(User user)
    {
        var userInDb = await _context.Users.FirstAsync(u => u.Id == user.Id);

        userInDb.Password = user.Password;
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _context.Users.FirstAsync(u => u.Id == id);

        _context.Remove(user);
    }
}