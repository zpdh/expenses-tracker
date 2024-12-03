using ExpensesTracker.Domain.Repositories;
using ExpensesTracker.Domain.Repositories.User;
using ExpensesTracker.Infrastructure.Data;
using ExpensesTracker.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpensesTracker.Infrastructure;

public static class Dependencies
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddRepositories();

        return services;
    }

    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options => {
            var connectionString = configuration.GetConnectionString("MySql");
            var version = ServerVersion.AutoDetect(connectionString);

            options.UseMySql(connectionString, version);
        });
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserReadRepository, UserRepository>();
        services.AddScoped<IUserWriteRepository, UserRepository>();

        services.AddScoped<CategoryRepository>();
        services.AddScoped<CategoryRepository>();

        services.AddScoped<ExpenseRepository>();
        services.AddScoped<ExpenseRepository>();
    }
}