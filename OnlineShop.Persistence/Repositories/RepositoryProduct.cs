using OnlineShop.Domain;
using OnlineShop.Application.Repositories.Interfaces;
using OnlineShop.Application.Products.Commands.CreateProduct;
using OnlineShop.Application.Products.Commands.UpdateProduct;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Persistence.Repositories;

public class RepositoryProduct(OnlineStoreDbContext context) : IRepositoryProduct
{
    public async Task<int> AddAsync(CreateProductDto createProductDto, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = createProductDto.Name,
            Description = createProductDto.Description,
            Price = createProductDto.Price,
            ProductCategory = createProductDto.ProductCategory
        };

        await context.AddAsync(product, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return product.Id;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var product = await GetByIdAsync(id, cancellationToken);
        context.Product.Remove(product);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        await context.Product.SingleOrDefaultAsync(product => product.Id == id, cancellationToken) ??
        throw new Exception();

    public async Task<Product> GetDetailsByIdAsync(int id, CancellationToken cancellationToken) =>
        await context.Product.Include(x => x.ProductCategory).SingleOrDefaultAsync(product => product.Id == id, cancellationToken) ??
        throw new Exception();

    public async Task<List<Product>> GetRangeAsync(int countSkip, int countTake, CancellationToken cancellationToken) =>
        await context.Product.Skip(countSkip).Take(countTake).ToListAsync(cancellationToken);

    public async Task UpdateAsync(UpdateProductDto updateProductDto, CancellationToken cancellationToken) =>
        await context.Product.Where(product => product.Id == updateProductDto.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(product => product.Name, updateProductDto.Name)
                .SetProperty(product => product.Description, updateProductDto.Description)
                .SetProperty(product => product.Price, updateProductDto.Price)
                .SetProperty(product => product.ProductCategory, updateProductDto.ProductCategory), cancellationToken);
}