using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain;
using System.Reflection;

namespace OnlineShop.Persistence;

public class OnlineStoreDbContext(DbContextOptions<OnlineStoreDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}