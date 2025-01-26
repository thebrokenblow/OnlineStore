using OnlineShop.Application.Products.Commands.ProductUpdate;
using OnlineShop.Application.Products.Queries.GetAllProduct;
using OnlineShop.Application.Products.Queries.GetDetailsProduct;
using OnlineShop.Application.Products.Queries.GetRangeProduct;
using OnlineShop.Domain;

namespace OnlineShop.Application.Repositories.Interfaces;

public interface IRepositoryProduct
{
    Task<int> AddAsync(Product product, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task UpdateAsync(UpdateProductDto updateProductDto, CancellationToken cancellationToken = default);
    Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<GetDetailsProductDto> GetDetailsByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<List<GetRangeProductDto>> GetRangeAsync(int countSkip, int countTake, CancellationToken cancellationToken = default);
    Task<List<GetAllProductDto>> GetAllAsync(CancellationToken cancellationToken);
}