using MediatR;
using OnlineShop.Application.Repositories.Interfaces;
using OnlineShop.Domain;

namespace OnlineShop.Application.Products.Commands.ProductCreation;

public class CreateProductCommandHandler(IRepositoryProduct repositoryProduct, IRepositoryProductCategory repositoryProductCategory) : IRequestHandler<CreateProductCommand, int>
{
    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productCategory = await repositoryProductCategory.GetByIdAsync(request.IdProductCategory, cancellationToken);

        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            IdProductCategory = productCategory.Id,
            ProductCategory = productCategory,
        };

        var idProduct = await repositoryProduct.AddAsync(product, cancellationToken);

        return idProduct;
    }
}