using OnlineShop.Application.Products.Commands.CreateProduct;
using OnlineShop.Application.Products.Commands.UpdateProduct;
using OnlineShop.Application.Products.Queries.GetProductDetails;
using OnlineShop.Application.Products.Queries.GetProductId;
using OnlineShop.Application.Products.Queries.GetProductRange;

namespace OnlineShop.Application.Repositories.Interfaces;

public interface IRepositoryProduct
{
    Task<int> AddAsync(CreateProductDto createProductDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task UpdateAsync(UpdateProductDto updateProductDto, CancellationToken cancellationToken = default);
    Task<GetProductIdVM> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<GetProductDetailsVM> GetDetailsByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ProductRangeVM> GetRangeAsync(int countSkip, int countTake, CancellationToken cancellationToken = default);
}