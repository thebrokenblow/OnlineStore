using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.ProductCategories.Commands.ProductCategoryCreation;
using OnlineShop.Persistence.Repositories;
using OnlineStore.UnitTests.Common;

namespace OnlineStore.UnitTests.ProductCategories.Commands;

public class CreateProductCategoryCommandHandlerTest
{
    [Fact]
    public async Task CreateProductCategoryCommandHandler_Success()
    {
        // Arrange

        var context = ProductCategoryContextFactory.Create();
        var repository = new RepositoryProductCategory(context);
        var handler = new CreateProductCategoryCommandHandler(repository);

        //Количество категорий продукта уже с новой добавленной категорией

        var countProductCategory = context.ProductCategories.Count() + 1;

        var productCategoryName = "Category11";
        var productCategoryDescription = "Description for Category11";

        var createProductCategoryCommand = new CreateProductCategoryCommand
        {
            Name = productCategoryName,
            Description = productCategoryDescription
        };

        //Act

        var productCategoryId = await handler.Handle(
            createProductCategoryCommand, 
            CancellationToken.None);

        // Assert

        var productCategory = await context.ProductCategories.SingleOrDefaultAsync(
            productCategory =>
                productCategory.Id == productCategoryId &&
                productCategory.Name == productCategoryName &&
                productCategory.Description == productCategoryDescription);

        Assert.NotNull(productCategory);

        Assert.Equal(countProductCategory, context.ProductCategories.Count());
    }
}