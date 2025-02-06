using FluentValidation;
using MediatR;
using OnlineShop.Application.Repositories.Interfaces;

namespace OnlineShop.Application.ProductCategories.Queries.GetRangeProductCategory;

public class GetRangeProductCategoryQueryHandler(
    IRepositoryProductCategory repositoryProductCategory,
    IValidator<GetRangeProductCategoryQuery> validator) : IRequestHandler<GetRangeProductCategoryQuery, List<RangeProductCategoryDto>>
{
    public async Task<List<RangeProductCategoryDto>> Handle(
        GetRangeProductCategoryQuery request, 
        CancellationToken cancellationToken)
    {
        validator.ValidateAndThrow(request);

        return await repositoryProductCategory.GetRangeAsync(request.CountSkip, request.CountTake, cancellationToken);
    }
}