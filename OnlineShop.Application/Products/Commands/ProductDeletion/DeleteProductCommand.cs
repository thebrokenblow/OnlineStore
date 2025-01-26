using MediatR;

namespace OnlineShop.Application.Products.Commands.ProductDeletion;

public class DeleteProductCommand : IRequest
{
    public int Id { get; set; }
}
