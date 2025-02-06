using MediatR;

namespace OnlineShop.Application.ProductCategories.Commands.ProductCategoryCreation;

public class CreateProductCategoryCommand : IRequest<int>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}