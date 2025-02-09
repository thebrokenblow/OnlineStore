using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.ProductCategories.Queries.GetRangeProductCategory;
using OnlineStore.UnitTests.Common.CommonProductCategory;
using Shouldly;

namespace OnlineStore.UnitTests.ProductCategories.Queries;

public class GetRangeProductCategoryQueryHandlerTest : TestProductCategoryBase
{
    private readonly GetRangeProductCategoryQueryHandler _handler;
    private readonly int _maxCountTake;

    public GetRangeProductCategoryQueryHandlerTest()
    {
        var getRangeProductCategoryQueryValidation = new GetRangeProductCategoryQueryValidation();
        _handler = new GetRangeProductCategoryQueryHandler(
                        _productCategoryRepository,
                        getRangeProductCategoryQueryValidation);

        _maxCountTake = GetRangeProductCategoryQueryValidation.MaxCountTake;
    }

    [Theory(DisplayName = "Should return correct number of product categories")]
    [InlineData(0, 10)]
    [InlineData(5, 5)]
    [InlineData(10, 10)]
    [InlineData(0, 20)]
    public async Task GetRangeProductCategoryQueryHandler_Success(int countSkip, int countTake)
    {
        // Arrange
        var getRangeProductCategoryQuery = new GetRangeProductCategoryQuery
        {
            CountSkip = countSkip,
            CountTake = countTake,
        };

        var countProductCategory = await _context.ProductCategories
                                            .Skip(countSkip)
                                            .Take(countTake)
                                            .CountAsync();

        // Act
        var result = await _handler.Handle(
                                getRangeProductCategoryQuery,
                                CancellationToken.None);

        // Assert
        result.Count.ShouldBe(countProductCategory);
    }

    [Fact(DisplayName = "Should throw ValidationException for negative CountSkip")]
    public async Task GetRangeProductCategoryQueryHandler_FailOnNegativeCountSkip()
    {
        // Arrange
        var getRangeProductCategoryQuery = new GetRangeProductCategoryQuery
        {
            CountSkip = -1,
            CountTake = 10,
        };

        // Act & Assert
        (await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(getRangeProductCategoryQuery, CancellationToken.None))).ShouldNotBeNull();
    }

    [Fact(DisplayName = "Should throw ValidationException for zero CountTake")]
    public async Task GetRangeProductCategoryQueryHandler_FailOnZeroCountTake()
    {
        // Arrange
        int countTake = 0;

        var getRangeProductCategoryQuery = new GetRangeProductCategoryQuery
        {
            CountSkip = 0,
            CountTake = countTake,
        };

        var result = await _handler.Handle(getRangeProductCategoryQuery, CancellationToken.None);

        result.Count.ShouldBe(countTake);
    }

    [Fact(DisplayName = "Should throw ValidationException for negative CountTake")]
    public async Task GetRangeProductCategoryQueryHandler_FailOnNegativeCountTake()
    {
        // Arrange
        var getRangeProductCategoryQuery = new GetRangeProductCategoryQuery
        {
            CountSkip = 0,
            CountTake = -1,
        };

        // Act & Assert
        (await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(getRangeProductCategoryQuery, CancellationToken.None))).ShouldNotBeNull();
    }

    [Fact(DisplayName = "Should throw ValidationException for CountTake greater than MaxCountTake")]
    public async Task GetRangeProductCategoryQueryHandler_FailOnCountTakeGreaterThanMax()
    {
        // Arrange
        var invalidCountTake = _maxCountTake + 1;
        var getRangeProductCategoryQuery = new GetRangeProductCategoryQuery
        {
            CountSkip = 0,
            CountTake = invalidCountTake,
        };

        // Act & Assert
        (await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(getRangeProductCategoryQuery, CancellationToken.None))).ShouldNotBeNull();
    }

    [Fact(DisplayName = "Should return all product categories for max CountTake")]
    public async Task GetRangeProductCategoryQueryHandler_SuccessOnMaxCountTake()
    {
        // Arrange
        int countSkip = 0;
        var getRangeProductCategoryQuery = new GetRangeProductCategoryQuery
        {
            CountSkip = countSkip,
            CountTake = _maxCountTake,
        };

        var countProductCategory = await _context.ProductCategories
                                            .Skip(countSkip)
                                            .Take(_maxCountTake)
                                            .CountAsync();

        // Act
        var result = await _handler.Handle(
                                getRangeProductCategoryQuery,
                                CancellationToken.None);

        // Assert
        result.Count.ShouldBe(countProductCategory);
    }

    [Fact(DisplayName = "Should return correct number of product categories for CountSkip equal to CountTake")]
    public async Task GetRangeProductCategoryQueryHandler_SuccessOnCountSkipEqualToCountTake()
    {
        // Arrange
        int countSkip = 5;
        int countTake = 5;

        var getRangeProductCategoryQuery = new GetRangeProductCategoryQuery
        {
            CountSkip = countSkip,
            CountTake = countTake,
        };

        int countProductCategory = await _context.ProductCategories
                                                        .Skip(countSkip)
                                                        .Take(countTake)
                                                        .CountAsync();

        // Act
        var result = await _handler.Handle(
                                getRangeProductCategoryQuery,
                                CancellationToken.None);

        // Assert
        result.Count.ShouldBe(countProductCategory);
    }

    [Fact(DisplayName = "Should return empty result for CountSkip greater than CountTake")]
    public async Task GetRangeProductCategoryQueryHandler_SuccessOnCountSkipGreaterThanCountTake()
    {
        // Arrange
        int countSkip = 10;
        int countTake = 5;

        var getRangeProductCategoryQuery = new GetRangeProductCategoryQuery
        {
            CountSkip = countSkip,
            CountTake = countTake,
        };

        var countProductCategory = await _context.ProductCategories
                                                        .Skip(countSkip)
                                                        .Take(countTake)
                                                        .CountAsync();

        // Act
        var result = await _handler.Handle(
                                getRangeProductCategoryQuery,
                                CancellationToken.None);

        // Assert
        result.Count.ShouldBe(countProductCategory);
    }
}
