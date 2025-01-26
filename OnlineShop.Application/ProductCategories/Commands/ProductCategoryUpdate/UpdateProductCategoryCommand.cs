using MediatR;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Application.ProductCategories.Commands.ProductCategoryUpdate;

public class UpdateProductCategoryCommand : IRequest
{
    [Required]
    public required int Id { get; set; }

    [MaxLength(250)]
    public required string Name { get; set; }

    [MaxLength(1024)]
    public string? Description { get; set; }
}