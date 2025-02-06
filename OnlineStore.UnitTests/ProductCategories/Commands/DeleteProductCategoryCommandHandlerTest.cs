using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Common.Exceptions;
using OnlineShop.Application.ProductCategories.Commands.ProductCategoryDeletion;
using OnlineStore.UnitTests.Common.CommonProductCategory;

namespace OnlineStore.UnitTests.ProductCategories.Commands;

public class DeleteProductCategoryCommandHandlerTest : TestProductCategoryBase
{
    [Fact]
    public async Task DeleteProductCategoryCommandHandler_Success()
    {
        // Arrange
        var handler = new DeleteProductCategoryCommandHandler(_productCategoryRepository);

        // Act
        var deleteProductCategoryCommand = new DeleteProductCategoryCommand
        {
            Id = _productCategoryContextFactory.ProductCategoryIdForDelete
        };

        await handler.Handle(deleteProductCategoryCommand, CancellationToken.None);

        // Assert
        var productCategory = _context.ProductCategories.SingleOrDefault(productCategory =>
                                        productCategory.Id == _productCategoryContextFactory.ProductCategoryIdForDelete);

        Assert.Null(productCategory);
    }

    [Fact]
    public async Task DeleteProductCategoryCommandHandler_FailOnWrongId()
    {
        // Arrange
        var handler = new DeleteProductCategoryCommandHandler(_productCategoryRepository);

        //Генерация случайного идентификатора
        var id = await _context.ProductCategories.MaxAsync(productCategory => productCategory.Id) + 1;
        var deleteProductCategoryCommand = new DeleteProductCategoryCommand
        {
            Id = id
        };

        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(
                deleteProductCategoryCommand,
                CancellationToken.None));
    }
}