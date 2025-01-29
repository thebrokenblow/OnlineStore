using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain;
using OnlineShop.Persistence;

namespace OnlineStore.UnitTests.Common;

public class ProductContextFactory
{
    public static List<ProductCategory>? ProductCategories { get; private set; }

    public ProductContextFactory()
    {
        
    }

    public static OnlineStoreDbContext Create()
    {
        var options = new DbContextOptionsBuilder<OnlineStoreDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new OnlineStoreDbContext(options);
        context.Database.EnsureCreated();

        ProductCategories =
        [
            new()
            {
                Name = "Electronics",
                Description = "Electronic devices"
            },
            new()
            {
                Name = "Books",
                Description = "Books of all genres"
            },
            new()
            {
                Name = "Clothing",
                Description = "Clothing for all ages"
            }
        ];

        context.AddRange(ProductCategories);
        context.SaveChanges();

        var products = new List<Product>
            {
                new()
                {
                    Name = "Smartphone",
                    Description = "A high-end smartphone",
                    Price = 999.99m,
                    IdProductCategory = ProductCategories[0].Id,
                    ProductCategory = ProductCategories[0]
                },
                new()
                {
                    Name = "Laptop",
                    Description = "A powerful laptop",
                    Price = 1499.99m,
                    IdProductCategory = ProductCategories[0].Id,
                    ProductCategory = ProductCategories[0]
                },
                new()
                {
                    Name = "Novel",
                    Description = "An exciting novel",
                    Price = 19.99m,
                    IdProductCategory = ProductCategories[1].Id,
                    ProductCategory = ProductCategories[1]
                },
                new()
                {
                    Name = "Textbook",
                    Description = "An educational textbook",
                    Price = 49.99m,
                    IdProductCategory = ProductCategories[1].Id,
                    ProductCategory = ProductCategories[1]
                },
                new()
                {
                    Name = "T-Shirt",
                    Description = "A comfortable T-Shirt",
                    Price = 9.99m,
                    IdProductCategory = ProductCategories[2].Id,
                    ProductCategory = ProductCategories[2]
                },
                new()
                {
                    Name = "Jeans",
                    Description = "A pair of jeans",
                    Price = 29.99m,
                    IdProductCategory = ProductCategories[2].Id,
                    ProductCategory = ProductCategories[2]
                },
                new()
                {
                    Name = "Tablet",
                    Description = "A versatile tablet",
                    Price = 299.99m,
                    IdProductCategory = ProductCategories[0].Id,
                    ProductCategory = ProductCategories[0]
                },
                new()
                {
                    Name = "Cookbook",
                    Description = "A cookbook with delicious recipes",
                    Price = 24.99m,
                    IdProductCategory = ProductCategories[1].Id,
                    ProductCategory = ProductCategories[1]
                },
                new()
                {
                    Name = "Sweater",
                    Description = "A warm sweater",
                    Price = 39.99m,
                    IdProductCategory = ProductCategories[2].Id,
                    ProductCategory = ProductCategories[2]
                },
                new()
                {
                    Name = "Headphones",
                    Description = "High-quality headphones",
                    Price = 199.99m,
                    IdProductCategory = ProductCategories[0].Id,
                    ProductCategory = ProductCategories[0]
                }
            };


        context.AddRange(products);
        context.SaveChanges();

        return context;
    }

    public static void Destroy(OnlineStoreDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}
