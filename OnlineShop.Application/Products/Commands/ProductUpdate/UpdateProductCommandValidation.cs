using FluentValidation;

namespace OnlineShop.Application.Products.Commands.ProductUpdate;

public class UpdateProductCommandValidation : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidation()
    {
        RuleFor(updateProductCommand =>
            updateProductCommand.Name)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(updateProductCommand =>
            updateProductCommand.Description)
            .NotEmpty()
            .MaximumLength(1024);

        RuleFor(updateProductCommand =>
            updateProductCommand.Price)
            .NotEqual(0);
    }
}