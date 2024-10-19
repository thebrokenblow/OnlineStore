using MediatR;
using OnlineShop.Application.Repositories.Interfaces;

namespace OnlineShop.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(IRepositoryProduct repositoryProduct, IRepositoryProductCategory repositoryProductCategory) : IRequestHandler<CreateProductCommand, int>
{
    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productCategory = await repositoryProductCategory.GetByIdAsync(request.IdProductCategory, cancellationToken);
        var productDto = new CreateProductDto
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            ProductCategory = productCategory,
        };

        var idProduct = await repositoryProduct.AddAsync(productDto, cancellationToken);

        return idProduct;
    }
}