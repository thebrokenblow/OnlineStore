using MediatR;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Application.Products.Commands.ProductCreation;

public class CreateProductCommand : IRequest<int>
{
    [MaxLength(250)]
    public required string Name { get; set; }

    [MaxLength(1024)]
    public required string Description { get; set; }

    public required decimal Price { get; set; }

    public required int IdProductCategory { get; set; }
}