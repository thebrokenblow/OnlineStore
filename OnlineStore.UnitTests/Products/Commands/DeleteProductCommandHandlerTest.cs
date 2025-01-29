﻿using OnlineShop.Application.Common.Exceptions;
using OnlineShop.Application.ProductCategories.Commands.ProductCategoryDeletion;
using OnlineShop.Application.Products.Commands.ProductDeletion;
using OnlineStore.UnitTests.Common.CommonProduct;

namespace OnlineStore.UnitTests.Products.Commands;

public class DeleteProductCommandHandlerTest : TestProductBase
{
    [Fact]
    public async Task DeleteProductCommandHandler_Success()
    {
        // Arrange

        var handler = new DeleteProductCommandHandler(_repositoryProduct);

        // Act

        var deleteProductCategoryCommand = new DeleteProductCommand
        {
            Id = _factoryProductCategoryContext.IdProductForDelete
        };

        await handler.Handle(deleteProductCategoryCommand, CancellationToken.None);

        // Assert

        var productCategory = _context.ProductCategories.SingleOrDefault(productCategory =>
                                        productCategory.Id == _factoryProductCategoryContext.IdProductForDelete);

        Assert.Null(productCategory);
    }

    [Fact]
    public async Task DeleteProductCommandHandler_FailOnWrongId()
    {
        // Arrange

        var handler = new DeleteProductCategoryCommandHandler(_repositoryProductCategory);

        //Генерация случайного идентификатора

        var deleteProductCategoryCommand = new DeleteProductCategoryCommand
        {
            Id = new Random().Next(_context.ProductCategories.Count(), 1000)
        };

        // Act
        // Assert

        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(
                deleteProductCategoryCommand,
                CancellationToken.None));
    }
}