using OnlineShop.Domain;

namespace OnlineShop.Application.Products.Queries.GetProductRange;

public class ProductVM
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required ProductCategory ProductCategory { get; set; }
}