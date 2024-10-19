using MediatR;
using OnlineShop.Application.Repositories.Interfaces;

namespace OnlineShop.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler(IRepositoryProduct repositoryProduct, IRepositoryProductCategory repositoryProductCategory) : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productCategory = await repositoryProductCategory.GetByIdAsync(request.Id, cancellationToken);

        var updateProductDto = new UpdateProductDto
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            ProductCategory = productCategory,
        };

        await repositoryProduct.UpdateAsync(updateProductDto, cancellationToken);
    }
}