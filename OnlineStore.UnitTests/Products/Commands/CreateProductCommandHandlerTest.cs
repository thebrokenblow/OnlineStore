using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Products.Commands.ProductCreation;
using OnlineStore.UnitTests.Common.CommonProduct;

namespace OnlineStore.UnitTests.Products.Commands;

public class CreateProductCommandHandlerTest : TestProductBase
{
    [Fact]
    public async Task CreateProductCommandHandler_Success()
    {
        // Arrange
        var createProductCommandValidator = new CreateProductCommandValidator();
        var handler = new CreateProductCommandHandler(_repositoryProduct, createProductCommandValidator);

        //Количество продуктов уже с добавленным продуктом
        var countProduct = _context.Products.Count() + 1;

        string nameProduct = "Camera";
        string descriptionProduct = "A professional camera";
        decimal priceProduct = 799.99m;

        var createProductCommand = new CreateProductCommand()
        {
            Name = nameProduct,
            Description = descriptionProduct,
            Price = priceProduct,
            IdProductCategory = _factoryProductCategoryContext.IdElectronicProductCategory,
        };

        //Act
        var productId = await handler.Handle(
            createProductCommand,
            CancellationToken.None);

        // Assert
        var product = await _context.Products.SingleOrDefaultAsync(
            product =>
                product.Id == productId &&
                product.Name == nameProduct &&
                product.Description == descriptionProduct &&
                product.Price == priceProduct);

        Assert.NotNull(product);

        Assert.Equal(countProduct, _context.Products.Count());
    }
}