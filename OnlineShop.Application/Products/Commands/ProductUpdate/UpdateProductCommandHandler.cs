using FluentValidation;
using MediatR;
using OnlineShop.Application.Repositories.Interfaces;

namespace OnlineShop.Application.Products.Commands.ProductUpdate;

public class UpdateProductCommandHandler(IRepositoryProduct repositoryProduct, IValidator<UpdateProductCommand> validator) : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        validator.ValidateAndThrow(request);

        var updateProductDto = new UpdateProductDto
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            IdProductCategory = request.IdProductCategory,
        };

        await repositoryProduct.UpdateAsync(updateProductDto, cancellationToken);
    }
}