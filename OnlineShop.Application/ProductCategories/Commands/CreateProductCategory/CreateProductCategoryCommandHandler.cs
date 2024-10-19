using MediatR;
using OnlineShop.Application.Repositories.Interfaces;

namespace OnlineShop.Application.ProductCategories.Commands.CreateProductCategory;

public class CreateProductCategoryCommandHandler(IRepositoryProductCategory repositoryProductCategory) : IRequestHandler<CreateProductCategoryCommand, int>
{
    public async Task<int> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken) =>
        await repositoryProductCategory.AddAsync(request, cancellationToken);
}