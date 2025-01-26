using MediatR;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Application.ProductCategories.Commands.ProductCategoryCreation;

public class CreateProductCategoryCommand : IRequest<int>
{
    [Required]
    [MinLength(1)]
    [MaxLength(250)]
    public required string Name { get; set; }

    [MinLength(1)]
    [MaxLength(1024)]
    public string? Description { get; set; }
}