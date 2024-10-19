using MediatR;
using OnlineShop.Application.Repositories.Interfaces;

namespace OnlineShop.Application.Products.Queries.GetProductId;

public class GetProductIdQueryHandler(IRepositoryProduct repositoryProduct) : IRequestHandler<GetProductIdQuery, GetProductIdVM>
{
    public async Task<GetProductIdVM> Handle(GetProductIdQuery request, CancellationToken cancellationToken) =>
        await repositoryProduct.GetByIdAsync(request.Id, cancellationToken);
}