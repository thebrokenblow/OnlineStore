using OnlineShop.Application.ProductCategories.Queries.GetAllProductCategory;
using OnlineStore.UnitTests.Common.CommonProductCategory;
using Shouldly;

namespace OnlineStore.UnitTests.ProductCategories.Queries;

public class GetAllProductCategoryHandlerTest : TestProductCategoryBase
{
    [Fact(DisplayName = "Should return all product categories")]
    public async Task GetAllProductCategoryQueryHandler_Success()
    {
        // Arrange
        var countProductCategory = _context.ProductCategories.Count();
        var handler = new GetAllProductCategoryHandler(_productCategoryRepository);
        var getAllProductCategoryQuery = new GetAllProductCategoryQuery();

        // Act
        var result = await handler.Handle(
            getAllProductCategoryQuery,
            CancellationToken.None);

        // Assert
        result.Count.ShouldBe(countProductCategory);
    }
}
