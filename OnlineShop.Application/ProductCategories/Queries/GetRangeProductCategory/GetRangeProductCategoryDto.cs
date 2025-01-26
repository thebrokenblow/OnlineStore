using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Application.ProductCategories.Queries.GetRangeProductCategory;

public class GetRangeProductCategoryDto
{
    public required int Id { get; set; }

    [MaxLength(250)]
    public required string Name { get; set; }
}