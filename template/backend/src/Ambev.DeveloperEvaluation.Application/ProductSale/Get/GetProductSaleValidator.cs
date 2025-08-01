using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.ProductSale.Get;

public class GetProductSaleValidator : AbstractValidator<GetProductSaleCommand>
{
    public GetProductSaleValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ProductSale ID is required");
    }
} 