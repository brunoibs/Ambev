using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sale.Get;

public class GetSaleValidator : AbstractValidator<GetSaleCommand>
{
    public GetSaleValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required");
    }
} 