using Bogus;
using OnlineShop.Persistence;
using OnlineShop.Persistence.Repositories;

namespace OnlineStore.UnitTests.Common.CommonProductCategory;

public abstract class TestProductCategoryBase : IDisposable
{
    protected readonly OnlineStoreDbContext _context;
    protected readonly RepositoryProductCategory _productCategoryRepository;

    protected readonly ProductCategoryContextFactory _productCategoryContextFactory = new();

    protected readonly Faker _faker = new();
    public TestProductCategoryBase()
    {
        _context = _productCategoryContextFactory.Create();
        _productCategoryRepository = new(_context);
    }

    public void Dispose()
    {
        _productCategoryContextFactory.Destroy();
    }
}