using FluentValidation;

namespace OnlineShop.Application.ProductCategories.Queries.GetRangeProductCategory;

public class GetRangeProductCategoryQueryValidation : AbstractValidator<GetRangeProductCategoryQuery>
{
    public GetRangeProductCategoryQueryValidation()
    {
        RuleFor(getProductCategoryRangeQuery =>
                getProductCategoryRangeQuery.CountSkip)
            .GreaterThan(-1);

        RuleFor(getProductCategoryRangeQuery =>
            getProductCategoryRangeQuery.CountTake)
            .GreaterThan(0);
    }
}