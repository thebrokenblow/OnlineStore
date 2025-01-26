using Microsoft.EntityFrameworkCore;
using OnlineShop.Persistence;

namespace OnlineStore.UnitTests.Common;

public class ProductCategoryContextFactory
{
    public static int ProductCategoryIdForDelete => 1;
    public static int ProductCategoryIdForUpdate => 2;
    public static int ProductCategoryIdForDetails => 3;

    public static OnlineStoreDbContext Create()
    {
        var options = new DbContextOptionsBuilder<OnlineStoreDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new OnlineStoreDbContext(options);
        context.Database.EnsureCreated();

        context.ProductCategories.AddRange(
            new()
            {
                Id = ProductCategoryIdForDelete,
                Name = "Category1",
                Description = "Description for Category1"
            },
            new()
            {
                Id = ProductCategoryIdForUpdate,
                Name = "Category2",
                Description = "Description for Category2"
            },
            new()
            {
                Id = ProductCategoryIdForDetails,
                Name = "Category3",
                Description = "Description for Category3"
            },
            new()
            {
                Id = 4,
                Name = "Category4",
                Description = "Description for Category4"
            },
            new()
            {
                Id = 5,
                Name = "Category5",
                Description = "Description for Category5"
            },
            new()
            {
                Id = 6,
                Name = "Category6",
                Description = "Description for Category6"
            },
            new()
            {
                Id = 7,
                Name = "Category7",
                Description = "Description for Category7"
            },
            new()
            {
                Id = 8,
                Name = "Category8",
                Description = "Description for Category8"
            },
            new()
            {
                Id = 9,
                Name = "Category9",
                Description = "Description for Category9"
            },
            new()
            {
                Id = 10,
                Name = "Category10",
                Description = "Description for Category10"
            }
        );

        context.SaveChanges();

        return context;
    }

    public static void Destroy(OnlineStoreDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}