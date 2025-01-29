using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Common.Exceptions;
using OnlineShop.Application.ProductCategories.Commands.ProductCategoryUpdate;
using OnlineStore.UnitTests.Common.CommonProductCategory;

namespace OnlineStore.UnitTests.ProductCategories.Commands;

public class UpdateProductCategoryCommandHandlerTest : TestProductCategoryBase
{
    [Fact]
    public async Task UpdateProductCategoryCommandHandler_Success()
    {
        // Arrange

        var handler = new UpdateProductCategoryCommandHandler(_productCategoryRepository);

        var updatedName = "new name";
        var updatedDescription = "new description";

        var updateProductCategoryCommand = new UpdateProductCategoryCommand
        {
            Id = _productCategoryContextFactory.ProductCategoryIdForUpdate,
            Name = updatedName,
            Description = updatedDescription
        };

        // Act

        await handler.Handle(updateProductCategoryCommand, CancellationToken.None);

        // Assert

        var productCategory = await _context.ProductCategories.SingleOrDefaultAsync(productCategory => 
                                        productCategory.Id == _productCategoryContextFactory.ProductCategoryIdForUpdate &&
                                        productCategory.Name == updatedName &&
                                        productCategory.Description == updatedDescription);

        Assert.NotNull(productCategory);
    }

    [Fact]
    public async Task UpdateProductCategoryCommandHandler_FailOnWrongId()
    {
        // Arrange

        var handler = new UpdateProductCategoryCommandHandler(_productCategoryRepository);

        //Генерация случайного идентификатора

        var updateProductCategoryCommand = new UpdateProductCategoryCommand
        {
            Id = new Random().Next(_context.ProductCategories.Count(), 1000),
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