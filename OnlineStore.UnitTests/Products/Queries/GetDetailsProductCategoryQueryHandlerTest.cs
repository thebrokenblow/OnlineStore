using OnlineShop.Application.Common.Exceptions;
using OnlineShop.Application.ProductCategories.Queries.GetDetailsProductCategory;
using OnlineShop.Application.Products.Queries.GetDetailsProduct;
using OnlineStore.UnitTests.Common.CommonProduct;

namespace OnlineStore.UnitTests.Products.Queries;

public class GetDetailsProductCategoryQueryHandlerTest : TestProductBase
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

        Assert.Equal(_factoryProductCategoryContext.ProductForDetails.Name, 
            result.Name);

        Assert.Equal(_factoryProductCategoryContext.ProductForDetails.Description, 
            result.Description);

        Assert.Equal(_factoryProductCategoryContext.ProductForDetails.Price, 
            result.Price);

        Assert.Equal(_factoryProductCategoryContext.ProductForDetails.ProductCategory.Name, 
            result.ProductCategory.Name);

        Assert.Equal(_factoryProductCategoryContext.ProductForDetails.ProductCategory.Description,
            result.ProductCategory.Description);
    }


    [Fact]
    public async Task GetDetailsProductQueryHandler_FailOnWrongId()
    {
        // Arrange

        var handler = new GetDetailsProductCategoryQueryHandler(_repositoryProductCategory);

        //Генерация случайного идентификатора

        var id = new Random().Next(_context.ProductCategories.Count(), 1000);

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