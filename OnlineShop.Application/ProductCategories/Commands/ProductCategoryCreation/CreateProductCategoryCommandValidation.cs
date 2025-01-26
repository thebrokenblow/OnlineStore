using FluentValidation;

namespace OnlineShop.Application.ProductCategories.Commands.ProductCategoryCreation;

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