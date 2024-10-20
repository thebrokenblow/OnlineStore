using FluentValidation;

namespace OnlineShop.Application.ProductCategories.Commands.UpdateProductCategory;

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
            .NotEmpty()
            .MaximumLength(1024);
    }
}