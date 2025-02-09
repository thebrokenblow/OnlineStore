using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Common.Exceptions;
using OnlineShop.Application.ProductCategories.Commands.ProductCategoryDeletion;
using OnlineStore.UnitTests.Common.CommonProductCategory;
using Shouldly;

namespace OnlineStore.UnitTests.ProductCategories.Commands;

public class DeleteProductCategoryCommandHandlerTest : TestProductCategoryBase
{
    private readonly DeleteProductCategoryCommandHandler _handler;

    public DeleteProductCategoryCommandHandlerTest()
    {
        _handler = new(_productCategoryRepository);
    }

    [Fact(DisplayName = "Should successfully delete a product category")]
    public async Task DeleteProductCategoryCommandHandler_Success()
    {
        // Arrange
        var id = _productCategoryContextFactory.ProductCategoryIdForDelete;

        // Act
        var deleteProductCategoryCommand = new DeleteProductCategoryCommand
        {
            Id = id
        };

        await _handler.Handle(deleteProductCategoryCommand, CancellationToken.None);

        // Assert
        var productCategory = await _context.ProductCategories.SingleOrDefaultAsync(productCategory =>
                                        productCategory.Id == _productCategoryContextFactory.ProductCategoryIdForDelete);

        productCategory.ShouldBeNull();
    }
}