using Bogus;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Products.Commands.ProductCreation;
using OnlineStore.UnitTests.Common.CommonProduct;
using Shouldly;
using ValidationException = FluentValidation.ValidationException;

namespace OnlineStore.UnitTests.Products.Commands;

public class CreateProductCommandHandlerTest : TestProductBase
{
    private readonly CreateProductCommandValidator _createProductCommandValidator;
    private readonly CreateProductCommandHandler _handler;

    private readonly Faker<CreateProductCommand> _fakerProductCreate;
    public CreateProductCommandHandlerTest()
    {
        _createProductCommandValidator = new CreateProductCommandValidator();
        _handler = new CreateProductCommandHandler(_repositoryProduct, _createProductCommandValidator);

        _fakerProductCreate = new Faker<CreateProductCommand>()
                .RuleFor(product => product.Name,
                         faker => faker.Lorem.Letter(CreateProductCommandValidator.MaxNameLength - 10))
                .RuleFor(product => product.Description,
                         faker => faker.Lorem.Letter(CreateProductCommandValidator.MaxDescriptionLength - 10))
                .RuleFor(product => product.Price,
                         faker => faker.Random.Decimal(1, 1000))
                .RuleFor(product => product.IdProductCategory,
                         faker => faker.PickRandom(_factoryProductCategoryContext.IdProductCategories));
    }

    [Fact(DisplayName = "Successfully create a product")]
    public async Task CreateProductCommandHandler_Success()
    {
        // Arrange
        var countProduct = _context.Products.Count() + 1;
        var createProductCommand = _fakerProductCreate.Generate();
        createProductCommand.IdProductCategory = _factoryProductCategoryContext.IdElectronicProductCategory;

        // Act
        var productId = await _handler.Handle(createProductCommand, CancellationToken.None);

        // Assert
        var product = await _context.Products.SingleOrDefaultAsync(
            product =>
                product.Id == productId &&
                product.Name == createProductCommand.Name &&
                product.Description == createProductCommand.Description &&
                product.Price == createProductCommand.Price &&
                product.IdProductCategory == _factoryProductCategoryContext.IdElectronicProductCategory);

        product.ShouldNotBeNull();
        _context.Products.Count().ShouldBe(countProduct);
    }

