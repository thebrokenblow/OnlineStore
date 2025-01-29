using OnlineShop.Application.Products.Queries.GetAllProduct;
using OnlineStore.UnitTests.Common.CommonProduct;

namespace OnlineStore.UnitTests.Products.Queries;

public class GetAllProductQueryHandlerTest : TestProductBase
{
    [Fact]
    public async Task GetAllProductCategoryQueryHandler_Success()
    {
        // Arrange

        var handler = new GetAllProductQueryHandler(_repositoryProduct);
        var getAllProductQuery = new GetAllProductQuery();

        // Act

        var result = await handler.Handle(
            getAllProductQuery,
            CancellationToken.None);

        // Assert

        Assert.Equal(10, result.Count);
    }
}