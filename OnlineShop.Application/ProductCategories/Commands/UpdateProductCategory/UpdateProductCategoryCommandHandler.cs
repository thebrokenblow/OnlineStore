using MediatR;
using OnlineShop.Application.Repositories.Interfaces;

namespace OnlineShop.Application.ProductCategories.Commands.UpdateProductCategory;

public class UpdateProductCategoryCommandHandler(IRepositoryProductCategory repositoryProductCategory) : IRequestHandler<UpdateProductCategoryCommand, int>
{
    public async Task<int> Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        await repositoryProductCategory.UpdateAsync(request, cancellationToken);

        return request.Id;
    }
}