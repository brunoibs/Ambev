using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.ProductSale.Create;

public class CreateProductSaleValidator : AbstractValidator<CreateProductSaleCommand>
{
    public CreateProductSaleValidator()
    {
        RuleFor(ps => ps.IdSale).NotEmpty();
        RuleFor(ps => ps.IdProduct).NotEmpty();
        RuleFor(ps => ps.Amount).GreaterThan(0);
        RuleFor(ps => ps.Price).GreaterThan(0);
        RuleFor(ps => ps.Total).GreaterThan(0);
    }
} 