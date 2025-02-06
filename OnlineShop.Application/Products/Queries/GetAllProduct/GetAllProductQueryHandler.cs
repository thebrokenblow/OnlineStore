using MediatR;
using OnlineShop.Application.Repositories.Interfaces;

namespace OnlineShop.Application.Products.Queries.GetAllProduct;

public class GetAllProductQueryHandler(IRepositoryProduct repositoryProduct) : IRequestHandler<GetAllProductQuery, List<AllProductDto>>
{
    public async Task<List<AllProductDto>> Handle(GetAllProductQuery request, CancellationToken cancellationToken) =>
        await repositoryProduct.GetAllAsync(cancellationToken);
}