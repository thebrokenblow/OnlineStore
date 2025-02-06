using MediatR;

namespace OnlineShop.Application.Products.Queries.GetDetailsProduct;

public class GetDetailsProductQuery : IRequest<DetailsProductDto>
{
    public int Id { get; set; }
}