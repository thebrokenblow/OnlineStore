using MediatR;
using OnlineShop.Domain;

namespace OnlineShop.Application.ProductCategories.Queries.GetProductCategoryId;

public class GetProductCategoryIdQuery : IRequest<ProductCategory>
{
    public int Id { get; set; }
}
