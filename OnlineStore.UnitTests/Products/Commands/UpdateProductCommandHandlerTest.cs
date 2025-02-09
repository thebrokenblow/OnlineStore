using Bogus;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Common.Exceptions;
using OnlineShop.Application.Products.Commands.ProductUpdate;
using OnlineStore.UnitTests.Common.CommonProduct;
using Shouldly;
using ValidationException = FluentValidation.ValidationException;

namespace OnlineStore.UnitTests.Products.Commands;

public class UpdateProductCommandHandlerTest : TestProductBase
{
    private readonly UpdateProductCommandValidation _updateProductCommandValidation;
    private readonly UpdateProductCommandHandler _handler;
    private readonly Faker<UpdateProductCommand> _fakerProductUpdate;

    public UpdateProductCommandHandlerTest()
    {
        _updateProductCommandValidation = new UpdateProductCommandValidation();
        _handler = new UpdateProductCommandHandler(_repositoryProduct, _updateProductCommandValidation);
       
        _fakerProductUpdate = new Faker<UpdateProductCommand>()
            .RuleFor(product => product.Id, _factoryProductCategoryContext.IdProductForUpdate)
            .RuleFor(product => product.Name, faker => faker.Lorem.Letter(UpdateProductCommandValidation.MaxNameLength - 10))
            .RuleFor(product => product.Description, faker => faker.Lorem.Letter(UpdateProductCommandValidation.MaxDescriptionLength - 10))
            .RuleFor(product => product.Price, faker => faker.Random.Decimal(1, 1000))
            .RuleFor(product => product.IdProductCategory, faker => faker.PickRandom(_factoryProductCategoryContext.IdProductCategories));
    }

    [Fact(DisplayName = "Successfully update a product")]
    public async Task UpdateProductCommandHandler_Success()
    {
        // Arrange
        var updateProductCommand = _fakerProductUpdate.Generate();

        // Act
        await _handler.Handle(updateProductCommand, CancellationToken.None);

        // Assert
        var product = await _context.Products.SingleOrDefaultAsync(product =>
                                        product.Id == _factoryProductCategoryContext.IdProductForUpdate &&
                                        product.Name == updateProductCommand.Name &&
                                        product.Description == updateProductCommand.Description &&
                                        product.Price == updateProductCommand.Price);

        product.ShouldNotBeNull();
    }