    [Fact(DisplayName = "Fail to create a product with an empty name")]
    public async Task CreateProductCommandHandler_FailOnEmptyName()
    {
        // Arrange
        var createProductCommand = _fakerProductCreate.Generate();
        createProductCommand.Name = string.Empty;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(createProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to create a product with a large name")]
    public async Task CreateProductCommandHandler_FailOnLargeName()
    {
        // Arrange
        var createProductCommand = _fakerProductCreate.Generate();
        createProductCommand.Name = _faker.Lorem.Letter(CreateProductCommandValidator.MaxNameLength + 1);

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(createProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to create a product with an empty description")]
    public async Task CreateProductCommandHandler_FailOnEmptyDescription()
    {
        // Arrange
        var createProductCommand = _fakerProductCreate.Generate();
        createProductCommand.Description = string.Empty;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(createProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to create a product with a large description")]
    public async Task CreateProductCommandHandler_FailOnLargeDescription()
    {
        // Arrange
        var createProductCommand = _fakerProductCreate.Generate();
        createProductCommand.Description = _faker.Lorem.Letter(CreateProductCommandValidator.MaxDescriptionLength + 1);

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(createProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to create a product with a zero price")]
    public async Task CreateProductCommandHandler_FailOnZeroPrice()
    {
        // Arrange
        var createProductCommand = _fakerProductCreate.Generate();
        createProductCommand.Price = 0;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(createProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to create a product with a negative price")]
    public async Task CreateProductCommandHandler_FailOnNegativePrice()
    {
        // Arrange
        var createProductCommand = _fakerProductCreate.Generate();
        createProductCommand.Price = -100;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(createProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to create a product with an empty name and description")]
    public async Task CreateProductCommandHandler_FailOnEmptyNameAndDescription()
    {
        // Arrange
        var createProductCommand = _fakerProductCreate.Generate();
        createProductCommand.Name = string.Empty;
        createProductCommand.Description = string.Empty;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(createProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to create a product with an empty name and zero price")]
    public async Task CreateProductCommandHandler_FailOnEmptyNameAndZeroPrice()
    {
        // Arrange
        var createProductCommand = _fakerProductCreate.Generate();
        createProductCommand.Name = string.Empty;
        createProductCommand.Price = 0;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(createProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to create a product with an empty description and zero price")]
    public async Task CreateProductCommandHandler_FailOnEmptyDescriptionAndZeroPrice()
    {
        // Arrange
        var createProductCommand = _fakerProductCreate.Generate();
        createProductCommand.Description = string.Empty;
        createProductCommand.Price = 0;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(createProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to create a product with an empty name, description, and zero price")]
    public async Task CreateProductCommandHandler_FailOnEmptyNameDescriptionAndZeroPrice()
    {
        // Arrange
        var createProductCommand = _fakerProductCreate.Generate();
        createProductCommand.Name = string.Empty;
        createProductCommand.Description = string.Empty;
        createProductCommand.Price = 0;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(createProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to create a product with an empty name and negative price")]
    public async Task CreateProductCommandHandler_FailOnEmptyNameAndNegativePrice()
    {
        // Arrange
        var createProductCommand = _fakerProductCreate.Generate();
        createProductCommand.Name = string.Empty;
        createProductCommand.Price = -100;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(createProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to create a product with an empty description and negative price")]
    public async Task CreateProductCommandHandler_FailOnEmptyDescriptionAndNegativePrice()
    {
        // Arrange
        var createProductCommand = _fakerProductCreate.Generate();
        createProductCommand.Description = string.Empty;
        createProductCommand.Price = -100;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(createProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to create a product with an empty name, description, and negative price")]
    public async Task CreateProductCommandHandler_FailOnEmptyNameDescriptionAndNegativePrice()
    {
        // Arrange
        var createProductCommand = _fakerProductCreate.Generate();
        createProductCommand.Name = string.Empty;
        createProductCommand.Description = string.Empty;
        createProductCommand.Price = -100;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(createProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to create a product with a large name and zero price")]
    public async Task CreateProductCommandHandler_FailOnLargeNameAndZeroPrice()
    {
        // Arrange
        var createProductCommand = _fakerProductCreate.Generate();
        createProductCommand.Name = _faker.Lorem.Letter(CreateProductCommandValidator.MaxNameLength + 1);
        createProductCommand.Price = 0;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(createProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to create a product with a large name and negative price")]
    public async Task CreateProductCommandHandler_FailOnLargeNameAndNegativePrice()
    {
        // Arrange
        var createProductCommand = _fakerProductCreate.Generate();
        createProductCommand.Name = _faker.Lorem.Letter(CreateProductCommandValidator.MaxNameLength + 1);
        createProductCommand.Price = -100;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(createProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to create a product with a large description and zero price")]
    public async Task CreateProductCommandHandler_FailOnLargeDescriptionAndZeroPrice()
    {
        // Arrange
        var createProductCommand = _fakerProductCreate.Generate();
        createProductCommand.Description = _faker.Lorem.Letter(CreateProductCommandValidator.MaxDescriptionLength + 1);
        createProductCommand.Price = 0;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(createProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to create a product with a large description and negative price")]
    public async Task CreateProductCommandHandler_FailOnLargeDescriptionAndNegativePrice()
    {
        // Arrange
        var createProductCommand = _fakerProductCreate.Generate();
        createProductCommand.Description = _faker.Lorem.Letter(CreateProductCommandValidator.MaxDescriptionLength + 1);
        createProductCommand.Price = -100;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(createProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to create a product with a large name, description, and zero price")]
    public async Task CreateProductCommandHandler_FailOnLargeNameDescriptionAndZeroPrice()
    {
        // Arrange
        var createProductCommand = _fakerProductCreate.Generate();
        createProductCommand.Name = _faker.Lorem.Letter(CreateProductCommandValidator.MaxNameLength + 1);
        createProductCommand.Description = _faker.Lorem.Letter(CreateProductCommandValidator.MaxDescriptionLength + 1);
        createProductCommand.Price = 0;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(createProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to create a product with a large name, description, and negative price")]
    public async Task CreateProductCommandHandler_FailOnLargeNameDescriptionAndNegativePrice()
    {
        // Arrange
        var createProductCommand = _fakerProductCreate.Generate();
        createProductCommand.Name = _faker.Lorem.Letter(CreateProductCommandValidator.MaxNameLength + 1);
        createProductCommand.Description = _faker.Lorem.Letter(CreateProductCommandValidator.MaxDescriptionLength + 1);
        createProductCommand.Price = -100;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(createProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to create a product with an empty name and large description")]
    public async Task CreateProductCommandHandler_FailOnEmptyNameAndLargeDescription()
    {
        // Arrange
        var createProductCommand = _fakerProductCreate.Generate();
        createProductCommand.Name = string.Empty;
        createProductCommand.Description = _faker.Lorem.Letter(CreateProductCommandValidator.MaxDescriptionLength + 1);

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(createProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to create a product with a large name and empty description")]
    public async Task CreateProductCommandHandler_FailOnLargeNameAndEmptyDescription()
    {
        // Arrange
        var createProductCommand = _fakerProductCreate.Generate();
        createProductCommand.Name = _faker.Lorem.Letter(CreateProductCommandValidator.MaxNameLength + 1);
        createProductCommand.Description = string.Empty;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(createProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to create a product with a large name and large description")]
    public async Task CreateProductCommandHandler_FailOnLargeNameAndLargeDescription()
    {
        // Arrange
        var createProductCommand = _fakerProductCreate.Generate();
        createProductCommand.Name = _faker.Lorem.Letter(CreateProductCommandValidator.MaxNameLength + 1);
        createProductCommand.Description = _faker.Lorem.Letter(CreateProductCommandValidator.MaxDescriptionLength + 1);

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(createProductCommand, CancellationToken.None));
    }
}
