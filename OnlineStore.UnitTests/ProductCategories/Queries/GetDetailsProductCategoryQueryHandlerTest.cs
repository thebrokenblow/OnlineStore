using OnlineShop.Application.Common.Exceptions;
using OnlineShop.Application.ProductCategories.Queries.GetDetailsProductCategory;
using OnlineShop.Persistence.Repositories;
using OnlineStore.UnitTests.Common;

namespace OnlineStore.UnitTests.ProductCategories.Queries;

public class GetDetailsProductCategoryQueryHandlerTest
{
    [Fact]
    public async Task GetDetailsProductCategoryQueryHandler_Success()
    {
        // Arrange

        var context = ProductCategoryContextFactory.Create();
        var repository = new RepositoryProductCategory(context);
        var handler = new GetDetailsProductCategoryQueryHandler(repository);

        var getDetailsProductCategoryQuery = new GetDetailsProductCategoryQuery
        {
            Id = ProductCategoryContextFactory.ProductCategoryIdForDetails,
        };

        // Act

        var result = await handler.Handle(
            getDetailsProductCategoryQuery,
            CancellationToken.None);

        // Assert

        Assert.Equal("Category3", result.Name);
        Assert.Equal("Description for Category3", result.Description);
    }


    [Fact]
    public async Task GetDetailsProductCategoryQueryHandler_FailOnWrongId()
    {
        // Arrange

        var context = ProductCategoryContextFactory.Create();
        var repository = new RepositoryProductCategory(context);
        var handler = new GetDetailsProductCategoryQueryHandler(repository);

        //Генерация случайного идентификатора

        var getDetailsProductCategoryQuery = new GetDetailsProductCategoryQuery
        {
            Id = new Random().Next(context.ProductCategories.Count(), 1000),
        };

        // Act
        // Assert

        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(
                getDetailsProductCategoryQuery,
                CancellationToken.None));
    }
}