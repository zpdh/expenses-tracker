using ExpensesTracker.Domain.Infrastructure.Hasher;
using ExpensesTracker.Domain.Infrastructure.Repositories;
using ExpensesTracker.Domain.Infrastructure.Repositories.Category;
using ExpensesTracker.Domain.Infrastructure.Repositories.Expenses;
using ExpensesTracker.Domain.Infrastructure.Repositories.User;
using ExpensesTracker.Domain.Infrastructure.Tokens;
using ExpensesTracker.Infrastructure.Authentication;
using ExpensesTracker.Infrastructure.Authentication.Jwt;
using ExpensesTracker.Infrastructure.Authentication.Permissions;
using ExpensesTracker.Infrastructure.Data;
using ExpensesTracker.Infrastructure.Hasher;
using ExpensesTracker.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ExpensesTracker.Infrastructure;

public static class Dependencies
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddRepositories();
        services.AddAuthenticationServices();
        services.AddHasher();
        services.AddPermissions();
        services.AddServices();

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

        services.AddScoped<ICategoryReadRepository, CategoryRepository>();
        services.AddScoped<ICategoryWriteRepository, CategoryRepository>();

        services.AddScoped<IExpenseReadRepository, ExpenseRepository>();
        services.AddScoped<IExpenseWriteRepository, ExpenseRepository>();
    }

    private static void AddAuthenticationServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtGenerator, JwtGenerator>();
    }

    private static void AddHasher(this IServiceCollection services)
    {
        services.AddScoped<IHasherService, BCryptHasher>();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPermissionService, PermissionService>();
    }

    private static void AddPermissions(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
    }
}