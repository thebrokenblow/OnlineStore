using FluentValidation;
using MediatR;
using OnlineShop.Application.Repositories.Interfaces;

namespace OnlineShop.Application.ProductCategories.Commands.ProductCategoryUpdate;

public class UpdateProductCategoryCommandHandler(
    IRepositoryProductCategory repositoryProductCategory, 
    IValidator<UpdateProductCategoryCommand> validator) : IRequestHandler<UpdateProductCategoryCommand>
{
    public async Task Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        validator.ValidateAndThrow(request);

        var updateProductCategoryDto = new UpdateProductCategoryDto
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
        };

        await repositoryProductCategory.UpdateAsync(updateProductCategoryDto, cancellationToken);
    }
}