using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Common.Exceptions;
using OnlineShop.Application.Products.Commands.ProductDeletion;
using OnlineStore.UnitTests.Common.CommonProduct;
using Shouldly;

namespace OnlineStore.UnitTests.Products.Commands;

public class DeleteProductCommandHandlerTest : TestProductBase
{
    [Fact]
    public async Task DeleteProductCommandHandler_Success()
    {
        // Arrange
        var handler = new DeleteProductCommandHandler(_repositoryProduct);

        // Act
        var deleteProductCommand = new DeleteProductCommand
        {
            Id = _factoryProductCategoryContext.IdProductForDelete
        };

        await handler.Handle(deleteProductCommand, CancellationToken.None);

        // Assert
        var product = await _context.Products.SingleOrDefaultAsync(product =>
                                        product.Id == _factoryProductCategoryContext.IdProductForDelete);

        product.ShouldBeNull();
    }
}