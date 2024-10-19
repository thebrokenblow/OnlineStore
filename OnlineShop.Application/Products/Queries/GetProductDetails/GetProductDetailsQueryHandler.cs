using MediatR;
using OnlineShop.Application.Repositories.Interfaces;
using OnlineShop.Domain;

namespace OnlineShop.Application.Products.Queries.GetProductDetails;

public class GetProductDetailsQueryHandler(IRepositoryProduct repositoryProduct) : IRequestHandler<GetProductDetailsQuery, Product>
{
    public async Task<Product> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
    {
        var product = await repositoryProduct.GetDetailsByIdAsync(request.Id, cancellationToken);
        return product;
    }
}