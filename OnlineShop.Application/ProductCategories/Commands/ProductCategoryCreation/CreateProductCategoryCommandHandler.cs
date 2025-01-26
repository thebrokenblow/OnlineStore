using MediatR;
using OnlineShop.Application.Repositories.Interfaces;
using OnlineShop.Domain;

namespace OnlineShop.Application.ProductCategories.Commands.ProductCategoryCreation;

public class CreateProductCategoryCommandHandler(IRepositoryProductCategory repositoryProductCategory) : IRequestHandler<CreateProductCategoryCommand, int>
{
    public async Task<int> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var productCategory = new ProductCategory
        {
            Name = request.Name,
            Description = request.Description,
        };

        return await repositoryProductCategory.AddAsync(productCategory, cancellationToken);
    }
}