using MediatR;

namespace OnlineShop.Application.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<int>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required int IdProductCategory { get; set; }
}