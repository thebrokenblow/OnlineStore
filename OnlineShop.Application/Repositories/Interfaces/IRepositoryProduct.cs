using OnlineShop.Application.Products.Commands.CreateProduct;
using OnlineShop.Application.Products.Commands.UpdateProduct;
using OnlineShop.Domain;

namespace OnlineShop.Application.Repositories.Interfaces;

public interface IRepositoryProduct
{
    Task<int> AddAsync(CreateProductDto createProductDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task UpdateAsync(UpdateProductDto updateProductDto, CancellationToken cancellationToken = default);
    Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Product> GetDetailsByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<List<Product>> GetRangeAsync(int countSkip, int countTake, CancellationToken cancellationToken = default);
    Task<bool> IsExistsAsync(int id, CancellationToken cancellationToken = default);
}