using MediatR;
using OnlineShop.Domain;

namespace OnlineShop.Application.ProductCategories.Queries.GetDetailsProductCategory;

public class GetDetailsProductCategoryQuery : IRequest<GetDetailsProductCategoryDto>
{
    public int Id { get; set; }
}