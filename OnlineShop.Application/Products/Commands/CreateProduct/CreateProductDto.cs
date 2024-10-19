using OnlineShop.Domain;

namespace OnlineShop.Application.Products.Commands.CreateProduct;

public class CreateProductDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required ProductCategory ProductCategory { get; set; }
}