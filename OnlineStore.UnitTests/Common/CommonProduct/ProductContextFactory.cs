using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain;
using OnlineShop.Persistence;

namespace OnlineStore.UnitTests.Common.CommonProduct;

public class ProductContextFactory
{
    public int IdElectronicProductCategory { get; }
    public ProductCategory ElectronicProductCategory { get; }

    public int IdBookProductCategory { get; }
    public ProductCategory BookProductCategory { get; }

    public int IdClothingProductCategory { get; }
    public ProductCategory ClothingProductCategory { get; }

    public int IdProductForDelete { get; }
    public int IdProductForUpdate { get; }

    public int IdProductForDetails { get; }
    public Product ProductForDetails { get; }
    public ProductCategory ProductCategoryForDetails { get; }


    private OnlineStoreDbContext? _context;

    public ProductContextFactory()
    {
        IdElectronicProductCategory = 1;
        ElectronicProductCategory = new()
        {
            Id = IdElectronicProductCategory,
            Name = "Electronics",
            Description = "Electronic devices"
        };

        IdBookProductCategory = 2;
        BookProductCategory = new()
        {
            Id = IdBookProductCategory,
            Name = "Books",
            Description = "Books of all genres"
        };

        IdClothingProductCategory = 3;
        ClothingProductCategory = new()
        {
            Id = IdClothingProductCategory,
            Name = "Clothing",
            Description = "Clothing for all ages"
        };

        IdProductForDelete = 4;
        IdProductForUpdate = 5;
        IdProductForDetails = 6;

        ProductForDetails = new()
        {
            Id = IdProductForDetails,
            Name = "Novel",
            Description = "An exciting novel",
            Price = 19.99m,
            IdProductCategory = IdBookProductCategory,
            ProductCategory = BookProductCategory
        };

        ProductCategoryForDetails = new()
        {
            Id = IdBookProductCategory,
            Name = "Books",
            Description = "Books of all genres"
        };
    }

    public OnlineStoreDbContext Create()
    {
        var options = new DbContextOptionsBuilder<OnlineStoreDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new OnlineStoreDbContext(options);
        _context.Database.EnsureCreated();

        _context.AddRange(
            ElectronicProductCategory,
            BookProductCategory,
            ClothingProductCategory);

        _context.SaveChanges();

        var products = new List<Product>
        {
            new()
            {
                Id = IdProductForDelete,
                Name = "Smartphone",
                Description = "A high-end smartphone",
                Price = 999.99m,
                IdProductCategory = IdElectronicProductCategory,
                ProductCategory = ElectronicProductCategory
            },
            new()
            {
                Id = IdProductForUpdate,
                Name = "Laptop",
                Description = "A powerful laptop",
                Price = 1499.99m,
                IdProductCategory = IdElectronicProductCategory,
                ProductCategory = ElectronicProductCategory
            },
            new()
            {
                Id = IdProductForDetails,
                Name = "Novel",
                Description = "An exciting novel",
                Price = 19.99m,
                IdProductCategory = IdBookProductCategory,
                ProductCategory = BookProductCategory
            },
            new()
            {
                Id = 7,
                Name = "Textbook",
                Description = "An educational textbook",
                Price = 49.99m,
                IdProductCategory = IdBookProductCategory,
                ProductCategory = BookProductCategory
            },
            new()
            {
                Id = 8,
                Name = "T-Shirt",
                Description = "A comfortable T-Shirt",
                Price = 9.99m,
                IdProductCategory = IdClothingProductCategory,
                ProductCategory = ClothingProductCategory
            },
            new()
            {
                Id = 9,
                Name = "Jeans",
                Description = "A pair of jeans",
                Price = 29.99m,
                IdProductCategory = IdClothingProductCategory,
                ProductCategory = ClothingProductCategory
            },
            new()
            {
                Id = 10,
                Name = "Tablet",
                Description = "A versatile tablet",
                Price = 299.99m,
                IdProductCategory = IdElectronicProductCategory,
                ProductCategory = ElectronicProductCategory
            },
            new()
            {
                Id = 11,
                Name = "Cookbook",
                Description = "A cookbook with delicious recipes",
                Price = 24.99m,
                IdProductCategory = IdBookProductCategory,
                ProductCategory = BookProductCategory
            },
            new()
            {
                Id = 12,
                Name = "Sweater",
                Description = "A warm sweater",
                Price = 39.99m,
                IdProductCategory = IdClothingProductCategory,
                ProductCategory = ClothingProductCategory
            },
            new()
            {
                Id = 13,
                Name = "Headphones",
                Description = "High-quality headphones",
                Price = 199.99m,
                IdProductCategory = IdElectronicProductCategory,
                ProductCategory = ElectronicProductCategory
            }
        };

        _context.AddRange(products);
        _context.SaveChanges();

        return _context;
    }

    public void Destroy()
    {
        _context?.Database.EnsureDeleted();
        _context?.Dispose();
    }
}
