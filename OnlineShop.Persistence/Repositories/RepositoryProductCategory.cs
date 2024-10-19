﻿using OnlineShop.Domain;
using OnlineShop.Application.Repositories.Interfaces;
using OnlineShop.Application.ProductCategories.Commands.CreateProductCategory;
using OnlineShop.Application.ProductCategories.Commands.UpdateProductCategory;
using Microsoft.EntityFrameworkCore;

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
        await context.ProductCategories.SingleOrDefaultAsync(productCategory => productCategory.Id == id, cancellationToken) ?? 
        throw new Exception();

    public Task<List<ProductCategory>> GetRangeAsync(int countSkip, int countTake, CancellationToken cancellationToken) =>
        context.ProductCategories.Skip(countSkip).Take(countTake).ToListAsync(cancellationToken);

    public async Task UpdateAsync(UpdateProductCategoryCommand updateProductCategory, CancellationToken cancellationToken) =>
        await context.ProductCategories
        .Where(product => product.Id == updateProductCategory.Id)
        .ExecuteUpdateAsync(s => s
            .SetProperty(productCategory => productCategory.Name, updateProductCategory.Name)
            .SetProperty(productCategory => productCategory.Description, updateProductCategory.Description), 
                cancellationToken);
}