    [Fact(DisplayName = "Fail to update a product with an empty name")]
    public async Task UpdateProductCommandHandler_FailOnEmptyName()
    {
        // Arrange
        var updateProductCommand = _fakerProductUpdate.Generate();
        updateProductCommand.Name = string.Empty;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(updateProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to update a product with a large name")]
    public async Task UpdateProductCommandHandler_FailOnLargeName()
    {
        // Arrange
        var updateProductCommand = _fakerProductUpdate.Generate();
        updateProductCommand.Name = _faker.Lorem.Letter(UpdateProductCommandValidation.MaxNameLength + 1);

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(updateProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to update a product with an empty description")]
    public async Task UpdateProductCommandHandler_FailOnEmptyDescription()
    {
        // Arrange
        var updateProductCommand = _fakerProductUpdate.Generate();
        updateProductCommand.Description = string.Empty;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(updateProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to update a product with a large description")]
    public async Task UpdateProductCommandHandler_FailOnLargeDescription()
    {
        // Arrange
        var updateProductCommand = _fakerProductUpdate.Generate();
        updateProductCommand.Description = _faker.Lorem.Letter(UpdateProductCommandValidation.MaxDescriptionLength + 1);

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(updateProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to update a product with a zero price")]
    public async Task UpdateProductCommandHandler_FailOnZeroPrice()
    {
        // Arrange
        var updateProductCommand = _fakerProductUpdate.Generate();
        updateProductCommand.Price = 0;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(updateProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to update a product with a negative price")]
    public async Task UpdateProductCommandHandler_FailOnNegativePrice()
    {
        // Arrange
        var updateProductCommand = _fakerProductUpdate.Generate();
        updateProductCommand.Price = -100;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(updateProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to update a product with an empty name and description")]
    public async Task UpdateProductCommandHandler_FailOnEmptyNameAndDescription()
    {
        // Arrange
        var updateProductCommand = _fakerProductUpdate.Generate();
        updateProductCommand.Name = string.Empty;
        updateProductCommand.Description = string.Empty;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(updateProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to update a product with an empty name and zero price")]
    public async Task UpdateProductCommandHandler_FailOnEmptyNameAndZeroPrice()
    {
        // Arrange
        var updateProductCommand = _fakerProductUpdate.Generate();
        updateProductCommand.Name = string.Empty;
        updateProductCommand.Price = 0;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(updateProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to update a product with an empty description and zero price")]
    public async Task UpdateProductCommandHandler_FailOnEmptyDescriptionAndZeroPrice()
    {
        // Arrange
        var updateProductCommand = _fakerProductUpdate.Generate();
        updateProductCommand.Description = string.Empty;
        updateProductCommand.Price = 0;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(updateProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to update a product with an empty name, description, and zero price")]
    public async Task UpdateProductCommandHandler_FailOnEmptyNameDescriptionAndZeroPrice()
    {
        // Arrange
        var updateProductCommand = _fakerProductUpdate.Generate();
        updateProductCommand.Name = string.Empty;
        updateProductCommand.Description = string.Empty;
        updateProductCommand.Price = 0;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(updateProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to update a product with an empty name and negative price")]
    public async Task UpdateProductCommandHandler_FailOnEmptyNameAndNegativePrice()
    {
        // Arrange
        var updateProductCommand = _fakerProductUpdate.Generate();
        updateProductCommand.Name = string.Empty;
        updateProductCommand.Price = -100;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(updateProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to update a product with an empty description and negative price")]
    public async Task UpdateProductCommandHandler_FailOnEmptyDescriptionAndNegativePrice()
    {
        // Arrange
        var updateProductCommand = _fakerProductUpdate.Generate();
        updateProductCommand.Description = string.Empty;
        updateProductCommand.Price = -100;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(updateProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to update a product with an empty name, description, and negative price")]
    public async Task UpdateProductCommandHandler_FailOnEmptyNameDescriptionAndNegativePrice()
    {
        // Arrange
        var updateProductCommand = _fakerProductUpdate.Generate();
        updateProductCommand.Name = string.Empty;
        updateProductCommand.Description = string.Empty;
        updateProductCommand.Price = -100;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(updateProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to update a product with a large name and zero price")]
    public async Task UpdateProductCommandHandler_FailOnLargeNameAndZeroPrice()
    {
        // Arrange
        var updateProductCommand = _fakerProductUpdate.Generate();
        updateProductCommand.Name = _faker.Lorem.Letter(UpdateProductCommandValidation.MaxNameLength + 1);
        updateProductCommand.Price = 0;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(updateProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to update a product with a large name and negative price")]
    public async Task UpdateProductCommandHandler_FailOnLargeNameAndNegativePrice()
    {
        // Arrange
        var updateProductCommand = _fakerProductUpdate.Generate();
        updateProductCommand.Name = _faker.Lorem.Letter(UpdateProductCommandValidation.MaxNameLength + 1);
        updateProductCommand.Price = -100;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(updateProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to update a product with a large description and zero price")]
    public async Task UpdateProductCommandHandler_FailOnLargeDescriptionAndZeroPrice()
    {
        // Arrange
        var updateProductCommand = _fakerProductUpdate.Generate();
        updateProductCommand.Description = _faker.Lorem.Letter(UpdateProductCommandValidation.MaxDescriptionLength + 1);
        updateProductCommand.Price = 0;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(updateProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to update a product with a large description and negative price")]
    public async Task UpdateProductCommandHandler_FailOnLargeDescriptionAndNegativePrice()
    {
        // Arrange
        var updateProductCommand = _fakerProductUpdate.Generate();
        updateProductCommand.Description = _faker.Lorem.Letter(UpdateProductCommandValidation.MaxDescriptionLength + 1);
        updateProductCommand.Price = -100;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(updateProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to update a product with a large name, description, and zero price")]
    public async Task UpdateProductCommandHandler_FailOnLargeNameDescriptionAndZeroPrice()
    {
        // Arrange
        var updateProductCommand = _fakerProductUpdate.Generate();
        updateProductCommand.Name = _faker.Lorem.Letter(UpdateProductCommandValidation.MaxNameLength + 1);
        updateProductCommand.Description = _faker.Lorem.Letter(UpdateProductCommandValidation.MaxDescriptionLength + 1);
        updateProductCommand.Price = 0;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(updateProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to update a product with a large name, description, and negative price")]
    public async Task UpdateProductCommandHandler_FailOnLargeNameDescriptionAndNegativePrice()
    {
        // Arrange
        var updateProductCommand = _fakerProductUpdate.Generate();
        updateProductCommand.Name = _faker.Lorem.Letter(UpdateProductCommandValidation.MaxNameLength + 1);
        updateProductCommand.Description = _faker.Lorem.Letter(UpdateProductCommandValidation.MaxDescriptionLength + 1);
        updateProductCommand.Price = -100;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(updateProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to update a product with an empty name and large description")]
    public async Task UpdateProductCommandHandler_FailOnEmptyNameAndLargeDescription()
    {
        // Arrange
        var updateProductCommand = _fakerProductUpdate.Generate();
        updateProductCommand.Name = string.Empty;
        updateProductCommand.Description = _faker.Lorem.Letter(UpdateProductCommandValidation.MaxDescriptionLength + 1);

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(updateProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to update a product with a large name and empty description")]
    public async Task UpdateProductCommandHandler_FailOnLargeNameAndEmptyDescription()
    {
        // Arrange
        var updateProductCommand = _fakerProductUpdate.Generate();
        updateProductCommand.Name = _faker.Lorem.Letter(UpdateProductCommandValidation.MaxNameLength + 1);
        updateProductCommand.Description = string.Empty;

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(updateProductCommand, CancellationToken.None));
    }

    [Fact(DisplayName = "Fail to update a product with a large name and large description")]
    public async Task UpdateProductCommandHandler_FailOnLargeNameAndLargeDescription()
    {
        // Arrange
        var updateProductCommand = _fakerProductUpdate.Generate();
        updateProductCommand.Name = _faker.Lorem.Letter(UpdateProductCommandValidation.MaxNameLength + 1);
        updateProductCommand.Description = _faker.Lorem.Letter(UpdateProductCommandValidation.MaxDescriptionLength + 1);

        // Act & Assert
        await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(updateProductCommand, CancellationToken.None));
    }
}
