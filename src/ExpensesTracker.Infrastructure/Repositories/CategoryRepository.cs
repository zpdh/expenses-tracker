using System.Data;
using Dapper;
using ExpensesTracker.Domain.Entities;
using ExpensesTracker.Domain.Infrastructure.Repositories.Category;
using ExpensesTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Infrastructure.Repositories;

public class CategoryRepository : ICategoryReadRepository, ICategoryWriteRepository
{
    private readonly DataContext _context;
    private readonly IDbConnection _connection;

    public CategoryRepository(DataContext context)
    {
        _context = context;
        _connection = context.Database.GetDbConnection();
    }

    public async Task<List<Category>?> GetAllCategoriesAsync()
    {
        const string query = "SELECT * FROM Categories";

        var categories = await _connection.QueryAsync<Category>(query);

        return categories.AsList();
    }

    public bool CategoryWithNameDoesNotExist(string name)
    {
        return GetCategoryByName(name) is null;
    }

    public bool CategoryExists(int id)
    {
        const string query = "SELECT COUNT(1) FROM Categories WHERE Id = @id";
        var parameters = new { Id = id };

        var count = _connection.ExecuteScalar<int>(query, parameters);

        return count > 0;
    }

    private Category? GetCategoryByName(string name)
    {
        const string query = "SELECT * FROM Categories WHERE Name = @name";
        var parameters = new { Name = name };

        var category = _connection.QueryFirstOrDefault<Category>(query, parameters);

        return category;
    }

    public async Task AddCategoryAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
    }
}