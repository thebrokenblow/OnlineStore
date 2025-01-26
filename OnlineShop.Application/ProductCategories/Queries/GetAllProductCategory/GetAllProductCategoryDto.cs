using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Application.ProductCategories.Queries.GetAllProductCategory;

public class GetAllProductCategoryDto
{
    public required int Id { get; set; }

    [MaxLength(250)]
    public required string Name { get; set; }
}