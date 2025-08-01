using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Sale.List;

public class ListSaleResult
{
    public List<SaleListItem> Sales { get; set; } = new();
}

public class SaleListItem
{
    public Guid Id { get; set; }
    public DateTime DtSale { get; set; }
    public decimal Total { get; set; }
    public decimal Discount { get; set; }
    public bool Cancel { get; set; }
    public Guid IdCustomer { get; set; }
} 