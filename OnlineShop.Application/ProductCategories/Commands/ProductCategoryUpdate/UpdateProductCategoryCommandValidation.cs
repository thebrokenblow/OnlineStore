using FluentValidation;

namespace OnlineShop.Application.ProductCategories.Commands.ProductCategoryUpdate;

public class UpdateProductCategoryCommandValidation : AbstractValidator<UpdateProductCategoryCommand>
{
    public UpdateProductCategoryCommandValidation()
    {
        RuleFor(updateProductCategoryCommand =>
            updateProductCategoryCommand.Name)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(updateProductCategoryCommand =>
            updateProductCategoryCommand.Description)
            .MaximumLength(1024);
    }
}