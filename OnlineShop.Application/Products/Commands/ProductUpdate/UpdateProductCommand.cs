using MediatR;

namespace OnlineShop.Application.Products.Commands.ProductUpdate;

public class UpdateProductCommand : IRequest
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required int IdProductCategory { get; set; }
}