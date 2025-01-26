using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Application.Products.Queries.GetAllProduct;

public class GetAllProductDto
{
    public required int Id { get; set; }

    [MaxLength(250)]
    public required string Name { get; set; }
}