using MediatR;

namespace OnlineShop.Application.ProductCategories.Queries.GetAllProductCategory;

public class GetAllProductCategoryQuery : IRequest<List<GetAllProductCategoryDto>>
{
}