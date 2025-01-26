using MediatR;
using OnlineShop.Application.Repositories.Interfaces;

namespace OnlineShop.Application.ProductCategories.Queries.GetDetailsProductCategory;

public class GetDetailsProductCategoryQueryHandler(IRepositoryProductCategory repositoryProductCategory) : IRequestHandler<GetDetailsProductCategoryQuery, GetDetailsProductCategoryDto>
{
    public async Task<GetDetailsProductCategoryDto> Handle(GetDetailsProductCategoryQuery request, CancellationToken cancellationToken) =>
        await repositoryProductCategory.GetDetailsAsync(request.Id, cancellationToken);
}
