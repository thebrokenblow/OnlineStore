namespace OnlineShop.Application.Products.Queries.GetAllProduct;

public class AllProductDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
}