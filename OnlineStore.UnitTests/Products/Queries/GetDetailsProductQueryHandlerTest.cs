using OnlineShop.Application.Products.Queries.GetDetailsProduct;
using OnlineStore.UnitTests.Common.CommonProduct;
using Shouldly;

namespace OnlineStore.UnitTests.Products.Queries;

public class GetDetailsProductQueryHandlerTest : TestProductBase
{
    [Fact(DisplayName = "Successfully retrieve product details")]
    public async Task GetDetailsProductQueryHandler_Success()
    {
        // Arrange
        var handler = new GetDetailsProductQueryHandler(_repositoryProduct);

        var getDetailsProductQuery = new GetDetailsProductQuery
        {
            Id = _factoryProductCategoryContext.IdProductForDetails,
        };

        // Act
        var result = await handler.Handle(
            getDetailsProductQuery,
            CancellationToken.None);

        // Assert
        var productForDetails = _factoryProductCategoryContext.ProductForDetails;

        result.Name.ShouldBe(productForDetails.Name);
        result.Description.ShouldBe(productForDetails.Description);
        result.Price.ShouldBe(productForDetails.Price);

        var resultProductCategory = result.ProductCategory;
        var productCategory = productForDetails.ProductCategory;

        productCategory.ShouldNotBeNull();

        resultProductCategory.Name.ShouldBe(productCategory.Name);
        resultProductCategory.Description.ShouldBe(productCategory.Description);
    }
}