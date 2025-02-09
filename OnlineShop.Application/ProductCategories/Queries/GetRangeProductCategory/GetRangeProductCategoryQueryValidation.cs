using FluentValidation;

namespace OnlineShop.Application.ProductCategories.Queries.GetRangeProductCategory;

public class GetRangeProductCategoryQueryValidation : AbstractValidator<GetRangeProductCategoryQuery>
{
    public const int MaxCountTake = 1000;

    public GetRangeProductCategoryQueryValidation()
    {
        RuleFor(getProductCategoryRangeQuery =>
                getProductCategoryRangeQuery.CountSkip)
            .GreaterThan(-1);

        RuleFor(getProductCategoryRangeQuery =>
            getProductCategoryRangeQuery.CountTake)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(MaxCountTake);
    }
}