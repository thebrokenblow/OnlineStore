using OnlineShop.Application.ProductCategories.Queries.GetRangeProductCategory;
using OnlineShop.Persistence.Repositories;
using OnlineStore.UnitTests.Common;

namespace OnlineStore.UnitTests.ProductCategories.Queries;

public class GetRangeProductCategoryQueryHandlerTest
{
    [Theory]
    [InlineData(0, 10, 10)]
    [InlineData(5, 5, 5)]
    [InlineData(10, 10, 0)]
    [InlineData(0, 20, 10)]
    public async Task GetRangeProductCategoryQueryHandler_Success(int countSkip, int countTake, int countResult)
    {
        // Arrange

        var context = ProductCategoryContextFactory.Create();
        var repository = new RepositoryProductCategory(context);
        var handler = new GetRangeProductCategoryQueryHandler(repository);

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