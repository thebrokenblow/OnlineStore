using MediatR;
using OnlineShop.Application.Repositories.Interfaces;

namespace OnlineShop.Application.ProductCategories.Commands.UpdateProductCategory;

public class UpdateProductCategoryCommandHandler(IRepositoryProductCategory repositoryProductCategory) : IRequestHandler<UpdateProductCategoryCommand>
{
    public async Task Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken) =>
        await repositoryProductCategory.UpdateAsync(request, cancellationToken);
}