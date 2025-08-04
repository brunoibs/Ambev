namespace Ambev.DeveloperEvaluation.Application.Sale.Create;

/// <summary>
/// Resultado da criação de venda.
/// </summary>
public class CreateSaleResult
{
    public Guid Id { get; set; }
    public DateTime DtSale { get; set; }
    public decimal Total { get; set; }
    public decimal Discount { get; set; }
    public Guid IdCustomer { get; set; }
    public Guid IdCreate { get; set; }
} 