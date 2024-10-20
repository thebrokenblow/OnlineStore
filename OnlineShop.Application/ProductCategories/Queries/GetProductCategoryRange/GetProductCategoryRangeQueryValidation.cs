using FluentValidation;

namespace OnlineShop.Application.ProductCategories.Queries.GetProductCategoryRange;

public class GetProductCategoryRangeQueryValidation : AbstractValidator<GetProductCategoryRangeQuery>
{
    public GetProductCategoryRangeQueryValidation()
    {
        RuleFor(getProductCategoryRangeQuery =>
                getProductCategoryRangeQuery.CountSkip)
            .GreaterThan(-1);

        RuleFor(getProductCategoryRangeQuery =>
            getProductCategoryRangeQuery.CountTake)
            .GreaterThan(0);
    }
}