using FluentValidation;
using MediatR;
using OnlineShop.Application.Repositories.Interfaces;
using OnlineShop.Domain;

namespace OnlineShop.Application.Products.Commands.ProductCreation;

public class CreateProductCommandHandler(
    IRepositoryProduct repositoryProduct, 
    IValidator<CreateProductCommand> validator) : IRequestHandler<CreateProductCommand, int>
{
    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        validator.ValidateAndThrow(request);

        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            IdProductCategory = request.IdProductCategory,
            ProductCategory = null,
        };

        var idProduct = await repositoryProduct.AddAsync(product, cancellationToken);

        return idProduct;
    }
}