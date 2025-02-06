using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Common.Exceptions;
using OnlineShop.Application.ProductCategories.Queries.GetDetailsProductCategory;
using OnlineStore.UnitTests.Common.CommonProductCategory;

namespace OnlineStore.UnitTests.ProductCategories.Queries;

public class GetDetailsProductCategoryQueryHandlerTest : TestProductCategoryBase
{
    [Fact]
    public async Task GetDetailsProductCategoryQueryHandler_Success()
    {
        // Arrange
        var handler = new GetDetailsProductCategoryQueryHandler(_productCategoryRepository);

        var getDetailsProductCategoryQuery = new GetDetailsProductCategoryQuery
        {
            Id = _productCategoryContextFactory.ProductCategoryIdForDetails,
        };

        // Act
        var result = await handler.Handle(
            getDetailsProductCategoryQuery,
            CancellationToken.None);

        // Assert
        Assert.Equal(_productCategoryContextFactory.ProductCategoryNameGardenTools, result.Name);
        Assert.Equal(_productCategoryContextFactory.ProductCategoryDescriptionGardenTools, result.Description);
    }


    [Fact]
    public async Task GetDetailsProductCategoryQueryHandler_FailOnWrongId()
    {
        // Arrange
        var handler = new GetDetailsProductCategoryQueryHandler(_productCategoryRepository);

        //Генерация случайного идентификатора
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