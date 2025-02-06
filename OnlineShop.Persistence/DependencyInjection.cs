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
        if (environment == "Development")
        {
            var connectionString = configuration.GetConnectionString(developmentConnectionString);

            services.AddDbContext<OnlineStoreDbContext>(options =>
                options.UseNpgsql(connectionString));
        }
        else if (environment == "Testing")
        {
            var connectionString = configuration.GetConnectionString(testingConnectionString);

            services.AddDbContext<OnlineStoreDbContext>(options =>
                options.UseNpgsql(connectionString));
        }

        services.AddScoped<IRepositoryProduct, RepositoryProduct>();
        services.AddScoped<IRepositoryProductCategory, RepositoryProductCategory>();

        return services;
    }
}