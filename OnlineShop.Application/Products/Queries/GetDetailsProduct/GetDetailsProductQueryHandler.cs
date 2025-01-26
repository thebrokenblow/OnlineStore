using MediatR;
using OnlineShop.Application.Repositories.Interfaces;

namespace OnlineShop.Application.Products.Queries.GetDetailsProduct;

public class GetDetailsProductQueryHandler(IRepositoryProduct repositoryProduct) : IRequestHandler<GetDetailsProductQuery, GetDetailsProductDto>
{
    public async Task<GetDetailsProductDto> Handle(GetDetailsProductQuery request, CancellationToken cancellationToken) => 
        await repositoryProduct.GetDetailsByIdAsync(request.Id, cancellationToken);
}