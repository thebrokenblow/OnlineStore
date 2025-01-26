using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Domain;

public class ProductCategory
{
    public int Id { get; set; }

    [MaxLength(250)]
    public required string Name { get; set; }

    [MaxLength(1024)]
    public string? Description { get; set; }
}