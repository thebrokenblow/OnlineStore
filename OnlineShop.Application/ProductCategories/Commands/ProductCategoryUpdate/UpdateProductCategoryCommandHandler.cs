using MediatR;
using OnlineShop.Application.Repositories.Interfaces;

namespace OnlineShop.Application.ProductCategories.Commands.ProductCategoryUpdate;

public class UpdateProductCategoryCommandHandler(IRepositoryProductCategory repositoryProductCategory) : IRequestHandler<UpdateProductCategoryCommand>
{
    public async Task Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var updateProductCategoryDto = new UpdateProductCategoryDto
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
        };

        await repositoryProductCategory.UpdateAsync(updateProductCategoryDto, cancellationToken);
    }
}