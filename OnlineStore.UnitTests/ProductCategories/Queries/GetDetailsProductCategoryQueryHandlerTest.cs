using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Common.Exceptions;
using OnlineShop.Application.ProductCategories.Queries.GetDetailsProductCategory;
using OnlineStore.UnitTests.Common.CommonProductCategory;
using Shouldly;

namespace OnlineStore.UnitTests.ProductCategories.Queries;

public class GetDetailsProductCategoryQueryHandlerTest : TestProductCategoryBase
{
    private readonly GetDetailsProductCategoryQueryHandler _handler;

    public GetDetailsProductCategoryQueryHandlerTest()
    {
        _handler = new GetDetailsProductCategoryQueryHandler(_productCategoryRepository);
    }

    [Fact(DisplayName = "Should return correct product category details")]
    public async Task GetDetailsProductCategoryQueryHandler_Success()
    {
        // Arrange
        var getDetailsProductCategoryQuery = new GetDetailsProductCategoryQuery
        {
            Id = _productCategoryContextFactory.ProductCategoryIdForDetails,
        };

        var expectedName = _productCategoryContextFactory.ProductCategoryNameGardenTools;
        var expectedDescription = _productCategoryContextFactory.ProductCategoryDescriptionGardenTools;

        // Act
        var result = await _handler.Handle(
                                getDetailsProductCategoryQuery,
                                CancellationToken.None);

        // Assert
        result.Name.ShouldBe(expectedName);
        result.Description.ShouldBe(expectedDescription);
    }
}