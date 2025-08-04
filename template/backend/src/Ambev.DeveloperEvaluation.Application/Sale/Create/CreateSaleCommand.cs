using MediatR;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sale.Create;

/// <summary>
/// Command para criar uma nova venda.
/// </summary>
public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    public DateTime DtSale { get; set; }
    public decimal Total { get; set; }
    public decimal Discount { get; set; }
    public bool Cancel { get; set; } = false;
    public DateTime DtCreate { get; set; }
    public DateTime? DtEdit { get; set; }
    public DateTime? DtCancel { get; set; }
    public Guid IdCustomer { get; set; }
    public Guid IdCreate { get; set; }
    public Guid? IdEdit { get; set; }
    public Guid? IdCancel { get; set; }
    public Guid IdBranch { get; set; }
    
    /// <summary>
    /// Lista de itens da venda (ProductSale)
    /// </summary>
    public List<Domain.Entities.ProductSale> ProductSales { get; set; } = new List<Domain.Entities.ProductSale>();

    public ValidationResultDetail Validate()
    {
        var validator = new CreateSaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
} 