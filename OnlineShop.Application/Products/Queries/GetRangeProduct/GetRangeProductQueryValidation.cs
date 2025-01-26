using FluentValidation;

namespace OnlineShop.Application.Products.Queries.GetRangeProduct;

public class GetRangeProductQueryValidation : AbstractValidator<GetRangeProductQuery>
{
    public GetRangeProductQueryValidation()
    {
        RuleFor(getProductRangeQuery =>
                getProductRangeQuery.CountSkip)
            .GreaterThan(-1);

        RuleFor(getProductRangeQuery =>
            getProductRangeQuery.CountTake)
            .GreaterThan(0);
    }
}