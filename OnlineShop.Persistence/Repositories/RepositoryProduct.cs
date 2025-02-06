﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Common.Exceptions;
using OnlineShop.Application.ProductCategories.Queries.GetDetailsProductCategory;
using OnlineShop.Application.Products.Commands.ProductUpdate;
using OnlineShop.Application.Products.Queries.GetAllProduct;
using OnlineShop.Application.Products.Queries.GetDetailsProduct;
using OnlineShop.Application.Products.Queries.GetRangeProduct;
using OnlineShop.Application.Repositories.Interfaces;
using OnlineShop.Domain;

namespace OnlineShop.Persistence.Repositories;

public class RepositoryProduct(OnlineStoreDbContext context, IRepositoryProductCategory repositoryProductCategory) : IRepositoryProduct
{
    public async Task<int> AddAsync(Product product, CancellationToken cancellationToken)
    {
        var productCategory = await repositoryProductCategory.GetByIdAsync(product.IdProductCategory, cancellationToken);
        product.ProductCategory = productCategory;

        await context.AddAsync(product, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return product.Id;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var product = await GetByIdTrackingAsync(id, cancellationToken);

        context.Products.Remove(product);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(UpdateProductDto updateProductDto, CancellationToken cancellationToken)
    {
        var product = await GetByIdTrackingAsync(updateProductDto.Id, cancellationToken);
        var productCategory = await repositoryProductCategory.GetByIdAsync(updateProductDto.IdProductCategory, cancellationToken);

        product.Name = updateProductDto.Name;
        product.Description = updateProductDto.Description;
        product.Price = updateProductDto.Price;
        product.ProductCategory = productCategory;

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<GetRangeProductDto>> GetRangeAsync(int countSkip, int countTake, CancellationToken cancellationToken) =>
        await context.Products
                        .Select(product => new GetRangeProductDto
                        {
                            Id = product.Id,
                            Name = product.Name,
                        })
                        .Skip(countSkip)
                        .Take(countTake)
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);

    public async Task<GetDetailsProductDto> GetDetailsByIdAsync(int id, CancellationToken cancellationToken)
    {
        var product = await context.Products
                                   .Include(product => product.ProductCategory)
                                   .AsNoTracking()
                                   .SingleOrDefaultAsync(product => product.Id == id, cancellationToken) ?? throw new NotFoundException(nameof(Product), id);
        
        var detailsProductDto = CreateDetailsProductDto(product);

        return detailsProductDto;
    }


    private async Task<Product> GetByIdTrackingAsync(int id, CancellationToken cancellationToken) =>
        await context.Products.SingleOrDefaultAsync(
                product => product.Id == id, cancellationToken)
                    ?? throw new NotFoundException(nameof(ProductCategory), id);

    public async Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        await context.Products.SingleOrDefaultAsync(
                    product => product.Id == id, cancellationToken)
                        ?? throw new NotFoundException(nameof(ProductCategory), id);

    public async Task<List<GetAllProductDto>> GetAllAsync(CancellationToken cancellationToken) =>
         await context.Products
                        .Select(product => new GetAllProductDto
                        {
                            Id = product.Id,
                            Name = product.Name,
                        })
                        .ToListAsync(cancellationToken);

    private static GetDetailsProductDto CreateDetailsProductDto(Product product)
    {
        var productCategory = new GetDetailsProductCategoryDto
        {
            Id = product.ProductCategory!.Id,
            Name = product.ProductCategory.Name,
            Description = product.ProductCategory.Description,
        };

        var detailsProductDto = new GetDetailsProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            ProductCategory = productCategory,
        };

        return detailsProductDto;
    }
}