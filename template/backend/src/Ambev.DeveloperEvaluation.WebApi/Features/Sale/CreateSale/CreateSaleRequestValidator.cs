using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale;

/// <summary>
/// Validador para CreateSaleRequest
/// </summary>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
    {
        RuleFor(x => x.DtSale)
            .NotEmpty()
            .WithMessage("A data da venda é obrigatória");

        RuleFor(x => x.IdCustomer)
            .NotEmpty()
            .WithMessage("O ID do cliente é obrigatório");

        RuleFor(x => x.IdCreate)
            .NotEmpty()
            .WithMessage("O ID do usuário que está criando a venda é obrigatório");

        RuleFor(x => x.ProductSales)
            .NotEmpty()
            .WithMessage("A venda deve ter pelo menos um item");

        RuleForEach(x => x.ProductSales)
            .SetValidator(new ProductSaleRequestValidator());
    }
}

/// <summary>
/// Validador para ProductSaleRequest
/// </summary>
public class ProductSaleRequestValidator : AbstractValidator<ProductSaleRequest>
{
    public ProductSaleRequestValidator()
    {
        RuleFor(x => x.IdProduct)
            .NotEmpty()
            .WithMessage("O ID do produto é obrigatório");

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .LessThanOrEqualTo(20)
            .WithMessage("A quantidade deve estar entre 1 e 20");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("O preço deve ser maior que zero");

        RuleFor(x => x.Total)
            .GreaterThan(0)
            .WithMessage("O total deve ser maior que zero");

        RuleFor(x => x.Total)
            .Must((request, total) => total == request.Price * request.Amount)
            .WithMessage("O total deve corresponder ao cálculo (Preço x Quantidade)");
    }
} 