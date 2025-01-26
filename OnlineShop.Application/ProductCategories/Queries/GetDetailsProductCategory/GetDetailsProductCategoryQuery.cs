using MediatR;

namespace OnlineShop.Application.ProductCategories.Queries.GetDetailsProductCategory;

public class GetDetailsProductCategoryQuery : IRequest<GetDetailsProductCategoryDto>
{
    public required int Id { get; set; }
}