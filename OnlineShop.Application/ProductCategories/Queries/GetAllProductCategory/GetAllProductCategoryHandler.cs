using MediatR;
using OnlineShop.Application.Repositories.Interfaces;

namespace OnlineShop.Application.ProductCategories.Queries.GetAllProductCategory;

public class GetAllProductCategoryHandler(IRepositoryProductCategory repositoryProductCategory) : IRequestHandler<GetAllProductCategoryQuery, List<GetAllProductCategoryDto>>
{
    public async Task<List<GetAllProductCategoryDto>> Handle(GetAllProductCategoryQuery request, CancellationToken cancellationToken) =>
        await repositoryProductCategory.GetAllAsync(cancellationToken);
}