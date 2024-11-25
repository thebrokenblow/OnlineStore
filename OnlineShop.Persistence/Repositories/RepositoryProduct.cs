using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Common.Exceptions;
using OnlineShop.Application.Products.Commands.CreateProduct;
using OnlineShop.Application.Products.Commands.UpdateProduct;
using OnlineShop.Application.Repositories.Interfaces;
using OnlineShop.Domain;

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
        await context.Product
        .SingleOrDefaultAsync(
            product => product.Id == id, 
            cancellationToken) ??
        throw new NotFoundException(nameof(Product), id);

    public async Task<Product> GetDetailsByIdAsync(int id, CancellationToken cancellationToken) =>
        await context.Product
            .Include(product => product.ProductCategory)
            .SingleOrDefaultAsync(
                product => product.Id == id, 
                cancellationToken) ??
        throw new NotFoundException(nameof(Product), id);


    public async Task<List<Product>> GetRangeAsync(int countSkip, int countTake, CancellationToken cancellationToken) =>
        await context.Product
            .Skip(countSkip)
            .Take(countTake)
            .ToListAsync(cancellationToken);

    public async Task UpdateAsync(UpdateProductDto updateProductDto, CancellationToken cancellationToken)
    {
        await using var transaction = context.Database.BeginTransaction();

        try
        {
            var product = await GetByIdAsync(updateProductDto.Id, cancellationToken);

            product.Name = updateProductDto.Name;
            product.Description = updateProductDto.Description;
            product.Price = updateProductDto.Price;
            product.ProductCategory = updateProductDto.ProductCategory;

            await context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            var isClientExistsAsync = await IsExistsAsync(updateProductDto.Id, cancellationToken);

            if (!isClientExistsAsync)
            {
                throw new NotFoundException(nameof(Product), updateProductDto.Id);
            }
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);

            throw;
        }
    }

    public async Task<bool> IsExistsAsync(int id, CancellationToken cancellationToken) =>
        await context.Product.AnyAsync(product => product.Id == id, cancellationToken);
}