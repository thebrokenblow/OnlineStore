using MediatR;
using OnlineShop.Application.Repositories.Interfaces;

namespace OnlineShop.Application.ProductCategories.Queries.GetProductCategoryRange;

public class GetProductCategoryRangeQueryHandler(IRepositoryProductCategory repositoryProductCategory) : IRequestHandler<GetProductCategoryRangeQuery, ProductCategoryRangeVM>
{
    public async Task<ProductCategoryRangeVM> Handle(GetProductCategoryRangeQuery request, CancellationToken cancellationToken) =>
        await repositoryProductCategory.GetRangeAsync(request.CountSkip, request.CountTake, cancellationToken);
}
