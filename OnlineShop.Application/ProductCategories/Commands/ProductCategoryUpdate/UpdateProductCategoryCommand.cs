using MediatR;

namespace OnlineShop.Application.ProductCategories.Commands.ProductCategoryUpdate;

public class UpdateProductCategoryCommand : IRequest
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}