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
        var updateProductCategoryCommandValidation = new UpdateProductCategoryCommandValidation();
        var handler = new UpdateProductCategoryCommandHandler(
            _productCategoryRepository, 
            updateProductCategoryCommandValidation);

        var updatedName = "Kitchen Essentials";
        var updatedDescription = "Utensils, appliances, and gadgets for the kitchen, including pots, pans, and cooking tools.";

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
        var updateProductCategoryCommandValidation = new UpdateProductCategoryCommandValidation();
        var handler = new UpdateProductCategoryCommandHandler(
            _productCategoryRepository,
            updateProductCategoryCommandValidation);

        var updatedName = "Kitchen Essentials";

        var id = await _context.ProductCategories.MaxAsync(productCategory => productCategory.Id) + 1;

        var updateProductCategoryCommand = new UpdateProductCategoryCommand
        {
            Id = id,
            Name = updatedName
        };

        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(
                updateProductCategoryCommand,
                CancellationToken.None));
    }
}