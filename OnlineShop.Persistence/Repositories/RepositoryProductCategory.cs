using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Common.Exceptions;
using OnlineShop.Application.ProductCategories.Commands.ProductCategoryUpdate;
using OnlineShop.Application.ProductCategories.Queries.GetAllProductCategory;
using OnlineShop.Application.ProductCategories.Queries.GetDetailsProductCategory;
using OnlineShop.Application.ProductCategories.Queries.GetRangeProductCategory;
using OnlineShop.Application.Repositories.Interfaces;
using OnlineShop.Domain;

namespace OnlineShop.Persistence.Repositories;

public class RepositoryProductCategory(OnlineStoreDbContext context) : IRepositoryProductCategory
{
    public async Task<int> AddAsync(ProductCategory productCategory, CancellationToken cancellationToken)
    {
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

    public async Task UpdateAsync(UpdateProductCategoryDto updateProductCategory, CancellationToken cancellationToken)
    {
        var productCategory = await GetByIdAsync(updateProductCategory.Id, cancellationToken);

        productCategory.Name = updateProductCategory.Name;
        productCategory.Description = updateProductCategory.Description;

        await context.SaveChangesAsync(cancellationToken);
    }

    public Task<List<GetRangeProductCategoryDto>> GetRangeAsync(int countSkip, int countTake, CancellationToken cancellationToken) =>
        context.ProductCategories
            .Select(productCategory => new GetRangeProductCategoryDto
            {
                Id = productCategory.Id,
                Name = productCategory.Name,
            })
            .Skip(countSkip)
            .Take(countTake)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task<GetDetailsProductCategoryDto> GetDetailsAsync(int id, CancellationToken cancellationToken) =>
        await context.ProductCategories
                    .Select(productCategory => new GetDetailsProductCategoryDto
                    { 
                        Id = productCategory.Id,
                        Name = productCategory.Name,
                        Description = productCategory.Description
                    })
                    .AsNoTracking()
                    .SingleOrDefaultAsync(
                            productCategory => productCategory.Id == id,
                            cancellationToken)
                                ?? throw new NotFoundException(nameof(ProductCategory), id);

    public async Task<List<GetAllProductCategoryDto>> GetAllAsync(CancellationToken cancellationToken) =>
        await context.ProductCategories
                    .Select(productCategory => new GetAllProductCategoryDto
                    {
                        Id = productCategory.Id,
                        Name = productCategory.Name,
                    })
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

    public async Task<ProductCategory> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        await context.ProductCategories
                        .SingleOrDefaultAsync(
                            productCategory => productCategory.Id == id, cancellationToken)
                                ?? throw new NotFoundException(nameof(ProductCategory), id);
}