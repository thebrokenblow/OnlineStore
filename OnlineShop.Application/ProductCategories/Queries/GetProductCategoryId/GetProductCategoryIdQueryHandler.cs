using MediatR;
using OnlineShop.Application.Repositories.Interfaces;
using OnlineShop.Domain;

namespace OnlineShop.Application.ProductCategories.Queries.GetProductCategoryId;

public class GetProductCategoryIdQueryHandler(IRepositoryProductCategory repositoryProductCategory) : IRequestHandler<GetProductCategoryIdQuery, ProductCategory>
{
    public async Task<ProductCategory> Handle(GetProductCategoryIdQuery request, CancellationToken cancellationToken) =>
        await repositoryProductCategory.GetByIdAsync(request.Id, cancellationToken);
}
