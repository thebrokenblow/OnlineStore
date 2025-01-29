using OnlineShop.Application.ProductCategories.Queries.GetAllProductCategory;
using OnlineStore.UnitTests.Common.CommonProductCategory;

namespace OnlineStore.UnitTests.ProductCategories.Queries;

public class GetAllProductCategoryHandlerTest : TestProductCategoryBase
{
    [Fact]
    public async Task GetAllProductCategoryQueryHandler_Success()
    {
        // Arrange

        var handler = new GetAllProductCategoryHandler(_productCategoryRepository);
        var getAllProductCategoryQuery = new GetAllProductCategoryQuery();

        // Act

        var result = await handler.Handle(
            getAllProductCategoryQuery,
            CancellationToken.None);

        // Assert

        Assert.Equal(10, result.Count);
    }
}