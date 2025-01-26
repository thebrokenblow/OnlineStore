using MediatR;

namespace OnlineShop.Application.Products.Queries.GetDetailsProduct;

public class GetDetailsProductQuery : IRequest<GetDetailsProductDto>
{
    public int Id { get; set; }
}