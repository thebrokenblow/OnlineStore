using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.ProductCategories.Commands.ProductCategoryCreation;
using OnlineStore.UnitTests.Common.CommonProductCategory;
using Shouldly;
using ValidationException = FluentValidation.ValidationException;

namespace OnlineStore.UnitTests.ProductCategories.Commands;

public class CreateProductCategoryCommandHandlerTest : TestProductCategoryBase
{
    private readonly CreateProductCategoryCommandHandler _handler;

    public CreateProductCategoryCommandHandlerTest()
    {
        var createProductCategoryCommandValidation = new CreateProductCategoryCommandValidation();
        _handler = new CreateProductCategoryCommandHandler(_productCategoryRepository, createProductCategoryCommandValidation);
    }

    [Fact(DisplayName = "Should successfully create a new product category")]
    public async Task CreateProductCategoryCommandHandler_Success()
    {
        // Arrange
        var nameLength = CreateProductCategoryCommandValidation.MaxNameLength - 10;
        var descriptionLength = CreateProductCategoryCommandValidation.MaxDescriptionLength - 10;

        // Количество категорий продукта уже с новой добавленной категорией
        var countProductCategory = _context.ProductCategories.Count() + 1;

        var productCategoryName = _faker.Lorem.Letter(nameLength);
        var productCategoryDescription = _faker.Lorem.Letter(descriptionLength);

        var createProductCategoryCommand = new CreateProductCategoryCommand
        {
            Name = productCategoryName,
            Description = productCategoryDescription
        };

        // Act
        var productCategoryId = await _handler.Handle(
            createProductCategoryCommand,
            CancellationToken.None);

        var productCategory = await _context.ProductCategories.SingleOrDefaultAsync(
            productCategory =>
                productCategory.Id == productCategoryId &&
                productCategory.Name == productCategoryName &&
                productCategory.Description == productCategoryDescription);

        // Assert
        productCategory.ShouldNotBeNull();
        _context.ProductCategories.Count().ShouldBe(countProductCategory);
    }

    [Fact(DisplayName = "Should throw validation exception for empty name")]
    public async Task CreateProductCategoryCommandHandler_FailOnEmptyName()
    {
        // Arrange
        var descriptionLength = CreateProductCategoryCommandValidation.MaxDescriptionLength - 10;

        var createProductCategoryCommand = new CreateProductCategoryCommand
        {
            Name = string.Empty,
            Description = _faker.Lorem.Letter(descriptionLength)
        };

        // Act & Assert
        (await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(createProductCategoryCommand, CancellationToken.None))).ShouldNotBeNull();
    }

    [Fact(DisplayName = "Should throw validation exception for large name")]
    public async Task CreateProductCategoryCommandHandler_FailOnLargeName()
    {
        // Arrange
        var nameLength = CreateProductCategoryCommandValidation.MaxNameLength + 10;
        var descriptionLength = CreateProductCategoryCommandValidation.MaxDescriptionLength - 10;

        var createProductCategoryCommand = new CreateProductCategoryCommand
        {
            Name = _faker.Lorem.Letter(nameLength),
            Description = _faker.Lorem.Letter(descriptionLength)
        };

        // Act & Assert
        (await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(createProductCategoryCommand, CancellationToken.None))).ShouldNotBeNull();
    }

    [Fact(DisplayName = "Should throw validation exception for large description")]
    public async Task CreateProductCategoryCommandHandler_FailOnLargeDescription()
    {
        // Arrange
        var nameLength = CreateProductCategoryCommandValidation.MaxNameLength - 10;
        var descriptionLength = CreateProductCategoryCommandValidation.MaxDescriptionLength + 10;

        var createProductCategoryCommand = new CreateProductCategoryCommand
        {
            Name = _faker.Lorem.Letter(nameLength),
            Description = _faker.Lorem.Letter(descriptionLength)
        };

        // Act & Assert
        (await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(createProductCategoryCommand, CancellationToken.None))).ShouldNotBeNull();
    }

    [Fact(DisplayName = "Should throw validation exception for large name and large description")]
    public async Task CreateProductCategoryCommandHandler_FailOnLargeDescriptionAndLargeName()
    {
        // Arrange
        var nameLength = CreateProductCategoryCommandValidation.MaxNameLength + 10;
        var descriptionLength = CreateProductCategoryCommandValidation.MaxDescriptionLength + 10;

        var createProductCategoryCommand = new CreateProductCategoryCommand
        {
            Name = _faker.Lorem.Letter(nameLength),
            Description = _faker.Lorem.Letter(descriptionLength)
        };

        // Act & Assert
        (await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(createProductCategoryCommand, CancellationToken.None))).ShouldNotBeNull();
    }

    [Fact(DisplayName = "Should successfully create a new product category with null description")]
    public async Task CreateProductCategoryCommandHandler_SuccessOnNullDescription()
    {
        // Arrange
        var nameLength = CreateProductCategoryCommandValidation.MaxNameLength - 10;

        // Количество категорий продукта уже с новой добавленной категорией
        var countProductCategory = _context.ProductCategories.Count() + 1;

        var productCategoryName = _faker.Lorem.Letter(nameLength);
        var createProductCategoryCommand = new CreateProductCategoryCommand
        {
            Name = productCategoryName,
            Description = null,
        };

        // Act
        var productCategoryId = await _handler.Handle(createProductCategoryCommand, CancellationToken.None);

        var productCategory = await _context.ProductCategories.SingleOrDefaultAsync(
            productCategory =>
                productCategory.Id == productCategoryId &&
                productCategory.Name == productCategoryName &&
                productCategory.Description == null);

        // Assert
        productCategory.ShouldNotBeNull();
        _context.ProductCategories.Count().ShouldBe(countProductCategory);
    }

    [Fact(DisplayName = "Should throw validation exception for null name")]
    public async Task CreateProductCategoryCommandHandler_FailOnNullName()
    {
        // Arrange
        var descriptionLength = CreateProductCategoryCommandValidation.MaxDescriptionLength - 10;

        var createProductCategoryCommand = new CreateProductCategoryCommand
        {
            Name = null!,
            Description = _faker.Lorem.Letter(descriptionLength)
        };

        // Act & Assert
        (await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(createProductCategoryCommand, CancellationToken.None))).ShouldNotBeNull();
    }
}
