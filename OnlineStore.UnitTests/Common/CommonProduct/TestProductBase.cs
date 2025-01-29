using OnlineShop.Persistence;
using OnlineShop.Persistence.Repositories;

namespace OnlineStore.UnitTests.Common.CommonProduct;

public abstract class TestProductBase : IDisposable
{
    protected readonly OnlineStoreDbContext _context;

    protected readonly RepositoryProduct _repositoryProduct;
    protected readonly RepositoryProductCategory _repositoryProductCategory;

    protected readonly ProductContextFactory _factoryProductCategoryContext = new();

    public TestProductBase()
    {
        _context = _factoryProductCategoryContext.Create();

        _repositoryProduct = new(_context);
        _repositoryProductCategory = new(_context);
    }

    public void Dispose()
    {
        _factoryProductCategoryContext.Destroy();
    }
}
