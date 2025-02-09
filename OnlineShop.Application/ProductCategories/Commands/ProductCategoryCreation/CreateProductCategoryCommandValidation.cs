using FluentValidation;

namespace OnlineShop.Application.ProductCategories.Commands.ProductCategoryCreation;

public class CreateProductCategoryCommandValidation : AbstractValidator<CreateProductCategoryCommand>
{
    public const int MaxNameLength = 250;
    public const int MaxDescriptionLength = 1024;

    public CreateProductCategoryCommandValidation()
    {
        RuleFor(createProductCategoryCommand =>
            createProductCategoryCommand.Name)
            .NotEmpty()
            .MaximumLength(MaxNameLength);

        RuleFor(createProductCategoryCommand =>
            createProductCategoryCommand.Description)
            .MaximumLength(MaxDescriptionLength);
    }
}