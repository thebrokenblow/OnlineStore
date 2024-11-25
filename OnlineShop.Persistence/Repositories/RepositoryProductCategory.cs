using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Common.Exceptions;
using OnlineShop.Application.ProductCategories.Commands.CreateProductCategory;
using OnlineShop.Application.ProductCategories.Commands.UpdateProductCategory;
using OnlineShop.Application.Repositories.Interfaces;
using OnlineShop.Domain;

namespace OnlineShop.Persistence.Repositories;

public class RepositoryProductCategory(OnlineStoreDbContext context) : IRepositoryProductCategory
{
    public async Task<int> AddAsync(CreateProductCategoryCommand createProductCategory, CancellationToken cancellationToken)
    {
        var productCategory = new ProductCategory
        {
            Name = createProductCategory.Name,
            Description = createProductCategory.Description,
        };

        await context.AddAsync(productCategory, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return productCategory.Id;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var productCategory = await GetByIdAsync(id, cancellationToken);
        context.Remove(productCategory);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<ProductCategory> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        await context.ProductCategories.SingleOrDefaultAsync(
            productCategory => productCategory.Id == id, cancellationToken) ??
        throw new NotFoundException(nameof(ProductCategory), id);

    public Task<List<ProductCategory>> GetRangeAsync(int countSkip, int countTake, CancellationToken cancellationToken) =>
        context.ProductCategories
            .Skip(countSkip)
            .Take(countTake)
            .ToListAsync(cancellationToken);

    public async Task UpdateAsync(UpdateProductCategoryCommand updateProductCategory, CancellationToken cancellationToken)
    {
        var productCategory = await GetByIdAsync(updateProductCategory.Id, cancellationToken);

        try
        {
            productCategory.Name = updateProductCategory.Name;
            productCategory.Description = updateProductCategory.Description;

            await context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            var isProductCategoryExists = await IsExistsAsync(updateProductCategory.Id, cancellationToken);

            if (!isProductCategoryExists)
            {
                throw new NotFoundException(nameof(ProductCategory), updateProductCategory.Id);
            }
            else
            {
                throw;
            }
        }
    }

    public async Task<bool> IsExistsAsync(int id, CancellationToken cancellationToken) =>
        await context.ProductCategories.AnyAsync(
            productCategory => productCategory.Id == id, 
            cancellationToken);
}