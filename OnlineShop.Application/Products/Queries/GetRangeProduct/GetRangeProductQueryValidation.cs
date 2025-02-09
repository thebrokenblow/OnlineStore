using FluentValidation;

namespace OnlineShop.Application.Products.Queries.GetRangeProduct;

public class GetRangeProductQueryValidation : AbstractValidator<GetRangeProductQuery>
{
    public const int MaxCountTake = 1000;

    public GetRangeProductQueryValidation()
    {
        RuleFor(getProductRangeQuery =>
                getProductRangeQuery.CountSkip)
            .GreaterThan(-1);

        RuleFor(getProductRangeQuery =>
            getProductRangeQuery.CountTake)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(MaxCountTake);
    }
}