using MediatR;
using OnlineShop.Application.Repositories.Interfaces;

namespace OnlineShop.Application.Products.Queries.GetProductDetails;

public class GetProductDetailsQueryHandler(IRepositoryProduct repositoryProduct) : IRequestHandler<GetProductDetailsQuery, GetProductDetailsVM>
{
    public async Task<GetProductDetailsVM> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
    {
        var getProductDetailsVM = await repositoryProduct.GetDetailsByIdAsync(request.Id, cancellationToken);
        return getProductDetailsVM;
    }
}