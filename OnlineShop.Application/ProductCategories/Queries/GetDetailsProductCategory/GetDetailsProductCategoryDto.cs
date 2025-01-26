using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Application.ProductCategories.Queries.GetDetailsProductCategory;

public class GetDetailsProductCategoryDto
{
    public required int Id { get; set; }

    [MaxLength(250)]
    public required string Name { get; set; }

    [MaxLength(1024)]
    public required string? Description { get; set; }
}