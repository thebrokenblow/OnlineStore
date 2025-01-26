using MediatR;
using OnlineShop.Application.Repositories.Interfaces;

namespace OnlineShop.Application.Products.Queries.GetAllProduct;

public class GetAllProductQueryHandler(IRepositoryProduct repositoryProduct) : IRequestHandler<GetAllProductQuery, List<GetAllProductDto>>
{
    public async Task<List<GetAllProductDto>> Handle(GetAllProductQuery request, CancellationToken cancellationToken) =>
        await repositoryProduct.GetAllAsync(cancellationToken);
}