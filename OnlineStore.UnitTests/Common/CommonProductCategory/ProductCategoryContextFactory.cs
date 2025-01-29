using Microsoft.EntityFrameworkCore;
using OnlineShop.Persistence;

namespace OnlineStore.UnitTests.Common.CommonProductCategory;

public class ProductCategoryContextFactory
{
    public int ProductCategoryIdForDelete { get; }
    public int ProductCategoryIdForUpdate { get; }
    public int ProductCategoryIdForDetails { get; }

    public string ProductCategoryName3 { get; }
    public string ProductCategoryDescription3 { get; }


    private OnlineStoreDbContext? _context;

    public ProductCategoryContextFactory()
    {
        ProductCategoryIdForDelete = 1;
        ProductCategoryIdForUpdate = 2;
        ProductCategoryIdForDetails = 3;

        ProductCategoryName3 = "Category3";
        ProductCategoryDescription3 = "Description for Category3";
    }

    public OnlineStoreDbContext Create()
    {
        var options = new DbContextOptionsBuilder<OnlineStoreDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new OnlineStoreDbContext(options);
        _context.Database.EnsureCreated();

        _context.ProductCategories.AddRange(
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
                Name = ProductCategoryName3,
                Description = ProductCategoryDescription3
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

        _context.SaveChanges();

        return _context;
    }

    public void Destroy()
    {
        _context?.Database.EnsureDeleted();
        _context?.Dispose();
    }
}