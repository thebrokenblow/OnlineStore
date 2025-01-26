using MediatR;
using OnlineShop.Application.Repositories.Interfaces;

namespace OnlineShop.Application.ProductCategories.Commands.ProductCategoryDeletion;

public class DeleteProductCategoryCommandHandler(IRepositoryProductCategory repositoryProductCategory) : IRequestHandler<DeleteProductCategoryCommand>
{
    public async Task Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken) =>
        await repositoryProductCategory.DeleteAsync(request.Id, cancellationToken);
}