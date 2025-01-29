using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Common.Exceptions;
using OnlineShop.Application.ProductCategories.Commands.ProductCategoryUpdate;
using OnlineShop.Persistence.Repositories;
using OnlineStore.UnitTests.Common;

namespace OnlineStore.UnitTests.ProductCategories.Commands;

public class UpdateProductCategoryCommandHandlerTest
{
    [Fact]
    public async Task UpdateProductCategoryCommandHandler_Success()
    {
        // Arrange

        var context = ProductCategoryContextFactory.Create();
        var repository = new RepositoryProductCategory(context);
        var handler = new UpdateProductCategoryCommandHandler(repository);

        var updatedName = "new name";
        var updatedDescription = "new description";

        var updateProductCategoryCommand = new UpdateProductCategoryCommand
        {
            Id = ProductCategoryContextFactory.ProductCategoryIdForUpdate,
            Name = updatedName,
            Description = updatedDescription
        };

        // Act

        await handler.Handle(updateProductCategoryCommand, CancellationToken.None);

        // Assert

        var productCategory = await context.ProductCategories.SingleOrDefaultAsync(productCategory => 
                                        productCategory.Id == ProductCategoryContextFactory.ProductCategoryIdForUpdate &&
                                        productCategory.Name == updatedName &&
                                        productCategory.Description == updatedDescription);

        Assert.NotNull(productCategory);
    }

    [Fact]
    public async Task UpdateProductCategoryCommandHandler_FailOnWrongId()
    {
        // Arrange

        var context = ProductCategoryContextFactory.Create();
        var repository = new RepositoryProductCategory(context);
        var handler = new UpdateProductCategoryCommandHandler(repository);


        //Генерация случайного идентификатора

        var updateProductCategoryCommand = new UpdateProductCategoryCommand
        {
            Id = new Random().Next(context.ProductCategories.Count(), 1000),
            Name = string.Empty,
        };

        // Act
        // Assert

        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(
                updateProductCategoryCommand,
                CancellationToken.None));
    }
}