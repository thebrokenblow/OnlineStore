using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain;
using OnlineShop.Persistence.Extensions;

namespace OnlineShop.Persistence;

public class OnlineStoreDbContext(DbContextOptions<OnlineStoreDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyAllConfigurations();
        base.OnModelCreating(modelBuilder);
    }
}