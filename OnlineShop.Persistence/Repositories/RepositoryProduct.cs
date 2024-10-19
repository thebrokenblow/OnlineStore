using OnlineShop.Domain;
using OnlineShop.Application.Repositories.Interfaces;
using OnlineShop.Application.Products.Commands.CreateProduct;
using OnlineShop.Application.Products.Commands.UpdateProduct;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Common.Exceptions;

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
        throw new NotFoundException(nameof(Product), id);

    public async Task<Product> GetDetailsByIdAsync(int id, CancellationToken cancellationToken) =>
        await context.Product.Include(x => x.ProductCategory).SingleOrDefaultAsync(product => product.Id == id, cancellationToken) ??
        throw new NotFoundException(nameof(Product), id);


    public async Task<List<Product>> GetRangeAsync(int countSkip, int countTake, CancellationToken cancellationToken) =>
        await context.Product
            .Skip(countSkip)
            .Take(countTake)
            .ToListAsync(cancellationToken);

    public async Task UpdateAsync(UpdateProductDto updateProductDto, CancellationToken cancellationToken)
    {
        var product = await GetByIdAsync(updateProductDto.Id, cancellationToken);

        product.Name = updateProductDto.Name;
        product.Description = updateProductDto.Description;
        product.Price = updateProductDto.Price;
        product.ProductCategory = updateProductDto.ProductCategory;

        await context.SaveChangesAsync(cancellationToken);
    }
}