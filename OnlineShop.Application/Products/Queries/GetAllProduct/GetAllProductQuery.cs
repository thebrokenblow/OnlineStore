using MediatR;

namespace OnlineShop.Application.Products.Queries.GetAllProduct;

public class GetAllProductQuery : IRequest<List<AllProductDto>>
{
}