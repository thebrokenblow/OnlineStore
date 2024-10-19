using OnlineShop.Application.ProductCategories.Commands.CreateProductCategory;
using OnlineShop.Application.ProductCategories.Commands.DeleteProductCategory;
using OnlineShop.Application.ProductCategories.Commands.UpdateProductCategory;
using OnlineShop.Application.ProductCategories.Queries.GetProductCategoryRange;
using OnlineShop.Domain;

namespace OnlineShop.Application.Repositories.Interfaces;

public interface IRepositoryProductCategory
{
    Task<int> AddAsync(CreateProductCategoryCommand createProductCategory, CancellationToken cancellationToken = default);
    Task DeleteAsync(DeleteProductCategoryCommand deleteProductCategory, CancellationToken cancellationToken = default);
    Task UpdateAsync(UpdateProductCategoryCommand updateProductCategory, CancellationToken cancellationToken = default);
    Task<ProductCategoryRangeVM> GetRangeAsync(int countSkip, int countTake, CancellationToken cancellationToken = default);
    Task<ProductCategory> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}