namespace Ambev.DeveloperEvaluation.Application.ProductSale.Get;

public class GetProductSaleResult
{
    public Guid Id { get; set; }
    public Guid IdSale { get; set; }
    public Guid IdProduct { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }
    public decimal Total { get; set; }
} 