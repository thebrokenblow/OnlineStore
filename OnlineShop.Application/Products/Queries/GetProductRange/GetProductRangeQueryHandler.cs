using AutoMapper;
using MediatR;
using OnlineShop.Application.Repositories.Interfaces;
using OnlineShop.Domain;
using System.Diagnostics.Metrics;

namespace OnlineShop.Application.Products.Queries.GetProductRange;

public class GetProductRangeQueryHandler(IRepositoryProduct repositoryProduct, IMapper mapper) : IRequestHandler<GetProductRangeQuery, ProductRangeVM>
{
    public async Task<ProductRangeVM> Handle(GetProductRangeQuery request, CancellationToken cancellationToken)
    {
        var products = await repositoryProduct.GetRangeAsync(request.CountSkip, request.CountTake, cancellationToken);
        var productsVM = mapper.Map<List<Product>, List<GetProductVM>>(products);

        return new ProductRangeVM
        {
            Products = productsVM
        };
    }
}