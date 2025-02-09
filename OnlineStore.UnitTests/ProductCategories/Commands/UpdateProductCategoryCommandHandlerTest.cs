using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Common.Exceptions;
using OnlineShop.Application.ProductCategories.Commands.ProductCategoryCreation;
using OnlineShop.Application.ProductCategories.Commands.ProductCategoryUpdate;
using OnlineStore.UnitTests.Common.CommonProductCategory;
using Shouldly;
using ValidationException = FluentValidation.ValidationException;

namespace OnlineStore.UnitTests.ProductCategories.Commands;

public class UpdateProductCategoryCommandHandlerTest : TestProductCategoryBase
{
    private readonly UpdateProductCategoryCommandHandler _handler;

    public UpdateProductCategoryCommandHandlerTest()
    {
        var updateProductCategoryCommandValidation = new UpdateProductCategoryCommandValidation();
        _handler = new UpdateProductCategoryCommandHandler(
                            _productCategoryRepository,
                            updateProductCategoryCommandValidation);
    }

    [Fact(DisplayName = "Should successfully update a product category")]
    public async Task UpdateProductCategoryCommandHandler_Success()
    {
        // Arrange
        var updatedNameLength = CreateProductCategoryCommandValidation.MaxNameLength - 10;
        var updatedDescriptionLength = CreateProductCategoryCommandValidation.MaxDescriptionLength - 10;

        var updatedName = _faker.Lorem.Letter(updatedNameLength);
        var updatedDescription = _faker.Lorem.Letter(updatedDescriptionLength);

        var updateProductCategoryCommand = new UpdateProductCategoryCommand
        {
            Id = _productCategoryContextFactory.ProductCategoryIdForUpdate,
            Name = updatedName,
            Description = updatedDescription
        };

        // Act
        await _handler.Handle(updateProductCategoryCommand, CancellationToken.None);

        // Assert
        var productCategory = await _context.ProductCategories.SingleOrDefaultAsync(productCategory =>
                                        productCategory.Id == _productCategoryContextFactory.ProductCategoryIdForUpdate &&
                                        productCategory.Name == updatedName &&
                                        productCategory.Description == updatedDescription);

        productCategory.ShouldNotBeNull();
    }

    [Fact(DisplayName = "Should throw NotFoundException for non-existent product category ID")]
    public async Task UpdateProductCategoryCommandHandler_FailOnWrongId()
    {
        // Arrange
        var updatedNameLength = CreateProductCategoryCommandValidation.MaxNameLength - 10;
        var updatedName = _faker.Lorem.Letter(updatedNameLength);

        var id = await _context.ProductCategories.MaxAsync(productCategory => productCategory.Id) + 1;

        var updateProductCategoryCommand = new UpdateProductCategoryCommand
        {
            Id = id,
            Name = updatedName
        };

        // Act & Assert
        (await Should.ThrowAsync<NotFoundException>(async () =>
            await _handler.Handle(updateProductCategoryCommand, CancellationToken.None))).ShouldNotBeNull();
    }

