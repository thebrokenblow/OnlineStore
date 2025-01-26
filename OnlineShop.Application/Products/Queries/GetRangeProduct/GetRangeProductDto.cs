using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Application.Products.Queries.GetRangeProduct;

public class GetRangeProductDto
{
    public required int Id { get; set; }

    [MaxLength(250)]
    public required string Name { get; set; }
}