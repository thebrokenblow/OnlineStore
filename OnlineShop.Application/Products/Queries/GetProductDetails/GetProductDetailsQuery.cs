using MediatR;

namespace OnlineShop.Application.Products.Queries.GetProductDetails;

public class GetProductDetailsQuery : IRequest<GetProductDetailsVM>
{
    public int Id { get; set; }
}