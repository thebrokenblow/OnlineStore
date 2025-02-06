using FluentValidation;
using MediatR;
using OnlineShop.Application.Repositories.Interfaces;

namespace OnlineShop.Application.Products.Queries.GetRangeProduct;

public class GetRangeProductQueryHandler(IRepositoryProduct repositoryProduct, IValidator<GetRangeProductQuery> validator) : IRequestHandler<GetRangeProductQuery, List<RangeProductDto>>
{
    public async Task<List<RangeProductDto>> Handle(GetRangeProductQuery request, CancellationToken cancellationToken)
    {
        validator.ValidateAndThrow(request);

        return await repositoryProduct.GetRangeAsync(request.CountSkip, request.CountTake, cancellationToken);
    }
}