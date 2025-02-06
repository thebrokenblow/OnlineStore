using FluentValidation;
using MediatR;
using OnlineShop.Application.Repositories.Interfaces;
using OnlineShop.Domain;

namespace OnlineShop.Application.ProductCategories.Commands.ProductCategoryCreation;

public class CreateProductCategoryCommandHandler(
    IRepositoryProductCategory repositoryProductCategory, 
    IValidator<CreateProductCategoryCommand> validator) : IRequestHandler<CreateProductCategoryCommand, int>
{
    public async Task<int> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        validator.ValidateAndThrow(request);

        var productCategory = new ProductCategory
        {
            Name = request.Name,
            Description = request.Description,
        };

        return await repositoryProductCategory.AddAsync(productCategory, cancellationToken);
    }
}