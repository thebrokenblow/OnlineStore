using FluentValidation;

namespace OnlineShop.Application.Products.Commands.ProductUpdate;

public class UpdateProductCommandValidation : AbstractValidator<UpdateProductCommand>
{
    public const int MaxNameLength = 250;
    public const int MaxDescriptionLength = 1024;

    public UpdateProductCommandValidation()
    {
        RuleFor(updateProductCommand =>
            updateProductCommand.Name)
            .NotEmpty()
            .MaximumLength(MaxNameLength);

        RuleFor(updateProductCommand =>
            updateProductCommand.Description)
            .NotEmpty()
            .MaximumLength(MaxDescriptionLength);

        RuleFor(updateProductCommand =>
            updateProductCommand.Price)
            .GreaterThan(0);
    }
}