using FluentValidation;

namespace OnlineShop.Application.Products.Commands.ProductCreation;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(createProductCommand =>
            createProductCommand.Name)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(createProductCommand =>
            createProductCommand.Description)
            .NotEmpty()
            .MaximumLength(1024);

        RuleFor(createProductCommand =>
            createProductCommand.Price)
            .GreaterThan(0);
    }
}