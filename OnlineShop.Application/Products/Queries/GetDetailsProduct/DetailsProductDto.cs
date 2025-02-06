using OnlineShop.Application.ProductCategories.Queries.GetDetailsProductCategory;

namespace OnlineShop.Application.Products.Queries.GetDetailsProduct;

public class DetailsProductDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required DetailsProductCategoryDto ProductCategory { get; set; }
}