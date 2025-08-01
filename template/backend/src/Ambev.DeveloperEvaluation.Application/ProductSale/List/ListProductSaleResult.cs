using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.ProductSale.List;

public class ListProductSaleResult
{
    public List<ProductSaleListItem> ProductSales { get; set; } = new();
}

public class ProductSaleListItem
{
    public Guid Id { get; set; }
    public Guid IdSale { get; set; }
    public Guid IdProduct { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }
    public decimal Total { get; set; }
} 