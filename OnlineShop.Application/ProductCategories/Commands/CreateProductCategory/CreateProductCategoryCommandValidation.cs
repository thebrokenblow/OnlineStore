using FluentValidation;

namespace OnlineShop.Application.ProductCategories.Commands.CreateProductCategory;

public class CreateProductCategoryCommandValidation : AbstractValidator<CreateProductCategoryCommand>
{
    public CreateProductCategoryCommandValidation()
    {
        RuleFor(createProductCategoryCommand =>
            createProductCategoryCommand.Name)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(createProductCategoryCommand =>
            createProductCategoryCommand.Description)
            .NotEmpty()
            .MaximumLength(1024);
    }
}