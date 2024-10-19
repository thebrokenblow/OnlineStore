using MediatR;
using OnlineShop.Domain;

namespace OnlineShop.Application.Products.Queries.GetProductDetails;

public class GetProductDetailsQuery : IRequest<Product>
{
    public int Id { get; set; }
}