using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Products.Commands.ProductCreation;
using OnlineShop.Persistence.Repositories;
using OnlineStore.UnitTests.Common;

namespace OnlineStore.UnitTests.Products.Commands;

public class CreateProductCommandHandlerTest
{
    [Fact]
    public async Task CreateProductCategoryCommandHandler_Success()
    {
        // Arrange

        var context = ProductContextFactory.Create();
        var repositoryProduct = new RepositoryProduct(context);
        var repositoryProductCategory = new RepositoryProductCategory(context);

        var handler = new CreateProductCommandHandler(repositoryProduct, repositoryProductCategory);

        //Количество продуктов уже с добавленным продуктом

        var countProduct = context.Products.Count() + 1;

        string nameProduct = "Camera";
        string descriptionProduct = "A professional camera";
        decimal priceProduct = 799.99m;

        var createProductCommand = new CreateProductCommand()
        {
            Name = nameProduct,
            Description = descriptionProduct,
            Price = priceProduct,
            IdProductCategory = ProductContextFactory.ProductCategories!.First().Id,
        };

        //Act

        var productId = await handler.Handle(
            createProductCommand,
            CancellationToken.None);

        // Assert

        var product = await context.Products.SingleOrDefaultAsync(
            product =>
                product.Id == productId &&
                product.Name == nameProduct &&
                product.Description == descriptionProduct &&
                product.Price == priceProduct);

        Assert.NotNull(product);

        Assert.Equal(countProduct, context.Products.Count());
    }
}