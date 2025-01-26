using MediatR;

namespace OnlineShop.Application.ProductCategories.Commands.ProductCategoryDeletion;

public class DeleteProductCategoryCommand : IRequest
{
    public required int Id { get; set; }
}