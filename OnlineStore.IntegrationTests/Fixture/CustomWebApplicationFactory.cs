using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineShop.Persistence;

namespace OnlineStore.IntegrationTests.Fixture;

public class CustomWebApplicationFactory<TEntryPoint>(Action<DbContext> attachDbContext, string dbConnectionString)
    : WebApplicationFactory<TEntryPoint>
    where TEntryPoint : class
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureHostConfiguration(configurationBuilder =>
        {
            configurationBuilder.AddInMemoryCollection(new Dictionary<string, string?>
            {
                { "ConnectionStrings:TestingDbConnection", dbConnectionString }
            });
        });

        return base.CreateHost(builder);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.ConfigureTestServices(DecorateDbContext<OnlineStoreDbContext>);
    }

    private void DecorateDbContext<T>(IServiceCollection services) where T : DbContext
    {
        var descriptor = services.Single(serviceDescriptor => serviceDescriptor.ServiceType == typeof(T));

        if (descriptor.ImplementationFactory != null)
        {
            throw new NotImplementedException($"Cannot decorate {typeof(T)} which uses implementation factory");
        }

        if (descriptor.ImplementationInstance != null)
        {
            throw new NotImplementedException($"Cannot decorate {typeof(T)} which uses implementation instance");
        }

        services.AddScoped(serviceProvider =>
        {
            T dbContext = ActivatorUtilities.CreateInstance<T>(serviceProvider);
            attachDbContext(dbContext);
            return dbContext;
        });
    }
}