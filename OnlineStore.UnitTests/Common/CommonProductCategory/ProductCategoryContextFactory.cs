using Bogus;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.ProductCategories.Commands.ProductCategoryCreation;
using OnlineShop.Domain;
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

    private const int countProductCategoriesInDb = 10;

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

        var productCategoryFaker = new Faker<ProductCategory>()
            .RuleFor(productCategory => productCategory.Id, faker => faker.UniqueIndex + 4) // Start IDs from 4 to avoid collision with specific categories
            .RuleFor(productCategory => productCategory.Name, faker =>
            {
                var name = faker.Commerce.ProductName();
                return name.Length > CreateProductCategoryCommandValidation.MaxNameLength
                    ? name[..CreateProductCategoryCommandValidation.MaxNameLength]
                    : name;
            })
            .RuleFor(productCategory => productCategory.Description, faker =>
            {
                var description = faker.Commerce.ProductDescription();
                return description.Length > CreateProductCategoryCommandValidation.MaxDescriptionLength
                    ? description[..CreateProductCategoryCommandValidation.MaxDescriptionLength]
                    : description;
            });

        var productCategories = productCategoryFaker.Generate(countProductCategoriesInDb);

        var specificCategories = new List<ProductCategory>
        {
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
            }
        };

        productCategories.AddRange(specificCategories);

        _context.ProductCategories.AddRange(productCategories);
        _context.SaveChanges();

        return _context;
    }

    public void Destroy()
    {
        _context?.Database.EnsureDeleted();
        _context?.Dispose();
    }
}
