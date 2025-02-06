using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Common.Exceptions;
using OnlineShop.Application.ProductCategories.Queries.GetDetailsProductCategory;
using OnlineShop.Application.Products.Queries.GetDetailsProduct;
using OnlineStore.UnitTests.Common.CommonProduct;

namespace OnlineStore.UnitTests.Products.Queries;

public class GetDetailsProductQueryHandlerTest : TestProductBase
{
    [Fact]
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

        Assert.Equal(productForDetails.Name, result.Name);
        Assert.Equal(productForDetails.Description, result.Description);
        Assert.Equal(productForDetails.Price, result.Price);

        var resultProductCategory = result.ProductCategory;
        var productCategory = productForDetails.ProductCategory;
       
        Assert.NotNull(productCategory);

        Assert.Equal(productCategory.Name, resultProductCategory.Name);
        Assert.Equal(productCategory.Description, resultProductCategory.Description);
    }


    [Fact]
    public async Task GetDetailsProductQueryHandler_FailOnWrongId()
    {
        // Arrange
        var handler = new GetDetailsProductCategoryQueryHandler(_repositoryProductCategory);

        //Генерация несуществующего id
        var id = await _context.ProductCategories.MaxAsync(productCategory => productCategory.Id) + 1;

        var getDetailsProductCategoryQuery = new GetDetailsProductCategoryQuery
        {
            Id = id
        };

        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(
                getDetailsProductCategoryQuery,
                CancellationToken.None));
    }
}