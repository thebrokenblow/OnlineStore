using MediatR;

namespace OnlineShop.Application.ProductCategories.Commands.DeleteProductCategory;

public class DeleteProductCategoryCommand : IRequest
{
    public required int Id { get; set; }
}