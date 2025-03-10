﻿using OnlineShop.Application.ProductCategories.Commands.ProductCategoryUpdate;
using OnlineShop.Application.ProductCategories.Queries.GetAllProductCategory;
using OnlineShop.Application.ProductCategories.Queries.GetDetailsProductCategory;
using OnlineShop.Application.ProductCategories.Queries.GetRangeProductCategory;
using OnlineShop.Domain;

namespace OnlineShop.Application.Repositories.Interfaces;

public interface IRepositoryProductCategory
{
    Task<int> AddAsync(ProductCategory productCategory, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task UpdateAsync(UpdateProductCategoryDto updateProductCategory, CancellationToken cancellationToken = default);
    Task<DetailsProductCategoryDto> GetDetailsAsync(int id, CancellationToken cancellationToken = default);
    Task<List<AllProductCategoryDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<RangeProductCategoryDto>> GetRangeAsync(int countSkip, int countTake, CancellationToken cancellationToken = default);
    Task<ProductCategory> GetByIdAsync(int idProductCategory, CancellationToken cancellationToken);
}