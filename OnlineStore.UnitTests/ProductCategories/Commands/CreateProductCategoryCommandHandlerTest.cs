﻿using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.ProductCategories.Commands.ProductCategoryCreation;
using OnlineStore.UnitTests.Common.CommonProductCategory;

namespace OnlineStore.UnitTests.ProductCategories.Commands;

public class CreateProductCategoryCommandHandlerTest : TestProductCategoryBase
{
    [Fact]
    public async Task CreateProductCategoryCommandHandler_Success()
    {
        // Arrange
        var createProductCategoryCommandValidation = new CreateProductCategoryCommandValidation();
        var handler = new CreateProductCategoryCommandHandler(_productCategoryRepository, createProductCategoryCommandValidation);

        //Количество категорий продукта уже с новой добавленной категорией
        var countProductCategory = _context.ProductCategories.Count() + 1;

        var productCategoryName = "Home Decor";
        var productCategoryDescription = "Items for home decoration, including wall art, decorative pillows, and home accents.";

        var createProductCategoryCommand = new CreateProductCategoryCommand
        {
            Name = productCategoryName,
            Description = productCategoryDescription
        };

        //Act
        var productCategoryId = await handler.Handle(
            createProductCategoryCommand, 
            CancellationToken.None);

        // Assert
        var productCategory = await _context.ProductCategories.SingleOrDefaultAsync(
            productCategory =>
                productCategory.Id == productCategoryId &&
                productCategory.Name == productCategoryName &&
                productCategory.Description == productCategoryDescription);

        Assert.NotNull(productCategory);

        Assert.Equal(countProductCategory, _context.ProductCategories.Count());
    }
}