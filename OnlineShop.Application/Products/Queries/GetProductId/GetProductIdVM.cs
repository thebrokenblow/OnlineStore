using OnlineShop.Domain;

namespace OnlineShop.Application.Products.Queries.GetProductId;

public class GetProductIdVM
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
}