using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Product.Get;

public class GetProductValidator : AbstractValidator<GetProductCommand>
{
    public GetProductValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ID do produto é obrigatório");
    }
} 