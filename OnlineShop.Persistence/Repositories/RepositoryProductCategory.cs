using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Common.Exceptions;
using OnlineShop.Application.ProductCategories.Commands.ProductCategoryUpdate;
using OnlineShop.Application.ProductCategories.Queries.GetAllProductCategory;
using OnlineShop.Application.ProductCategories.Queries.GetDetailsProductCategory;
using OnlineShop.Application.ProductCategories.Queries.GetRangeProductCategory;
using OnlineShop.Application.Repositories.Interfaces;
using OnlineShop.Domain;
using OnlineShop.Persistence.QueryObjects;

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

    public Task<List<RangeProductCategoryDto>> GetRangeAsync(int countSkip, int countTake, CancellationToken cancellationToken)
    {
        var rangeProductCategories = context.ProductCategories
                                                .Select(productCategory => new RangeProductCategoryDto
                                                {
                                                    Id = productCategory.Id,
                                                    Name = productCategory.Name,
                                                })
                                                .Page(countSkip, countTake)
                                                .AsNoTracking()
                                                .ToListAsync(cancellationToken);

        return rangeProductCategories;
    }

    public async Task<DetailsProductCategoryDto> GetDetailsAsync(int id, CancellationToken cancellationToken)
    {
        var detailsProductCategories = await context.ProductCategories
                                                        .Select(productCategory => new DetailsProductCategoryDto
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
        return detailsProductCategories;    
    }

    public async Task<List<AllProductCategoryDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var productCategories = await context.ProductCategories
                                                .Select(productCategory => new AllProductCategoryDto
                                                {
                                                    Id = productCategory.Id,
                                                    Name = productCategory.Name,
                                                })
                                                .AsNoTracking()
                                                .ToListAsync(cancellationToken);

        return productCategories;
    }

    public async Task<ProductCategory> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var productCategory = await context.ProductCategories
                                                .SingleOrDefaultAsync(
                                                        productCategory => productCategory.Id == id, 
                                                        cancellationToken)
                                ?? throw new NotFoundException(nameof(ProductCategory), id);

        return productCategory;
    }
}