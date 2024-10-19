using MediatR;

namespace OnlineShop.Application.Products.Queries.GetProductId;

public class GetProductIdQuery : IRequest<GetProductIdVM>
{
    public int Id { get; set; }
}