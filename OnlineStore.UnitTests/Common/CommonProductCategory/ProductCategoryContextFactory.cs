using Microsoft.EntityFrameworkCore;
using OnlineShop.Persistence;

namespace OnlineStore.UnitTests.Common.CommonProductCategory;

public class ProductCategoryContextFactory
{
    public int ProductCategoryIdForDelete { get; }
    public int ProductCategoryIdForUpdate { get; }
    public int ProductCategoryIdForDetails { get; }

    public string ProductCategoryNameGardenTools { get; }
    public string ProductCategoryDescriptionGardenTools { get; }


    private OnlineStoreDbContext? _context;

    public ProductCategoryContextFactory()
    {
        ProductCategoryIdForDelete = 1;
        ProductCategoryIdForUpdate = 2;
        ProductCategoryIdForDetails = 3;

        ProductCategoryNameGardenTools = "Garden Tools";
        ProductCategoryDescriptionGardenTools = "Tools and equipment for gardening";
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
                Name = "Home Decor",
                Description = "Items for home decoration"
            },
            new()
            {
                Id = ProductCategoryIdForUpdate,
                Name = "Kitchenware",
                Description = "Utensils and appliances for the kitchen"
            },
            new()
            {
                Id = ProductCategoryIdForDetails,
                Name = ProductCategoryNameGardenTools,
                Description = ProductCategoryDescriptionGardenTools
            },
            new()
            {
                Id = 4,
                Name = "Pet Supplies",
                Description = "Supplies and accessories for pets"
            },
            new()
            {
                Id = 5,
                Name = "Office Supplies",
                Description = "Supplies and equipment for the office"
            },
            new()
            {
                Id = 6,
                Name = "Health & Wellness",
                Description = "Products for health and wellness"
            },
            new()
            {
                Id = 7,
                Name = "Automotive",
                Description = "Parts and accessories for vehicles"
            },
            new()
            {
                Id = 8,
                Name = "Jewelry",
                Description = "Jewelry and accessories"
            },
            new()
            {
                Id = 9,
                Name = "Toys & Games",
                Description = "Toys and games for children"
            },
            new()
            {
                Id = 10,
                Name = "Furniture",
                Description = "Furniture for home and office"
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