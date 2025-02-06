namespace OnlineShop.Application.ProductCategories.Queries.GetDetailsProductCategory;

public class DetailsProductCategoryDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string? Description { get; set; }
}