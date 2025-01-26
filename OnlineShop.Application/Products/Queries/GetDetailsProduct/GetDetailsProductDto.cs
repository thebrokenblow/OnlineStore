using OnlineShop.Application.ProductCategories.Queries.GetDetailsProductCategory;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Application.Products.Queries.GetDetailsProduct;

public class GetDetailsProductDto
{
    public required int Id { get; set; }

    [MaxLength(250)]
    public required string Name { get; set; }

    [MaxLength(1024)]
    public required string Description { get; set; }

    public required decimal Price { get; set; }

    public required GetDetailsProductCategoryDto ProductCategory { get; set; }
}