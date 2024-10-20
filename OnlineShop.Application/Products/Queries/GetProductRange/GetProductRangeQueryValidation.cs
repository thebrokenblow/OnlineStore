using FluentValidation;

namespace OnlineShop.Application.Products.Queries.GetProductRange;

public class GetProductRangeQueryValidation : AbstractValidator<GetProductRangeQuery>
{
    public GetProductRangeQueryValidation()
    {
        RuleFor(getProductRangeQuery =>
                getProductRangeQuery.CountSkip)
            .GreaterThan(-1);

        RuleFor(getProductRangeQuery =>
            getProductRangeQuery.CountTake)
            .GreaterThan(0);
    }
}