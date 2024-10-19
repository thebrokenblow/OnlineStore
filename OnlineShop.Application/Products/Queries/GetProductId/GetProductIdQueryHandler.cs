using AutoMapper;
using MediatR;
using OnlineShop.Application.Repositories.Interfaces;

namespace OnlineShop.Application.Products.Queries.GetProductId;

public class GetProductIdQueryHandler(IRepositoryProduct repositoryProduct, IMapper mapper) : IRequestHandler<GetProductIdQuery, GetProductIdVM>
{
    public async Task<GetProductIdVM> Handle(GetProductIdQuery request, CancellationToken cancellationToken)
    {
        var product = await repositoryProduct.GetByIdAsync(request.Id, cancellationToken);
        var productIdVM = mapper.Map<GetProductIdVM>(product);

        return productIdVM;
    }
}