using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Product.Create;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(p => p.Name).NotEmpty().Length(2, 100);
        RuleFor(p => p.Price).GreaterThan(0);
        RuleFor(p => p.Amount).GreaterThanOrEqualTo(0);
    }
} 