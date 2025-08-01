using MediatR;
using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.Application.ProductSale.Create;

/// <summary>
/// Command para criar um novo item de venda.
/// </summary>
public class CreateProductSaleCommand : IRequest<CreateProductSaleResult>
{
    public Guid IdSale { get; set; }
    public Guid IdProduct { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }
    public decimal Total { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new CreateProductSaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
} 