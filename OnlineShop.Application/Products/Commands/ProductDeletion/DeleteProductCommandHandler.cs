using MediatR;
using OnlineShop.Application.Repositories.Interfaces;

namespace OnlineShop.Application.Products.Commands.ProductDeletion;

public class DeleteProductCommandHandler(IRepositoryProduct repositoryProduct) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken) =>
        await repositoryProduct.DeleteAsync(request.Id, cancellationToken);
}
