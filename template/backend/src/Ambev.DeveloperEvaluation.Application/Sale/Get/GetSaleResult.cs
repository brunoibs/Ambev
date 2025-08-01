namespace Ambev.DeveloperEvaluation.Application.Sale.Get;

public class GetSaleResult
{
    public Guid Id { get; set; }
    public DateTime DtSale { get; set; }
    public decimal Total { get; set; }
    public decimal Discount { get; set; }
    public bool Cancel { get; set; }
    public DateTime DtCreate { get; set; }
    public DateTime? DtEdit { get; set; }
    public DateTime? DtCancel { get; set; }
    public Guid IdCustomer { get; set; }
    public Guid IdCreate { get; set; }
    public Guid? IdEdit { get; set; }
    public Guid? IdCancel { get; set; }
} 