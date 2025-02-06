using OnlineShop.Application.Products.Queries.GetRangeProduct;
using OnlineStore.UnitTests.Common.CommonProduct;

namespace OnlineStore.UnitTests.Products.Queries;

public class GetRangeProductQueryHandlerTest : TestProductBase
{
    [Theory]
    [InlineData(0, 10, 10)]
    [InlineData(5, 5, 5)]
    [InlineData(10, 10, 0)]
    [InlineData(0, 20, 10)]
    public async Task GetRangeProductQueryHandler_Success(int countSkip, int countTake, int countResult)
    {
        // Arrange
        var getRangeProductQueryValidation = new GetRangeProductQueryValidation();
        var handler = new GetRangeProductQueryHandler(_repositoryProduct, getRangeProductQueryValidation);

        var getRangeProductCategoryQuery = new GetRangeProductQuery
        {
            CountSkip = countSkip,
            CountTake = countTake,
        };

        // Act
        var result = await handler.Handle(
            getRangeProductCategoryQuery,
            CancellationToken.None);

        // Assert
        Assert.Equal(countResult, result.Count);
    }
}