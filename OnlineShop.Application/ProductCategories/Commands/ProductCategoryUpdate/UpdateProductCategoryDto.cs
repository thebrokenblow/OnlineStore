namespace OnlineShop.Application.ProductCategories.Commands.ProductCategoryUpdate;

public class UpdateProductCategoryDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}