using MediatR;
using OnlineShop.Application.Repositories.Interfaces;

namespace OnlineShop.Application.Products.Queries.GetProductRange;

public class GetProductRangeQueryHandler(IRepositoryProduct repositoryProduct) : IRequestHandler<GetProductRangeQuery, ProductRangeVM>
{
    public async Task<ProductRangeVM> Handle(GetProductRangeQuery request, CancellationToken cancellationToken) =>
        await repositoryProduct.GetRangeAsync(request.CountSkip, request.CountTake, cancellationToken);
}