using ExpensesTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpensesTracker.Infrastructure;

public static class Dependencies
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
    }

    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options => {
            var connectionString = configuration.GetConnectionString("MySql");
            var version = ServerVersion.AutoDetect(connectionString);

            options.UseMySql(connectionString, version);
        });
    }
}