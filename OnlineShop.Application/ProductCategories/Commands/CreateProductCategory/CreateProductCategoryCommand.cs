using MediatR;

namespace OnlineShop.Application.ProductCategories.Commands.CreateProductCategory;

public class CreateProductCategoryCommand : IRequest<int>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
}