using FluentValidation;

namespace OnlineShop.Application.Products.Commands.ProductCreation;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public const int MaxNameLength = 250;
    public const int MaxDescriptionLength = 1024;

    public CreateProductCommandValidator()
    {
        RuleFor(createProductCommand =>
            createProductCommand.Name)
            .NotEmpty()
            .MaximumLength(MaxNameLength);

        RuleFor(createProductCommand =>
            createProductCommand.Description)
            .NotEmpty()
            .MaximumLength(MaxDescriptionLength);

        RuleFor(createProductCommand =>
            createProductCommand.Price)
            .GreaterThan(0);
    }
}