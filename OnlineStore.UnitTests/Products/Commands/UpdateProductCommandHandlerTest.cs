using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Common.Exceptions;
using OnlineShop.Application.Products.Commands.ProductUpdate;
using OnlineStore.UnitTests.Common.CommonProduct;

namespace OnlineStore.UnitTests.Products.Commands;

public class UpdateProductCommandHandlerTest : TestProductBase
{
    [Fact]
    public async Task UpdateProductCommandHandler_Success()
    {
        // Arrange
        var updateProductCommandValidation = new UpdateProductCommandValidation();
        var handler = new UpdateProductCommandHandler(_repositoryProduct, updateProductCommandValidation);

        var updatedName = "Modern Web Development";
        var updatedDescription = "A detailed guide to modern web development practices.";
        var updatedPrice = 10m;

        var updateProductCommand = new UpdateProductCommand
        {
            Id = _factoryProductCategoryContext.IdProductForUpdate,
            Name = updatedName,
            Description = updatedDescription,
            Price = updatedPrice,
            IdProductCategory = _factoryProductCategoryContext.IdBookProductCategory,
        };

        // Act
        await handler.Handle(updateProductCommand, CancellationToken.None);

        // Assert
        var product = await _context.Products.SingleOrDefaultAsync(product =>
                                        product.Id == _factoryProductCategoryContext.IdProductForUpdate &&
                                        product.Name == updatedName &&
                                        product.Description == updatedDescription &&
                                        product.Price == updatedPrice &&
                                        product.IdProductCategory == _factoryProductCategoryContext.IdBookProductCategory);

        Assert.NotNull(product);
    }

    [Fact]
    public async Task UpdateProductCommandHandler_FailOnWrongId()
    {
        // Arrange
        var updateProductCommandValidation = new UpdateProductCommandValidation();
        var handler = new UpdateProductCommandHandler(_repositoryProduct, updateProductCommandValidation);

        //Генерация несуществующего id
        var id = await _context.Products.MaxAsync(productCategory => productCategory.Id) + 1;
        var updatedName = "Wireless Noise-Cancelling Headphones";
        var updatedDescription = "High-quality wireless headphones with active noise-cancelling technology.";
        var updatedPrice = 249.99m;

        var updateProductCommand = new UpdateProductCommand
        {
            Id = id,
            Name = updatedName,
            Description = updatedDescription,
            Price = updatedPrice,
            IdProductCategory = _factoryProductCategoryContext.IdElectronicProductCategory,
        };

        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(
                updateProductCommand,
                CancellationToken.None));
    }
}