    [Fact(DisplayName = "Should throw ValidationException for empty name")]
    public async Task UpdateProductCategoryCommandHandler_FailOnEmptyName()
    {
        // Arrange
        var updatedDescriptionLength = CreateProductCategoryCommandValidation.MaxDescriptionLength - 10;
        var updatedDescription = _faker.Lorem.Letter(updatedDescriptionLength);

        var updateProductCategoryCommand = new UpdateProductCategoryCommand
        {
            Id = _productCategoryContextFactory.ProductCategoryIdForUpdate,
            Name = string.Empty,
            Description = updatedDescription
        };

        // Act & Assert
        (await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(updateProductCategoryCommand, CancellationToken.None))).ShouldNotBeNull();
    }

    [Fact(DisplayName = "Should throw ValidationException for large name")]
    public async Task UpdateProductCategoryCommandHandler_FailOnLargeName()
    {
        // Arrange
        var updatedNameLength = CreateProductCategoryCommandValidation.MaxNameLength + 10;
        var updatedDescriptionLength = CreateProductCategoryCommandValidation.MaxDescriptionLength - 10;

        var updatedName = _faker.Lorem.Letter(updatedNameLength);
        var updatedDescription = _faker.Lorem.Letter(updatedDescriptionLength);

        var updateProductCategoryCommand = new UpdateProductCategoryCommand
        {
            Id = _productCategoryContextFactory.ProductCategoryIdForUpdate,
            Name = updatedName,
            Description = updatedDescription
        };

        // Act & Assert
        (await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(updateProductCategoryCommand, CancellationToken.None))).ShouldNotBeNull();
    }

    [Fact(DisplayName = "Should throw ValidationException for large description")]
    public async Task UpdateProductCategoryCommandHandler_FailOnLargeDescription()
    {
        // Arrange
        var updatedNameLength = CreateProductCategoryCommandValidation.MaxNameLength - 10;
        var updatedDescriptionLength = CreateProductCategoryCommandValidation.MaxDescriptionLength + 10;

        var updatedName = _faker.Lorem.Letter(updatedNameLength);
        var updatedDescription = _faker.Lorem.Letter(updatedDescriptionLength);

        var updateProductCategoryCommand = new UpdateProductCategoryCommand
        {
            Id = _productCategoryContextFactory.ProductCategoryIdForUpdate,
            Name = updatedName,
            Description = updatedDescription
        };

        // Act & Assert
        (await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(updateProductCategoryCommand, CancellationToken.None))).ShouldNotBeNull();
    }

    [Fact(DisplayName = "Should throw ValidationException for large name and large description")]
    public async Task UpdateProductCategoryCommandHandler_FailOnLargeDescriptionAndLargeName()
    {
        // Arrange
        var updatedNameLength = CreateProductCategoryCommandValidation.MaxNameLength + 10;
        var updatedDescriptionLength = CreateProductCategoryCommandValidation.MaxDescriptionLength + 10;

        var updatedName = _faker.Lorem.Letter(updatedNameLength);
        var updatedDescription = _faker.Lorem.Letter(updatedDescriptionLength);

        var updateProductCategoryCommand = new UpdateProductCategoryCommand
        {
            Id = _productCategoryContextFactory.ProductCategoryIdForUpdate,
            Name = updatedName,
            Description = updatedDescription
        };

        // Act & Assert
        (await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(updateProductCategoryCommand, CancellationToken.None))).ShouldNotBeNull();
    }

    [Fact(DisplayName = "Should successfully update a product category with null description")]
    public async Task UpdateProductCategoryCommandHandler_SuccessOnNullDescription()
    {
        // Arrange
        var updatedNameLength = CreateProductCategoryCommandValidation.MaxNameLength - 10;
        var updatedName = _faker.Lorem.Letter(updatedNameLength);

        var updateProductCategoryCommand = new UpdateProductCategoryCommand
        {
            Id = _productCategoryContextFactory.ProductCategoryIdForUpdate,
            Name = updatedName,
            Description = null
        };

        // Act
        await _handler.Handle(updateProductCategoryCommand, CancellationToken.None);

        // Assert
        var productCategory = await _context.ProductCategories.SingleOrDefaultAsync(productCategory =>
                                        productCategory.Id == _productCategoryContextFactory.ProductCategoryIdForUpdate &&
                                        productCategory.Name == updatedName &&
                                        productCategory.Description == null);

        productCategory.ShouldNotBeNull();
    }

    [Fact(DisplayName = "Should throw ValidationException for null name")]
    public async Task UpdateProductCategoryCommandHandler_FailOnNullName()
    {
        // Arrange
        var updatedDescriptionLength = CreateProductCategoryCommandValidation.MaxDescriptionLength - 10;
        var updatedDescription = _faker.Lorem.Letter(updatedDescriptionLength);

        var updateProductCategoryCommand = new UpdateProductCategoryCommand
        {
            Id = _productCategoryContextFactory.ProductCategoryIdForUpdate,
            Name = null!,
            Description = updatedDescription
        };

        // Act & Assert
        (await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(updateProductCategoryCommand, CancellationToken.None))).ShouldNotBeNull();
    }
}
