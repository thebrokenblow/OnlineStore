using OnlineShop.Application.ProductCategories.Queries.GetRangeProductCategory;
using OnlineStore.UnitTests.Common.CommonProductCategory;

namespace OnlineStore.UnitTests.ProductCategories.Queries;

public class GetRangeProductCategoryQueryHandlerTest : TestProductCategoryBase
{
    [Theory]
    [InlineData(0, 10, 10)]
    [InlineData(5, 5, 5)]
    [InlineData(10, 10, 0)]
    [InlineData(0, 20, 10)]
    public async Task GetRangeProductCategoryQueryHandler_Success(int countSkip, int countTake, int countResult)
    {
        // Arrange

        var handler = new GetRangeProductCategoryQueryHandler(_productCategoryRepository);

        var getRangeProductCategoryQuery = new GetRangeProductCategoryQuery
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