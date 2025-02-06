using MediatR;

namespace OnlineShop.Application.ProductCategories.Queries.GetDetailsProductCategory;

public class GetDetailsProductCategoryQuery : IRequest<DetailsProductCategoryDto>
{
    public required int Id { get; set; }
}