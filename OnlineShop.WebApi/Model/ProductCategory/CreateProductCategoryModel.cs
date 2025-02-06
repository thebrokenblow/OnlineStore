namespace OnlineShop.WebApi.Model.ProductCategory;

public class CreateProductCategoryModel
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}