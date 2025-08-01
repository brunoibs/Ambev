using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.ProductSale.Delete;

public class DeleteProductSaleValidator : AbstractValidator<DeleteProductSaleCommand>
{
    public DeleteProductSaleValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ProductSale ID is required");
    }
} 