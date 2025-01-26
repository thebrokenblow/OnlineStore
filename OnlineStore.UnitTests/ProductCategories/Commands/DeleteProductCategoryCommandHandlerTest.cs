using OnlineShop.Application.Common.Exceptions;
using OnlineShop.Application.ProductCategories.Commands.ProductCategoryDeletion;
using OnlineShop.Persistence.Repositories;
using OnlineStore.UnitTests.Common;

namespace OnlineStore.UnitTests.ProductCategories.Commands;

public class DeleteProductCategoryCommandHandlerTest
{
    [Fact]
    public async Task DeleteProductCategoryCommandHandler_Success()
    {
        // Arrange

        var context = ProductCategoryContextFactory.Create();
        var repository = new RepositoryProductCategory(context);
        var handler = new DeleteProductCategoryCommandHandler(repository);

        // Act

        var deleteProductCategoryCommand = new DeleteProductCategoryCommand
        {
            Id = ProductCategoryContextFactory.ProductCategoryIdForDelete
        };

        await handler.Handle(deleteProductCategoryCommand, CancellationToken.None);

        // Assert

        var productCategory = context.ProductCategories.SingleOrDefault(productCategory =>
                                        productCategory.Id == ProductCategoryContextFactory.ProductCategoryIdForDelete);

        Assert.Null(productCategory);
    }

    [Fact]
    public async Task DeleteProductCategoryCommandHandler_FailOnWrongId()
    {
        // Arrange

        var context = ProductCategoryContextFactory.Create();
        var repository = new RepositoryProductCategory(context);
        var handler = new DeleteProductCategoryCommandHandler(repository);

        //Генерация случайного идентификатора

        var deleteProductCategoryCommand = new DeleteProductCategoryCommand
        {
            Id = new Random().Next(context.ProductCategories.Count(), 1000)
        };

        // Act
        // Assert

        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(
                deleteProductCategoryCommand,
                CancellationToken.None));
    }
}
