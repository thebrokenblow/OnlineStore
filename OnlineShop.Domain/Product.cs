namespace OnlineShop.Domain;

public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public int IdProductCategory { get; set; }
    public required ProductCategory ProductCategory { get; set; }
}