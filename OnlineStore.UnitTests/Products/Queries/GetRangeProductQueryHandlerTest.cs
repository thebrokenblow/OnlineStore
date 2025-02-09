using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Products.Queries.GetRangeProduct;
using OnlineStore.UnitTests.Common.CommonProduct;
using Shouldly;

namespace OnlineStore.UnitTests.Products.Queries;

public class GetRangeProductQueryHandlerTest : TestProductBase
{
    private readonly GetRangeProductQueryHandler _handler;
    private readonly int _maxCountTake;

    public GetRangeProductQueryHandlerTest()
    {
        var getRangeProductQueryValidation = new GetRangeProductQueryValidation();
        _handler = new GetRangeProductQueryHandler(_repositoryProduct, getRangeProductQueryValidation);
        _maxCountTake = GetRangeProductQueryValidation.MaxCountTake;
    }

    [Theory(DisplayName = "Should return correct number of products")]
    [InlineData(0, 10)]
    [InlineData(5, 5)]
    [InlineData(10, 10)]
    [InlineData(0, 20)]
    public async Task GetRangeProductQueryHandler_Success(int countSkip, int countTake)
    {
        // Arrange
        var getRangeProductQuery = new GetRangeProductQuery
        {
            CountSkip = countSkip,
            CountTake = countTake,
        };

        // Act
        var result = await _handler.Handle(getRangeProductQuery, CancellationToken.None);

        var countProduct = await _context.Products
                                    .Skip(countSkip)
                                    .Take(countTake)
                                    .CountAsync();

        // Assert
        result.Count.ShouldBe(countProduct);
    }

    [Fact(DisplayName = "Should throw ValidationException for negative CountSkip")]
    public async Task GetRangeProductQueryHandler_FailOnNegativeCountSkip()
    {
        // Arrange
        var getRangeProductQuery = new GetRangeProductQuery
        {
            CountSkip = -1,
            CountTake = 10,
        };

        // Act & Assert
        (await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(getRangeProductQuery, CancellationToken.None))).ShouldNotBeNull();
    }

    [Fact(DisplayName = "Should throw ValidationException for negative CountTake")]
    public async Task GetRangeProductQueryHandler_FailOnNegativeCountTake()
    {
        // Arrange
        var getRangeProductQuery = new GetRangeProductQuery
        {
            CountSkip = 0,
            CountTake = -1,
        };

        // Act & Assert
        (await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(getRangeProductQuery, CancellationToken.None))).ShouldNotBeNull();
    }

    [Fact(DisplayName = "Should throw ValidationException for CountTake greater than MaxCountTake")]
    public async Task GetRangeProductQueryHandler_FailOnCountTakeGreaterThanMax()
    {
        // Arrange
        var invalidCountTake = _maxCountTake + 1;
        var getRangeProductQuery = new GetRangeProductQuery
        {
            CountSkip = 0,
            CountTake = invalidCountTake,
        };

        // Act & Assert
        (await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(getRangeProductQuery, CancellationToken.None))).ShouldNotBeNull();
    }

    [Fact(DisplayName = "Should return all products for max CountTake")]
    public async Task GetRangeProductQueryHandler_SuccessOnMaxCountTake()
    {
        // Arrange
        int countSkip = 0;
        var getRangeProductQuery = new GetRangeProductQuery
        {
            CountSkip = countSkip,
            CountTake = _maxCountTake,
        };

        var countProduct = await _context.Products
                                        .Skip(countSkip)
                                        .Take(_maxCountTake)
                                        .CountAsync();

        // Act
        var result = await _handler.Handle(getRangeProductQuery, CancellationToken.None);

        // Assert
        result.Count.ShouldBe(countProduct);
    }

    [Fact(DisplayName = "Should return correct number of products for CountSkip equal to CountTake")]
    public async Task GetRangeProductQueryHandler_SuccessOnCountSkipEqualToCountTake()
    {
        // Arrange
        int countSkip = 5;
        int countTake = 5;

        var getRangeProductQuery = new GetRangeProductQuery
        {
            CountSkip = countSkip,
            CountTake = countTake,
        };

        int countProduct = await _context.Products
                                                .Skip(countSkip)
                                                .Take(countTake)
                                                .CountAsync();

        // Act
        var result = await _handler.Handle(getRangeProductQuery, CancellationToken.None);

        // Assert
        result.Count.ShouldBe(countProduct);
    }

    [Fact(DisplayName = "Should return empty result for zero CountSkip and CountTake")]
    public async Task GetRangeProductQueryHandler_SuccessOnZeroValues()
    {
        // Arrange
        var getRangeProductQuery = new GetRangeProductQuery
        {
            CountSkip = 0,
            CountTake = 0,
        };

        // Act
        var result = await _handler.Handle(getRangeProductQuery, CancellationToken.None);

        // Assert
        result.Count.ShouldBe(0);
    }

    [Fact(DisplayName = "Should return empty result for CountSkip exceeding available products")]
    public async Task GetRangeProductQueryHandler_SuccessOnLargeCountSkip()
    {
        // Arrange
        int countSkip = int.MaxValue; // Extremely large value
        int countTake = 10;

        var getRangeProductQuery = new GetRangeProductQuery
        {
            CountSkip = countSkip,
            CountTake = countTake,
        };

        // Act
        var result = await _handler.Handle(getRangeProductQuery, CancellationToken.None);

        // Assert
        result.Count.ShouldBe(0);
    }

    [Fact(DisplayName = "Should return products for CountTake just below MaxCountTake")]
    public async Task GetRangeProductQueryHandler_SuccessOnLargeCountTake()
    {
        // Arrange
        int countSkip = 0;
        int countTake = _maxCountTake - 1;

        var getRangeProductQuery = new GetRangeProductQuery
        {
            CountSkip = countSkip,
            CountTake = countTake,
        };

        var countProduct = await _context.Products
                                        .Skip(countSkip)
                                        .Take(countTake)
                                        .CountAsync();

        // Act
        var result = await _handler.Handle(getRangeProductQuery, CancellationToken.None);

        // Assert
        result.Count.ShouldBe(countProduct);
    }

    [Fact(DisplayName = "Should throw ValidationException for negative CountSkip and CountTake")]
    public async Task GetRangeProductQueryHandler_FailOnNegativeCountSkipAndCountTake()
    {
        // Arrange
        var getRangeProductQuery = new GetRangeProductQuery
        {
            CountSkip = -1,
            CountTake = -1,
        };

        // Act & Assert
        (await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(getRangeProductQuery, CancellationToken.None))).ShouldNotBeNull();
    }

    [Fact(DisplayName = "Should throw ValidationException for negative CountSkip and zero CountTake")]
    public async Task GetRangeProductQueryHandler_FailOnNegativeCountSkipAndZeroCountTake()
    {
        // Arrange
        var getRangeProductQuery = new GetRangeProductQuery
        {
            CountSkip = -1,
            CountTake = 0,
        };

        // Act & Assert
        (await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(getRangeProductQuery, CancellationToken.None))).ShouldNotBeNull();
    }

    [Fact(DisplayName = "Should throw ValidationException for zero CountSkip and negative CountTake")]
    public async Task GetRangeProductQueryHandler_FailOnZeroCountSkipAndNegativeCountTake()
    {
        // Arrange
        var getRangeProductQuery = new GetRangeProductQuery
        {
            CountSkip = 0,
            CountTake = -1,
        };

        // Act & Assert
        (await Should.ThrowAsync<ValidationException>(async () =>
            await _handler.Handle(getRangeProductQuery, CancellationToken.None))).ShouldNotBeNull();
    }
}
