using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Application.Repositories.Interfaces;
using OnlineShop.Persistence.Repositories;

namespace OnlineShop.Persistence;

public static class DependencyInjection
{
    private const string keyConnectionString = "DbConnection";
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration[keyConnectionString];
        services.AddDbContext<OnlineStoreDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IRepositoryProduct, RepositoryProduct>();
        services.AddScoped<IRepositoryProductCategory, RepositoryProductCategory>();

        return services;
    }
}
