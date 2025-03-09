using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Application.Repositories.Interfaces;
using OnlineShop.Persistence.Repositories;

namespace OnlineShop.Persistence;

public static class DependencyInjection
{
    private const string developmentConnectionString = "DevelopmentDbConnection";
    private const string testingConnectionString = "TestingDbConnection";

    public static IServiceCollection AddPersistence(
        this IServiceCollection services, 
        IConfiguration configuration,
        string environment)
    {
        string? connectionString = null;

        if (environment == "Testing")
        {
            connectionString = configuration.GetConnectionString(testingConnectionString);
        }
        else if (environment == "Development")
        {
            connectionString = configuration.GetConnectionString(developmentConnectionString);
        }

        if (connectionString == null)
        {
            throw new InvalidOperationException("Connection string is not initialized");
        }

        services.AddDbContext<OnlineStoreDbContext>(options =>
                options.UseNpgsql(connectionString));

        services.AddScoped<IRepositoryProduct, RepositoryProduct>();
        services.AddScoped<IRepositoryProductCategory, RepositoryProductCategory>();

        return services;
    }
}