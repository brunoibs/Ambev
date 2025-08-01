using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sale.Delete;

public class DeleteSaleValidator : AbstractValidator<DeleteSaleCommand>
{
    public DeleteSaleValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required");
    }
} 