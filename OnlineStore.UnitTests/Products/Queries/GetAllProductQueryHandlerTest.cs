using OnlineShop.Application.Products.Queries.GetAllProduct;
using OnlineStore.UnitTests.Common.CommonProduct;

namespace OnlineStore.UnitTests.Products.Queries;

public class GetAllProductQueryHandlerTest : TestProductBase
{
    [Fact(DisplayName = "Successfully retrieve all products")]
    public async Task GetAllProductQueryHandler_Success()
    {
        // Arrange
        var countProduct = _context.Products.Count();
        var handler = new GetAllProductQueryHandler(_repositoryProduct);
        var getAllProductQuery = new GetAllProductQuery();

        // Act
        var result = await handler.Handle(
            getAllProductQuery,
            CancellationToken.None);

        // Assert
        Assert.Equal(countProduct, result.Count);
    }
}