using MediatR;
using OnlineShop.Application.Repositories.Interfaces;

namespace OnlineShop.Application.ProductCategories.Queries.GetAllProductCategory;

public class GetAllProductCategoryHandler(IRepositoryProductCategory repositoryProductCategory) : IRequestHandler<GetAllProductCategoryQuery, List<AllProductCategoryDto>>
{
    public async Task<List<AllProductCategoryDto>> Handle(GetAllProductCategoryQuery request, CancellationToken cancellationToken) =>
        await repositoryProductCategory.GetAllAsync(cancellationToken);
}