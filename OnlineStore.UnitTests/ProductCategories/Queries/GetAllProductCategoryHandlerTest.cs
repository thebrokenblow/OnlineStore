using OnlineShop.Application.ProductCategories.Queries.GetAllProductCategory;
using OnlineShop.Persistence.Repositories;
using OnlineStore.UnitTests.Common;

namespace OnlineStore.UnitTests.ProductCategories.Queries;

public class GetAllProductCategoryHandlerTest
{
    [Fact]
    public async Task GetAllProductCategoryQueryHandler_Success()
    {
        // Arrange

        var context = ProductCategoryContextFactory.Create();
        var repository = new RepositoryProductCategory(context);
        var handler = new GetAllProductCategoryHandler(repository);
        var getAllProductCategoryQuery = new GetAllProductCategoryQuery();

        // Act

        var result = await handler.Handle(
            getAllProductCategoryQuery,
            CancellationToken.None);

        // Assert

        Assert.Equal(10, result.Count);
    }
}